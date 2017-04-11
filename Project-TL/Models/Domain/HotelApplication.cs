using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class HotelApplication
    {
        public HotelApplication()
        {

        }

        public HotelApplication(string hotelId,int appId,double cost,DateTime startDate,DateTime endDate,string hotelName,string appName)
        {
            HotelId = hotelId;
            ApplicationId = appId;
            Cost = cost;
            StartDate = startDate;
            EndDate = endDate;
            HotelName = hotelName;
            ApplicationName = appName;
        }


        public string HotelId { get; set; }
        public int ApplicationId { get; set; }
        public string HotelName { get; set; }
        public string ApplicationName { get; set; }

        public double Cost { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}