namespace Suftnet.Cos.DataAccess
{   
    using System.Collections.Generic;

    public interface ISubTopic
    {
        SubTopicDto Get(int Id);
        bool Delete(int Id);
        int Insert(SubTopicDto entity);       
        bool Update(SubTopicDto entity);
        List<SubTopicDto> GetAll(int Id);
        List<SubTopicDto> Fetch(int Id);


    }
}
