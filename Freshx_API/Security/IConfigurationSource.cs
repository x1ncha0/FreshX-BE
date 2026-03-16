public class EncryptedConfigurationSource : IConfigurationSource
{
    private readonly string _password;
    private readonly byte[] _salt;

    public EncryptedConfigurationSource( string password, byte[] salt)
    {
    
        _password = password;
        _salt = salt;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new EncryptedConfigurationProvider( _password, _salt);
    }
}
