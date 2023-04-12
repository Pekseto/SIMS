using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.DTO;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.ViewModels;



public class GuestOneViewModel
{
    //name, locationFullName, MaxGuestNum, CancelationThreshold, MinStayingDays
    private LocationService _locationService;
    public List<String> LocationsFullName = new List<string>();

    public static List<String> Countries = new List<string>();
    public static List<String> Cities = new List<string>();


    public GuestOneViewModel()
    {
        LocationsFullName = GetFullLocatoinsName();

    }
    public List<String> GetFullLocatoinsName()
    {
        foreach (Location location in _locationService.GetAll())
        {
            LocationsFullName.Add(location.City + " " + location.Country);
        }
        return LocationsFullName;
    }
}
