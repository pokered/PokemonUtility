namespace PokemonUtility.Struct
{
    class BattleResult
    {
        public static readonly int RESULT_WIN = 0;
        public static readonly int RESULT_LOSE = 1;
        public static readonly int RESULT_DRAW = 2;

        public static readonly string RESULT_WIN_JAPANESE = "勝";
        public static readonly string RESULT_LOSE_JAPANESE = "負";
        public static readonly string RESULT_DRAW_JAPANESE = "引";

        public static readonly string RESULT_WIN_ENGLISH = "WIN";
        public static readonly string RESULT_LOSE_ENGLISH = "LOSE";
        public static readonly string RESULT_DRAW_ENGLISH = "DRAW";

        // 対戦結果ID
        public int Id { get; set; }

        // 対戦結果
        public string Name { get; set; }

        // 対戦結果反転
        public static int FlipBattleResult(int battleResultId)
        {
            if (battleResultId == RESULT_WIN) return RESULT_LOSE;
            if (battleResultId == RESULT_LOSE) return RESULT_WIN;
            return RESULT_DRAW;
        }

        public static string ToBattleResultJapanese(int battleResultId)
        {
            if (battleResultId == BattleResult.RESULT_WIN) return RESULT_WIN_JAPANESE;
            if (battleResultId == BattleResult.RESULT_LOSE) return RESULT_LOSE_JAPANESE;
            if (battleResultId == BattleResult.RESULT_DRAW) return RESULT_DRAW_JAPANESE;
            return "";
        }

        public static string ToBattleResultEnglish(int battleResultId)
        {
            if (battleResultId == BattleResult.RESULT_WIN) return RESULT_WIN_ENGLISH;
            if (battleResultId == BattleResult.RESULT_LOSE) return RESULT_LOSE_ENGLISH;
            if (battleResultId == BattleResult.RESULT_DRAW) return RESULT_DRAW_ENGLISH;
            return "";
        }
    }
}
