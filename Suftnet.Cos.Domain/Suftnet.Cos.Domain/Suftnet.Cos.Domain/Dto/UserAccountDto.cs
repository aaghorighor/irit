﻿namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserAccountDto
    {
        public string UserId { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool Active { get; set; }      
        public string Email { get; set; }
        public string Area { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginDate { get; set; }      
        public string FirstName { get; set; }     
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public string Phone { get; set; }
        [Required()]
        public int AreaId { get; set; }
        public Guid TenantId { get; set; }
        public bool ChangePassword { get; set; }
        public string TenantName { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool? IsExpired { get; set; }
        public string FullName { get {
                return FirstName + " " + LastName;
            }
        }

    }
}