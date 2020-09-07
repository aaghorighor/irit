namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;

    public interface IEventCommand : ICommand
    {
        EventDto Event { get; set; }
        string externalTenantId { get; set; }
        string externalId { get; set; }
    }
}
