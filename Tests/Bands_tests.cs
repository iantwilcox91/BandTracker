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
    [Fact]
    public void Test_Save_CanWeSaveABandToTheDatabase()
    {
      //Arrange
      Band band =  new Band ("BandName");
      //Act
      band.Save();
      //Assert
      List<Band> allBand = Band.GetAll();
      List<Band> testBand = new List<Band> {band};
      Assert.Equal( testBand, allBand );
    }

    [Fact]
    public void Test_DeleteOnlyOneBand()
    {
      //Arrange
      Band newBand = new Band("BandName");
      newBand.Save();
      Band newBand2 = new Band("BandName2");
      newBand2.Save();
      // Act
      newBand.Delete();
      //Arrange
      List<Band> allBands = Band.GetAll();
      List<Band> resultBands = new List<Band> {newBand2};
      //Assert
      Assert.Equal(resultBands , allBands);
    }




    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
