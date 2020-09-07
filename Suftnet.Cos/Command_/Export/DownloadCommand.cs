namespace Suftnet.Cos.Web.Command
{
    using System.IO;
    using System.IO.Compression;

    public class DownloadCommand : ICommand
    {
        private readonly IFactoryCommand _factoryCommand;

        public DownloadCommand(IFactoryCommand factoryCommand)
        {
            _factoryCommand = factoryCommand;
        }

        public int TenantId { get; set; }
        public MemoryStream Content { get; set; }
        public void Execute()
        {
            var commandTenant = _factoryCommand.Create<ExportTenantCommand>();
            commandTenant.TenantId = this.TenantId;
            commandTenant.Execute();

            var memberCommand = _factoryCommand.Create<ExportMemberCommand>();
            memberCommand.TenantId = this.TenantId;
            memberCommand.Execute();

            var assetCommand = _factoryCommand.Create<ExportAssetCommand>();
            assetCommand.TenantId = this.TenantId;
            assetCommand.Execute();

            var eventCommand = _factoryCommand.Create<ExportEventCommand>();
            eventCommand.TenantId = this.TenantId;
            eventCommand.Execute();

            var attendanceCommand = _factoryCommand.Create<ExportAttendanceCommand>();
            attendanceCommand.TenantId = this.TenantId;
            attendanceCommand.Execute();

            var incomeCommand = _factoryCommand.Create<ExportIncomeCommand>();
            incomeCommand.TenantId = this.TenantId;
            incomeCommand.Execute();
           
            var smallGroupCommand = _factoryCommand.Create<ExportSmallGroupCommand>();
            smallGroupCommand.TenantId = this.TenantId;
            smallGroupCommand.Execute();

            var venueCommand = _factoryCommand.Create<ExportVenueCommand>();
            venueCommand.TenantId = this.TenantId;
            venueCommand.Execute();

            var pledgeCommand = _factoryCommand.Create<ExportPledgeCommand>();
            pledgeCommand.TenantId = this.TenantId;
            pledgeCommand.Execute();

            using (MemoryStream ms = new MemoryStream())
            {
                using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    var zipArchiveEntry = archive.CreateEntry("Tenant.xls", CompressionLevel.Fastest);
                    if(commandTenant.Content != null)
                    {
                        using (var zipStream = zipArchiveEntry.Open()) zipStream.Write(commandTenant.Content.ToArray(), 0, commandTenant.Content.ToArray().Length);
                    }

                    if(memberCommand.Content != null)
                    {
                        zipArchiveEntry = archive.CreateEntry("Membership.xls", CompressionLevel.Fastest);
                        using (var zipStream = zipArchiveEntry.Open()) zipStream.Write(memberCommand.Content.ToArray(), 0, memberCommand.Content.ToArray().Length);
                    }
              
                    if(assetCommand.Content != null)
                    {
                        zipArchiveEntry = archive.CreateEntry("Asset.xls", CompressionLevel.Fastest);
                        using (var zipStream = zipArchiveEntry.Open()) zipStream.Write(assetCommand.Content.ToArray(), 0, assetCommand.Content.ToArray().Length);
                    }

                    if(eventCommand.Content != null)
                    {
                        zipArchiveEntry = archive.CreateEntry("Events.xls", CompressionLevel.Fastest);
                        using (var zipStream = zipArchiveEntry.Open()) zipStream.Write(eventCommand.Content.ToArray(), 0, eventCommand.Content.ToArray().Length);
                    }
                   
                    if(attendanceCommand.Content != null)
                    {
                        zipArchiveEntry = archive.CreateEntry("Attendance.xls", CompressionLevel.Fastest);
                        using (var zipStream = zipArchiveEntry.Open()) zipStream.Write(attendanceCommand.Content.ToArray(), 0, attendanceCommand.Content.ToArray().Length);
                    }

                    if (incomeCommand.Content != null)
                    {
                        zipArchiveEntry = archive.CreateEntry("Income.xls", CompressionLevel.Fastest);
                        using (var zipStream = zipArchiveEntry.Open()) zipStream.Write(incomeCommand.Content.ToArray(), 0, incomeCommand.Content.ToArray().Length);
                    }
                   
                    if (smallGroupCommand.Content != null)
                    {
                        zipArchiveEntry = archive.CreateEntry("Group.xls", CompressionLevel.Fastest);
                        using (var zipStream = zipArchiveEntry.Open()) zipStream.Write(smallGroupCommand.Content.ToArray(), 0, smallGroupCommand.Content.ToArray().Length);
                    }

                    if (venueCommand.Content != null)
                    {
                        zipArchiveEntry = archive.CreateEntry("Venue.xls", CompressionLevel.Fastest);
                        using (var zipStream = zipArchiveEntry.Open()) zipStream.Write(venueCommand.Content.ToArray(), 0, venueCommand.Content.ToArray().Length);
                    }

                    if (pledgeCommand.Content != null)
                    {
                        zipArchiveEntry = archive.CreateEntry("Pledge.xls", CompressionLevel.Fastest);
                        using (var zipStream = zipArchiveEntry.Open()) zipStream.Write(pledgeCommand.Content.ToArray(), 0, pledgeCommand.Content.ToArray().Length);
                    }

                    Content = ms;
                };
            }
        }
    }
}