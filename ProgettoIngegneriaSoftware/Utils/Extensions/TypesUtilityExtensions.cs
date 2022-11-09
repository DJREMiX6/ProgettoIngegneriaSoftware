using System.IO.Pipelines;
using System.Text;

namespace ProgettoIngegneriaSoftware.Utils.Extensions
{
    public static class BaseTypeExtensions
    {

        #region BOOL

        /// <summary>
        /// Does a comparison byte per byte of the two arrays
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

        #endregion BOOL

        #region LONG

        ///9007199254740991 is equal to JavaScript Number.MAX_SAFE_INTEGER to avoid wrong number conversion
        public static bool IsValidConfirmationToken(this long confirmationToken) => confirmationToken is > 12345 and < 9007199254740991;

        #endregion LONG

        #region STRING

        /// <summary>
        /// Checks if a <seealso cref="string"/> contains at least one <seealso cref="char"/> from a <seealso cref="char"/> array
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chars"></param>
        /// <returns><c>true</c> if the <seealso cref="string"/> contains at least on of the <seealso cref="char"/> from the <seealso cref="char"/> array. 
        /// Otherwise it returns <c>false</c></returns>
        public static bool Contains(this string str, char[] chars)
        {
            foreach(char stringChar in str)
            {
                foreach(char c in chars)
                {
                    if(stringChar.Equals(c))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static byte[] GetUTF8Bytes(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        #endregion STRING

        #region TIMESPAN

        public static DateTime ToDateTime(this TimeSpan timeSpan)
        {
            return new DateTime(timeSpan.Ticks);
        }

        #endregion TIMESPAN

        #region DATETIME

        public static TimeSpan ToTimeSpan(this DateTime dateTime)
        {
            return new TimeSpan(dateTime.Ticks);
        }

        #endregion DATETIME

        #region PATHSTRING

        /// <summary>
        /// Determines if the beginning of this <seealso cref="PathString"/> instance matches at least one of the <seealso cref="PathString"/> instances in the collection.
        /// </summary>
        /// <param name="pathString"></param>
        /// <param name="pathStrings"></param>
        /// <returns><c>True</c> if one of the <seealso cref="PathString"/> instances matches this <seealso cref="PathString"/> instance, otherwise, <c>false</c>.</returns>
        public static bool StartsWithSegments(this PathString pathString, ICollection<PathString> pathStrings)
        {
            foreach (var path in pathStrings)
            {
                if (pathString.StartsWithSegments(path))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion PATHSTRING

        #region STREAM

        public static async Task WriteLineAsync(this Stream stream, string message)
        {
            var messageBytes = (message + Environment.NewLine).GetUTF8Bytes();
            await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
        }

        #endregion STREAM

        #region PIPEWRITER

        public static async Task WriteLineEndAsync(this PipeWriter pipeWriter, string message)
        {
            var messageBytes = (message + Environment.NewLine).GetUTF8Bytes();
            await pipeWriter.WriteAsync(messageBytes);
            await pipeWriter.FlushAsync();
            await pipeWriter.CompleteAsync();
        }

        #endregion PIPEWRITER

    }
}
