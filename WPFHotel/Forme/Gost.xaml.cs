using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;

namespace WPFHotel.Forme
{
    /// <summary>
    /// Interaction logic for Gost.xaml
    /// </summary>
    public partial class Gost : Window
    {
        SqlConnection konekcija = Konekcija.KreirajKonekciju();
        public Gost()
        {
            InitializeComponent();
            txtIme.Focus();
            
        }

       

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                konekcija.Open();
                if (MainWindow.azuriraj != true)
                {
                    string insert = @"insert into Gost(ImeGost,PrezimeGost,KontaktGost,Adresa,Grad)
                                    values('" + txtIme.Text + "','" + txtPrezime.Text + "','" + txtKontakt.Text + "','" + txtAdresa.Text + "','" + txtGrad.Text + "');";
                    SqlCommand cmd = new SqlCommand(insert, konekcija);
                    cmd.ExecuteNonQuery();
                    MainWindow.pomocnired = null;
                }
                else
                {
                    DataRowView red = MainWindow.pomocnired;
                    string update = @"Update Gost
                     set ImeGost='" + txtIme.Text + "',PrezimeGost='" + txtPrezime.Text + "',KontaktGost='" + txtKontakt.Text + "',Adresa='" + txtAdresa.Text + "',Grad='" + txtGrad.Text + "' Where GostID=" + red["ID"];
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
