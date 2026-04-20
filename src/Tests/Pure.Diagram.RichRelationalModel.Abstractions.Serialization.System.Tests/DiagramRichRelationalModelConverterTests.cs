using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Diagram.RichRelationalModel.HashCodes;
using Pure.Primitives.Abstractions.Serialization.System;
using Pure.Primitives.Random.String;
using Char = Pure.Primitives.Char.Char;
using Guid = Pure.Primitives.Guid.Guid;

namespace Pure.Diagram.RichRelationalModel.Abstractions.Serialization.System.Tests;

public sealed record DiagramRichRelationalModelConverterTests
{
    private readonly JsonSerializerOptions _options;

    public DiagramRichRelationalModelConverterTests()
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
        RandomString title = new RandomString(new Char('a'), new Char('z'));
        RandomString description = new RandomString(new Char('a'), new Char('z'));

        Guid typeId = new Guid();
        RandomString typeName = new RandomString(new Char('a'), new Char('z'));
        DiagramTypeRichRelationalModel type = new DiagramTypeRichRelationalModel(
            typeId,
            typeName
        );

        Guid seriesId = new Guid();
        Guid seriesDiagramId = new Guid();
        RandomString seriesLabel = new RandomString(new Char('a'), new Char('z'));
        RandomString seriesSource = new RandomString(new Char('a'), new Char('z'));
        DiagramSeriesRichRelationalModel series = new DiagramSeriesRichRelationalModel(
            seriesId,
            seriesDiagramId,
            seriesLabel,
            seriesSource
        );

        IDiagramRichRelationalModel diagram = new DiagramRichRelationalModel(
            id,
            title,
            description,
            typeId,
            type,
            [series]
        );

        string serialized = JsonSerializer.Serialize(diagram, _options);

        Assert.Equal(
            $$"""
            {
              "Id": "{{id.GuidValue}}",
              "Title": "{{title.TextValue}}",
              "Description": "{{description.TextValue}}",
              "TypeId": "{{typeId.GuidValue}}",
              "Type": {
                "Id": "{{typeId.GuidValue}}",
                "Name": "{{typeName.TextValue}}"
              },
              "Series": [
                {
                  "Id": "{{seriesId.GuidValue}}",
                  "DiagramId": "{{seriesDiagramId.GuidValue}}",
                  "Label": "{{seriesLabel.TextValue}}",
                  "Source": "{{seriesSource.TextValue}}"
                }
              ]
            }
            """,
            serialized
        );
    }

    [Fact]
    public void Read()
    {
        Guid id = new Guid();
        RandomString title = new RandomString(new Char('a'), new Char('z'));
        RandomString description = new RandomString(new Char('a'), new Char('z'));

        Guid typeId = new Guid();
        RandomString typeName = new RandomString(new Char('a'), new Char('z'));

        Guid seriesId = new Guid();
        Guid seriesDiagramId = new Guid();
        RandomString seriesLabel = new RandomString(new Char('a'), new Char('z'));
        RandomString seriesSource = new RandomString(new Char('a'), new Char('z'));

        IDiagramRichRelationalModel expected = new DiagramRichRelationalModel(
            id,
            title,
            description,
            typeId,
            new DiagramTypeRichRelationalModel(typeId, typeName),
            [
                new DiagramSeriesRichRelationalModel(
                    seriesId,
                    seriesDiagramId,
                    seriesLabel,
                    seriesSource
                ),
            ]
        );

        string input = $$"""
            {
              "Id": "{{id.GuidValue}}",
              "Title": "{{title.TextValue}}",
              "Description": "{{description.TextValue}}",
              "TypeId": "{{typeId.GuidValue}}",
              "Type": {
                "Id": "{{typeId.GuidValue}}",
                "Name": "{{typeName.TextValue}}"
              },
              "Series": [
                {
                  "Id": "{{seriesId.GuidValue}}",
                  "DiagramId": "{{seriesDiagramId.GuidValue}}",
                  "Label": "{{seriesLabel.TextValue}}",
                  "Source": "{{seriesSource.TextValue}}"
                }
              ]
            }
            """;

        Assert.True(
            new DiagramRichRelationalModelHash(expected).SequenceEqual(
                new DiagramRichRelationalModelHash(
                    JsonSerializer.Deserialize<IDiagramRichRelationalModel>(
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
        IDiagramRichRelationalModel diagram = new DiagramRichRelationalModel(
            new Guid(),
            new RandomString(new Char('a'), new Char('z')),
            new RandomString(new Char('a'), new Char('z')),
            new Guid(),
            new DiagramTypeRichRelationalModel(
                new Guid(),
                new RandomString(new Char('a'), new Char('z'))
            ),
            [
                new DiagramSeriesRichRelationalModel(
                    new Guid(),
                    new Guid(),
                    new RandomString(new Char('a'), new Char('z')),
                    new RandomString(new Char('a'), new Char('z'))
                ),
            ]
        );

        IDiagramRichRelationalModel deserialized =
            JsonSerializer.Deserialize<IDiagramRichRelationalModel>(
                JsonSerializer.Serialize(diagram, _options),
                _options
            )!;

        Assert.True(
            new DiagramRichRelationalModelHash(diagram).SequenceEqual(
                new DiagramRichRelationalModelHash(deserialized)
            )
        );
    }

    [Fact]
    public void WriteNoSeries()
    {
        Guid id = new Guid();
        RandomString title = new RandomString(new Char('a'), new Char('z'));
        RandomString description = new RandomString(new Char('a'), new Char('z'));

        Guid typeId = new Guid();
        RandomString typeName = new RandomString(new Char('a'), new Char('z'));

        IDiagramRichRelationalModel diagram = new DiagramRichRelationalModel(
            id,
            title,
            description,
            typeId,
            new DiagramTypeRichRelationalModel(typeId, typeName),
            []
        );

        string serialized = JsonSerializer.Serialize(diagram, _options);

        Assert.Equal(
            $$"""
            {
              "Id": "{{id.GuidValue}}",
              "Title": "{{title.TextValue}}",
              "Description": "{{description.TextValue}}",
              "TypeId": "{{typeId.GuidValue}}",
              "Type": {
                "Id": "{{typeId.GuidValue}}",
                "Name": "{{typeName.TextValue}}"
              },
              "Series": []
            }
            """,
            serialized
        );
    }

    [Fact]
    public void RoundTripMultipleSeries()
    {
        IDiagramRichRelationalModel diagram = new DiagramRichRelationalModel(
            new Guid(),
            new RandomString(new Char('a'), new Char('z')),
            new RandomString(new Char('a'), new Char('z')),
            new Guid(),
            new DiagramTypeRichRelationalModel(
                new Guid(),
                new RandomString(new Char('a'), new Char('z'))
            ),
            [
                new DiagramSeriesRichRelationalModel(
                    new Guid(),
                    new Guid(),
                    new RandomString(new Char('a'), new Char('z')),
                    new RandomString(new Char('a'), new Char('z'))
                ),
                new DiagramSeriesRichRelationalModel(
                    new Guid(),
                    new Guid(),
                    new RandomString(new Char('a'), new Char('z')),
                    new RandomString(new Char('a'), new Char('z'))
                ),
                new DiagramSeriesRichRelationalModel(
                    new Guid(),
                    new Guid(),
                    new RandomString(new Char('a'), new Char('z')),
                    new RandomString(new Char('a'), new Char('z'))
                ),
            ]
        );

        IDiagramRichRelationalModel deserialized =
            JsonSerializer.Deserialize<IDiagramRichRelationalModel>(
                JsonSerializer.Serialize(diagram, _options),
                _options
            )!;

        Assert.True(
            new DiagramRichRelationalModelHash(diagram).SequenceEqual(
                new DiagramRichRelationalModelHash(deserialized)
            )
        );
    }
}
