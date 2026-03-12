using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bevolkerung
{
    internal class Allampolgar
    {
        public int Id { get; set; }
        public string Nem { get; set; }
        public int SzuletesiEv { get; set; }
        public int Suly { get; set; }
        public int Magassag { get; set; }
        public string DohanyzikSzovegesen { get; set; }
        public bool Dohanyzik { get; set; }
        public string Nemzetiseg { get; set; }
        public string Nepcsoport { get; set; }
        public string Tartomany { get; set; }
        public int NettoJovedelem { get; set; }
        public string IskolaiVegzettseg { get; set; }
        public string PolitikaiNezet { get; set; }
        public string AktivSzavazoSzovegesen { get; set; }
        public bool AktivSzavazo { get; set; }
        public string SorFogyasztasEventeSzovegesen { get; set; }
        public int SorFogyasztasEvente { get; set; }
        public string KrumpliFogyasztasEventeSzovegesen { get; set; }
        public int KrumpliFogyasztasEvente { get; set; }

        public Allampolgar(string sor)
        {
            var temp = sor.Split(";");
            Id = int.Parse(temp[0]);
            Nem = temp[1];
            SzuletesiEv = int.Parse(temp[2]);
            Suly = int.Parse(temp[3]);
            Magassag = int.Parse(temp[4]);
            DohanyzikSzovegesen = temp[5];
            Dohanyzik = temp[5] == "igen";
            Nemzetiseg = temp[6];
            Nepcsoport = !string.IsNullOrEmpty(temp[7]) ? temp[7] :string.Empty; //hármas operátor (ternary), elvis
            Tartomany = temp[8];
            NettoJovedelem = int.Parse(temp[9]);
            IskolaiVegzettseg = !string.IsNullOrEmpty(temp[10]) ? temp[10] : string.Empty;
            PolitikaiNezet = temp[11];
            AktivSzavazoSzovegesen = temp[12];
            AktivSzavazo = temp[12] == "igen";
            SorFogyasztasEventeSzovegesen = temp[13];
            SorFogyasztasEvente = temp[13] == "NA" ? -1 : int.Parse(temp[13]);
            KrumpliFogyasztasEventeSzovegesen = temp[14];
            KrumpliFogyasztasEvente = temp[14] == "NA" ? -1 : int.Parse(temp[14]);
        }

        public override string ToString()
        {
            return $"{Id}, {Nem}, {SzuletesiEv}, {Suly}, {Magassag}, {Dohanyzik}, {Nemzetiseg}, {Nepcsoport}, {Tartomany}, {NettoJovedelem}, {IskolaiVegzettseg}, {PolitikaiNezet}, {AktivSzavazo}, {SorFogyasztasEvente}, {KrumpliFogyasztasEvente}";
        }

        public string ToString(bool firstFive)
        {
            if (firstFive == true)
            {
                return $"{Id}, {Nem}, {SzuletesiEv}, {Suly}, {Magassag}";
            }
            else
            {
                return $"{Id}, {Nemzetiseg}, {Tartomany}, {NettoJovedelem}";
            }
        }

        public double HaviNettoJovedelem()
        {
            return NettoJovedelem / 12.0;
        }

        public int Eletkor(int ev)
        {
            return ev - SzuletesiEv;
        }  
    }
}
