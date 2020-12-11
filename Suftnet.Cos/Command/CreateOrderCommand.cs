namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;    
    using System;   
    using Suftnet.Cos.Common;
    using System.Threading.Tasks;

    public class CreateOrderCommand : ICreateOrderCommand
    {
        private readonly IOrder _order;
        private readonly ITable _table;

        public CreateOrderCommand(
            IOrder order, ITable table)
        {
            _order = order;
            _table = table;
        }               
              
        public CreateOrder entityToCreate { get; set; }     

        public void Execute()
        {
            var order = new OrderDto
            {
                CreatedDT = DateTime.UtcNow,
                CreatedBy = entityToCreate.UserName,

                UpdateDate = DateTime.UtcNow,
                UpdateBy = entityToCreate.UserName,

                StartDt = DateTime.UtcNow,

                OrderTypeId = new Guid(eOrderType.DineIn),
                StatusId = new Guid(eOrderStatus.Occupied),

                TableId = new Guid(entityToCreate.TableId),
                TenantId = new Guid(entityToCreate.ExternalId),
                Id = Guid.NewGuid(),

                ExpectedGuest = entityToCreate.TableFor,
                FirstName = string.Empty,
                LastName = string.Empty
            };

           _order.Insert(order);

            entityToCreate.OrderId = order.Id;
            SetOrderTable(order);
        }

        #region private function
        private void SetOrderTable(OrderDto entityToCreate)
        {
            if (entityToCreate.StatusId == new Guid(eOrderStatus.Occupied.ToLower()))
            {              
                Task.Run(() => _table.UpdateStatus(entityToCreate.StatusId,
                        entityToCreate.TableId,
                        entityToCreate.Id, 
                        DateTime.UtcNow, 
                        entityToCreate.CreatedBy
                    ));
            }
        }
        #endregion

    }
}