namespace Suftnet.Cos.DataAccess
{   
    using System.Collections.Generic;

    public interface ITopic
    {
        bool Find(int topicId, int chapterId);
        TopicDto Get(int Id);
        bool Delete(int Id);
        int Insert(TopicDto entity);       
        bool Update(TopicDto entity);
        List<TopicDto> GetAll(int Id);
        List<TopicDto> Fetch(int Id);


    }
}
