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

            return value.ToString();
        }

        public static int ToInt(object value)
        {
            int result;
            if (int.TryParse(ToString(value), out result))
            {
                return Convert.ToInt32(value);
            }

            return 0;
        }

        public static bool ToBoolean(object value)
        {
            if (value == null) return false;

            if (Convert.ToBoolean(ToString(value))) return true;

            return false;
        }
    }
}
