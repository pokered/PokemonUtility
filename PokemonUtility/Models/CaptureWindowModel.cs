namespace PokemonUtility.Models
{
    class CaptureWindowModel : WindowModel
    {
        #region Singleton

        static CaptureWindowModel Instance;
        public static CaptureWindowModel GetInstance()
        {
            if (Instance == null)
                Instance = new CaptureWindowModel();
            return Instance;
        }

        #endregion
    }
}
