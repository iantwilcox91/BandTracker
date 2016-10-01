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

      Get["/bands/{id}"] = parameters =>
      {
        Band band = Band.Find(parameters.id);
        List<Venue> bandVenue = band.ViewVenues();
        List<Venue> allVenues = Venue.GetAll();
        Dictionary<string ,object> model = new Dictionary<string, object> {};
        model.Add("band", band);
        model.Add("venues", bandVenue);
        model.Add("allVenues", allVenues);
        return View["bands.cshtml", model];
      };


      Get["/venues/{id}"] = parameters =>
      {
        Venue venue = Venue.Find(parameters.id);
        List<Band> venueBand = venue.ViewBands();
        List<Band> allBands = Band.GetAll();
        Dictionary<string ,object> model = new Dictionary<string, object> {};
        model.Add("venue", venue);
        model.Add("bands", venueBand);
        model.Add("allBands", allBands);
        return View["venues.cshtml", model];
      };

    }
  }
}
