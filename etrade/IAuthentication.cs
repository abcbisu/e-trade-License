namespace etrade
{
    public interface IAuthentication<T>
    {
        T Authenticate();
    }
}