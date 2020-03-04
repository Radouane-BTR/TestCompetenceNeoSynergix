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

        public double CalculerMontantHorsTaxes()
        {
            double r=0;
            foreach(Produit p in Commande.Produits)
            {
                r += p.Prix * p.Quantite;
            }
            return Math.Round(r,2);
        }

        public double CalculerMontantAvecTaxes()
        {
            NumberFormatInfo setPrecision = new NumberFormatInfo();
            setPrecision.NumberDecimalDigits = 2;

            double MontantTPS = CalculerMontantHorsTaxes() * TPS;
            double MontantTVQ = CalculerMontantHorsTaxes() * TVQ;
            return Math.Round(CalculerMontantHorsTaxes() + MontantTPS + MontantTVQ,2);
        }

        public double CalculerTotalTaxes()
        {
            return Math.Round(CalculerMontantAvecTaxes() - CalculerMontantHorsTaxes(),2);
        }

        public void AfficherFacture()
        {
            var table = new ConsoleTable("Total Hors Taxes", "Total des taxes", "Total avec taxes");
            table.AddRow(CalculerMontantHorsTaxes(), CalculerTotalTaxes(), CalculerMontantAvecTaxes());
            Console.WriteLine(table);
        }

        public bool PayerFacture()
        {
            if (Client.MontantDepart > CalculerMontantAvecTaxes())
            {
                Client.MontantDepart -= CalculerMontantAvecTaxes();
                return true;
            }
            else
                return false;
        }
        public double CalculerMontantRestantDuDepenses()
        {
            return Math.Round(Client.MontantDepart - CalculerMontantAvecTaxes(),2);
        }
    }
}
