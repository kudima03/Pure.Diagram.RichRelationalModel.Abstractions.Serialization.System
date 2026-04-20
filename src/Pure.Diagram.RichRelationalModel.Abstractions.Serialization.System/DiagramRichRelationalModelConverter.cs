using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Diagram.Model.Abstractions;
using Pure.Diagram.RelationalModel.Abstractions;
using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.String;

namespace Pure.Diagram.RichRelationalModel.Abstractions.Serialization.System;

internal sealed record DiagramRichRelationalModelJsonModel : IDiagramRichRelationalModel
{
    public DiagramRichRelationalModelJsonModel(IDiagramRichRelationalModel model)
        : this(
            model.Id,
            ((IDiagramRelationalModel)model).Title,
            ((IDiagramRelationalModel)model).Description,
            model.TypeId,
            (IDiagramTypeRichRelationalModel)model.Type,
            model.Series.Cast<IDiagramSeriesRichRelationalModel>()
        )
    { }

    [JsonConstructor]
    public DiagramRichRelationalModelJsonModel(
        IGuid id,
        IString title,
        IString description,
        IGuid typeId,
        IDiagramTypeRichRelationalModel type,
        IEnumerable<IDiagramSeriesRichRelationalModel> series
    )
    {
        Id = id;
        Title = title;
        Description = description;
        TypeId = typeId;
        Type = type;
        Series = series;
    }

    public IGuid Id { get; }

    public IString Title { get; }

    public IString Description { get; }

    public IGuid TypeId { get; }

    public IDiagramTypeRichRelationalModel Type { get; }

    public IEnumerable<IDiagramSeriesRichRelationalModel> Series { get; }

    IDiagramType IDiagram.Type => Type;

    IEnumerable<IDiagramSeries> IDiagram.Series => Series;
}

public sealed class DiagramRichRelationalModelConverter
    : JsonConverter<IDiagramRichRelationalModel>
{
    public override IDiagramRichRelationalModel Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<DiagramRichRelationalModelJsonModel>(
            ref reader,
            options
        )!;
    }

    public override void Write(
        Utf8JsonWriter writer,
        IDiagramRichRelationalModel value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            new DiagramRichRelationalModelJsonModel(value),
            options
        );
    }
}
