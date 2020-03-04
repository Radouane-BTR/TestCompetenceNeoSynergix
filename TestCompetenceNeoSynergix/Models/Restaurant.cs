using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTables;

namespace TestCompetenceNeoSynergix.Models
{
    public class Restaurant
    {
        public static string Nom; 
        public Menu Menu;

        public Restaurant(string nom, Menu menu) {
            Nom = nom;
            Menu = menu;
        }
        public static string GetNom() {
            return Nom;
        }

        public Menu GetMenu()
        {
            return Menu;
        }

        public void AfficherMenu() {
            var table = new ConsoleTable("Numéro", "Description", "Prix", "Quantité");
            foreach (Produit p in this.Menu.Produits)
            {
                table.AddRow(p.Numero, p.Description, p.Prix, p.Quantite);
            }
            Console.WriteLine( table);
        }

        public void ReduireQuantiteProduit( int numeroProduit, int quantite)
        {
            Produit p = GetProduit(numeroProduit);
            p.Quantite -= quantite;
        }

        public void AugmenterQuantiteProduit(int numeroProduit, int quantite)
        {
            Produit p = GetProduit(numeroProduit);
            p.Quantite += quantite;
        }

        public Produit GetProduit(int v)
        {
            return Menu.Produits.Where(s => s.Numero == v).FirstOrDefault();
        }
    }
}
