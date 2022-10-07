namespace ProgettoIngegneriaSoftware.Utils.Extensions
{
    public static class BaseTypeExtensions
    {
        public static bool EqualsByByte(this byte[] byteArray1, byte[] byteArray2)
        {
            if (byteArray1.Length != byteArray2.Length)
            {
                return false;
            }
            for (int i = 0; i < byteArray1.Length; i++)
            {
                if (byteArray1[i] != byteArray2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
