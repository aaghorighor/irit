namespace Suftnet.Cos.Core
{
    internal class LocalizeContentCryptoParameter : CrypoParameterBase
    {
        public LocalizeContentCryptoParameter(byte[] cryptoKey, byte[] cryptoIV)
            : base(cryptoKey, cryptoIV)
        {

        }
        public string Path { get; set; }
        public string Key { get; set; }
    }
}
