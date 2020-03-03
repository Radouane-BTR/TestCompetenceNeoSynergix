using System;
using ConsoleTables; // il faut ajouter un package NuGet "ConsoleTables"

namespace TestCompetenceNeoSynergix.Utils
{
    public class ProgramUtilitaire
    {
        public static void AfficherMenuProgram() {
            Console.WriteLine("Veuillez sélectioner une option {0}", Environment.NewLine);
            Console.WriteLine("1. Afficher Le menu");
            Console.WriteLine("2. Ajouter un produit à la commande");
            Console.WriteLine("3. Supprimer un produit de la commande");
            Console.WriteLine("4. Payer la facture");
            Console.WriteLine("5. Afficher le solde  de la facture");
            Console.WriteLine("6. Afficher l'inventaire de la restaurant");
            Console.WriteLine("7. Aide");
            Console.WriteLine("8. Quitter {0}", Environment.NewLine);
        }
        public static void AfficherAide()
        {
            var table = new ConsoleTable("Numéro de l'action","Nom de l’action", "Description");
            table.AddRow(1, "Afficher Le menu", "Afficher le menu du restaurant; les produits offerts, leur prix et la quantité restante de chacun.");
            table.AddRow(2, "Ajouter un produit à la commande", "Ajouter un produit à la commande du client.");
            table.AddRow(3, "Supprimer un produit de la commande", "Supprimer un produit de la commande du client");
            table.AddRow(4, "Payer la facture", "Payer la commande du client et afficher son solde en argent.");
            table.AddRow(5, "Afficher le solde  de la facture", "Afficher le solde en argent du client");
            table.AddRow(6, "Afficher l'inventaire de la restaurant", "Afficher la liste des produits du restaurant avec la quantité restant de chacun");
            table.AddRow(7, "Aide", "Afficher la liste des actions possibles");
            table.AddRow(8, "Quitter", "Quitter le programme");

            Console.WriteLine(table);
        }

        public static int SaisirNumeroProduit()
        {
            bool IsValid = false;
            int NumeroProduit = -1;
            while (!IsValid)
            {
                Console.Write(" Veuillez saisir le numéro de produit : ");
                string saisie = Console.ReadLine();
                if (int.TryParse(saisie, out NumeroProduit))
                    if (NumeroProduit < 0) {
                        IsValid = false;
                        Console.WriteLine("le numero doit être positive");
                    }
                    else
                        IsValid = true;
                else
                {
                    IsValid = false;
                    Console.WriteLine("le numéro de produit que vous avez saisi est incorrect ...");
                }
            }
            return NumeroProduit;
        }

        public static int SaisirQuantite()
        {
            bool IsValid = false;
            int Quantite = -1;
            while (!IsValid)
            {
                Console.Write("Veuillez saisir la quantitée commander : ");
                string saisie = Console.ReadLine();
                if (int.TryParse(saisie, out Quantite))
                    if (Quantite < 1)
                    {
                        IsValid = false;
                        Console.WriteLine("Quantitée non valide...");
                    }
                    else
                        IsValid = true;
                else
                {
                    IsValid = false;
                    Console.WriteLine("La quantitée que vous avez saisi est incorrect / (0 pour annuler) ...");
                }
            }
            return Quantite;
        }

    }
}
