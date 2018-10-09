using System;

namespace PokemonUtility
{
    class ObjectConverter
    {
        public static string ToString(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            else
            {
                return value.ToString();
            }
        }

        public static int ToInt(object value)
        {
            int result;
            if (int.TryParse(ToString(value), out result))
            {
                return Convert.ToInt32(value);
            }
            else
            {
                return 0;
            }
        }
    }
}
