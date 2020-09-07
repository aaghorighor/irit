namespace Suftnet.Cos.Web
{
    using System;

    public interface IPerRequest
    {
        object GetValue(object key);
        void SetValue(object key, object value);
        void RemoveValue(object key);

        event EventHandler EndRequest;
    }
}
