using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StringPrep.Tests
{
  public class ValueRangeCompilerTest
  {
    [Fact]
    public void WillCompileSingleTable()
    {
      var a = new int[]
      {
        1, 1
      };

      var result = ValueRangeCompiler.Compile(new[] { a }, new int[0], new int[0]);
      Assert.Equal(new[] { 1, 1 }, result);
    }

    [Fact]
    public void WillSortSingleTable()
    {
      var a = new int[]
      {
        3, 7,
        18, 22,
        1, 1,
        9, 13
      };

      var result = ValueRangeCompiler.Compile(new[] { a }, new int[0], new int[0]);
      Assert.Equal(
        new[] { 1, 1, 3, 7, 9, 13, 18, 22 },
        result );
    }

    [Fact]
    public void WillCombileTwoTables()
    {
      var a = new int[]
      {
        3, 7,
        22, 25
      };

      var b = new int[]
      {
        9, 15,
        18, 20
      };

      var result = ValueRangeCompiler.Compile(new[] { a, b }, new int[0], new int[0]);
      Assert.Equal(
        new[] { 3, 7, 9, 15, 18, 20, 22, 25 },
        result);
    }

    [Fact]
    public void WillCombineTablesOfDifferentLengths()
    {
      var a = new int[]
      {
        1, 2
      };

      var b = new int[]
      {
        75, 75,
        42, 46,
        13, 15
      };

      var c = new int[]
      {
        33, 35,
        77, 79
      };

      var d = new int[]
      {
        4, 10,
        99, 99,
        101, 105,
        303, 307
      };

      var result = ValueRangeCompiler.Compile(new[] { a, b, c, d }, new int[0], new int[0]);
      Assert.Equal(
        new[] { 1, 2, 4, 10, 13, 15, 33, 35, 42, 46, 75, 75, 77, 79, 99, 99, 101, 105, 303, 307 },
        result);
    }

    [Fact]
    public void WillReduceOverlappingValueRanges()
    {
      var a = new int[]
      {
        1, 10,
        9, 12,
        14, 17,
        15, 18
      };

      var result = ValueRangeCompiler.Compile(new[] { a }, new int[0], new int[0]);
      Assert.Equal(
        new[] { 1, 12, 14, 18 },
        result);
    }

    [Fact]
    public void WillReduceInclusiveValueRanges()
    {
      var a = new int[]
      {
        1, 100,
        9, 15,
        18, 22, 
        33, 75
      };

      var result = ValueRangeCompiler.Compile(new[] { a }, new int[0], new int[0]);
      Assert.Equal(new[] { 1, 100 }, result);
    }

    [Fact]
    public void WillReduceAdjacentValueRanges()
    {
      var a = new int[]
      {
        1, 20,
        20, 35,
        35, 48,
        48, 50
      };

      var result = ValueRangeCompiler.Compile(new[] { a }, new int[0], new int[0]);
      Assert.Equal(new[] { 1, 50 }, result);
    }

    [Fact]
    public void WillIncludeValueRangesAfterBaseTable()
    {
      var a = new int[]
      {
        1, 5
      };

      var result = ValueRangeCompiler.Compile(new[] { a }, new int[] { 10, 10 }, new int[0]);
      Assert.Equal(new[] { 1, 5, 10, 10 }, result);
    }

    [Fact]
    public void WillIncludeMultipleValueRangesAfterBaseTable()
    {
      var a = new int[]
      {
        1, 5
      };

      var result = ValueRangeCompiler.Compile(new[] { a }, new int[] { 10, 10, 15, 20 }, new int[0]);
      Assert.Equal(new[] { 1, 5, 10, 10, 15, 20 }, result);
    }

    [Fact]
    public void WillIncludeValueRangesBeforeBaseTable()
    {
      var a = new int[]
      {
        15, 20
      };

      var result = ValueRangeCompiler.Compile(new[] { a }, new int[] { 4, 7 }, new int[0]);
      Assert.Equal(new[] { 4, 7, 15, 20 }, result);
    }

    [Fact]
    public void WillIncludeMultipleValueRangesBeforeBaseTable()
    {
      var a = new int[]
      {
        15, 20
      };

      var result = ValueRangeCompiler.Compile(new[] { a }, new int[] { 1, 1, 4, 7 }, new int[0]);
      Assert.Equal(new[] { 1, 1, 4, 7, 15, 20 }, result);
    }

    [Fact]
    public void WillRemoveValueRangeEqualToStartValue()
    {
      var a = new int[]
      {
        10, 20
      };

      var result = ValueRangeCompiler.Compile(new[] { a }, new int[0], new int[] { 10, 15 });
      Assert.Equal(new[] { 16, 20 }, result);
    }

    [Fact]
    public void WillRemoveValueRangeEqualToEndValue()
    {
      var a = new int[]
      {
        10, 20
      };

      var result = ValueRangeCompiler.Compile(new[] { a }, new int[0], new int[] { 15, 20 });
      Assert.Equal(new[] { 10, 14 }, result);
    }

    [Fact]
    public void WillRemoveValueRangeOverlappingStartValue()
    {
      var a = new int[]
      {
        10, 20
      };

      var result = ValueRangeCompiler.Compile(new[] { a }, new int[0], new int[] { 5, 15 });
      Assert.Equal(new[] { 16, 20 }, result);
    }

    [Fact]
    public void WillRemoveValueRangeOverlappingStartValueOnLaterRanges()
    {
      var a = new int[]
      {
        10, 20,
        30, 40
      };

      var result = ValueRangeCompiler.Compile(new[] { a }, new int[0], new int[] { 25, 35 });
      Assert.Equal(new[] { 10, 20, 36, 40 }, result);
    }

    [Fact]
    public void WillRemoveValueRangeOverlappingEndValue()
    {
      var a = new int[]
      {
        10, 20
      };

      var result = ValueRangeCompiler.Compile(new[] { a }, new int[0], new int[] { 15, 25 });
      Assert.Equal(new[] { 10, 14 }, result);
    }

    [Fact]
    public void WillRemoveValueRangeOverlappingEndValueOnEarlierRanges()
    {
      var a = new int[]
      {
        10, 20,
        30, 40
      };

      var result = ValueRangeCompiler.Compile(new[] { a }, new int[0], new int[] { 15, 25 });
      Assert.Equal(new[] { 10, 14, 30, 40 }, result);
    }

    [Fact]
    public void ThrowsForInvalidRange()
    {
      var a = new int[]
      {
        7, 5
      };

      Assert.Throws<ArgumentException>(() => { ValueRangeCompiler.Compile(new[] { a }, new int[0], new int[0]); });
    }

    [Fact]
    public void ThrowsForOddLengthArray()
    {
      var a = new int[]
      {
        5, 7,
        4
      };

      Assert.Throws<ArgumentException>(() => { ValueRangeCompiler.Compile(new[] { a }, new int[0], new int[0]); });
    }
  }
}
