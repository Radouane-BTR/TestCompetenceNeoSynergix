using System;
using System.Collections.Generic;
using ConsoleTables;
using TestCompetenceNeoSynergix.Models;
using TestCompetenceNeoSynergix.Utils;
using System.Linq;


namespace TestCompetenceNeoSynergix
{
    class Program
    {
        static void Main(string[] args)
        {
            Restaurant restaurant = new Restaurant();
            Client client = new Client();
            Commande commande= new Commande();
            commande.Produits = new List<Produit>();
            Facture facture;
            Console.WriteLine("Bienvenue au restaurant {0}{1}", Restaurant.GetNom(), Environment.NewLine);
            bool optionIsValid = false;
            while (!optionIsValid)
            {
                ProgramUtilitaire.AfficherMenuProgram();
                var valueSelected = Console.ReadLine();
                if (int.TryParse(valueSelected, out int MenuSelection))
                {
                    switch (MenuSelection)
                    {
                        case 8: // Quitter
                            Console.WriteLine("Merci de votre visite");
                            Environment.Exit(0);
                            break;
                        case 1: // Afficher Le menu
                            Console.WriteLine(restaurant.MenuRestaurant());
                            break; 
                        case 2: // Ajouter un produit à la commande
                            bool IsExist = false;
                            while (!IsExist)
                            {
                                Console.WriteLine(restaurant.MenuRestaurant());
                                int numeroProduit = ProgramUtilitaire.SaisirNumeroProduit();
                                Produit p = restaurant.GetProduit(numeroProduit);
                                if (p is null)
                                {
                                    Console.WriteLine("un produit avec le numero {0} est introuvable", numeroProduit);
                                    IsExist = false;
                                }
                                else {
                                    commande.client = client;
                                    commande.AjouterProduit(p);
                                    bool IsValidQuantite = false;
                                    while (!IsValidQuantite)
                                    {
                                        int Quantite = ProgramUtilitaire.SaisirQuantite();
                                        if (p.Quantite < Quantite)
                                        {
                                            Console.WriteLine("Pas assez de quantite !!!");
                                            IsValidQuantite = false;
                                        }
                                        else
                                        {
                                            restaurant.ReduireQuantiteProduit(p, Quantite);
                                            commande.AjouterQuantiteCommander(Quantite, p);
                                            IsValidQuantite = true;
                                        }
                                    }
                                    Console.WriteLine(restaurant.MenuRestaurant());
                                    IsExist = true;
                                }
                            }
                            break;

                        case 3: // Supprimer un produit de la commande
                            if (commande.Produits.Any()) { 
                                Console.WriteLine("Voici votre commande {0}", Environment.NewLine);
                                Console.WriteLine(commande.DetailCommande());
                                bool IfExist = false;
                                while (!IfExist)
                                {
                                    int n = ProgramUtilitaire.SaisirNumeroProduit();
                                    Produit p = commande.GetProduitCommander(n);
                                    if (p is null)
                                    {
                                        Console.WriteLine("Votre commande ne contient aucun produit avec le numero {0}", n);
                                        Console.WriteLine(commande.DetailCommande());
                                        IfExist = false;
                                    }
                                    else {
                                        restaurant.AugmenterQuantiteProduit(p, p.Quantite);
                                        commande.Produits.Remove(p);
                                        IfExist = true;
                                    }
                                }
                            }else
                                Console.WriteLine("Votre panier est vide !!", Environment.NewLine);

                            break;
                        case 4: // Payer la facture
                            facture = new Facture();
                            facture.CalculerMontantHorsTaxes(commande);
                            facture.CalculerMontantAvecTaxes();
                            facture.CalculerTotalTaxes();
                            facture.AfficherFacture();
                            if (client.PayerFacture(facture))
                            {
                                commande.Produits.Clear();
                                Console.WriteLine("Votre solde actuel est de : {0} $", client.MontantDepart);
                            }
                            else {
                                Console.WriteLine("Votre Facture dépasse votre montant de dépense avec {0} $", client.RetournerEcartDepense(facture));
                            }
                            break;
                        case 5: // Afficher le solde de la facture
                            facture = new Facture();
                            facture.CalculerMontantHorsTaxes(commande);
                            facture.CalculerMontantAvecTaxes();
                            facture.CalculerTotalTaxes();
                            facture.AfficherFacture();
                            break;
                        case 6: // Afficher l'inventaire de la restaurant
                            Console.WriteLine(restaurant.MenuRestaurant());
                            break;
                        case 7: // Aide
                            ProgramUtilitaire.AfficherAide();
                            break;
                        default:
                            Console.WriteLine("Veuillez choisir entre les options 1 à 8 ...");
                            break;
                    }
                }
                else {
                    optionIsValid = false;
                    Console.WriteLine("l'option choisi n'est pas valide");
                }
            }
        }
    };
}
