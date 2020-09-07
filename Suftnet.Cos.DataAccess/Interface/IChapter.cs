namespace Suftnet.Cos.DataAccess
{      
    public interface IChapter : IRepository<ChapterDto>
    {
        bool Find(int subSectionId);
    }
}
