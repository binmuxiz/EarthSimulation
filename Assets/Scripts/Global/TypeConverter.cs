using System.Text;

namespace Global
{
    public static class TypeConverter
    {
        public static string ByteToString(byte[] val)
        {
            return Encoding.Default.GetString(val);
        }

        public static byte[] StringToByte(string val)
        {
            return Encoding.UTF8.GetBytes(val);
        }
    }
}