namespace ProgettoIngegneriaSoftware.API.Security
{
    public struct HashResult
    {

        #region CTORS

        public HashResult()
        {
            Hash = null;
            Salt = null;
        }

        public HashResult(byte[]? hash, byte[]? salt)
        {
            Hash = hash;
            Salt = salt;
        }

        #endregion CTORS

        #region PUBLIC PROPS

        public byte[]? Hash { get; set; } = null;
        public byte[]? Salt { get; set; } = null;

        #endregion PUBLIC PROPS

    }
}
