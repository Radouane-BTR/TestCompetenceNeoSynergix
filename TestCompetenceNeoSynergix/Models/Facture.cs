using System;
using ConsoleTables; // il faut ajouter un package NuGet "ConsoleTables"

namespace TestCompetenceNeoSynergix.Models
{
    public class Facture
    {
        readonly double TPS = 0.05;
        readonly double TVQ = 0.085;
        public double MontantHorsTaxes { get; private set; }
        public double MontantAvecTaxes { get; private set; }
        public double TotalTaxes { get; private set; }

        public void CalculerMontantHorsTaxes(Commande commande)
        {
            double r=0;
            foreach(Produit p in commande.Produits)
            {
                r += (p.Prix * p.Quantite);
            }
            MontantHorsTaxes = r;
        }

        public void CalculerMontantAvecTaxes()
        {
            double MontantTPS = MontantHorsTaxes * TPS;
            double MontantTVQ = MontantHorsTaxes * TVQ;
            MontantAvecTaxes = MontantHorsTaxes + MontantTPS + MontantTVQ;
        }

        public void CalculerTotalTaxes()
        {
            TotalTaxes = MontantAvecTaxes - MontantHorsTaxes;
        }

        public void AfficherFacture()
        {
            var table = new ConsoleTable("Total Hors Taxes", "Total des taxes", "Total avec taxes");
            table.AddRow(MontantHorsTaxes, TotalTaxes, MontantAvecTaxes);
            Console.WriteLine(table);
        }
    }
}
