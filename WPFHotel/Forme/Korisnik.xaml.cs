using System;
using System.Collections.Generic;
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
    /// Interaction logic for Korisnik.xaml
    /// </summary>
    public partial class Korisnik : Window
    {
        SqlConnection konekcija = Konekcija.KreirajKonekciju();
        public Korisnik()
        {
            InitializeComponent();
            txtIme.Focus();
        }

        private void BtnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {

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
    }
}
