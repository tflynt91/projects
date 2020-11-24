using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data
{
    public class VehicleInventoryRepository
    {
        public VehicleInventoryListingDetails GetVehicleDetails(string vinNumber)
        {
            VehicleInventoryListingDetails vehicle = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetVehicleDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VinNumber", vinNumber);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        vehicle = new VehicleInventoryListingDetails();
                        vehicle.VinNumber = dr["@VinNumber"].ToString();
                        vehicle.ModelTypeId = (int)dr["@ModelTypeId"];
                        vehicle.ModelType = dr["@ModelType"].ToString();
                        vehicle.MakeTypeId = (int)dr["@MakeTypeId"];
                        vehicle.MakeType = dr["@MakeType"].ToString();
                        vehicle.BodyStyleId = (int)dr["@BodyStyleId"];
                        vehicle.BodyStyle = dr["BodyStyle"].ToString();
                        vehicle.InteriorColorId = (int)dr["@InteriorColorId"];
                        vehicle.InteriorColor = dr["InteriorColor"].ToString();
                        vehicle.ExteriorColorId = (int)dr["@ExteriorColorId"];
                        vehicle.ExteriorColor = dr["@ExteriorColor"].ToString();
                        vehicle.TransmissionTypeId = (int)dr["@TransmissionTypeId"];
                        vehicle.TransmissionType = dr["@TransmissionType"].ToString();
                        vehicle.ImageFileName = dr["@ImageFileName"].ToString();
                        vehicle.MSRP = (decimal)dr["@MSRP"];
                        vehicle.Mileage = (int)dr["@Mileage"];
                        vehicle.SalePrice = (decimal)dr["@SalePrice"];
                        vehicle.Year = (int)dr["@Year"];
                        vehicle.VehicleDescription = dr["@VehicleDescription"].ToString();

                    }
                }
            }

            return vehicle;
        }
        public void Insert(Vehicle vehicle)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VinNumber", vehicle.VinNumber);
                cmd.Parameters.AddWithValue("@ModelTypeId", vehicle.ModelTypeId);
                cmd.Parameters.AddWithValue("@BodyStyleId", vehicle.BodyStyleId);
                cmd.Parameters.AddWithValue("@InteriorColorId", vehicle.InteriorColorId);
                cmd.Parameters.AddWithValue("@ExteriorColorId", vehicle.ExteriorColorId);
                cmd.Parameters.AddWithValue("@TransmissionTypeId", vehicle.TransmissionTypeId);
                cmd.Parameters.AddWithValue("@NeworUsedTypeId", vehicle.NewOrUsedTypeId);
                cmd.Parameters.AddWithValue("@ImageFileName", vehicle.ImageFileName);
                cmd.Parameters.AddWithValue("@MSRP", vehicle.MSRP);
                cmd.Parameters.AddWithValue("@Mileage", vehicle.Mileage);
                cmd.Parameters.AddWithValue("@SalePrice", vehicle.SalePrice);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@VehicleDescription", vehicle.VehicleDescription);
                cmd.Parameters.AddWithValue("@Sold", false);
                cmd.Parameters.AddWithValue("@Featured", false);

                cn.Open();

                cmd.ExecuteNonQuery();

            }
        }

        public List<VehicleInventoryListingDetails> NewInventorySearch(VehicleInventorySearchParameters parameters)
        {
            List<VehicleInventoryListingDetails> inventoryResults = new List<VehicleInventoryListingDetails>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT TOP 20 v.VinNumber, mo.ModelTypeId, mo.ModelType, mk.MakeTypeId, mk.MakeType, b.BodyStyleId, b.BodyStyle, i.InteriorColorId, i.InteriorColor, e.ExteriorColorId, e.ExteriorColor, t.TransmissionTypeId, t.TransmissionType, v.ImageFileName, v.MSRP, v.Mileage, v.SalePrice, v.Year, v.VehicleDescription FROM Vehicles v INNER JOIN ModelTypes mo ON v.ModelTypeId = mo.ModelTypeId INNER JOIN MakeTypes mk ON mk.MakeTypeId = mo.MakeTypeId INNER JOIN BodyStyles b ON v.BodyStyleId = b.BodyStyleId INNER JOIN  InteriorColors i ON v.InteriorColorId = i.InteriorColorId INNER JOIN ExteriorColors e ON v.ExteriorColorId = e.ExteriorColorId INNER JOIN TransmissionTypes t ON v.TransmissionTypeId = t.TransmissionTypeId WHERE 1 = 1 AND v.NeworUsedTypeId = 1 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.MinPrice.HasValue)
                {
                    query += "AND v.SalePrice >= @MinPrice ";
                    cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                }

                if (parameters.MaxPrice.HasValue)
                {
                    query += "AND v.SalePrice <= @MaxPrice ";
                    cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                }

                if (parameters.MinYear.HasValue)
                {
                    query += "AND v.Year >= @MinYear ";
                    cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear);
                }

                if (parameters.MaxYear.HasValue)
                {
                    query += "AND v.Year <= @MaxYear ";
                    cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear);
                }

                if (Int32.TryParse(parameters.MakeModelOrYearInput, out int year))
                {
                    query += "AND v.Year = @Year ";
                    cmd.Parameters.AddWithValue("@Year", year);
                }

                else if (!string.IsNullOrEmpty(parameters.MakeModelOrYearInput))
                {
                    query += "AND mo.ModelType OR mk.MakeType LIKE @MakeModelOrYearInput";
                    cmd.Parameters.AddWithValue("@MakeModelOrYearInput", parameters.MakeModelOrYearInput);
                }

                
                query += "ORDER BY v.MSRP DESC";
                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleInventoryListingDetails row = new VehicleInventoryListingDetails();

                        row.VinNumber = dr["VinNumber"].ToString();
                        row.ModelTypeId = (int)dr["ModelTypeId"];
                        row.ModelType = dr["ModelType"].ToString();
                        row.MakeTypeId = (int)dr["MakeTypeId"];
                        row.MakeType = dr["MakeType"].ToString();
                        row.BodyStyleId = (int)dr["BodyStyleId"];
                        row.BodyStyle = dr["BodyStyle"].ToString();
                        row.InteriorColorId = (int)dr["InteriorColorId"];
                        row.InteriorColor = dr["InteriorColor"].ToString();
                        row.ExteriorColorId = (int)dr["ExteriorColorId"];
                        row.ExteriorColor = dr["ExteriorColor"].ToString();
                        row.TransmissionTypeId = (int)dr["TransmissionTypeId"];
                        row.TransmissionType = dr["TransmissionType"].ToString();
                        row.ImageFileName = dr["ImageFileName"].ToString();
                        row.MSRP = (decimal)dr["MSRP"];
                        row.Mileage = (int)dr["Mileage"];
                        row.SalePrice = (decimal)dr["SalePrice"];
                        row.Year = (int)dr["Year"];
                        row.VehicleDescription = dr["VehicleDescription"].ToString();

                        inventoryResults.Add(row);
                    }
                }
            }

            return inventoryResults;
        }

        public List<VehicleInventoryListingDetails> UsedInventorySearch(VehicleInventorySearchParameters parameters)
        {
            List<VehicleInventoryListingDetails> inventoryResults = new List<VehicleInventoryListingDetails>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT TOP 20 v.VinNumber, mo.ModelTypeId, mo.ModelType, mk.MakeTypeId, mk.MakeType, b.BodyStyleId, b.BodyStyle, i.InteriorColorId, i.InteriorColor, e.ExteriorColorId, e.ExteriorColor, t.TransmissionTypeId, t.TransmissionType, v.ImageFileName, v.MSRP, v.Mileage, v.SalePrice, v.Year, v.VehicleDescription FROM Vehicles v INNER JOIN ModelTypes mo ON v.ModelTypeId = mo.ModelTypeId INNER JOIN MakeTypes mk ON mk.MakeTypeId = mo.MakeTypeId INNER JOIN BodyStyles b ON v.BodyStyleId = b.BodyStyleId INNER JOIN  InteriorColors i ON v.InteriorColorId = i.InteriorColorId INNER JOIN ExteriorColors e ON v.ExteriorColorId = e.ExteriorColorId INNER JOIN TransmissionTypes t ON v.TransmissionTypeId = t.TransmissionTypeId WHERE v.NeworUsedTypeId = 2 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.MinPrice.HasValue)
                {
                    query += "AND v.SalePrice >= @MinPrice ";
                    cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                }

                if (parameters.MaxPrice.HasValue)
                {
                    query += "AND v.SalePrice <= @MaxPrice ";
                    cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                }

                if (parameters.MinYear.HasValue)
                {
                    query += "AND v.Year >= @MinYear ";
                    cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear);
                }

                if (parameters.MaxYear.HasValue)
                {
                    query += "AND v.Year <= @MaxYear ";
                    cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear);
                }

                if (Int32.TryParse(parameters.MakeModelOrYearInput, out int year))
                {
                    query += "AND v.Year = @Year ";
                    cmd.Parameters.AddWithValue("@Year", year);
                }

                else if (!string.IsNullOrEmpty(parameters.MakeModelOrYearInput))
                {
                    query += "AND mo.ModelType OR mk.MakeType LIKE @MakeModelOrYearInput";
                    cmd.Parameters.AddWithValue("@MakeModelOrYearInput", parameters.MakeModelOrYearInput);
                }


                query += "ORDER BY v.MSRP DESC";
                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleInventoryListingDetails row = new VehicleInventoryListingDetails();

                        row.VinNumber = dr["VinNumber"].ToString();
                        row.ModelTypeId = (int)dr["ModelTypeId"];
                        row.ModelType = dr["ModelType"].ToString();
                        row.MakeTypeId = (int)dr["MakeTypeId"];
                        row.MakeType = dr["MakeType"].ToString();
                        row.BodyStyleId = (int)dr["BodyStyleId"];
                        row.BodyStyle = dr["BodyStyle"].ToString();
                        row.InteriorColorId = (int)dr["InteriorColorId"];
                        row.InteriorColor = dr["InteriorColor"].ToString();
                        row.ExteriorColorId = (int)dr["ExteriorColorId"];
                        row.ExteriorColor = dr["ExteriorColor"].ToString();
                        row.TransmissionTypeId = (int)dr["TransmissionTypeId"];
                        row.TransmissionType = dr["TransmissionType"].ToString();
                        row.ImageFileName = dr["ImageFileName"].ToString();
                        row.MSRP = (decimal)dr["MSRP"];
                        row.Mileage = (int)dr["Mileage"];
                        row.SalePrice = (decimal)dr["SalePrice"];
                        row.Year = (int)dr["Year"];
                        row.VehicleDescription = dr["VehicleDescription"].ToString();

                        inventoryResults.Add(row);
                    }
                }
            }

            return inventoryResults;
        }

        public void Update(Vehicle updatedVehicle)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VinNumber", updatedVehicle.VinNumber);
                cmd.Parameters.AddWithValue("@ModelTypeId", updatedVehicle.ModelTypeId);
                cmd.Parameters.AddWithValue("@BodyStyleId", updatedVehicle.BodyStyleId);
                cmd.Parameters.AddWithValue("@InteriorColorId", updatedVehicle.InteriorColorId);
                cmd.Parameters.AddWithValue("@ExteriorColorId", updatedVehicle.ExteriorColorId);
                cmd.Parameters.AddWithValue("@TransmissionTypeId", updatedVehicle.TransmissionTypeId);
                cmd.Parameters.AddWithValue("@NeworUsedTypeId", updatedVehicle.NewOrUsedTypeId);
                cmd.Parameters.AddWithValue("@ImageFileName", updatedVehicle.ImageFileName);
                cmd.Parameters.AddWithValue("@MSRP", updatedVehicle.MSRP);
                cmd.Parameters.AddWithValue("@Mileage", updatedVehicle.Mileage);
                cmd.Parameters.AddWithValue("@SalePrice", updatedVehicle.SalePrice);
                cmd.Parameters.AddWithValue("@Year", updatedVehicle.Year);
                cmd.Parameters.AddWithValue("@VehicleDescription", updatedVehicle.VehicleDescription);
                cmd.Parameters.AddWithValue("@Sold", false);
                cmd.Parameters.AddWithValue("@Featured", updatedVehicle.Featured);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public List<InventoryReportRow> NewVehicleInventoryReport()
        {
            List<InventoryReportRow> newInventoryReport = new List<InventoryReportRow>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InventoryReportNewVehicles", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InventoryReportRow row = new InventoryReportRow();

                        row.Year = (int)dr["Year"];
                        row.ModelType = dr["ModelType"].ToString();
                        row.MakeType = dr["MakeType"].ToString();
                        row.Count = (int)dr["Count"];
                        row.StockValue = (decimal)dr["StockValue"];


                        newInventoryReport.Add(row);
                    }
                }
            }

            return newInventoryReport;
        }

        public List<InventoryReportRow> UsedVehicleInventoryReport()
        {
            List<InventoryReportRow> usedInventoryReport = new List<InventoryReportRow>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InventoryReportUsedVehicles", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InventoryReportRow row = new InventoryReportRow();

                        row.Year = (int)dr["Year"];
                        row.ModelType = dr["ModelType"].ToString();
                        row.MakeType = dr["MakeType"].ToString();
                        row.Count = (int)dr["Count"];
                        row.StockValue = (decimal)dr["StockValue"];


                        usedInventoryReport.Add(row);
                    }
                }
            }

            return usedInventoryReport;
        }
    }
}
