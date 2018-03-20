using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATSystem.Models.ViewModel.Movement
{
    public class MovementListVM
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Asset { get; set; }
        public string MoveFromOrg { get; set; }
        public string MoveFromBranch { get; set; }
        public string MoveToOrg { get; set; }
        public string MoveToBranch { get; set; }
        public string MoveBy { get; set; }
    }
}