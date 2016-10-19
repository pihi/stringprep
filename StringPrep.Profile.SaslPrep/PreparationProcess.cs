namespace StringPrep.Profile.SaslPrep
{
  public static class PreparationProcess
  {
    public static IPreparationProcess Create()
    {
      return StringPrep.PreparationProcess.Build()
        .WithMappingStep(MappingTable.Build()
          .WithValueRangeTable(Prohibited.ASCIISpaceCharacters, ' ')
          .WithMappingTable(Mapping.MappedToNothing)
          .Compile())
        .WithNormalizationStep()
        .WithProhibitedValueStep(ValueRangeTable.Create(
          Prohibited.NonASCIISpaceCharacters,
          Prohibited.ASCIIControlCharacters,
          Prohibited.NonASCIIControlCharacters,
          Prohibited.PrivateUseCharacters,
          Prohibited.NonCharacterCodePoints,
          Prohibited.SurrogateCodePoints,
          Prohibited.InappropriateForPlainText,
          Prohibited.InappropriateForCanonicalRepresentation,
          Prohibited.TaggingCharacters))
        .WithBidirectionalStep()
        .WithProhibitedValueStep(ValueRangeTable.Create(
          Unassigned.UnassignedCodePoints))
        .Compile();
    }
  }
}
