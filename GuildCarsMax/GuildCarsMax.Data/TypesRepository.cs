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
        public IEnumerable<MakeType> GetAllMakeTypes()
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

        public IEnumerable<ModelType> GetAllModelTypesByMake(int makeTypeId)
        {
            List<ModelType> modelTypes = new List<ModelType>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SelectModelTypesByMake", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MakeTypeId", makeTypeId);

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

        public IEnumerable<NewOrUsedType> GetNewOrUsedTypeOptions()
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

        public IEnumerable<PurchaseType> GetAllPurchaseTypes()
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

        public IEnumerable<TransmissionType> GetAllTransmissionTypes()
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
                        currentRow.TransmissionTypeId = (int)dr["TransmissionTypeId"];
                        currentRow.TransmissionTypeName = dr["TransmissionType"].ToString();

                        transmissionTypes.Add(currentRow);
                    }
                }
            }

            return transmissionTypes;
        }

        public IEnumerable<ExteriorColor> GetAllExteriorColors()
        {
            List<ExteriorColor> exteriorColors = new List<ExteriorColor>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ExteriorColorsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ExteriorColor currentRow = new ExteriorColor();
                        currentRow.ExteriorColorId = (int)dr["ExteriorColorId"];
                        currentRow.ExteriorColorName = dr["ExteriorColor"].ToString();

                        exteriorColors.Add(currentRow);
                    }
                }
            }

            return exteriorColors;
        }

        public IEnumerable<InteriorColor> GetAllInteriorColors()
        {
            List<InteriorColor> interiorColors = new List<InteriorColor>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InteriorColorsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InteriorColor currentRow = new InteriorColor();
                        currentRow.InteriorColorId = (int)dr["InteriorColorId"];
                        currentRow.InteriorColorName = dr["InteriorColor"].ToString();

                        interiorColors.Add(currentRow);
                    }
                }
            }

            return interiorColors;
        }

        public IEnumerable<BodyStyle> GetAllBodyStyles()
        {
            List<BodyStyle> bodyStyles = new List<BodyStyle>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("BodyStylesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BodyStyle currentRow = new BodyStyle();
                        currentRow.BodyStyleId = (int)dr["BodyStyleId"];
                        currentRow.BodyStyleName = dr["BodyStyle"].ToString();

                        bodyStyles.Add(currentRow);
                    }
                }
            }

            return bodyStyles;
        }
    }
}
