namespace App.Core.Parameters.ParameterVariations
{
    public class EMAParameterVariation : AbstractParameterVariation
    {
        public int MiddlePeriod { get; set; }
        public int GapBetweenMiddles { get; set; }

        public override AbstractParameterVariation Derive()
        {
            int newMiddlePeriod = (int)DeriveDecimal(MiddlePeriod, 1, 1, 5, 40);
            int newGapBetweenMiddles = (int)DeriveDecimal(GapBetweenMiddles, 1, 1, 1, 30);

            if (newMiddlePeriod - newGapBetweenMiddles < 2)
                newGapBetweenMiddles = newMiddlePeriod - 2;

            return new EMAParameterVariation()
            {
                MiddlePeriod = newMiddlePeriod,
                GapBetweenMiddles = newGapBetweenMiddles
            };
        }
    }
}
