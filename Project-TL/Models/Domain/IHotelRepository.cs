using Project_TL.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public interface IHotelRepository
    {
        Hotel FindByCode(string code);
        IQueryable<Hotel> FindByBranchName(string branch);
        IQueryable<Hotel> FindByOwner(string owner);
        IQueryable<Hotel> FindBySystem(int systemId);
        IQueryable<Hotel> FindAll();
        Context getContext();
        void AddHotel(Hotel hotel);
        void EditHotel(Hotel hotel);
        void RemoveHotel(Hotel hotel);
        void RemoveApplication(Hotel hotel, HotelApplication syst);
        void SaveChanges();


    }
}