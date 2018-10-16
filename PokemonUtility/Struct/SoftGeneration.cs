namespace PokemonUtility.Struct
{
    class SoftGeneration
    {
        public static readonly int RED_GREEN = 0;
        public static readonly int GOLD_SILVER = 1;
        public static readonly int RUBY_SAPPHIRE = 2;
        public static readonly int DIAMOND_PEARL = 3;
        public static readonly int BLACK_WHITE = 4;
        public static readonly int X_Y = 5;
        public static readonly int SUN_MOON = 6;

        public static readonly string RED_GREEN_NAME = "赤緑";
        public static readonly string GOLD_SILVER_NAME = "金銀";
        public static readonly string RUBY_SAPPHIRE_NAME = "ＲＳ";
        public static readonly string DIAMOND_PEARL_NAME = "ＤＰ";
        public static readonly string BLACK_WHITE_NAME = "黒白";
        public static readonly string X_Y_NAME = "ＸＹ";
        public static readonly string SUN_MOON_NAME = "ＳＭ";

        // ソフトの世代ID
        public int Id { get; set; }

        // ソフトの世代名
        public string Name { get; set; }
    }
}
