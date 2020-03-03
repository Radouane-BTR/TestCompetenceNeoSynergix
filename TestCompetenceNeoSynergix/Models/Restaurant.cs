using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTables;

namespace TestCompetenceNeoSynergix.Models
{
    public class Restaurant
    {
        public static string Nom; 
        public IList<Produit> Menu; 

        public Restaurant() {
            Nom = "Belle bouchée";
            Menu = new List<Produit>() {
                new Produit() { Numero= 1, Description= "Potage inspiration du marché", Prix= 5,Quantite= 5},
                new Produit() { Numero= 2, Description="Salade panachée aux noix de Grenoble",Prix= 7,Quantite= 5},
                new Produit() { Numero= 3, Description="Foie de canard",Prix= 15,Quantite= 2},
                new Produit() { Numero= 4, Description="Huîtres fraîches",Prix= 18,Quantite= 7},
                new Produit() { Numero= 5, Description="Royal chocolat",Prix= 5,Quantite= 8},
                new Produit() { Numero= 6, Description="Crème brûlée",Prix= 6,Quantite= 2}
            };
        }
        public static string GetNom() {
            return Nom;
        }

        public IList<Produit> GetMenu()
        {
            return Menu;
        }

        public ConsoleTable MenuRestaurant() {
            var table = new ConsoleTable("Numéro", "Description", "Prix", "Quantité");
            foreach (Produit p in this.Menu)
            {
                table.AddRow(p.Numero, p.Description, p.Prix, p.Quantite);
            }
            return table;
        }

        public void ReduireQuantiteProduit( Produit p, int quantite)
        {
            (from m in Menu
             where m == p
             select p).FirstOrDefault().Quantite -= quantite;
        }

        public void AugmenterQuantiteProduit( Produit p, int quantite)
        {
            (from m in Menu
             where m == p
             select p).FirstOrDefault().Quantite += quantite;
        }

        public Produit GetProduit(int v)
        {
            return Menu.Where(s => s.Numero == v).FirstOrDefault();
        }
    }
}
