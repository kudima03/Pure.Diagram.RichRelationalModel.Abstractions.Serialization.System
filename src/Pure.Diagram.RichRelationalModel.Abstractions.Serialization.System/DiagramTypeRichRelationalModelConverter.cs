using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Diagram.RelationalModel.Abstractions;
using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.String;

namespace Pure.Diagram.RichRelationalModel.Abstractions.Serialization.System;

internal sealed record DiagramTypeRichRelationalModelJsonModel
    : IDiagramTypeRichRelationalModel
{
    public DiagramTypeRichRelationalModelJsonModel(IDiagramTypeRichRelationalModel model)
        : this(model.Id, ((IDiagramTypeRelationalModel)model).Name) { }

    [JsonConstructor]
    public DiagramTypeRichRelationalModelJsonModel(IGuid id, IString name)
    {
        Id = id;
        Name = name;
    }

    public IGuid Id { get; }

    public IString Name { get; }
}

public sealed class DiagramTypeRichRelationalModelConverter
    : JsonConverter<IDiagramTypeRichRelationalModel>
{
    public override IDiagramTypeRichRelationalModel Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<DiagramTypeRichRelationalModelJsonModel>(
            ref reader,
            options
        )!;
    }

    public override void Write(
        Utf8JsonWriter writer,
        IDiagramTypeRichRelationalModel value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            new DiagramTypeRichRelationalModelJsonModel(value),
            options
        );
    }
}
