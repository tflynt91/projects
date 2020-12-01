using GuildCarsMax.Models.Queries;
using GuildCarsMax.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCarsMax.Data
{
    public class SalesRepository
    {
        public void Insert(Sale sale)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SaleInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@SalesId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@VinNumber", sale.VinNumber);
                cmd.Parameters.AddWithValue("@UserId", sale.UserId);
                cmd.Parameters.AddWithValue("@PurchasePrice", sale.PurchasePrice);
                cmd.Parameters.AddWithValue("@PurchaseDate", sale.PurchaseDate);
                cmd.Parameters.AddWithValue("@FirstName", sale.FirstName);
                cmd.Parameters.AddWithValue("@LastName", sale.LastName);
                cmd.Parameters.AddWithValue("@Email", sale.Email);
                cmd.Parameters.AddWithValue("@Phone", sale.Phone);
                cmd.Parameters.AddWithValue("@Street1", sale.Street1);
                cmd.Parameters.AddWithValue("@Street2", sale.Street2);
                cmd.Parameters.AddWithValue("@City", sale.City);
                cmd.Parameters.AddWithValue("@StateId", sale.StateId);
                cmd.Parameters.AddWithValue("@ZipCode", sale.ZipCode);
                cmd.Parameters.AddWithValue("@PurchaseTypeId", sale.PurchaseTypeId);


                cn.Open();

                cmd.ExecuteNonQuery();

                sale.SalesId = (int)param.Value;

            }
        }

        public List<SalesReportSearchResult> SalesReportSearch(SalesReportFilterParameters parameters)
        {
            List<SalesReportSearchResult> salesReport = new List<SalesReportSearchResult>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT u.FirstName + u.LastName AS User, SUM(s.PurchasePrice) AS TotalSales, COUNT(s.VinNumber) AS TotalVehicles FROM AspNet.Users u INNER JOIN Sales s ON u.UserId = s.UserId WHERE 1 = 1 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (!parameters.User.Equals("- All -"))
                {
                    query += "AND u.UserId = @UserId ";
                    cmd.Parameters.AddWithValue("@UserId", parameters.UserId);
                }

                if(parameters.FromDate.HasValue && parameters.ToDate.HasValue)
                {
                    query += "AND s.PurchaseDate BETWEEN @FromDate AND @ToDate ";
                    cmd.Parameters.AddWithValue("@FromDate", parameters.FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", parameters.ToDate);
                }

                else if(parameters.FromDate.HasValue)
                {
                    query += "AND s.PurchaseDate >= @FromDate ";
                    cmd.Parameters.AddWithValue("@FromDate", parameters.FromDate);
                }

                else if(parameters.ToDate.HasValue)
                {
                    query += "AND s.PurchaseDate <= @FromDate ";
                    cmd.Parameters.AddWithValue("@ToDate", parameters.ToDate);
                }

                query += "GROUP BY u.FirstName + u.LastName AS User";

                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        SalesReportSearchResult row = new SalesReportSearchResult();

                        row.User = dr["User"].ToString();
                        row.TotalSales = (decimal)dr["TotalSales"];
                        row.TotalVehicles = (int)dr["TotalVehicles"];

                        salesReport.Add(row);
                    }
                }
            }

            return salesReport;
        }

        public void ContactInsert(Contact contact)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ContactId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Name", contact.Name);

                if (string.IsNullOrEmpty(contact.Email))
                {
                    cmd.Parameters.AddWithValue("@Email", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Email", contact.Email);
                }

                if (string.IsNullOrEmpty(contact.Phone))
                {
                    cmd.Parameters.AddWithValue("@Phone", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Phone", contact.Phone);
                }

                cmd.Parameters.AddWithValue("@Message", contact.Message);

                cn.Open();

                cmd.ExecuteNonQuery();

                contact.ContactId = (int)param.Value;

            }
        }

        

    }
}
