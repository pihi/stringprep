using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StringPrep.Tests
{
  public class ProhibitedValueStepTest
  {
    [Fact]
    public void WillProhibitValuesInTable()
    {
      var input = new StringBuilder();
      input.Append(Convert.ToChar(0x20));
      var step = new ProhibitedValueStep(ValueRangeTable.Build(Tables.C_1_1).Compile());
      Assert.Throws<ProhibitedValueException>(() => step.Run(input.ToString()));
    }

    [Fact]
    public void WillNotProhibitValuesNotInTable()
    {
      var input = "ThisIsAStringWithoutSpaces";
      var step = new ProhibitedValueStep(ValueRangeTable.Build(Tables.C_1_1).Compile());
      var output = step.Run(input);
      Assert.Equal(input, output);
    }
  }
}
