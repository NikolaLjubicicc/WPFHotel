using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WPFHotel
{
    public class Konekcija
    {
        public static SqlConnection KreirajKonekciju()
        {
            SqlConnectionStringBuilder ccSb = new SqlConnectionStringBuilder();
            ccSb.DataSource = @"DESKTOP-N9VIA7V\SQLEXPRESS";
            ccSb.InitialCatalog = "Hotel";
            ccSb.IntegratedSecurity = true;
            string con = ccSb.ToString();
            SqlConnection konekcija = new SqlConnection(con);
            return konekcija;
        }
    }
}
