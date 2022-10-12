namespace ProgettoIngegneriaSoftware.Models.HttpResponseObjects
{
    public struct InternalServerErrorData
    {

        #region PROPS

        public int ErrorCode { get; private set; }

        public string ErrorMessage { get; private set; }

        #endregion PROPS

        #region CTORS

        public InternalServerErrorData(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public InternalServerErrorData(int errorCode)
        {
            ErrorCode = errorCode;
            ErrorMessage = string.Empty;
        }

        public InternalServerErrorData(string errorMessage)
        {
            ErrorCode = -1;
            ErrorMessage = errorMessage;
        }

        #endregion CTORS

    }
}
