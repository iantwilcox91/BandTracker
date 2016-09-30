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
    public void Test_Save_CanWeSaveToTheDatabase()
    {
      //Arrange
      Venue newVenue = new Venue("VenueName");
      //Act
      newVenue.Save();
      //Assert
      List<Venue> allVenue = Venue.GetAll();
      List<Venue> testVenue = new List<Venue> {newVenue};
      Assert.Equal( testVenue, allVenue );
    }





    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
