using System;
using System.Globalization;
using ConsoleTables; // il faut ajouter un package NuGet "ConsoleTables"

namespace TestCompetenceNeoSynergix.Models
{
    public class Facture
    {
        readonly double TPS = 0.05;
        readonly double TVQ = 0.085;
        public Commande Commande;
        public Client Client;

        public Facture(Commande commande, Client client)
        {
            this.Commande = commande;
            this.Client = client;
        }

        public Facture()
        {
        }

        public double MontantHorsTaxes()
        {
            double r=0;
            foreach(Produit p in Commande.Produits)
            {
                r += p.Prix * p.Quantite;
            }
            return Math.Round(r,2);
        }

        public double MontantAvecTaxes()
        {
            NumberFormatInfo setPrecision = new NumberFormatInfo();
            setPrecision.NumberDecimalDigits = 2;

            double MontantTPS = MontantHorsTaxes() * TPS;
            double MontantTVQ = MontantHorsTaxes() * TVQ;
            return Math.Round(MontantHorsTaxes() + MontantTPS + MontantTVQ,2);
        }

        public double TotalTaxes()
        {
            return Math.Round(MontantAvecTaxes() - MontantHorsTaxes(),2);
        }

        public void AfficherFacture()
        {
            var table = new ConsoleTable("Total Hors Taxes", "Total des taxes", "Total avec taxes");
            table.AddRow(MontantHorsTaxes(), TotalTaxes(), MontantAvecTaxes());
            Console.WriteLine(table);
        }

        public bool PayerFacture()
        {
            if (Client.MontantDepart > MontantAvecTaxes())
            {
                Client.MontantDepart -= MontantAvecTaxes();
                return true;
            }
            else
                return false;
        }
        public double MontantRestantDuDepenses()
        {
            return Client.MontantDepart - MontantAvecTaxes();
        }
    }
}
