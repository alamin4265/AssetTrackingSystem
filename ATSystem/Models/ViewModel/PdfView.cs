using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATSystem.Models.ViewModel
{
    public class PdfView
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }

        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public string AssecCode { get; set; }
        public string AssetSerialNo { get; set; }

        public string GeneralCategory { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }

        public string FromBranch { get; set; }
        public string ToBranch { get; set; }

        public string MovedBy { get; set; }

        public string Date { get; set; }

    }
}