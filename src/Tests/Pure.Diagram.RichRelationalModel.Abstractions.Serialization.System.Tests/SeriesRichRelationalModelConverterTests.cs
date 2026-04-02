using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Diagram.RichRelationalModel.HashCodes;
using Pure.Primitives.Abstractions.Serialization.System;
using Pure.Primitives.Random.String;
using Char = Pure.Primitives.Char.Char;
using Guid = Pure.Primitives.Guid.Guid;

namespace Pure.Diagram.RichRelationalModel.Abstractions.Serialization.System.Tests;

public sealed record SeriesRichRelationalModelConverterTests
{
    private readonly JsonSerializerOptions _options;

    public SeriesRichRelationalModelConverterTests()
    {
        _options = new JsonSerializerOptions();

        foreach (JsonConverter converter in new PrimitiveConverters())
        {
            _options.Converters.Add(converter);
        }

        foreach (
            JsonConverter converter in new DiagramRichRelationalModelAbstractionsConverters()
        )
        {
            _options.Converters.Add(converter);
        }

        _options.WriteIndented = true;
        _options.NewLine = "\n";
    }

    [Fact]
    public void Write()
    {
        Guid id = new Guid();
        Guid diagramId = new Guid();
        RandomString label = new RandomString(new Char('a'), new Char('z'));
        RandomString source = new RandomString(new Char('a'), new Char('z'));

        ISeriesRichRelationalModel series = new SeriesRichRelationalModel(
            id,
            diagramId,
            label,
            source
        );

        string serialized = JsonSerializer.Serialize(series, _options);

        Assert.Equal(
            $$"""
            {
              "Id": "{{id.GuidValue}}",
              "DiagramId": "{{diagramId.GuidValue}}",
              "Label": "{{label.TextValue}}",
              "Source": "{{source.TextValue}}"
            }
            """,
            serialized
        );
    }

    [Fact]
    public void Read()
    {
        Guid id = new Guid();
        Guid diagramId = new Guid();
        RandomString label = new RandomString(new Char('a'), new Char('z'));
        RandomString source = new RandomString(new Char('a'), new Char('z'));

        ISeriesRichRelationalModel expected = new SeriesRichRelationalModel(
            id,
            diagramId,
            label,
            source
        );

        string input = $$"""
            {
              "Id": "{{id.GuidValue}}",
              "DiagramId": "{{diagramId.GuidValue}}",
              "Label": "{{label.TextValue}}",
              "Source": "{{source.TextValue}}"
            }
            """;

        Assert.True(
            new SeriesRichRelationalModelHash(expected).SequenceEqual(
                new SeriesRichRelationalModelHash(
                    JsonSerializer.Deserialize<ISeriesRichRelationalModel>(
                        input,
                        _options
                    )!
                )
            )
        );
    }

    [Fact]
    public void RoundTrip()
    {
        ISeriesRichRelationalModel series = new SeriesRichRelationalModel(
            new Guid(),
            new Guid(),
            new RandomString(new Char('a'), new Char('z')),
            new RandomString(new Char('a'), new Char('z'))
        );

        ISeriesRichRelationalModel deserialized =
            JsonSerializer.Deserialize<ISeriesRichRelationalModel>(
                JsonSerializer.Serialize(series, _options),
                _options
            )!;

        Assert.True(
            new SeriesRichRelationalModelHash(series).SequenceEqual(
                new SeriesRichRelationalModelHash(deserialized)
            )
        );
    }
}
