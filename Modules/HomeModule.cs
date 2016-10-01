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

      Post["/band/{id}"] = parameters =>
      {
        Band band = Band.Find(parameters.id);
        Venue venue = Venue.Find(Request.Form["venue"]);
        band.AddVenue(venue);
        List<Venue> bandVenue = band.ViewVenues();
        List<Venue> allVenues = Venue.GetAll();
        Dictionary<string ,object> model = new Dictionary<string, object> {};
        model.Add("band", band);
        model.Add("venues", bandVenue);
        model.Add("allVenues", allVenues);
        return View["bands.cshtml", model];
      };

      Patch["/bands/edit/{id}"] = parameters => {
        Band SelectedBand = Band.Find(parameters.id);
        SelectedBand.Update(Request.Form["band-name"]);
        return View["index.cshtml", SelectedBand];
      };

      Get["/bands/edit/{id}"] = parameters => {
        Band SelectedBand = Band.Find(parameters.id);
        return View["bandsUpdate.cshtml", SelectedBand];
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

      Post["/venue/{id}"] = parameters =>
      {
        Venue venue = Venue.Find(parameters.id);
        Band band = Band.Find(Request.Form["band"]);
        venue.AddBand(band);
        List<Band> venueBand = venue.ViewBands();
        List<Band> allBands = Band.GetAll();
        Dictionary<string ,object> model = new Dictionary<string, object> {};
        model.Add("venue", venue);
        model.Add("bands", venueBand);
        model.Add("allBands", allBands);
        return View["venues.cshtml", model];
      };

      Patch["/venues/edit/{id}"] = parameters => {
        Venue SelectedVenue = Venue.Find(parameters.id);
        SelectedVenue.Update(Request.Form["venue-name"]);
        return View["index.cshtml", SelectedVenue];
      };


      Get["/venues/edit/{id}"] = parameters => {
        Venue SelectedVenue = Venue.Find(parameters.id);
        return View["venuesUpdate.cshtml", SelectedVenue];
      };



    }
  }
}
