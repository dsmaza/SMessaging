namespace SMessaging.Abstractions
{
    public struct MessageResult
    {
        public static readonly MessageResult Null = new MessageResult(0, "NULL");

        public MessageResult(int code, object value) : this()
        {
            Code = code;
            Value = value;
        }

        public int Code { get; }

        public object Value { get; }
    }
}
