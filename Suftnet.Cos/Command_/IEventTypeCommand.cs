namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using System.Collections.Generic;

    public interface IEventTypeCommand : ICommand
    {
        List<EventDto> Events { get; set; }
        string externalTenantId { get; set; }
        int EventTypeId { get; set; }
    }
}