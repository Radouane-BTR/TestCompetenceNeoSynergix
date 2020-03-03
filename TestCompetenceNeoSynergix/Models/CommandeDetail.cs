using System;
using ConsoleTables;

namespace TestCompetenceNeoSynergix.Models
{
    public class CommandeDetail
    {
        public Commande commande { get; set; }
        public Produit produit { get; set; }
        public int Quantite { get; set; }

       
    }
}
