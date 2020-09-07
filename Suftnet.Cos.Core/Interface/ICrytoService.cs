namespace Suftnet.Cos.Core
{
    public interface ICrytoService
    {
        string EncryptString(string data);
        string DecryptString(string data);
    }
}
