using System;
using System.Collections.Generic;
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
            List = h.Applications.ToList();
            NewListCost = new List<double>();
            NewListMaintenance = new List<double>();
            FillList();
        }

        private void FillList()
        {
            List.ForEach(t =>
            {
                NewListCost.Add(0.0);
                NewListMaintenance.Add(0.0);
            });
        }

        public string HotelName { get; set; }
        public string HotelId { get; set; }
        public string BranchName { get; set; }
        public string OwnerName { get; set; }
        public List<HotelApplication> List { get; set; }
        public List<double> NewListCost { get; set; }
        public List<double> NewListMaintenance { get; set; }
    }
}