using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.WebApi.Catalog.ViewModels
{
    public class Product_VM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public int Stock { get; set; }
    }
}
