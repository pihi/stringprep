using System;
using System.Collections.Generic;
using System.Linq;

namespace StringPrep
{
  internal class MappingTableBuilder : IMappingTableBuilder
  {
    private readonly List<IDictionary<int, int[]>> _baseTables;
    private readonly List<IDictionary<int, int[]>> _inclusions;
    private readonly List<int> _removals;

    public MappingTableBuilder(params IDictionary<int, int[]>[] baseTables)
    {
      if (!baseTables.Any()) throw new ArgumentException("At least one base table must be provided", nameof(baseTables));
      _baseTables = baseTables.ToList();

      _inclusions = new List<IDictionary<int, int[]>>();
      _removals = new List<int>();
    }

    public IMappingTableBuilder WithValueRangeTable(int[] values, int replacement)
    {
      return WithValueRangeTable(values, new[] { replacement });
    }

    public IMappingTableBuilder WithValueRangeTable(int[] values, int[] replacement)
    {
      _baseTables.Add(MappingTableCompiler.GetMappingsFromValueRange(values, replacement));
      return this;
    }

    public IMappingTableBuilder WithMappingTable(IDictionary<int, int[]> table)
    {
      _baseTables.Add(table);
      return this;
    }

    public IMappingTableBuilder Include(IDictionary<int, int[]> include)
    {
      _inclusions.Add(include);
      return this;
    }

    public IMappingTableBuilder Remove(int remove)
    {
      _removals.Add(remove);
      return this;
    }

    public IMappingTable Compile()
    {
      var compiled = MappingTableCompiler.Compile(_baseTables.ToArray(), _inclusions.ToArray(), _removals.ToArray());
      return new MappingTable(compiled);
    }
  }
}
