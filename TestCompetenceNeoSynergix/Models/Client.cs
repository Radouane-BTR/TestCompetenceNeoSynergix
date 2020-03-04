using System;
namespace TestCompetenceNeoSynergix.Models
{
    public class Client
    {
        public double MontantDepart { get; set; }

        public Client(double montantDepart)
        {
            MontantDepart = montantDepart;
        }

        public Client()
        {
            MontantDepart = 100;
        }
    }
}

