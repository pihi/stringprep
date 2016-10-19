using System.Collections.Generic;

namespace StringPrep
{
  public class MappingTable : IMappingTable
  {
    public static IMappingTable Create(int[] valueTable, int replacement)
    {
      return Create(valueTable, new[] { replacement });
    }

    public static IMappingTable Create(int[] valueTable, int[] replacement)
    {
      
      return Build(valueTable, replacement).Compile();
    }

    public static IMappingTable Create(params IDictionary<int, int[]>[] baseTables)
    {
      return Build(baseTables).Compile();
    }

    public static IMappingTableBuilder Build(int[] valueTable, int replacement)
    {
      return Build(valueTable, new[] { replacement });
    }

    public static IMappingTableBuilder Build(int[] valueTable, int[] replacement)
    {
      return Build(MappingTableCompiler.GetMappingsFromValueRange(valueTable, replacement));
    }

    public static IMappingTableBuilder Build(params IDictionary<int, int[]>[] baseTables)
    {
      return new MappingTableBuilder(baseTables);
    }

    private readonly SortedList<int, int[]> _mappings;

    internal MappingTable(IDictionary<int, int[]> values)
    {
      _mappings = new SortedList<int, int[]>(values);
    }

    public bool HasReplacement(int value)
    {
      return _mappings.ContainsKey(value);
    }

    public int[] GetReplacement(int value)
    {
      return _mappings[value];
    }
  }
}
