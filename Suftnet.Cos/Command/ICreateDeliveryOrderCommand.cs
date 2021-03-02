﻿namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.Web.ViewModel;
    using System;

    public interface ICreateDeliveryOrderCommand
    {
        Guid OrderId { get; set; }
        DeliveryOrderAdapter entityToCreate { get; set; }
        void Execute();
   }

   
}