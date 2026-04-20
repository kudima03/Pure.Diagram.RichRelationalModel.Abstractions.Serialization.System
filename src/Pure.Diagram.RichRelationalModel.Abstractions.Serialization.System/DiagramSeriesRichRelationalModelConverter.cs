using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Diagram.RelationalModel.Abstractions;
using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.String;

namespace Pure.Diagram.RichRelationalModel.Abstractions.Serialization.System;

internal sealed record DiagramSeriesRichRelationalModelJsonModel
    : IDiagramSeriesRichRelationalModel
{
    public DiagramSeriesRichRelationalModelJsonModel(
        IDiagramSeriesRichRelationalModel model
    )
        : this(
            model.Id,
            model.DiagramId,
            ((IDiagramSeriesRelationalModel)model).Label,
            ((IDiagramSeriesRelationalModel)model).Source
        )
    { }

    [JsonConstructor]
    public DiagramSeriesRichRelationalModelJsonModel(
        IGuid id,
        IGuid diagramId,
        IString label,
        IString source
    )
    {
        Id = id;
        DiagramId = diagramId;
        Label = label;
        Source = source;
    }

    public IGuid Id { get; }

    public IGuid DiagramId { get; }

    public IString Label { get; }

    public IString Source { get; }
}

public sealed class DiagramSeriesRichRelationalModelConverter
    : JsonConverter<IDiagramSeriesRichRelationalModel>
{
    public override IDiagramSeriesRichRelationalModel Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<DiagramSeriesRichRelationalModelJsonModel>(
            ref reader,
            options
        )!;
    }

    public override void Write(
        Utf8JsonWriter writer,
        IDiagramSeriesRichRelationalModel value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            new DiagramSeriesRichRelationalModelJsonModel(value),
            options
        );
    }
}
