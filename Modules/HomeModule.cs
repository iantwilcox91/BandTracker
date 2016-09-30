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

      Post["/add_band"] = _ =>
      {
        string bandName = Request.Form["bandName"];
        Band band = new Band(bandName);
        band.Save();

        List<Band> bandList = Band.GetAll();
        List<Venue> venueList = Venue.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object> {};
        model.Add("bandList", bandList);
        model.Add("venueList", venueList);
        return View["index.cshtml", model];
      };

      Post["/add_venue"] = _ =>
      {
        string venueName = Request.Form["venueName"];
        Venue venue = new Venue(venueName);
        venue.Save();

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
