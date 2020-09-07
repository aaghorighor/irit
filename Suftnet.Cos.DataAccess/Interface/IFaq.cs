namespace Suftnet.Cos.DataAccess
{   
    using System.Collections.Generic;

    public interface IFaq : IRepository<FaqDto>
    {                    
        List<FaqDto> LoadFaq();    
      
    }
}
