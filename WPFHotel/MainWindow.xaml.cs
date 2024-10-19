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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFHotel.Forme;

namespace WPFHotel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         public static string ucitanaTabela;
        public static bool azuriraj;
        public static DataRowView pomocnired;
        static SqlConnection konekcija = Konekcija.KreirajKonekciju();
        
        #region Select upiti
        static string gostSelect = @"Select GostID as ID, ImeGost, PrezimeGost, KontaktGost, Adresa, Grad from Gost";
        static string korisnikSelect = @"Select KorisnikID as ID, ImeKorisnika, PrezimeKorisnika, JMBGKorisnika, AdresaKorsinika, GradKorisnika,
                                          KontaktKorisnika from Korisnik";
        static string parkingSelect = @"Select ParkingID as ID, BrojMesta from Parking";
        static string racunSelect = @"Select RacunID as ID, Ime, Cena from Racun";
        static string sobaSelect = @"Select SobaID as ID, BrojSobe, SpratSobe, Raspolozivost from Soba";
        static string tipSobeSelect = @"Select TipSobeID as ID, BrojKreveta, NazivTipaSobe from TipSobe";
        static string tipUslugeSelect = @"Select TipUslugeID as ID, NazivUsluge from TipUsluge";
        static string rezervacijaSelect = @"Select RezervacijaID as ID, ImeKorisnika as Korisnik, ImeGost + ' ' + PrezimeGost as Gost,
                                            BrojSobe as Soba,  NazivUsluge as TipUsluge, BrojMesta as Parking ,Cena as Racun, Datum as Datum,Vreme as Vreme  
                                            from  Rezervacija join Gost on Rezervacija.GostID = Gost.GostID
				                            join Korisnik on Rezervacija.KorisnikID = Korisnik.KorisnikID
				                            join Parking on Rezervacija.ParkingID = Parking.ParkingID
				                            join Racun on Rezervacija.RacunID =  Racun.RacunID
				                            join Soba on Rezervacija.SobaID = Soba.SobaID
				                            join TipUsluge on Rezervacija.TipUslugeID = TipUsluge.TipUslugeID
				                            ";
        #endregion
        #region Select sa uslovom
        string selectUslovGost = @"select* from Gost where GostID=";
        string selectUslovKorisnik = @"select* from Korisnik where KorisnikID=";
        string selectUslovParking = @"select* from Parking where ParkingID=";
        string selectUslovRacun = @"select* from Racun where RacunID=";
        string selectUslovRezervacija = @"select* from Rezervacija where RezervacijaID=";
        string selectUslovSoba = @"select* from Soba where SobaID=";
        string selectUslovTipSobe = @"select* from TipSobe where TipSobeID=";
        string selectUslovTipUsluge = @"select* from TipUsluge where TipUslugeID=";
        #endregion
        #region Delete upit
        static string deleteGost = @"delete from Gost where GostID=";
        static string deleteParking = @"delete from Parking where ParkingID=";
        static string deleteRacun = @"delete from Racun where RacunID=";
        static string deleteRezervacija = @"delete from Rezervacija where RezervacijaID=";
        static string deleteSoba = @"delete from Soba where SobaID=";
        static string deleteTipSobe = @"delete from TipSobe where TipSobeID=";
        static string deleteTipUsluge = @"delete from TipUsluge where TipUslugeID=";




        #endregion
        public MainWindow()
        {
            InitializeComponent();
            UcitajPodatke(dataGridCentralni, rezervacijaSelect);

        }
        public static void UcitajPodatke(DataGrid grid, string selectUpit)
        {
            try { 
            konekcija.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(selectUpit, konekcija);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            grid.ItemsSource = dt.DefaultView;
            ucitanaTabela = selectUpit;
            }
            catch (SqlException)
            {
                MessageBox.Show("Neuspešno učitani podaci!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnKorisnik_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, korisnikSelect);
        }

        private void btnGost_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, gostSelect);
        }

        private void btnParking_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, parkingSelect);
        }

        private void btnSoba_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, sobaSelect);
        }

        private void btnTipSobe_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, tipSobeSelect);
        }

        private void btnTipUsluge_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, tipUslugeSelect);
        }

        private void btnRezervacija_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, rezervacijaSelect);
        }

        private void btnRacun_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, racunSelect);
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;
            if (ucitanaTabela.Equals(gostSelect))
            {
                prozor = new Gost();
                prozor.Show();
                UcitajPodatke(dataGridCentralni, gostSelect);
            }
            else if (ucitanaTabela.Equals(korisnikSelect))
            {
                prozor = new Korisnik();
                prozor.Show();
                UcitajPodatke(dataGridCentralni, korisnikSelect);
            }
            else if (ucitanaTabela.Equals(parkingSelect))
            {
                prozor = new Parking();
                prozor.Show();
                UcitajPodatke(dataGridCentralni, parkingSelect);
            }
            else if (ucitanaTabela.Equals(racunSelect))
            {
                prozor = new Racun();
                prozor.Show();
                UcitajPodatke(dataGridCentralni, racunSelect);
            }
            else if (ucitanaTabela.Equals(rezervacijaSelect))
            {
                prozor = new Rezervacija();
                prozor.Show();
                UcitajPodatke(dataGridCentralni, rezervacijaSelect);
            }
            else if (ucitanaTabela.Equals(sobaSelect))
            {
                prozor = new Soba();
                prozor.Show();
                UcitajPodatke(dataGridCentralni, sobaSelect);
            }
            else if (ucitanaTabela.Equals(tipSobeSelect))
            {
                prozor = new TipSobe();
                prozor.Show();
                UcitajPodatke(dataGridCentralni, tipSobeSelect);
            }
            else if (ucitanaTabela.Equals(tipUslugeSelect))
            {
                prozor = new TipUsluge();
                prozor.Show();
                UcitajPodatke(dataGridCentralni, tipUslugeSelect);
            }

        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if(ucitanaTabela.Equals(gostSelect))
            {
                popuniFormu(dataGridCentralni, selectUslovGost);
                UcitajPodatke(dataGridCentralni, gostSelect);
            }
            else if (ucitanaTabela.Equals(korisnikSelect))
            {
                popuniFormu(dataGridCentralni, selectUslovKorisnik);
                UcitajPodatke(dataGridCentralni, korisnikSelect);
            }
            else if (ucitanaTabela.Equals(parkingSelect))
            {
                popuniFormu(dataGridCentralni, selectUslovParking);
                UcitajPodatke(dataGridCentralni, parkingSelect);
            }
            else if (ucitanaTabela.Equals(racunSelect))
            {
                popuniFormu(dataGridCentralni, selectUslovRacun);
                UcitajPodatke(dataGridCentralni, racunSelect);
            }
            else if (ucitanaTabela.Equals(rezervacijaSelect))
            {
                popuniFormu(dataGridCentralni, selectUslovRezervacija);
                UcitajPodatke(dataGridCentralni, rezervacijaSelect);
            }
            else if (ucitanaTabela.Equals(sobaSelect))
            {
                popuniFormu(dataGridCentralni, selectUslovSoba);
                UcitajPodatke(dataGridCentralni, sobaSelect);
            }
            else if (ucitanaTabela.Equals(tipSobeSelect))
            {
                popuniFormu(dataGridCentralni, selectUslovTipSobe);
                UcitajPodatke(dataGridCentralni, tipSobeSelect);
            }
            else if (ucitanaTabela.Equals(tipUslugeSelect))
            {
                popuniFormu(dataGridCentralni, selectUslovTipUsluge);
                UcitajPodatke(dataGridCentralni, tipUslugeSelect);
            }
        }
        static void popuniFormu(DataGrid grid, string selectUslov)
        {
            try { 
            konekcija.Open();
            azuriraj = true;
            DataRowView red = (DataRowView)grid.SelectedItems[0];
                pomocnired = red;
                string upit = selectUslov + red["ID"];
                SqlCommand komanda = new SqlCommand(upit, konekcija);
                SqlDataReader citac = komanda.ExecuteReader();
                while (citac.Read())
                {
                    if (ucitanaTabela.Equals(gostSelect))
                    {
                        Gost prozorGost = new Gost();
                        prozorGost.txtIme.Text = citac["ImeGost"].ToString();
                        prozorGost.txtPrezime.Text = citac["PrezimeGost"].ToString();
                        prozorGost.txtKontakt.Text = citac["KontaktGost"].ToString();
                        prozorGost.txtAdresa.Text = citac["Adresa"].ToString();
                        prozorGost.txtGrad.Text = citac["Grad"].ToString();
                        prozorGost.ShowDialog();
                    }
                    
                    else if (ucitanaTabela.Equals(parkingSelect))
                    {
                        Parking prozorParking = new Parking();
                        prozorParking.txtParking.Text = citac["BrojMesta"].ToString();
                        prozorParking.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(racunSelect))
                    {
                        Racun prozorRacun = new Racun();
                        prozorRacun.txtIme.Text = citac["Ime"].ToString();
                        prozorRacun.txtCena.Text = citac["Cena"].ToString();
                        prozorRacun.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(rezervacijaSelect))
                    {
                        Rezervacija prozorRezervacija = new Rezervacija();
                        prozorRezervacija.dpDatum.SelectedDate = (DateTime)citac["Datum"];
                        prozorRezervacija.cbGost.SelectedValue = citac["GostID"];
                        prozorRezervacija.cbTipUsluge.SelectedValue = citac["TipUslugeID"];
                        prozorRezervacija.cbKorisnik.SelectedValue = citac["KorisnikID"];
                        prozorRezervacija.cbSoba.SelectedValue = citac["SobaID"];
                        prozorRezervacija.cbParking.SelectedValue = citac["ParkingID"];
                        prozorRezervacija.cbRacun.SelectedValue = citac["RacunID"];
                        prozorRezervacija.txtVreme.Text = citac["Vreme"].ToString() ;
                        prozorRezervacija.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(sobaSelect))
                    {
                        Soba prozorSoba = new Soba();
                        prozorSoba.txtSpratSobe.Text = citac["SpratSobe"].ToString();
                        prozorSoba.txtBrojSobe.Text = citac["BrojSobe"].ToString();
                        prozorSoba.cbxRaspolozivost.IsChecked = (bool)citac["Raspolozivost"];
                        prozorSoba.ShowDialog();

                    }
                    else if (ucitanaTabela.Equals(tipSobeSelect))
                    {
                        TipSobe prozorTipSobe = new TipSobe();
                        prozorTipSobe.txtBrojKreveta.Text = citac["BrojKreveta"].ToString();
                        prozorTipSobe.txtTipSobe.Text = citac["NazivTipaSobe"].ToString();
                        prozorTipSobe.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(tipUslugeSelect))
                    {
                        TipUsluge prozorTipUsluge = new TipUsluge();
                        prozorTipUsluge.txtNazivUsluge.Text = citac["NazivUsluge"].ToString();
                        prozorTipUsluge.ShowDialog();
                    }
                }

            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
                azuriraj = false;
            }
        }

        private void BtnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(gostSelect))
            {
                ObrisiZapis(dataGridCentralni, deleteGost);
                UcitajPodatke(dataGridCentralni, gostSelect);
            }
            else if(ucitanaTabela.Equals(parkingSelect))
            {
                ObrisiZapis(dataGridCentralni, deleteParking);
                UcitajPodatke(dataGridCentralni, parkingSelect);
            }
            else if (ucitanaTabela.Equals(racunSelect))
            {
                ObrisiZapis(dataGridCentralni, deleteRacun);
                UcitajPodatke(dataGridCentralni, racunSelect);
            }
            else if (ucitanaTabela.Equals(rezervacijaSelect))
            {
                ObrisiZapis(dataGridCentralni, deleteRezervacija);
                UcitajPodatke(dataGridCentralni, rezervacijaSelect);
            }
            else if (ucitanaTabela.Equals(sobaSelect))
            {
                ObrisiZapis(dataGridCentralni, deleteSoba);
                UcitajPodatke(dataGridCentralni, sobaSelect);
            }
            else if (ucitanaTabela.Equals(tipSobeSelect))
            {
                ObrisiZapis(dataGridCentralni, deleteTipSobe);
                UcitajPodatke(dataGridCentralni, tipSobeSelect);
            }
            else if (ucitanaTabela.Equals(tipUslugeSelect))
            {
                ObrisiZapis(dataGridCentralni, deleteTipUsluge);
                UcitajPodatke(dataGridCentralni, tipUslugeSelect);
            }
        }
        private void ObrisiZapis(DataGrid grid, string deleteUpit)
        {
            try
            {
                konekcija.Open();
                DataRowView red = (DataRowView)grid.SelectedItems[0];
                MessageBoxResult rezultat = MessageBox.Show("Da li ste sigurni da želite da obrišete?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rezultat == MessageBoxResult.Yes)
                {
                    SqlCommand komanda = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    komanda.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    komanda.CommandText = deleteUpit + "@id";
                    komanda.ExecuteNonQuery();
                    komanda.Dispose();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SqlException)
            {
                MessageBox.Show("Postoje povezani podaci u drugim tabelama!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                konekcija?.Close();
            }
        }
    }
}
