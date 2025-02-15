﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace TestCompetenceNeoSynergix.Models
{
    public class Produit 
    {
        public int Numero { get; set; }
        public string Description { get; set; }
        public double Prix { get; set; }
        public int Quantite { get; set; }

        public void ReduireQuantite(int q)
        {
            this.Quantite -= q;
        }
        public void AugmanterQuantite(int q)
        {
            this.Quantite += q; 
        }

        public Produit()
        {
        }

        public override string ToString()
        {
            return Numero+" - "+Description + " - " +Prix + " - " +Quantite;
        }
    }
}
