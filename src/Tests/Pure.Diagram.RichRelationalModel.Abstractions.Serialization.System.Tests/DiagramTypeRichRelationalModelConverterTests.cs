using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Diagram.RichRelationalModel.HashCodes;
using Pure.Primitives.Abstractions.Serialization.System;
using Pure.Primitives.Random.String;
using Char = Pure.Primitives.Char.Char;
using Guid = Pure.Primitives.Guid.Guid;

namespace Pure.Diagram.RichRelationalModel.Abstractions.Serialization.System.Tests;

public sealed record DiagramTypeRichRelationalModelConverterTests
{
    private readonly JsonSerializerOptions _options;

    public DiagramTypeRichRelationalModelConverterTests()
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
        RandomString name = new RandomString(new Char('a'), new Char('z'));

        IDiagramTypeRichRelationalModel diagramType = new DiagramTypeRichRelationalModel(
            id,
            name
        );

        string serialized = JsonSerializer.Serialize(diagramType, _options);

        Assert.Equal(
            $$"""
            {
              "Id": "{{id.GuidValue}}",
              "Name": "{{name.TextValue}}"
            }
            """,
            serialized
        );
    }

    [Fact]
    public void Read()
    {
        Guid id = new Guid();
        RandomString name = new RandomString(new Char('a'), new Char('z'));

        IDiagramTypeRichRelationalModel expected = new DiagramTypeRichRelationalModel(
            id,
            name
        );

        string input = $$"""
            {
              "Id": "{{id.GuidValue}}",
              "Name": "{{name.TextValue}}"
            }
            """;

        Assert.True(
            new DiagramTypeRichRelationalModelHash(expected).SequenceEqual(
                new DiagramTypeRichRelationalModelHash(
                    JsonSerializer.Deserialize<IDiagramTypeRichRelationalModel>(
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
        IDiagramTypeRichRelationalModel diagramType = new DiagramTypeRichRelationalModel(
            new Guid(),
            new RandomString(new Char('a'), new Char('z'))
        );

        IDiagramTypeRichRelationalModel deserialized =
            JsonSerializer.Deserialize<IDiagramTypeRichRelationalModel>(
                JsonSerializer.Serialize(diagramType, _options),
                _options
            )!;

        Assert.True(
            new DiagramTypeRichRelationalModelHash(diagramType).SequenceEqual(
                new DiagramTypeRichRelationalModelHash(deserialized)
            )
        );
    }
}
