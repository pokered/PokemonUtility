using PokemonUtility.Const;

namespace PokemonUtility.Models.Common
{
    public class SoftGenerationModel
    {
        // ソフトの世代ID
        public int Id { get; set; }

        // ソフトの世代名
        public string Name { get; set; }
    }

    public class SoftGenerations
    {
        public SoftGenerationModel[] GetSoftGenerations()
        {
            return new SoftGenerationModel[]
            {
                new SoftGenerationModel() { Id = SoftGenerationConst.SUN_MOON, Name = SoftGenerationConst.SUN_MOON_NAME},
                new SoftGenerationModel() { Id = SoftGenerationConst.X_Y, Name = SoftGenerationConst.X_Y_NAME},
                new SoftGenerationModel() { Id = SoftGenerationConst.BLACK_WHITE, Name = SoftGenerationConst.BLACK_WHITE_NAME},
                new SoftGenerationModel() { Id = SoftGenerationConst.DIAMOND_PEARL, Name = SoftGenerationConst.DIAMOND_PEARL_NAME},
                new SoftGenerationModel() { Id = SoftGenerationConst.RUBY_SAPPHIRE, Name = SoftGenerationConst.RUBY_SAPPHIRE_NAME},
                new SoftGenerationModel() { Id = SoftGenerationConst.GOLD_SILVER, Name = SoftGenerationConst.GOLD_SILVER_NAME},
                new SoftGenerationModel() { Id = SoftGenerationConst.RED_GREEN, Name = SoftGenerationConst.RED_GREEN_NAME}
            };
        }
    }
}
