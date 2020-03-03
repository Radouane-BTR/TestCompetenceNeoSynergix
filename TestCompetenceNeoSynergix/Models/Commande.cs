using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTables; // il faut ajouter un package NuGet "ConsoleTables"

namespace TestCompetenceNeoSynergix.Models
{
    public class Commande
    {
        public Client client;
        public IList<Produit> Produits { get;set;}

        public ConsoleTable DetailCommande()
        {
            var table = new ConsoleTable("Numéro", "Description","Quantité");
            foreach (Produit p in Produits)
            {
                table.AddRow(p.Numero, p.Description,p.Quantite);
            }
            return table;
        }

        public void AjouterQuantiteCommander(int quantite, Produit p) {
            (from m in this.Produits
             where m == p
             select m).FirstOrDefault().Quantite = quantite;
        }

        public Produit GetProduitCommander(int v)
        {
            return Produits.Where(s => s.Numero == v).FirstOrDefault();
        }

        public void AjouterProduit(Produit p)
        {
            Produits.Add(p);
        }
    }
}
