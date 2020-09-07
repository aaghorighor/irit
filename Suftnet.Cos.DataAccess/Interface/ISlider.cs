namespace Suftnet.Cos.DataAccess
{
    using System.Collections.Generic;

    public interface ISlider : IRepository<SliderDto>
    {
        IEnumerable<SliderDto> LoadSlides();
    }
}
