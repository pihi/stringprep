using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StringPrep.Tests
{
  public class MappingStepTest
  {
    [Fact]
    public void WillNotReplaceValuesNotMapped()
    {
      var input = "this will not be replaced";
      var step = new MappingStep(MappingTable.Create(Tables.B_1));
      var output = step.Run(input);
      Assert.Equal(input, output);
    }

    [Fact]
    public void WillReplaceValuesMappedToNothing()
    {
      var input = "this value: " + Convert.ToChar(0x180B) + " will be replaced";
      var expected = "this value:  will be replaced";
      var step = new MappingStep(MappingTable.Create(Tables.B_1));
      var output = step.Run(input);
      Assert.Equal(expected, output);
    }

    [Fact]
    public void WillReplaceValues()
    {
      var input = "this value: " + Convert.ToChar(0x0041) + " will be replaced";
      var expected = "this value: " + Convert.ToChar(0x0061) + " will be replaced";
      var step = new MappingStep(MappingTable.Create(Tables.B_2));
      var output = step.Run(input);
      Assert.Equal(expected, output);
    }

    [Fact]
    public void WillReplaceValuesWithMultipleReplacements()
    {
      var input = "this value: " + Convert.ToChar(0x00DF) + " will be replaced";
      var expected = "this value: " + Convert.ToChar(0x0073) + Convert.ToChar(0x0073) + " will be replaced";
      var step = new MappingStep(MappingTable.Create(Tables.B_2));
      var output = step.Run(input);
      Assert.Equal(expected, output);
    }
  }
}
