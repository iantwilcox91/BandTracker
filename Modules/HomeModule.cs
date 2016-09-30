using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Data;
using Xunit;
using Nancy;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ =>
      {
        List<Band> bandList = Band.GetAll();
        List<Venue> venueList = Venue.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object> {};
        model.Add("bandList", bandList);
        model.Add("venueList", venueList);
        return View["index.cshtml", model];
      };


    }
  }
}
