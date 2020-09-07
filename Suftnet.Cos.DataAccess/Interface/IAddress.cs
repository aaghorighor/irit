namespace Suftnet.Cos.DataAccess
{
    public interface IAddress 
    {
        AddressDto Get(Action.Address o);
        bool UpdateByAddressId(AddressDto entity);

        AddressDto Get(int id);       
        int Insert(AddressDto entity);      
        bool Delete(int id);
        bool Update(AddressDto entity);
    }
}
