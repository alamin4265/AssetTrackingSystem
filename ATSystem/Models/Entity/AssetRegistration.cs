using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ATSystem.Models.Entity
{
    public class AssetRegistration
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd--MM--yyyy}", ApplyFormatInEditMode = true)]
        public string RegistrationDate { get; set; }
        
        public string RegisteredBy { get; set; }

        public virtual ICollection<AssetRegistrationDetails> AssetRegistrationDetailses { get; set; }
    }
}