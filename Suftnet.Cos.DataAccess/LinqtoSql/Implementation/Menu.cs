namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;

    public class Menu : IMenu
    {
        public MenuDto Get(Guid Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Menus
                                 join c in context.Categories on o.CategoryId equals c.Id
                                 join u in context.Units on o.UnitId equals u.Id                                                            
                                 where o.Id == Id
                                 select new MenuDto {ImageUrl = o.ImageUrl, IsKitchen = o.IsKitchen, CutOff = o.CutOff, SubStractId = o.SubstractId, Name = o.Name, Quantity = o.Quantity, Active = o.Active, Unit = u.Name, Category = c.Name, CategoryId = o.CategoryId, Description = o.Description, UnitId = o.UnitId, Price = o.Price, CreatedBy = o.CreatedBy, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        } 
    
        public bool Delete(Guid Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Menus.SingleOrDefault(o => o.Id == Id);
                if (objToDelete != null)
                {
                    context.Menus.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;          
        }      

        public Guid Insert(MenuDto entity)
        {          
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Menu() { Id = entity.Id, TenantId = entity.TenantId, ImageUrl = entity.ImageUrl, CutOff = entity.CutOff, IsKitchen = entity.IsKitchen, SubstractId = entity.SubStractId, Quantity = entity.Quantity, Name = entity.Name, Active = entity.Active, CreatedDt = entity.CreatedDT, CategoryId = entity.CategoryId, Description = entity.Description,UnitId = entity.UnitId, Price = entity.Price, CreatedBy = entity.CreatedBy };
                context.Menus.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }          
        }

        public bool Update(MenuDto entity)
        {
            bool response = false;
            
                using (var context = DataContextFactory.CreateContext())
                {
                    var objToUpdate = context.Menus.SingleOrDefault(o => o.Id == entity.Id);

                    if (objToUpdate != null)
                    {
                        objToUpdate.Active = entity.Active;                       
                        objToUpdate.Description = entity.Description;
                        objToUpdate.CategoryId = entity.CategoryId;                        
                        objToUpdate.Price = entity.Price;                       
                        objToUpdate.UnitId = entity.UnitId;                   
                        objToUpdate.SubstractId = entity.SubStractId;
                        objToUpdate.Name = entity.Name;
                        objToUpdate.CutOff = entity.CutOff;
                        objToUpdate.IsKitchen = entity.IsKitchen;
                        objToUpdate.Quantity = entity.Quantity;
                        objToUpdate.ImageUrl = entity.ImageUrl;

                    try
                    {
                        context.SaveChanges();
                        response = true;
                    }
                    catch (ChangeConflictException)
                    {
                     
                        response = true;
                    }
                }

                return response;
            }            
        }        
        public List<MenuDto> GetAll(Guid tenantId, int iskip, int itake)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Menus
                                 join c in context.Categories on o.CategoryId equals c.Id
                                 join u in context.Units on o.UnitId equals u.Id
                                 where o.TenantId == tenantId
                                 orderby o.Id descending
                                 select new MenuDto { CreatedDT = o.CreatedDt, ImageUrl = o.ImageUrl, IsKitchen = o.IsKitchen, CutOff = o.CutOff, SubStractId = o.SubstractId, Name = o.Name, Quantity = o.Quantity, Active = o.Active, Unit = u.Name, Category = c.Name, CategoryId = o.CategoryId, Description = o.Description, UnitId = o.UnitId, Price = o.Price, CreatedBy = o.CreatedBy, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }
        }

        public List<MenuDto> GetAll(Guid tenantId, int iskip, int itake, string isearch)
        {
            if(!string.IsNullOrEmpty(isearch))
            {
                using (var context = DataContextFactory.CreateContext())
                {
                    var objResult = (from o in context.Menus
                                     join c in context.Categories on o.CategoryId equals c.Id
                                     join u in context.Units on o.UnitId equals u.Id
                                     where o.TenantId == tenantId && o.Name.Contains(isearch)
                                     orderby o.Id descending
                                     select new MenuDto { CreatedDT = o.CreatedDt, ImageUrl = o.ImageUrl, IsKitchen = o.IsKitchen, CutOff = o.CutOff, SubStractId = o.SubstractId, Name = o.Name, Quantity = o.Quantity, Active = o.Active, Unit = u.Name, Category = c.Name, CategoryId = o.CategoryId, Description = o.Description, UnitId = o.UnitId, Price = o.Price, CreatedBy = o.CreatedBy, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                    return objResult;
                }
            }
            else
            {
                return GetAll(tenantId, iskip, itake);
            }
           
        }
        public List<MenuDto> GetAll(Guid tenantId, Guid categoryId, int iskip, int itake)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Menus
                                 join c in context.Categories on o.CategoryId equals c.Id
                                 join u in context.Units on o.UnitId equals u.Id
                                 where o.TenantId == tenantId && o.CategoryId == categoryId
                                 orderby o.Id descending
                                 select new MenuDto { ImageUrl = o.ImageUrl, IsKitchen = o.IsKitchen, CutOff = o.CutOff, SubStractId = o.SubstractId, Name = o.Name, Quantity = o.Quantity, Active = o.Active, Unit = u.Name, Category = c.Name, CategoryId = o.CategoryId, Description = o.Description, UnitId = o.UnitId, Price = o.Price, CreatedBy = o.CreatedBy, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }
        }

        public List<MenuDto> GetAll(Guid tenantId, Guid categoryId, int iskip, int itake, string isearch)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Menus
                                 join c in context.Categories on o.CategoryId equals c.Id
                                 join u in context.Units on o.UnitId equals u.Id
                                 where o.TenantId == tenantId && (o.CategoryId == categoryId && o.Name.Contains(isearch))
                                 orderby o.Id descending
                                 select new MenuDto { ImageUrl = o.ImageUrl, IsKitchen = o.IsKitchen, CutOff = o.CutOff, SubStractId = o.SubstractId, Name = o.Name, Quantity = o.Quantity, Active = o.Active, Unit = u.Name, Category = c.Name, CategoryId = o.CategoryId, Description = o.Description, UnitId = o.UnitId, Price = o.Price, CreatedBy = o.CreatedBy, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }
        }

        public List<MenuDto> GetAll(Guid categoryId, Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Menus
                                 join c in context.Categories on o.CategoryId equals c.Id
                                 join u in context.Units on o.UnitId equals u.Id
                                 where o.CategoryId == categoryId && o.TenantId == tenantId
                                 orderby o.Id descending 
                                 select new MenuDto { ImageUrl = o.ImageUrl, IsKitchen = o.IsKitchen, CutOff = o.CutOff, SubStractId = o.SubstractId, Name = o.Name, Quantity = o.Quantity, Active = o.Active, Unit = u.Name, Category = c.Name, CategoryId = o.CategoryId, Description = o.Description, UnitId = o.UnitId, Price = o.Price, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }
        public List<MenuDto> GetAll(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Menus
                                 join c in context.Categories on o.CategoryId equals c.Id
                                 join u in context.Units on o.UnitId equals u.Id
                                 where o.TenantId == tenantId
                                 orderby o.Id descending
                                 select new MenuDto { ImageUrl = o.ImageUrl, IsKitchen = o.IsKitchen, CutOff = o.CutOff, SubStractId = o.SubstractId, Name = o.Name, Quantity = o.Quantity, Active = o.Active, Unit = u.Name, Category = c.Name, CategoryId = o.CategoryId, Description = o.Description, UnitId = o.UnitId, Price = o.Price, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }
        public List<MenuDto> GetByDefault(Guid tenantId, int take)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Menus
                                 join c in context.Categories on o.CategoryId equals c.Id
                                 join u in context.Units on o.UnitId equals u.Id
                                 let addons = (from x in context.Addons
                                               join c in context.AddonTypes on x.AddonTypeId equals c.Id
                                               where x.MenuId == o.Id
                                               orderby x.Id descending
                                               select new AddonDto { AddonType = c.Name, AddonTypeId = x.AddonTypeId, Active = x.Active, Price = x.Price, MenuId = x.MenuId, Name = x.Name, CreatedDT = x.CreatedDt, CreatedBy = x.CreatedBy, Id = x.Id }).ToList()

                                 where o.TenantId == tenantId & o.Active == true
                                 orderby o.Id descending
                                 select new MenuDto { AddonDto = addons, ImageUrl = o.ImageUrl, IsKitchen = o.IsKitchen, CutOff = o.CutOff, SubStractId = o.SubstractId, Name = o.Name, Quantity = o.Quantity, Active = o.Active, Unit = u.Name, Category = c.Name, CategoryId = o.CategoryId, Description = o.Description, UnitId = o.UnitId, Price = o.Price, CreatedBy = o.CreatedBy, Id = o.Id }).Take(take).ToList();
                return objResult;
            }
        }
        public List<MenuDto> GetByCategoryId(Guid categoryId, Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Menus
                                 join c in context.Categories on o.CategoryId equals c.Id
                                 join u in context.Units on o.UnitId equals u.Id
                                 let addons = (from x in context.Addons
                                               join c in context.AddonTypes on x.AddonTypeId equals c.Id
                                               where x.MenuId == o.Id
                                               orderby x.Id descending
                                               select new AddonDto { AddonType = c.Name, AddonTypeId = x.AddonTypeId, Active = x.Active, Price = x.Price, MenuId = x.MenuId, Name = x.Name, CreatedDT = x.CreatedDt, CreatedBy = x.CreatedBy, Id = x.Id }).ToList()
                                 where o.TenantId == tenantId && (o.CategoryId == categoryId && o.Active == true)                              
                                 orderby o.Id descending
                                 select new MenuDto { ImageUrl = o.ImageUrl, AddonDto = addons, IsKitchen = o.IsKitchen, CutOff = o.CutOff, SubStractId = o.SubstractId, Name = o.Name, Quantity = o.Quantity, Active = o.Active, Unit = u.Name, Category = c.Name, CategoryId = o.CategoryId, Description = o.Description, UnitId = o.UnitId, Price = o.Price, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }      

        public List<MenuDto> GetCutOffMenu(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Menus
                                 join c in context.Categories on o.CategoryId equals c.Id
                                 join u in context.Units on o.UnitId equals u.Id
                                 where o.TenantId == tenantId && o.CutOff > o.Quantity
                                 orderby o.Id descending
                                 select new MenuDto { ImageUrl = o.ImageUrl, IsKitchen = o.IsKitchen, CutOff = o.CutOff, SubStractId = o.SubstractId, Name = o.Name,Quantity = o.Quantity, Active = o.Active, Unit = u.Name, Category = c.Name, CategoryId = o.CategoryId, Description = o.Description, UnitId = o.UnitId, Price = o.Price, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }

        public void UpdateMenuQuantity(int quantity, Guid menuId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Menus.SingleOrDefault(o => o.Id == menuId);

                if (objToUpdate != null)
                {
                    if (objToUpdate.Quantity > 0 && objToUpdate.Quantity > quantity)
                    {
                        objToUpdate.Quantity = objToUpdate.Quantity - quantity;
                    }

                    try
                    {
                        context.SaveChanges();                      
                    }
                    catch (ChangeConflictException)
                    {
                                                  
                    }
                }
            }
        }
        public int CutOffCount(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Menus
                                 where o.TenantId == tenantId && o.CutOff > o.Quantity
                                 select o).Count();
                return objResult;
            }
        }
        public int Count(DateTime datetime, Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Menus
                                 where o.CreatedDt.Date == datetime.Date && o.TenantId == tenantId
                                 select o).Count();
                return objResult;
            }
        }
        public int Count(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Menus
                                 where o.TenantId == tenantId
                                 select o).Count();
                return objResult;
            }
        }

    }
}
