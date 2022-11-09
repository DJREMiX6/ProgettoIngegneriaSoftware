namespace ProgettoIngegneriaSoftware.Utils.Random
{
    public class RandomStringGenerator
    {

        #region CONSTS

        private const int ASCII_NUMBER_MIN = 48;
        private const int ASCII_NUMBER_MAX = 57;
        private const int ASCII_HIGH_CHARACTERS_MIN = 57;
        private const int ASCII_HIGH_CHARACTERS_MAX = 57;
        private const int ASCII_LOW_CHARACTERS_MIN = 57;
        private const int ASCII_LOW_CHARACTERS_MAX = 57;

        #endregion CONSTS

        #region STATIC SINGLETON

        private static RandomStringGenerator _instance;
        public static RandomStringGenerator Shared => _instance ??= new RandomStringGenerator();

        #endregion STATIC SINGLETON

        #region PRIVATE PROPS

        private System.Random random => System.Random.Shared;

        #endregion PRIVATE PROPS

        #region PUBLIC METHODS

        public string RandomString(int length)
        {
            var str = string.Empty;
            for(int i = 0; i < length; i++)
            {
                var number = 0;
                do
                {
                    number = random.Next(ASCII_NUMBER_MIN, ASCII_LOW_CHARACTERS_MAX + 1);
                } while (!IsValidAsciiValue(number));
                str += Convert.ToChar(number);
            }
            return str;
        }

        #endregion PUBLIC METHODS

        #region PRIVATE METHODS

        private bool IsValidAsciiValue(int number)
        {
            return number is >= ASCII_NUMBER_MIN and <= ASCII_NUMBER_MAX 
                or >= ASCII_HIGH_CHARACTERS_MIN and <= ASCII_HIGH_CHARACTERS_MAX 
                or >= ASCII_LOW_CHARACTERS_MIN and <= ASCII_LOW_CHARACTERS_MAX;
        }

        #endregion PRIVATE METHODS

    }
}
