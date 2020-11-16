using DvdLibrary.Models.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace DvdLibrary.Models.Repos
{
    public class DvdRepositoryADO : IDvdRepository
    {
      

        public void DeleteDvd(int dvdId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DeleteDvd", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DvdId", dvdId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<DvdItem> GetDvdByDirectorName(string director)
        {
            List<DvdItem> dvds = new List<DvdItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SearchByDirector", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Director", director);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DvdItem row = new DvdItem();

                        row.DvdId = (int)dr["DvdId"];
                        row.Title = dr["DvdTitle"].ToString();
                        row.ReleaseYear = dr["ReleaseYear"].ToString();
                        row.Director = dr["Director"].ToString();
                        row.RatingType = dr["Rating"].ToString();
                        row.Notes = dr["Notes"].ToString();

                        dvds.Add(row);
                    }
                }
            }

            return dvds;
        }

        public DvdItem GetDvdById(int id)
        {
            DvdItem dvd = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SearchById", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DvdId", id);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if(dr.Read())
                    {
                        dvd = new DvdItem();
                        dvd.DvdId = (int)dr["DvdId"];
                        dvd.Title = dr["DvdTitle"].ToString();
                        dvd.ReleaseYear = dr["ReleaseYear"].ToString();
                        dvd.Director = dr["Director"].ToString();
                        dvd.RatingType = dr["Rating"].ToString();
                        dvd.Notes = dr["Notes"].ToString();

                    }
                }
            }

            return dvd;
        }

        public IEnumerable<DvdItem> GetDvdByRating(string rating)
        {
            List<DvdItem> dvds = new List<DvdItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SearchByRating", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Rating", rating);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DvdItem row = new DvdItem();

                        row.DvdId = (int)dr["DvdId"];
                        row.Title = dr["DvdTitle"].ToString();
                        row.ReleaseYear = dr["ReleaseYear"].ToString();
                        row.Director = dr["Director"].ToString();
                        row.RatingType = dr["Rating"].ToString();
                        row.Notes = dr["Notes"].ToString();

                        dvds.Add(row);
                    }
                }
            }

            return dvds;
        }

        public IEnumerable<DvdItem> GetDvdByReleaseYear(string year)
        {
            List<DvdItem> dvds = new List<DvdItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SearchByReleaseYear", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ReleaseYear", year);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DvdItem row = new DvdItem();

                        row.DvdId = (int)dr["DvdId"];
                        row.Title = dr["DvdTitle"].ToString();
                        row.ReleaseYear = dr["ReleaseYear"].ToString();
                        row.Director = dr["Director"].ToString();
                        row.RatingType = dr["Rating"].ToString();
                        row.Notes = dr["Notes"].ToString();

                        dvds.Add(row);
                    }
                }
            }

            return dvds;
        }

        public IEnumerable<DvdItem> GetDvdByTitle(string title)
        {
            List<DvdItem> dvds = new List<DvdItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SearchByTitle", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DvdTitle", title);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DvdItem row = new DvdItem();

                        row.DvdId = (int)dr["DvdId"];
                        row.Title = dr["DvdTitle"].ToString();
                        row.ReleaseYear = dr["ReleaseYear"].ToString();
                        row.Director = dr["Director"].ToString();
                        row.RatingType = dr["Rating"].ToString();
                        row.Notes = dr["Notes"].ToString();

                        dvds.Add(row);
                    }
                }
            }

            return dvds;
        }

        public IEnumerable<DvdItem> GetDvds()
        {
            List<DvdItem> dvds = new List<DvdItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SelectAllDvds", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DvdItem row = new DvdItem();

                        row.DvdId = (int)dr["DvdId"];
                        row.Title = dr["DvdTitle"].ToString();
                        row.ReleaseYear = dr["ReleaseYear"].ToString();
                        row.Director = dr["Director"].ToString();
                        row.RatingType = dr["Rating"].ToString();
                        row.Notes = dr["Notes"].ToString();

                        dvds.Add(row);
                    }
                }
            }

            return dvds;
        }

        public void UpdateDvd(DvdItem dvdItem)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("UpdateDvd", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DvdId", dvdItem.DvdId);
                cmd.Parameters.AddWithValue("@DvdTitle", dvdItem.Title);
                cmd.Parameters.AddWithValue("@ReleaseYear", dvdItem.ReleaseYear);
                cmd.Parameters.AddWithValue("@Director", dvdItem.Director);
                cmd.Parameters.AddWithValue("@Rating", dvdItem.RatingType);
                cmd.Parameters.AddWithValue("@Notes", dvdItem.Notes);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        void IDvdRepository.CreateDvd(DvdItem dvdItem)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CreateDvd", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@DvdId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@DvdTitle", dvdItem.Title);
                cmd.Parameters.AddWithValue("@ReleaseYear", dvdItem.ReleaseYear);
                cmd.Parameters.AddWithValue("@Director", dvdItem.Director);
                cmd.Parameters.AddWithValue("@Rating", dvdItem.RatingType);
                cmd.Parameters.AddWithValue("@Notes", dvdItem.Notes);

                cn.Open();

                cmd.ExecuteNonQuery();

                dvdItem.DvdId = (int)param.Value;
            }
        }
    }
}