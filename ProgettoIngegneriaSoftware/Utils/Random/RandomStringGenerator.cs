namespace ProgettoIngegneriaSoftware.Utils.Random
{
    public class RandomStringGenerator
    {

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
                str += Convert.ToChar(random.Next(0, 26));
            }
            return str;
        }

        #endregion PUBLIC PROPS

    }
}
