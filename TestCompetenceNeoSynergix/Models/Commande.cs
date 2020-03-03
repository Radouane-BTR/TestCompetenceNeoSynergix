using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTables; // il faut ajouter un package NuGet "ConsoleTables"

namespace TestCompetenceNeoSynergix.Models
{
    public class Commande
    {
        public Client client;
        public IList<Produit> Produits { get; set; }

        public Commande()
        {
            Produits = new List<Produit>();
        }

        public void AfficherCommande()
        {
            var table = new ConsoleTable("Numéro", "Description","Quantité");
            foreach (Produit p in Produits)
            {
                table.AddRow(p.Numero, p.Description,p.Quantite);
            }
            Console.WriteLine(table);
        }

        public void AjouterQuantiteCommander(int quantite, int numeroProduit) {
            Produit p = GetProduitCommander(numeroProduit);
            p.Quantite += quantite;
        }

        public Produit GetProduitCommander(int v)
        {
            return Produits.Where(s => s.Numero == v).FirstOrDefault();
        }

        public IList<Produit> GetProduitsCommander()
        {
            return Produits;
        }

        public void AjouterProduit(Produit p)
        {
                this.Produits.Add(p);
        }

        public void SupprimerProduit(Produit p)
        {
            this.Produits.Remove(p);
        }
        public bool IsProduitExist(Produit p) {
            if (this.Produits.Contains(p))
                return false;
            else
                return true;
        }
    }
}
