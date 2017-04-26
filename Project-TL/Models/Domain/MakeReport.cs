using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class MakeReport
    {
        public MakeReport()
        {

        }
        public MakeReport(Hotel h )
        {
            Checked = false;
            HotelName = h.Name;
            HotelId = h.HotelId;
            BranchName = h.Branch.Name;
            BranchId = h.Branch.BranchId;
            OwnerName = h.Owner.LastName + " " +  h.Owner.FirstName;
            OwnerId = h.Owner.OwnerId;
           
        }
        public bool Checked { get; set; }

        [Display(Name ="Hotel")]
        public string HotelName { get; set; }
        public string HotelId { get; set; }

        [Display(Name ="Branche ")]
        public string BranchName { get; set; }
        public int BranchId { get; set; }

        [Display(Name ="Owner ")]
        public string OwnerName { get; set; }
        public int OwnerId { get; set; }
        


    }
}