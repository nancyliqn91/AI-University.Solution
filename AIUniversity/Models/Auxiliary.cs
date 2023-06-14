using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace AIUniversity.Models
{
  public class Auxiliary
  {
    public List<SelectListItem> getAllDaysList {get;set;}

    public List<SelectListItem> getTimeOfClass()
      {
        List<SelectListItem> myList = new List<SelectListItem>();
        var data = new[]{
              new SelectListItem{ Value="1",Text="0800 - 0930"},
              new SelectListItem{ Value="2",Text="0940 - 1120"},
              new SelectListItem{ Value="3",Text="1130 - 1300"},
              new SelectListItem{ Value="4",Text="1310 - 1440"},
              new SelectListItem{ Value="5",Text="1450 - 1620"},
              new SelectListItem{ Value="6",Text="1630 - 1800"}
          };
        myList = data.ToList();
        return myList;
      }
      public List<SelectListItem> getDaysOfTheWeek()
      {
        List<SelectListItem> myList = new List<SelectListItem>();
        var data = new[]{
              new SelectListItem{ Value="Monday",Text="Monday"},
              new SelectListItem{ Value="Tuesday",Text="Tuesday"},
              new SelectListItem{ Value="Wednesday",Text="Wednesday"},
              new SelectListItem{ Value="Thursday",Text="Thursday"},
              new SelectListItem{ Value="Friday",Text="Friday"},
              new SelectListItem{ Value="Saturday",Text="Saturday"},
              new SelectListItem{ Value="Sunday",Text="Sunday"}
          };
        myList = data.ToList();
        return myList;
      }
    

    //constructor
    public Auxiliary()
    {
    }
  }
}