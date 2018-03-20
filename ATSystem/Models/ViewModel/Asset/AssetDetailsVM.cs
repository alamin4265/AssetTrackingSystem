using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATSystem.Models.ViewModel.Asset
{
    public class AssetDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual string Code { get; set; }
        public double? Price { get; set; }
        public string SerialNo { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
    }
}