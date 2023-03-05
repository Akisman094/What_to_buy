using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatToBuy.Context.Entities
{
    /// <summary>
    /// Item entity, used to form a shopping list
    /// </summary>
    public class Item : BaseEntity
    {
        public String name { get; set; }
        public int amount { get; set; }

        public int? ShoppingListId { get; set; }
        public virtual ShoppingList ShoppingList { get; set; }
    }
}
