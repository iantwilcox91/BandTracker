using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Data;
using Xunit;

namespace BandTracker
{
  public class Venue
  {
    private int _id;
    private string _name;

    public Venue(string venueName, int id = 0)
    {
      _id = id;
      _name = venueName;
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
    public override bool Equals(System.Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = (this.GetId() == newVenue.GetId());
        bool nameEquality = (this.GetName() == newVenue.GetName());
        return (idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }
    public static List<Venue> GetAll()
    {
      List<Venue> venueList = new List<Venue> {};
      SqlConnection conn = DB.Connection();
      conn.Open();

      string query = "SELECT * FROM venues;";
      SqlCommand cmd = new SqlCommand(query, conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      int id = 0;
      string name = null;
      while ( rdr.Read() )
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
        Venue venue = new Venue(name, id);
        venueList.Add(venue);
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
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venuesBands;", conn);
      cmd.ExecuteNonQuery();

      SqlCommand cmd1 = new SqlCommand("DELETE FROM venues;", conn);
      cmd1.ExecuteNonQuery();
      conn.Close();
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      string query = "INSERT INTO venues (name) OUTPUT INSERTED.id VALUES (@venueName);";
      SqlCommand cmd = new SqlCommand(query,conn);
      SqlParameter parameter = new SqlParameter ("@venueName", this._name);
      cmd.Parameters.Add(parameter);

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

      SqlParameter pam = new SqlParameter ("@venueId", this._id);
      string query = "DELETE FROM venuesBands WHERE BandId = @venueId;";
      SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.Add(pam);
      cmd.ExecuteNonQuery();

      SqlParameter pam2 = new SqlParameter ("@venueId", this._id);
      string query2 = "DELETE FROM venues WHERE id = @venueId;";
      SqlCommand cmd2 = new SqlCommand(query2, conn);
      cmd2.Parameters.Add(pam2);
      cmd2.ExecuteNonQuery();

      conn.Close();
    }
    public List<Venue> ViewVenues()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands JOIN venuesBands ON (venues.id = venuesBands.venueId) JOIN bands ON (venuesBands.bandId = bands.id) WHERE bands.id = @venuesId;", conn);
      SqlParameter VenueIdParam = new SqlParameter();
      VenueIdParam.ParameterName = "@venuesId";
      VenueIdParam.Value = this.GetId().ToString();

      cmd.Parameters.Add(VenueIdParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Band> bandList = new List<Band>{};

      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        Band newBand = new Band(bandName, bandId);
        venueList.Add(newBand);
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

    public void AddBand(Band band)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
//(venueId , bandId)  may need to be reversed?...maybe?
      string query = "INSERT INTO venuesBands (venueId , bandId) VAlUES (@venueId ,@bandId );";
      SqlCommand cmd = new SqlCommand(query, conn);
      SqlParameter pamStudentId = new SqlParameter("@bandId", band.GetId() );
      cmd.Parameters.Add(pamStudentId);
      SqlParameter pamCourseId = new SqlParameter("@venueId", this._id );
      cmd.Parameters.Add(pamCourseId);
      cmd.ExecuteNonQuery();

      conn.Close();
    }


    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      string query = "SELECT * FROM venues WHERE id = @venueId;";
      SqlCommand cmd = new SqlCommand(query, conn);
      SqlParameter paramId = new SqlParameter("@venueId", id );
      cmd.Parameters.Add(paramId);
      SqlDataReader rdr = cmd.ExecuteReader();

      int venueId = 0;
      string venueName = null;
      while(rdr.Read())
      {
        venueId = rdr.GetInt32(0);
        venueName = rdr.GetString(1);
      }
      Venue venue = new Venue( venueName, venueId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return venue;
    }


  }
}
