namespace Typing.DataAccess
{
    public interface IAsyncProvider
    {
        void Initialize();
        void BeginRefresh();
        void EndRefresh();
    }
}
