using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATSystem.Models.Entity
{
    public class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string CodeWithPrefix { get; set; }
        public double Price { get; set; }
        public string SerialNo { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public bool Registered  { get; set; }
    }
}