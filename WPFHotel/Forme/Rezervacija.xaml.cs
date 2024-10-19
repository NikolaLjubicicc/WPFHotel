using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Interaction logic for Rezervacija.xaml
    /// </summary>
    public partial class Rezervacija : Window
    {
        SqlConnection konekcija = Konekcija.KreirajKonekciju();

        public Rezervacija()
        {
            InitializeComponent();
            try
            {
                konekcija.Open();
                string vratiGost = @"select GostID,ImeGost+ ' ' + PrezimeGost + ' ' + KontaktGost as Gost from Gost";
                DataTable dtGost=new DataTable();
                SqlDataAdapter daGost= new SqlDataAdapter(vratiGost, konekcija);
                daGost.Fill(dtGost);
                cbGost.ItemsSource = dtGost.DefaultView;

                string vratiParking = @"select ParkingID,BrojMesta as Parking from Parking";
                DataTable dtParking = new DataTable();
                SqlDataAdapter daParking = new SqlDataAdapter(vratiParking, konekcija);
                daParking.Fill(dtParking);
                cbParking.ItemsSource = dtParking.DefaultView;

                string vratiSoba = @"select SobaID,BrojSobe as Soba from Soba";
                DataTable dtSoba = new DataTable();
                SqlDataAdapter daSoba = new SqlDataAdapter(vratiSoba, konekcija);
                daSoba.Fill(dtSoba);
                cbSoba.ItemsSource = dtSoba.DefaultView;                

                string vratiTipUsluge = @"select TipUslugeID,NazivUsluge as TipUsluge from TipUsluge";
                DataTable dtTipUsluge = new DataTable();
                SqlDataAdapter daTipUsluge = new SqlDataAdapter(vratiTipUsluge, konekcija);
                daTipUsluge.Fill(dtTipUsluge);
                cbTipUsluge.ItemsSource = dtTipUsluge.DefaultView;

                string vratiRacun = @"select RacunID, Cena as Racun from Racun";
                DataTable dtRacun = new DataTable();
                SqlDataAdapter daRacun = new SqlDataAdapter(vratiRacun, konekcija);
                daRacun.Fill(dtRacun);
                cbRacun.ItemsSource = dtRacun.DefaultView;

                string vratiKorisnik = @"select KorisnikID, ImeKorisnika as Korisnik from Korisnik";
                DataTable dtKorisnik = new DataTable();
                SqlDataAdapter daKorisnik = new SqlDataAdapter(vratiKorisnik, konekcija);
                daKorisnik.Fill(dtKorisnik);
                cbKorisnik.ItemsSource = dtKorisnik.DefaultView;

            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuće liste nisu popunjene!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                DateTime date = (DateTime)dpDatum.SelectedDate;
                string datum = date.ToString("yyyy-MM-dd");
               
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@datum", SqlDbType.DateTime).Value = datum;
                cmd.Parameters.Add("@gostID", SqlDbType.Int).Value = cbGost.SelectedValue;
                cmd.Parameters.Add("@sobaID",SqlDbType.Int).Value=cbSoba.SelectedValue;
                cmd.Parameters.Add("@tipUslugeID",SqlDbType.Int).Value=cbTipUsluge.SelectedValue;
                cmd.Parameters.Add("@parkingID",SqlDbType.Int).Value=cbParking.SelectedValue;
                cmd.Parameters.Add("@racunID",SqlDbType.Int).Value=cbRacun.SelectedValue;
                cmd.Parameters.Add("@korisnikID", SqlDbType.Int).Value = cbKorisnik.SelectedValue;
                cmd.Parameters.Add("@vreme",SqlDbType.Time).Value=txtVreme.Text;
                if (MainWindow.azuriraj)
                {
                    DataRowView red = MainWindow.pomocnired;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update Rezervacija
                                     set Datum=@datum, GostID=@gostID, SobaID=@sobaID ,TipUslugeID=@tipUslugeID,
                                    ParkingID=@parkingID, RacunID=@racunID, KorisnikID=@korisnikID, Vreme=@vreme where RezervacijaID=@id";
                    MainWindow.pomocnired = null;

                }
                else
                {
                    cmd.CommandText = @"insert into Rezervacija(Datum,GostID,SobaID,TipUslugeID,ParkingID,RacunID,KorisnikID,Vreme)
                                values(@datum,@gostID,@sobaID,@tipUslugeID,@parkingID,@racunID,@korisnikID,@vreme)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
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
