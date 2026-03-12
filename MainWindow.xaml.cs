using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bevolkerung
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        List<Allampolgar> lakossag;
        const int feladatokSzama = 45;

        public MainWindow()
        {
            InitializeComponent();
            lakossag = new List<Allampolgar>();

            using var reader = new StreamReader(@"..\..\..\SRC\bevolkerung.txt");
            var line = reader.ReadLine();

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    lakossag.Add(new Allampolgar(line));
                }
            }
            MegoldasTeljes.ItemsSource = lakossag;

            for (int i = 1; i <= feladatokSzama; i++)
            {
                feladatComboBox.Items.Add($"{i}.");
            }

        }
        private void Nullazas()
        {
            MegoldasMondatos.Content = string.Empty;
            MegoldasTeljes.ItemsSource = null;
            MegoldasLista.Items.Clear();
        }

        //private void feladatComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        //{
        //    Nullazas();
        //    var combo = (ComboBox)sender;

        //    var methodName = $"Feladat{combo.SelectedIndex}_{combo.Tag}";
        //    var method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        //    method?.Invoke(this, null);
        //}

        private void Feladat1()
        {
            MegoldasMondatos.Content = $"A legmagasabb nettó éves jövedelem: {lakossag.Max(x => x.NettoJovedelem)} EUR" ;
        }
        private void Feladat2()
        {
            MegoldasMondatos.Content = $"Az állampolgárok nettó éves jövedelmének átlaga: {lakossag.Average(x => x.NettoJovedelem)}";
        }
        private void Feladat3()
        {
            var groupedByTartomany = lakossag.GroupBy(x => x.Tartomany).ToList();
            foreach (var item in groupedByTartomany)
            {
                MegoldasLista.Items.Add($"{item.Key} {item.Count()} állampolgár");
            }
        }

        private void Feladat4()
        {
            var angolaiAllampolgarok = lakossag.Where(x => x.Nemzetiseg == "angolai").ToList();
            MegoldasTeljes.ItemsSource = angolaiAllampolgarok;
        }

        private void Feladat5()
        {
            var legfiatalabbAllampolgar = lakossag.OrderBy(x => x.SzuletesiEv).LastOrDefault();
            var legfiatalabbEv = legfiatalabbAllampolgar.SzuletesiEv;
            var legfiatalabbakListaja = lakossag.Where(x => x.SzuletesiEv.Equals(legfiatalabbEv));
            MegoldasTeljes.ItemsSource = legfiatalabbakListaja;
        }

        private void Feladat6()
        {
            var nemDohanyzok = lakossag.Where(x => !x.Dohanyzik).Select(x => new {azonosito = x.Id, jovedelem = x.HaviNettoJovedelem() }).ToList();
            foreach (var item in nemDohanyzok)
            {
                MegoldasLista.Items.Add($"Azonosító: {item.azonosito} Jövedelem:{Math.Round(item.jovedelem, 2)} ");
            }

        }

        private void Feladat7()
        {
            var bajororszagiAllampolgarok = lakossag
                .Where(x => x.Tartomany == "Bajorország" && x.NettoJovedelem > 30000)
                .OrderBy(x => x.IskolaiVegzettseg);
            MegoldasTeljes.ItemsSource = bajororszagiAllampolgarok;
        }

        private void Feladat8()
        {
            var ferfiak = lakossag.
                Where(x => x.Nem == "férfi").ToList();
            foreach (var item in ferfiak)
            {
                MegoldasLista.Items.Add(item.ToString(true));
            }

        }
        private void Feladat9()
        {
            var nok = lakossag
                .Where(x => x.Nem.Equals("nő") && x.Tartomany == "Bajorország").ToList();
            foreach(var item in nok)
            {
                MegoldasLista.Items.Add(item.ToString(false));
            }
        }

        private void Feladat10()
        {
            var nemDohanyzok = lakossag
                .Where(x => !x.Dohanyzik)
                .OrderByDescending(x => x.NettoJovedelem)
                .Take(10)
                .ToList();
            MegoldasTeljes.ItemsSource = nemDohanyzok;
        }

        private void Feladat11()
        {
            var legidosebbAllampolgarok = lakossag
                .OrderByDescending(x => x.SzuletesiEv)
                .Take(5)
                .ToList();
            MegoldasTeljes.ItemsSource = legidosebbAllampolgarok;
        }

        private void Feladat12()
        {
            var groupedByNepcsoport = lakossag
                .Where(x => x.Nemzetiseg == "német")
                .GroupBy(x => x.Nepcsoport)
                .ToList();
            foreach (var group in groupedByNepcsoport)
            {
                MegoldasLista.Items.Add($"{group.Key}");
                foreach (var item in group)
                {
                    MegoldasLista.Items.Add($"\t{item.AktivSzavazoSzovegesen} {item.PolitikaiNezet}");
                }
            }
                
        }

        //private void Feladat()
        //{

        //}

        private void feladatComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Nullazas();

            var methodName = $"Feladat{feladatComboBox.SelectedIndex + 1}";
            var method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(this, null);
        }
    }
}