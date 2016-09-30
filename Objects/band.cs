using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Data;
using Xunit;

namespace BandTracker
{
  public class Band
  {
    private int _id;
    private string _name;

    public Band(string bandName, int id = 0)
    {
      _id = id;
      _name = bandName;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public override bool Equals(System.Object otherBand)
    {
      if (!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool idEquality = (this.GetId() == newBand.GetId());
        bool nameEquality = (this.GetName() == newBand.GetName());
        return (idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }
    public static List<Band> GetAll()
    {
      List<Band> bandList = new List<Band> {};
      SqlConnection conn = DB.Connection();
      conn.Open();

      string query = "SELECT * FROM bands;";
      SqlCommand cmd = new SqlCommand(query, conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      int id = 0;
      string name = null;
      while ( rdr.Read() )
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
        Band newBand = new Band(name, id);
        bandList.Add(newBand);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return bandList;
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venuesBands;", conn);
      cmd.ExecuteNonQuery();

      SqlCommand cmd1 = new SqlCommand("DELETE FROM bands;", conn);
      cmd1.ExecuteNonQuery();
      conn.Close();
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      string query = "INSERT INTO bands (name) OUTPUT INSERTED.id VALUES (@bandName);";
      SqlCommand cmd = new SqlCommand(query,conn);
      SqlParameter nameParameter = new SqlParameter ("@bandName", this._name);
      cmd.Parameters.Add(nameParameter);


      SqlDataReader rdr = cmd.ExecuteReader();

      while ( rdr.Read() )
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }







    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlParameter pam = new SqlParameter ("@bandId", this._id);
      string query = "DELETE FROM venuesBands WHERE BandId = @bandId;";
      SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.Add(pam);
      cmd.ExecuteNonQuery();

      SqlParameter pam2 = new SqlParameter ("@bandId", this._id);
      string query2 = "DELETE FROM bands WHERE id = @bandId;";
      SqlCommand cmd2 = new SqlCommand(query2, conn);
      cmd2.Parameters.Add(pam2);
      cmd2.ExecuteNonQuery();

      conn.Close();
    }


    public List<Venue> ViewVenues()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands JOIN venuesBands ON (bands.id = venuesBands.bandId) JOIN venues ON (venuesBands.venueId = venues.id) WHERE bands.id = @bandId;", conn);
      SqlParameter BandIdParam = new SqlParameter();
      BandIdParam.ParameterName = "@bandId";
      BandIdParam.Value = this.GetId().ToString();

      cmd.Parameters.Add(BandIdParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Venue> venueList = new List<Venue>{};

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        Venue newVenue = new Venue(venueName, venueId);
        venueList.Add(newVenue);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return venueList;
    }

    public void AddVenue(Venue venue)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
//(venueId , bandId)  may need to be reversed?...maybe?
      string query = "INSERT INTO venuesBands (venueId , bandId) VAlUES (@venueId ,@bandId );";
      SqlCommand cmd = new SqlCommand(query, conn);
      SqlParameter pamStudentId = new SqlParameter("@venueId", venue.GetId() );
      cmd.Parameters.Add(pamStudentId);
      SqlParameter pamCourseId = new SqlParameter("@bandId", this._id );
      cmd.Parameters.Add(pamCourseId);
      cmd.ExecuteNonQuery();

      conn.Close();
    }


    public static Band Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      string query = "SELECT * FROM bands WHERE id = @bandId;";
      SqlCommand cmd = new SqlCommand(query, conn);
      SqlParameter paramId = new SqlParameter("@bandId", id );
      cmd.Parameters.Add(paramId);
      SqlDataReader rdr = cmd.ExecuteReader();

      int bandId = 0;
      string bandName = null;
      while(rdr.Read())
      {
        bandId = rdr.GetInt32(0);
        bandName = rdr.GetString(1);
      }
      Band band = new Band( bandName, bandId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return band;
    }


  }
}
