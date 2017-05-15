using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class SecondMakeReport
    {

        public SecondMakeReport()
        {
                
        }
        public SecondMakeReport(Hotel h)
        {
            HotelName = h.Name;
            HotelId = h.HotelId;
            BranchName = h.Branch.Name;
            OwnerName = h.Owner.LastName + " " + h.Owner.FirstName;
            HAList = h.Applications.ToList();
            NewListCost = new List<double>();
            NewListMaintenance = new List<double>();
           
            //NewListMaintenance = new List<double>();
            FillList();
        }

        private void FillList()
        {
            //U have to fill the list with 0, otherwise you go out of range when iterate over it in the view for filling up
            HAList.ForEach(t =>
            {
                NewListCost.Add(0.0);
                NewListMaintenance.Add(0.0);
            });
        }

        public string HotelName { get; set; }
        public string HotelId { get; set; }
        public string BranchName { get; set; }
        public string OwnerName { get; set; }        
        public List<HotelApplication> HAList { get; set; }
        public List<double> NewListCost { get; set; }
        public List<double> NewListMaintenance { get; set; }



    }
}