using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StringPrep.Tests
{
  public class ValueRangeTableTest
  {
    [Fact]
    public void WillFindValuesInRange()
    {
      var a = new[]
      {
        1, 5,
        9, 12,
        15, 17,
        22, 33,
        37, 40,
        42, 50,
        55, 70,
        75, 100
      };

      var table = new ValueRangeTable(a);
      Assert.True(table.Contains(1));
      Assert.True(table.Contains(4));
      Assert.True(table.Contains(11));
      Assert.True(table.Contains(12));
      Assert.True(table.Contains(31));
      Assert.True(table.Contains(38));
      Assert.True(table.Contains(45));
      Assert.True(table.Contains(56));
      Assert.True(table.Contains(88));
    }

    [Fact]
    public void WillNotFindValuesOutOfRange()
    {
      var a = new[]
      {
        1, 5,
        9, 12,
        15, 17,
        22, 33,
        37, 40,
        42, 50,
        55, 70,
        75, 100
      };

      var table = new ValueRangeTable(a);
      Assert.False(table.Contains(0));
      Assert.False(table.Contains(8));
      Assert.False(table.Contains(13));
      Assert.False(table.Contains(19));
      Assert.False(table.Contains(35));
      Assert.False(table.Contains(41));
      Assert.False(table.Contains(52));
      Assert.False(table.Contains(73));
      Assert.False(table.Contains(101));
    }
  }
}
