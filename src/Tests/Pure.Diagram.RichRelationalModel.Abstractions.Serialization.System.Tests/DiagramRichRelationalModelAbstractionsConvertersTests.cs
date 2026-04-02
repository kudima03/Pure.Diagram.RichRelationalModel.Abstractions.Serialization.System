using System.Collections;

namespace Pure.Diagram.RichRelationalModel.Abstractions.Serialization.System.Tests;

public sealed record DiagramRichRelationalModelAbstractionsConvertersTests
{
    [Fact]
    public void EnumeratesThreeConverters()
    {
        Assert.Equal(3, new DiagramRichRelationalModelAbstractionsConverters().Count());
    }

    [Fact]
    public void NonGenericEnumeratorReturnsAllConverters()
    {
        IEnumerable converters = new DiagramRichRelationalModelAbstractionsConverters();

        int count = 0;

        foreach (object _ in converters)
        {
            count++;
        }

        Assert.Equal(3, count);
    }
}
