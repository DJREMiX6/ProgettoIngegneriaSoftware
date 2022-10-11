namespace ProgettoIngegneriaSoftware.Models.HttpResponseObjects
{
    public struct BadRequestData
    {

        public string Error { get; private set; }

        public BadRequestData(string message)
        {
            Error = message;
        }

        public BadRequestData()
        {
            Error = string.Empty;
        }

    }
}
