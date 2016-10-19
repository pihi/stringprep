using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StringPrep.Tests
{
  public class BidirectionalStepTest
  {
    [Fact]
    public void WillThrowForProhibitValues()
    {
      var input = Convert.ToChar(0x0340).ToString();
      var step = new BidirectionalStep(
        ValueRangeTable.Create(Prohibited.C_8),
        ValueRangeTable.Create(Bidirectional.D_1),
        ValueRangeTable.Create(Bidirectional.D_2));
      Assert.Throws<ProhibitedValueException>(() => step.Run(input));
    }

    [Fact]
    public void WillThrowForRALStringNotEndingWithRALCharacter()
    {
      var input = "" + Convert.ToChar(0x0627) + "1";
      var step = new BidirectionalStep(
        ValueRangeTable.Create(Prohibited.C_8),
        ValueRangeTable.Create(Bidirectional.D_1),
        ValueRangeTable.Create(Bidirectional.D_2));
      Assert.Throws<BidirectionalFormatException>(() => step.Run(input));
    }

    [Fact]
    public void WillThrowForMixedRALAndLCharacters()
    {
      var input = "" + Convert.ToChar(0x05BE) + Convert.ToChar(0x0041);
      var step = new BidirectionalStep(
        ValueRangeTable.Create(Prohibited.C_8),
        ValueRangeTable.Create(Bidirectional.D_1),
        ValueRangeTable.Create(Bidirectional.D_2));
      Assert.Throws<BidirectionalFormatException>(() => step.Run(input));
    }

    [Fact]
    public void WillPassForRALStringEndingWithRALCharacter()
    {
      var input = "" + Convert.ToChar(0x0627) + "1" + Convert.ToChar(0x0628);
      var step = new BidirectionalStep(
        ValueRangeTable.Create(Prohibited.C_8),
        ValueRangeTable.Create(Bidirectional.D_1),
        ValueRangeTable.Create(Bidirectional.D_2));
      var output = step.Run(input);
      Assert.Equal(input, output);
    }
  }
}
