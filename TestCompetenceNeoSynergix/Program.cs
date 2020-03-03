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
            Client client = new Client(); ;
            Commande commande = new Commande();
            Produit produitCommander, p = new Produit() ;
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
                                p = restaurant.GetProduit(numeroProduit);
                                if (p is null)
                                {
                                    Console.WriteLine("le produit avec le numero {0} est introuvable", numeroProduit);
                                    IsExist = false;
                                }
                                //ici je vérifier si le client a déja commander le même produit est j'ajoute la nouvelle quantite à l'encien
                                else if (commande.Produits.Any(x => x.Numero == p.Numero)) 
                                {
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
                                            commande.GetProduitCommander(p.Numero).Quantite+= Quantite;
                                            IsValidQuantite = true;
                                        }
                                        restaurant.AfficherMenu();
                                        IsExist = true;
                                    }
                                }
                                else {
                                    produitCommander = new Produit
                                    {
                                        Numero = p.Numero,
                                        Description = p.Description,
                                        Quantite = 0
                                    };
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
                            // ici je vérifier si le client à déja passer une commande
                            if (commande.Produits.Any()){ 
                                Console.WriteLine("Voici votre commande {0}", Environment.NewLine);
                                commande.AfficherCommande();
                                bool IfExist = false;
                                while (!IfExist)
                                {
                                    int n = ProgramUtilitaire.SaisirNumeroProduit();
                                    p = commande.GetProduitCommander(n);
                                    if (p is null)
                                    {
                                        Console.WriteLine("Votre commande ne contient aucun produit avec le numero {0}", n);
                                        commande.AfficherCommande();
                                        IfExist = false;
                                    }
                                    else {
                                        commande.AfficherCommande();
                                        //produitCommander = commande.GetProduitCommander
                                        commande.SupprimerProduit(p);
                                        Console.Write("Supprimer avec succes");
                                        restaurant.AugmenterQuantiteProduit(p.Numero, p.Quantite);
                                        IfExist = true;
                                    }
                                }
                            }else
                                Console.WriteLine("Votre panier est vide !!", Environment.NewLine);

                            break;
                        case 4: // Payer la facture
                            facture = new Facture(commande, client);
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
                            facture.AfficherFacture();
                            break;
                        case 6: // Afficher l'inventaire de la restaurant
                            restaurant.AfficherMenu();
                            break;
                        case 7: // Aide
                            ProgramUtilitaire.AfficherAide();
                            break;
                        default:
                            Console.WriteLine("Veuillez choisir un numéro entre les options 1 à 8 ...");
                            break;
                    }
                }
                else {
                    optionIsValid = false;
                    Console.WriteLine("le numéro saisie n'est pas valide");
                }
            }
        }
    };
}
