using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Diagram.RelationalModel.Abstractions;
using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.String;

namespace Pure.Diagram.RichRelationalModel.Abstractions.Serialization.System;

internal sealed record SeriesRichRelationalModelJsonModel : ISeriesRichRelationalModel
{
    public SeriesRichRelationalModelJsonModel(ISeriesRichRelationalModel model)
        : this(
            model.Id,
            model.DiagramId,
            ((ISeriesRelationalModel)model).Label,
            ((ISeriesRelationalModel)model).Source
        )
    { }

    [JsonConstructor]
    public SeriesRichRelationalModelJsonModel(
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

public sealed class SeriesRichRelationalModelConverter
    : JsonConverter<ISeriesRichRelationalModel>
{
    public override ISeriesRichRelationalModel Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<SeriesRichRelationalModelJsonModel>(
            ref reader,
            options
        )!;
    }

    public override void Write(
        Utf8JsonWriter writer,
        ISeriesRichRelationalModel value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            new SeriesRichRelationalModelJsonModel(value),
            options
        );
    }
}
