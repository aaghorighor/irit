namespace Suftnet.Cos.DataAccess
{
    using System.Collections.Generic;

    public interface ITour : IRepository<TourDto>
    {
        List<TourDto> LoadTours();
    }
}
