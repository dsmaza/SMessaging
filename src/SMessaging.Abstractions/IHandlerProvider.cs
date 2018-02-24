namespace SMessaging.Abstractions
{
    public interface IHandlerProvider
    {
        IHandlerScope CreateScope();
    }
}
