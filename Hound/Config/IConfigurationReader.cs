namespace Hound.Config
{
    public interface IConfigurationReader
    {
        string Read(string key);
    }
}
