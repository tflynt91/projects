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
    public class ColorsAndStylesRepository
    {
        public List<ExteriorColor> GetAllExteriorColors()
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

        public List<InteriorColor> GetAllInteriorColors()
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

        public List<BodyStyle> GetAllBodyStyles()
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
