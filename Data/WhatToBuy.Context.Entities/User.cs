using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatToBuy.Context.Entities
{
    public class User : BaseEntity
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public int? FamilyId { get; set; }
        public virtual Family Family { get; set; }
    }
}
