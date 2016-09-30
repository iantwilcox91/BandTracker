using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class BandsTest : IDisposable
  {
    public BandsTest()
    {
      DBConfiguration.ConnectionString =  "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=BandTracker_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_CheckForEmptyDataBase()
    {
      //Arrange
      //Act
      int tableRows = Band.GetAll().Count;
      //Assert
      Assert.Equal( 0, tableRows);
    }
    [Fact]
    public void Test_checkGetFunction()
    {
      Band newBand = new Band("BandName");
      Assert.Equal("BandName", newBand.GetName() );
    }




    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
