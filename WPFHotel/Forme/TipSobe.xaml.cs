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
    /// Interaction logic for TipSobe.xaml
    /// </summary>
    public partial class TipSobe : Window
    {
        SqlConnection konekcija = Konekcija.KreirajKonekciju();

        public TipSobe()
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
                    string insert = @"insert into TipSobe(BrojKreveta,NazivTipaSobe)
                                    values('" + txtBrojKreveta.Text + "','" + txtTipSobe.Text + "');";
                    SqlCommand cmd = new SqlCommand(insert, konekcija);
                    cmd.ExecuteNonQuery();
                    MainWindow.pomocnired = null;
                }
                else
                {
                    DataRowView red = MainWindow.pomocnired;
                    string update = @"Update TipSobe 
                                     set BrojKreveta='"+txtBrojKreveta.Text+"',NazivTipaSobe='"+txtTipSobe.Text +"' Where TipSobeID=" + red["ID"];
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
