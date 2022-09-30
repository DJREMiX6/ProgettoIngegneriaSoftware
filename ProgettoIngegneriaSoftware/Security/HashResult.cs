namespace ProgettoIngegneriaSoftware.Security
{
    public struct HashResult
    {
        public byte[]? Hash { get; set; } = null;
        public byte[]? Salt { get; set; } = null;
    }
}
