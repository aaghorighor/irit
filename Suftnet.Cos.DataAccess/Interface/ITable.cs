﻿namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    public interface ITable 
    {
        int GetOccupiedCount(Guid tenantId);
        int GetFreeCount(Guid tenantId);
        bool Update(TableDto entity);
        TableDto Get(Guid Id);
        bool Delete(Guid Id);
        Guid Insert(TableDto entity);
        List<TableDto> GetAll(Guid tenantId);
        List<TableDto> GetByStatus(bool status, Guid tenantId);     
        bool UpdateStatus(Guid statusId, Guid tableId, Guid orderId, DateTime updatedDt, string updateBy);
    }
}

