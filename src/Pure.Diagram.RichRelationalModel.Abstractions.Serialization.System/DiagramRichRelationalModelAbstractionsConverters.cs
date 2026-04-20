using System.Collections;
using System.Text.Json.Serialization;

namespace Pure.Diagram.RichRelationalModel.Abstractions.Serialization.System;

public sealed class DiagramRichRelationalModelAbstractionsConverters
    : IEnumerable<JsonConverter>
{
    public IEnumerator<JsonConverter> GetEnumerator()
    {
        yield return new DiagramTypeRichRelationalModelConverter();
        yield return new DiagramSeriesRichRelationalModelConverter();
        yield return new DiagramRichRelationalModelConverter();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
