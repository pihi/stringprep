using System;
using System.Collections.Generic;
using System.Linq;

namespace StringPrep
{
  internal class ValueRangeTableBuilder : IValueRangeTableBuilder
  {
    private readonly int[][] _baseTables;
    private readonly IList<int> _inclusions;
    private readonly IList<int> _removals;

    public ValueRangeTableBuilder(int[][] baseTables)
    {
      if (!baseTables.Any()) throw new ArgumentException("At least one base table must be provided", nameof(baseTables));

      _baseTables = baseTables;
      _inclusions = new List<int>();
      _removals = new List<int>();
    }

    public IValueRangeTableBuilder Include(int include)
    {
      IncludeRange(include, include);
      return this;
    }

    public IValueRangeTableBuilder IncludeRange(int start, int end)
    {
      _inclusions.Add(start);
      _inclusions.Add(end);
      return this;
    }

    public IValueRangeTableBuilder Remove(int remove)
    {
      RemoveRange(remove, remove);
      return this;
    }

    public IValueRangeTableBuilder RemoveRange(int start, int end)
    {
      _removals.Add(start);
      _removals.Add(end);
      return this;
    }

    public IValueRangeTable Compile()
    {
      var ranges = ValueRangeCompiler.Compile(_baseTables, _inclusions.ToArray(), _removals.ToArray());
      return new ValueRangeTable(ranges);
    }
  }
}
