namespace ProgettoIngegneriaSoftware.Models.HttpResponseObjects
{
    public struct OkData
    {
        public string Message { get; private set; }

        public OkData(string message)
        {
            Message = message;
        }

        public OkData()
        {
            Message = string.Empty;
        }

    }
}
