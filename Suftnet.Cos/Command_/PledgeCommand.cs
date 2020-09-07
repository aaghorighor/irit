namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using System;
    using Suftnet.Cos.Common;

    public class PledgeCommand : IPledgeCommand
    {
        public int PledgeId { get; set; }
        public string CreatedBy { get; set; } 
        public DateTime CreatedDT {get;set;}

        private readonly IPledge _pledge;
        private readonly IPledger _pledger;

        public PledgeCommand(IPledge pledge, IPledger pledger)
        {
            _pledge = pledge;
            _pledger = pledger;
        }

        public void Execute()
        {
            var pledge = _pledge.Get(this.PledgeId);
            decimal sum = 0m;

            if (pledge != null)
            {
                var pledgers = _pledger.GetAll(this.PledgeId);

                foreach (var pledger in pledgers)
                {
                    sum += pledger.Amount.NegativeDecimalToZero();
                }
                              
                pledge.Donated = Math.Round(sum,2);
                pledge.Remaning = Math.Round(pledge.Expected.NegativeDecimalToZero() - pledge.Donated.NegativeDecimalToZero(),2);
               
               _pledge.UpdatePledge(pledge);
            }           
        }
    }
}
