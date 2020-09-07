namespace Suftnet.Cos.Web.Command
{

    using Common;
    using DataAccess;
    using Suftnet.Cos.Extension; 
    using System;
    using System.Collections.Generic;
    using ViewModel;
    using System.Linq;

    public class IncomeCommand : ICommand
    {      
        private readonly IIncome __income;      

        public IncomeCommand(IIncome income)
        {         
            __income = income;         
        }

        public QuaryModel QuaryModel { get; set; }
        public ResponseModelAdapter ResponseModelAdapter { get; set; }

        public void Execute()
        {
            PrepareIncome();
        }    

        private void PrepareIncome()
        {
            var incomes = Fetch();
            var resposeAdapter = new ResponseModelAdapter();

            foreach(var income in incomes)
            {
                var responseModel = new ResponseModel
                {
                    Date = income.CreatedDT.ToLongDateString(),
                    Amount = income.Amount,
                    Reference = income.MemberReference,
                    IncomeType = income.IncomeType
                };

                resposeAdapter.Response.Add(responseModel);
            }

            resposeAdapter.Amount = resposeAdapter.Response.Sum(x => x.Amount);

            ResponseModelAdapter = resposeAdapter;
        }       

        private IEnumerable<IncomeDto> Fetch()
        {
            var termDto = new TermDto
            {
                StartDate = QuaryModel.StartDate,
                EndDate = QuaryModel.EndDate,
                TenantId = QuaryModel.ExternalId.ToDecrypt().ToInt()
            };

            var incomes = __income.Get(termDto);

            if(QuaryModel.IncomeTypeId != null)
            {
                incomes = incomes.Where(x => x.IncomeTypeId == QuaryModel.IncomeTypeId);
            }

            return incomes;
        }       
    }
}