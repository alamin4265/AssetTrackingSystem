using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ATSystem.Models.Entity
{
    public class MovementPermision
    {
        public int Id { get; set; }
        public string RegistrationDate { get; set; }
        public int AssetId { get; set; }
        public string OrganizationName { get; set; }
        public string BranchName { get; set; }

        //[Column("MoveToOrganization")]
        public int OrganizationId { get; set; }
        //[Column("MoveToBranch")]
        public int BranchId { get; set; }

        public string MoveBy { get; set; }

        public bool Permision { get; set; }
    }
}