namespace Suftnet.Cos.DataAccess
{
    using System.Collections.Generic;

    public interface IEditor : IRepository<EditorDTO>
    {
        List<EditorDTO> GetAll(int Id, int tenantId);
    }
}
