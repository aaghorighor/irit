namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using System.Collections.Generic;

    public interface IEventListCommand : ICommand
    {
        List<EventDto> Events { get; set; }
        string externalTenantId { get; set; }        
    }
}