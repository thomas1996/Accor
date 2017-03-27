using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.ViewModels
{
    public class SettingsViewModel
    {


        public SettingsViewModel(List<Branch> branches, List<ContactPerson> contacts, List<Owner> owners)
        {
            Branches = branches;
            Contacts = contacts;
            Owners = owners;
            Statusses = new List<Status>();
            Types = new List<Models.Domain.Type>();
            MakeList("Status");
            MakeList("Type");
        }

        public List<Branch> Branches { get; set; }
        public List<ContactPerson> Contacts { get; set; }
        public List<Owner> Owners { get; set; }
        public List<Status> Statusses { get; set; }
        public List<Models.Domain.Type> Types { get; set; }

    
    private void MakeList(string v)
    {
        switch (v.ToLower())
        {
            case "status":
                {
                    foreach(Status s in Enum.GetValues(typeof(Status)))
                    {
                            Statusses.Add(s);
                    }
                }
                break;
             case "type":
                    {
                       foreach(Models.Domain.Type t in Enum.GetValues(typeof(Models.Domain.Type)))
                        {
                            Types.Add(t);
                        }
                    }
                 break;
            }
    }
    }
}