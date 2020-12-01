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
    public class TypesRepository
    {
        public List<MakeType> GetAllMakeTypes()
        {
            List<MakeType> makeTypes = new List<MakeType>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakeTypesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        MakeType currentRow = new MakeType();
                        currentRow.MakeTypeId = (int)dr["MakeTypeId"];
                        currentRow.MakeTypeName = dr["MakeType"].ToString();

                        makeTypes.Add(currentRow);
                    }
                }
            }

            return makeTypes;
        }

        public List<ModelType> GetAllModelTypesByMake(MakeType makeType)
        {
            List<ModelType> modelTypes = new List<ModelType>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SelectModelTypesByMake", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MakeTypeId", makeType.MakeTypeId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ModelType currentRow = new ModelType();
                        currentRow.ModelTypeId = (int)dr["ModelTypeId"];
                        currentRow.ModelTypeName = dr["ModelType"].ToString();

                        modelTypes.Add(currentRow);
                    }
                }
            }

            return modelTypes;
        }

        public List<NewOrUsedType> GetNewOrUsedTypeOptions()
        {
            List<NewOrUsedType> newOrUsedTypes = new List<NewOrUsedType>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("NewOrUsedTypeOptionsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        NewOrUsedType currentRow = new NewOrUsedType();
                        currentRow.NewOrUsedTypeId = (int)dr["NewOrUsedTypeId"];
                        currentRow.NewOrUsedTypeOption = dr["NewOrUsedType"].ToString();

                        newOrUsedTypes.Add(currentRow);
                    }
                }
            }

            return newOrUsedTypes;
        }

        public List<PurchaseType> GetAllPurchaseTypes()
        {
            List<PurchaseType> purchaseTypes = new List<PurchaseType>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PurchaseTypesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        PurchaseType currentRow = new PurchaseType();
                        currentRow.PurchaseTypeId = (int)dr["PurchaseTypeId"];
                        currentRow.PurchaseTypeName = dr["PurchaseType"].ToString();

                        purchaseTypes.Add(currentRow);
                    }
                }
            }

            return purchaseTypes;
        }

        public List<TransmissionType> GetAllTransmissionTypes()
        {
            List<TransmissionType> transmissionTypes = new List<TransmissionType>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("TransmissionTypesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        TransmissionType currentRow = new TransmissionType();
                        currentRow.TransmissionTypeId = (int)dr["PurchaseTypeId"];
                        currentRow.TransmissionTypeName = dr["PurchaseType"].ToString();

                        transmissionTypes.Add(currentRow);
                    }
                }
            }

            return transmissionTypes;
        }
    }
}
