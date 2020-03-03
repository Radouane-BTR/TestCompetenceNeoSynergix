using System;
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
                r += (p.Prix * p.Quantite);
            }
            return r;
        }

        public double MontantAvecTaxes()
        {
            double MontantTPS = MontantHorsTaxes() * TPS;
            double MontantTVQ = MontantHorsTaxes() * TVQ;
            return MontantHorsTaxes() + MontantTPS + MontantTVQ;
        }

        public double TotalTaxes()
        {
            return MontantHorsTaxes() - MontantHorsTaxes();
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
