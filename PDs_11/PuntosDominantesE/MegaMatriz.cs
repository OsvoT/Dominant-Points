using System;
using System.Collections.Generic;

namespace PuntosDominantesE
{
    public class MegaMatriz
    {
        public bool Es_PD2(List<int> PDs_alfa_siguiente, int posicion)
        {
            bool es_pd = false;
            for (int k = 0; k < PDs_alfa_siguiente.Count; k ++)
            {
                if (PDs_alfa_siguiente[k] == 1000000000)
                    k++;
                else
                {
                    if (PDs_alfa_siguiente[k]  == posicion)
                    {
                        es_pd = true;
                        break;
                    }
                }

            }
            return es_pd;
        }
        public bool Es_PD(List<string> PDs_alfa_siguiente, int i, int j, int posicion)
        {
            bool es_pd = false;
            for (int k = 0; k < PDs_alfa_siguiente.Count; k +=3)
            {
                if (PDs_alfa_siguiente[k] == i.ToString() && PDs_alfa_siguiente[k + 1] == j.ToString() && PDs_alfa_siguiente[k + 2] == posicion.ToString())
                {
                    es_pd = true;
                    break;
                }
                    
            }
            return es_pd;
        }
    }
}
