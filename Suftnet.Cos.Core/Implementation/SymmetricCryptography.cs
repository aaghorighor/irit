namespace Suftnet.Cos.Core
{
    using System;
    using System.IO;
    using System.Text;
    using System.Security.Cryptography;

    class SymmetricCryptography<T> where T : SymmetricAlgorithm, new()
    {
        #region Fields

        private T _provider = new T();
        private UTF8Encoding _utf8 = new UTF8Encoding();

        #endregion Fields

        #region Properties

        private byte[] _key;
        public byte[] Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private byte[] _iv;
        public byte[] IV
        {
            get { return _iv; }
            set { _iv = value; }
        }

        #endregion Properties

        #region Constructors

        public SymmetricCryptography()
        {
            _provider.GenerateKey();
            _key = _provider.Key;
            _provider.GenerateIV();
            _iv = _provider.IV;
        }

        public SymmetricCryptography(byte[] key, byte[] iv)
        {
            _key = key;
            _iv = iv;
        }

        #endregion Constructors

        #region Byte Array Methods

        public byte[] Encrypt(byte[] input)
        {
            return Encrypt(input, _key, _iv);
        }

        public byte[] Decrypt(byte[] input)
        {
            return Decrypt(input, _key, _iv);
        }

        public byte[] Encrypt(byte[] input, byte[] key, byte[] iv)
        {
            return Transform(input,
                   _provider.CreateEncryptor(key, iv));
        }

        public byte[] Decrypt(byte[] input, byte[] key, byte[] iv)
        {
            return Transform(input,
                   _provider.CreateDecryptor(key, iv));
        }

        #endregion Byte Array Methods

        #region String Methods

        public string Encrypt(string text)
        {
            return Encrypt(text, _key, _iv);
        }

        public string Decrypt(string text)
        {
            return Decrypt(text, _key, _iv);
        }

        public string Encrypt(string text, byte[] key, byte[] iv)
        {
            var input = _utf8.GetBytes(text);
            var transform = _provider.CreateEncryptor(key, iv);
            byte[] output = Transform(input, transform);

            var result = Convert.ToBase64String(output);
            result = result.Replace('+', '-').Replace('/', '_');
            result = result.TrimEnd('=');
            return result;
        }

        public string Decrypt(string text, byte[] key, byte[] iv)
        {
            text = text.Trim().Replace('-', '+').Replace('_', '/');
            byte[] input = null;
            int loop = 0;
            while (true)
            {
                if (text.Length % 4 != 0)
                {
                    text = text + '=';
                    loop++;
                }
                else
                {
                    try
                    {
                        input = Convert.FromBase64String(text);
                        break;
                    }
                    catch
                    {
                        text = text + '=';
                        loop++;
                    }
                }
                if (loop > 2)
                {
                    break;
                }
            }
            var transform = _provider.CreateDecryptor(key, iv);

            byte[] output = Transform(input, transform);

            return _utf8.GetString(output);
        }


        #endregion String Methods

        #region Stream Methods

        public void Encrypt(Stream input, Stream output)
        {
            Encrypt(input, output, _key, _iv);
        }

        public void Decrypt(Stream input, Stream output)
        {
            Decrypt(input, output, _key, _iv);
        }

        public void Encrypt(Stream input, Stream output, byte[] key, byte[] iv)
        {
            TransformStream(true, ref input, ref output, key, iv);
        }

        public void Decrypt(Stream input, Stream output, byte[] key, byte[] iv)
        {
            TransformStream(false, ref input, ref output, key, iv);
        }

        #endregion Stream Methods

        #region Private Methods

        private byte[] Transform(byte[] input, ICryptoTransform CryptoTransform)
        {
            // create the necessary streams
            using (var memStream = new MemoryStream())
            using (var cryptStream = new CryptoStream(memStream, CryptoTransform, CryptoStreamMode.Write))
            {
                // transform the bytes as requested
                cryptStream.Write(input, 0, input.Length);
                cryptStream.FlushFinalBlock();
                // Read the memory stream and
                // convert it back into byte array
                memStream.Position = 0;
                var result = memStream.ToArray();
              
                return result;
            }               
            
        }

        private void TransformStream(bool encrypt, ref Stream input, ref Stream output, byte[] key, byte[] iv)
        {
            // defensive argument checking
            if (input == null)
                throw new ArgumentNullException("input");
            if (output == null)
                throw new ArgumentNullException("output");
            if (!input.CanRead)
                throw new ArgumentException("Unable to read from the input Stream.", "input");
            if (!output.CanWrite)
                throw new ArgumentException("Unable to write to the output Stream.", "input");
            // make the buffer just large enough for 
            // the portion of the stream to be processed
            var inputBuffer = new byte[input.Length - input.Position];
            // read the stream into the buffer
            input.Read(inputBuffer, 0, inputBuffer.Length);
            // transform the buffer
            var outputBuffer = encrypt ? Encrypt(inputBuffer, key, iv)
                                          : Decrypt(inputBuffer, key, iv);
            // write the transformed buffer to our output stream 
            output.Write(outputBuffer, 0, outputBuffer.Length);
        }

        #endregion Private Methods
    }
}
