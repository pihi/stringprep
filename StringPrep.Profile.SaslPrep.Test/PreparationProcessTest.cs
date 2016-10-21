using System;
using Xunit;

namespace StringPrep.Profile.SaslPrep.Test
{
  public class PreparationProcessTest
  {
    [Fact]
    public void WillPrepareValuesListedInSpec()
    {
      var process = SaslPrep.PreparationProcess.Create();
      Assert.Equal("IX", process.Run("I" + Convert.ToChar(0x00AD) + "X"));
      Assert.Equal("user", process.Run("user"));
      Assert.Equal("USER", process.Run("USER"));
      Assert.Equal("a", process.Run("" + Convert.ToChar(0x00AA)));
      Assert.Equal("IX", process.Run("" + Convert.ToChar(0x2168)));
      Assert.Throws<ProhibitedValueException>(() => process.Run("" + Convert.ToChar(0x0007)));
      Assert.Throws<BidirectionalFormatException>(() => process.Run("" + Convert.ToChar(0x0627) + Convert.ToChar(0x0031)));
    }
  }
}
