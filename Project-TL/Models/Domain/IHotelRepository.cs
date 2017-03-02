using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public interface IHotelRepository
    {
        Hotel FindByCode(int code);
        IQueryable<Hotel> FindByBranchName(string branch);
        IQueryable<Hotel> FindByOwner(string owner);
        IQueryable<Hotel> FindBySystem(string system);
        IQueryable<Hotel> FindAll();
        void AddHotel(Hotel hotel);
        void EditHotel(Hotel hotel);
        void RemoveHotel(Hotel hotel);
        void SaveChanges();


    }
}