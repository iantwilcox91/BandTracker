using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class VenuesTest : IDisposable
  {
    public VenuesTest()
    {
      DBConfiguration.ConnectionString =  "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=BandTracker_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_CheckForEmptyDataBase()
    {
      //Arrange
      //Act
      int tableRows = Venue.GetAll().Count;
      //Assert
      Assert.Equal( 0, tableRows);
    }
    [Fact]
    public void Test_checkGetFunction()
    {
      Venue newVenue = new Venue("VenueName");
      Assert.Equal("VenueName", newVenue.GetName() );
    }
    [Fact]
    public void Test_Save_CanWeSaveVenuesToTheDatabase()
    {
      Venue newVenue = new Venue("VenueName");
      newVenue.Save();
      List<Venue> allVenue = Venue.GetAll();
      List<Venue> testVenue = new List<Venue> {newVenue};
      Assert.Equal( testVenue, allVenue );
    }

    [Fact]
    public void Test_DeleteOnlyOneVenue()
    {
      //Arrange
      Venue newVenue = new Venue("VenueName");
      newVenue.Save();
      Venue newVenue2 = new Venue("VenueName2");
      newVenue2.Save();
      // Act
      newVenue.Delete();
      //Arrange
      List<Venue> allVenues = Venue.GetAll();
      List<Venue> resultVenues = new List<Venue> {newVenue2};
      //Assert
      Assert.Equal(resultVenues , allVenues);
    }




    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
