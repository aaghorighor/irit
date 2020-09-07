namespace Suftnet.Cos.DataAccess
{    
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;

    public class Global : IGlobal
    {
        public int Insert(GlobalDto entity)
        {          
            using (var context = DataContextFactory.CreateContext())
            {
                var global = new Action.Global {  TaxRate = entity.TaxRate, ServerEmail = entity.ServerEmail, CurrencyId = entity.CurrencyId, DateTimeFormat = entity.DateTimeFormat, Email = entity.Email, Password = entity.Password, UserName = entity.UserName, Server = entity.Server, Company = entity.Company, Telephone = entity.Telephone, Mobile = entity.Mobile, Description = entity.Description, Port = entity.Port, Id = entity.Id };
                context.Globals.Add(global);
                context.SaveChanges();                
            }
            return entity.Id;
        }

        public bool Update(GlobalDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Globals.Single(o => o.Id == entity.Id);

                if (objToUpdate != null)
                {
                    objToUpdate.CurrencyId = entity.CurrencyId;
                    objToUpdate.DateTimeFormat = entity.DateTimeFormat != null ? entity.DateTimeFormat : objToUpdate.DateTimeFormat ;                  
                    objToUpdate.Company = entity.Company;
                    objToUpdate.Telephone = entity.Telephone;                   
                    objToUpdate.Description = entity.Description;
                    objToUpdate.Mobile = entity.Mobile;
                    objToUpdate.Email = entity.Email;
                    objToUpdate.ServerEmail = entity.ServerEmail; 
                    objToUpdate.Password = entity.Password;
                    objToUpdate.Server = entity.Server;                  
                    objToUpdate.UserName = entity.UserName;                   
                    objToUpdate.Port = entity.Port;                  
                    objToUpdate.TaxRate = entity.TaxRate;

                    try
                    {
                        context.SaveChanges();                      
                    }
                    catch (ChangeConflictException)
                    {
                   
                        context.SaveChanges();                       
                    }                 
                }
            }

            return true;
        }       
             
        public GlobalDto Get()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Globals
                              join a in context.Addresses on o.AddressId equals a.Id
                              let currencySymbol = (from c in context.Commons
                                                    where c.ID == o.CurrencyId
                                                    select c.code).FirstOrDefault()
                              select new GlobalDto
                              {
                                  AddressId = a.Id,
                                  AddressLine1 = a.AddressLine1,
                                  AddressLine2 = a.AddressLine2,
                                  AddressLine3 = a.AddressLine3,                                  
                                  Country = a.Country,
                                  PostCode = a.PostCode,
                                  CurrencyId = o.CurrencyId,
                                  CurrencySymbol = currencySymbol,
                                  DateTimeFormat = o.DateTimeFormat,
                                  Email = o.Email,
                                  ServerEmail = o.ServerEmail,  
                                  Password = o.Password,
                                  UserName = o.UserName,
                                  Company = o.Company,
                                  Telephone = o.Telephone,
                                  Mobile = o.Mobile,
                                  Description = o.Description,
                                  Server = o.Server,
                                  Port = o.Port,                                 
                                  Id = o.Id,                                 
                                  TaxRate = o.TaxRate
                              }).FirstOrDefault();
                return result;
            }
        }       
    }
}
