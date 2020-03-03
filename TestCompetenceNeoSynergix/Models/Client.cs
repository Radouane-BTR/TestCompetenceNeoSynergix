using System;
namespace TestCompetenceNeoSynergix.Models
{
    public class Client
    {
        public double MontantDepart=100;

        public void ReduireMontantDepart(double mnt) {
            MontantDepart -= mnt;
        }

        public bool PayerFacture(Facture f) {
            if (MontantDepart > f.MontantAvecTaxes)
            {
                MontantDepart -= f.MontantAvecTaxes;
                return true;
            }
            else
                return false;
        }
        public double RetournerEcartDepense(Facture f) {
            return MontantDepart - f.MontantAvecTaxes;
        }
    }
}

