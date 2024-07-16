
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ElectriciteApp.Models
{
    public class Locataire
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int NumChambre { get; set; } 

        public string Nom { get; set; }

        public int AncienKilowatts { get; set; }

        public int NouveauKilowatts { get; set; }

        public int PrixUnitaire { get; set; }

        public int TauxForfaitaire { get; set; }

        public int PrixPoubelle { get; set; }

        public int Don { get; set; }

        public int MontantPaye { get; set; }

        public int PrixBalais { get; set; }

        public double Consommation
        {
            get
            {
                return NouveauKilowatts - AncienKilowatts;
            }
        }

        public  double Montant
        {
            get
            {
                return Consommation * PrixUnitaire;
            }
        }

        public double Reste
        {
            
            get
            {
                if(MontantPaye == 0) return 0;
                return Total - MontantPaye;
            }
        }


        public double Total
        {
            get
            {
                var tot = Montant + Don + TauxForfaitaire + PrixPoubelle + PrixBalais;
                if (MontantPaye == 0)
                {
                    return tot;
                }
                return tot - MontantPaye;

            }
        }



        public string TotalString
        {
            get
            {
                return $"({Total} FCFA)";
            }
        }

    }
}
