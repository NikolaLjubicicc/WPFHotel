using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFHotel.Forme
{
    /// <summary>
    /// Interaction logic for Soba.xaml
    /// </summary>
    public partial class Soba : Window
    {
        SqlConnection konekcija = Konekcija.KreirajKonekciju();

        public Soba()
        {
            InitializeComponent();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                if (MainWindow.azuriraj != true)
                {
                    string insert = @"insert into Soba(BrojSobe,SpratSobe,Raspolozivost)
                                values('" + txtBrojSobe.Text + "','" + txtSpratSobe.Text + "','" + Convert.ToInt32(cbxRaspolozivost.IsChecked) + "');";
                    SqlCommand cmd = new SqlCommand(insert, konekcija);
                    cmd.ExecuteNonQuery();
                    MainWindow.pomocnired = null;
                }
                else
                {
                    DataRowView red = MainWindow.pomocnired;

                    string update = @"Update Soba set 
                                 BrojSobe='" + txtBrojSobe.Text + "',SpratSobe ='" + txtSpratSobe.Text + "',Raspolozivost='" + Convert.ToInt32(cbxRaspolozivost.IsChecked) + "' Where SobaID="+ red["ID"];
                    SqlCommand cmd = new SqlCommand(update, konekcija);
                    cmd.ExecuteNonQuery();
                }
                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unos odredjenih vrednosti vrednosti nije validan!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }

            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
