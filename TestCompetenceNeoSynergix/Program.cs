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
            string NOMRESTAURANT = " Belle bouchée";
            Menu menu = new Menu();
            Restaurant restaurant = new Restaurant(NOMRESTAURANT, menu);
            Client client = new Client();
            Commande commande = new Commande{ Produits = new List<Produit>() };
            Produit produitCommander = new Produit();
            //Produit p = new Produit();
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
                            restaurant.AfficherMenu();
                            break; 
                        case 2: // Ajouter un produit à la commande
                            bool IsExist = false;
                            while (!IsExist)
                            {
                                restaurant.AfficherMenu();
                                int numeroProduit = ProgramUtilitaire.SaisirNumeroProduit();
                                Produit p = restaurant.GetProduit(numeroProduit);
                                if (p is null)
                                {
                                    Console.WriteLine("le produit avec le numero {0} est introuvable", numeroProduit);
                                    IsExist = false;
                                }
                                else {
                                    //commande = new Commande();
                                    commande.client = client;
                                    produitCommander.Numero = p.Numero;
                                    produitCommander.Description = p.Description;
                                    produitCommander.Quantite = 0;

                                    commande.AjouterProduit(produitCommander);

                                    bool IsValidQuantite = false;
                                    while (!IsValidQuantite)
                                    {
                                        int Quantite = ProgramUtilitaire.SaisirQuantite();
                                        if (p.Quantite < Quantite)
                                        {
                                            Console.WriteLine("Pas assez de quantite / (0 : Pour annuler) ...");
                                            IsValidQuantite = false;
                                        }
                                        else
                                        {
                                            restaurant.ReduireQuantiteProduit(p.Numero, Quantite);
                                            commande.AjouterQuantiteCommander(Quantite, produitCommander.Numero);
                                            IsValidQuantite = true;
                                        }
                                    }
                                    restaurant.AfficherMenu();
                                    IsExist = true;
                                }
                            }
                            break;

                        case 3: // Supprimer un produit de la commande
                            if (commande.Produits.Any()) { 
                                Console.WriteLine("Voici votre commande {0}", Environment.NewLine);
                                commande.AfficherCommande();
                                bool IfExist = false;
                                while (!IfExist)
                                {
                                    int n = ProgramUtilitaire.SaisirNumeroProduit();
                                    Produit p = commande.GetProduitCommander(n);
                                    if (p is null)
                                    {
                                        Console.WriteLine("Votre commande ne contient aucun produit avec le numero {0}", n);
                                        commande.AfficherCommande();
                                        IfExist = false;
                                    }
                                    else {
                                        commande.Produits.Remove(p);
                                        restaurant.AugmenterQuantiteProduit(p.Numero, p.Quantite);
                                        IfExist = true;
                                    }
                                }
                            }else
                                Console.WriteLine("Votre panier est vide !!", Environment.NewLine);

                            break;
                        case 4: // Payer la facture
                            facture = new Facture(commande, client);
                            //facture.MontantHorsTaxes();
                            //facture.MontantAvecTaxes();
                            //facture.TotalTaxes();
                            facture.AfficherFacture();
                            if (facture.PayerFacture())
                            {
                                commande.Produits.Clear();
                                Console.WriteLine("Votre solde actuel est de : {0} $", client.MontantDepart);
                            }
                            else {
                                Console.WriteLine("Votre Facture dépasse votre montant de dépense avec {0} $", facture.MontantRestantDuDepenses());
                            }
                            break;
                        case 5: // Afficher le solde de la facture
                            commande.AfficherCommande();
                            facture = new Facture(commande,client);
                            //facture.MontantHorsTaxes();
                            //facture.MontantAvecTaxes();
                            //facture.TotalTaxes();
                            facture.AfficherFacture();
                            break;
                        case 6: // Afficher l'inventaire de la restaurant
                            restaurant.AfficherMenu();
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
