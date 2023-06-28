namespace ProgettoIngegneriaSoftware.API.Extensions;

public static class UtilityExtensions
{

    /// <summary>
    /// Does a comparison byte to byte of the two arrays
    /// </summary>
    /// <param name="byteArray1"></param>
    /// <param name="byteArray2"></param>
    /// <returns><c>true</c> if every byte of <c>byteArray1</c> is equal and in the same position inside of <c>byteArray2</c></returns>
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