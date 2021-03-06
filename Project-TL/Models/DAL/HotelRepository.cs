﻿using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL
{
    public class HotelRepository : IHotelRepository
    {
        private readonly Context context;
        private DbSet<Hotel> hotels;

        public HotelRepository(Context context)
        {
            this.context = context;
            hotels = context.Hotels;
        }
        public void AddHotel(Hotel hotel)
        {
            hotels.Add(hotel);
        }

        public void EditHotel(Hotel hotel)
        {
            Hotel h = FindByCode(hotel.HotelId);
            if(h != null)
            {
                hotels.Remove(hotel);
                hotels.Add(h);
            }
        }
        public IQueryable<Hotel> FindAll()
        {
            return hotels;
        }

        public IQueryable<Hotel> FindByBranchName(string branch)
        {
            return hotels.Where(t => t.Branch.Name.Equals(branch)).ToList().AsQueryable();
        }

        public Hotel FindByCode(string code)
        {
            return hotels.FirstOrDefault(t => t.HotelId.Equals(code));
        }

        public IQueryable<Hotel> FindByOwner(string owner)
        {
            return hotels.Where(t => t.Owner.LastName.Equals(owner)).ToList().AsQueryable();
        }

        public IQueryable<Hotel> FindBySystem(int sysId)
        {
            List<Hotel> ho = new List<Hotel>();

            foreach (Hotel h in hotels)
            {
                h.Applications.ToList().ForEach(t =>
                {
                    if (t.ApplicationId == sysId)
                        ho.Add(h);
                });
               
            }      
                return ho.AsQueryable();            
       }
    
        public void RemoveHotel(Hotel hotel)
        {
            hotels.Remove(hotel);
        }

        public void RemoveApplication(Hotel hotel,HotelApplication syst)
        {
            hotel.Applications.Remove(syst);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
            
        }
        public Context getContext()
        {
            return context;
        }


    }
}