namespace Interface.Service
{
    public interface IMessagingServiceProvider<T> where T : class
    {
        void Save(T t);
    }
}
