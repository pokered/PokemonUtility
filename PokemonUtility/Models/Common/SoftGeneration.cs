using PokemonUtility.Const;

namespace PokemonUtility.Models.Common
{
    public class SoftGeneration
    {
        // ソフトの世代ID
        public int Id { get; set; }

        // ソフトの世代名
        public string Name { get; set; }
    }

    public class SoftGenerations
    {
        public SoftGeneration[] GetSoftGenerations()
        {
            return new SoftGeneration[]
            {
                new SoftGeneration() { Id = SoftGenerationConst.RED_GREEN, Name = SoftGenerationConst.RED_GREEN_NAME},
                new SoftGeneration() { Id = SoftGenerationConst.GOLD_SILVER, Name = SoftGenerationConst.GOLD_SILVER_NAME},
                new SoftGeneration() { Id = SoftGenerationConst.RUBY_SAPPHIRE, Name = SoftGenerationConst.RUBY_SAPPHIRE_NAME},
                new SoftGeneration() { Id = SoftGenerationConst.DIAMOND_PEARL, Name = SoftGenerationConst.DIAMOND_PEARL_NAME},
                new SoftGeneration() { Id = SoftGenerationConst.BLACK_WHITE, Name = SoftGenerationConst.BLACK_WHITE_NAME},
                new SoftGeneration() { Id = SoftGenerationConst.X_Y, Name = SoftGenerationConst.X_Y_NAME},
                new SoftGeneration() { Id = SoftGenerationConst.SUN_MOON, Name = SoftGenerationConst.SUN_MOON_NAME}
            };
        }
    }
}
