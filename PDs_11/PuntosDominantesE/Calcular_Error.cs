
using System;
using System.Collections.Generic;

namespace PuntosDominantesE
{
    public class Calcular_Error
    {
        public List<double> Obtener_Error(List<int> pds, List<int> coordenadas_rectas, int inicial, bool repetir_inicio)
        {
            List<int> coordenadas_pds = new List<int> { };
            int pd = 0, pd_sig;
            int coordenada_inicial, coordenada_final = inicial;
            //36
            double error_total = 0, errores = 0;
            List<double> errores_total = new List<double> { };
            while (pd < pds.Count)
            {
                if (pd + 3 != pds.Count)
                {
                    if (pds[pd + 3] != 1000000000 && pds[pd] != 1000000000)
                    {
                        coordenada_inicial = pds[pd + 2] * 2;
                        coordenada_final = pds[pd + 5] * 2;
                        if (coordenada_inicial == coordenada_final)
                            coordenada_final = Buscar_fin_recta(coordenadas_rectas, pds[pd + 3], pds[pd + 4], coordenada_final + 2);
                        pd_sig = pd + 3;
                        errores = Distancia(pds, coordenadas_rectas, coordenada_inicial, coordenada_final, pd, pd_sig);

                        pd += 3;
                        errores_total.Add(errores);
                        error_total += errores;
                    }
                    else
                    {
                        if (pds[pd + 3] == 1000000000)
                        {
                            pd +=4;
                        }
                        else
                            pd++;

                    }
                }
                else
                {
                    if (pds[pd] != 1000000000 && repetir_inicio)
                    {
                        coordenada_inicial = pds[pd + 2] * 2;
                        coordenada_final = pds[2] * 2;
                        if (coordenada_inicial == coordenada_final)
                        {
                            errores = 0;
                        }
                        else
                        {
                            pd_sig = 0;
                            errores = Distancia(pds, coordenadas_rectas, coordenada_inicial, coordenada_final, pd, pd_sig);
                        }
                        pd += 3;
                        errores_total.Add(errores);
                        error_total += errores;
                    }
                    else
                        pd += 3;
                }
            }
            error_total = Math.Round(error_total, 4);
            errores_total.Add(error_total);
            return errores_total;
        }
        public double Distancia(List<int> pds, List<int> coordenadas_rectas, int coordenada_inicial, int coordenada_final, int pd, int pd_sig)
        {
            int x = 0, y = 0, contador = 0;
            double parte_1, parte_2, parte_3, parte_4, parte_5, parte_6;
            double errores = 0, error = 0;


            if (coordenada_inicial != coordenada_final)
            {
                if (coordenada_inicial < coordenada_final)
                {
                    while (coordenada_inicial < coordenada_final)
                    {
                        x = coordenadas_rectas[coordenada_inicial];
                        y = coordenadas_rectas[coordenada_inicial + 1];
                        parte_1 = x - pds[pd];
                        parte_2 = pds[pd_sig + 1] - pds[pd + 1];
                        parte_3 = y - pds[pd + 1];
                        parte_4 = pds[pd_sig] - pds[pd];
                        parte_5 = Math.Pow((pds[pd] - pds[pd_sig]), 2);
                        parte_6 = Math.Pow((pds[pd + 1] - pds[pd_sig + 1]), 2);

                        if (parte_5 != 0 || parte_6 != 0)
                            error = Math.Pow(((parte_1 * parte_2) - (parte_3 * parte_4)), 2) / (parte_5 + parte_6);
                        else if (parte_1 != 0 || parte_2 != 0 || parte_3 != 0 || parte_4 != 0)
                            error = 1;
                        else
                            error += 0;
                        coordenada_inicial += 2;
                        contador++;
                        errores += error;
                    }
                }
                else
                {
                    while (coordenada_inicial < coordenadas_rectas.Count)
                    {
                        x = coordenadas_rectas[coordenada_inicial];
                        y = coordenadas_rectas[coordenada_inicial + 1];
                        parte_1 = x - pds[pd];
                        parte_2 = pds[pd_sig + 1] - pds[pd + 1];
                        parte_3 = y - pds[pd + 1];
                        parte_4 = pds[pd_sig] - pds[pd];
                        parte_5 = Math.Pow(pds[pd] - pds[pd_sig], 2);
                        parte_6 = Math.Pow(pds[pd + 1] - pds[pd_sig + 1], 2);
                        if (parte_5 != 0 || parte_6 != 0)
                            error = Math.Pow(((parte_1 * parte_2) - (parte_3 * parte_4)), 2) / (parte_5 + parte_6);
                        else if (parte_1 != 0 || parte_2 != 0 || parte_3 != 0 || parte_4 != 0)
                            error = 1;
                        else
                            error += 0;
                        coordenada_inicial += 2;
                        contador++;
                        errores += error;
                    }
                    coordenada_inicial = 0;
                    while (coordenada_inicial <= coordenada_final)
                    {
                        x = coordenadas_rectas[coordenada_inicial];
                        y = coordenadas_rectas[coordenada_inicial + 1];
                        parte_1 = x - pds[pd];
                        parte_2 = pds[pd_sig + 1] - pds[pd + 1];
                        parte_3 = y - pds[pd + 1];
                        parte_4 = pds[pd_sig] - pds[pd];
                        parte_5 = Math.Pow(pds[pd] - pds[pd_sig], 2);
                        parte_6 = Math.Pow(pds[pd + 1] - pds[pd_sig + 1], 2);
                        if (parte_5 != 0 || parte_6 != 0)
                            error = Math.Pow(((parte_1 * parte_2) - (parte_3 * parte_4)), 2) / (parte_5 + parte_6);
                        else if (parte_1 != 0 || parte_2 != 0 || parte_3 != 0 || parte_4 != 0)
                            error = 1;
                        else
                            error += 0;
                        coordenada_inicial += 2;
                        contador++;
                        errores += error;
                    }
                }
            }
            errores = Math.Round(errores, 4);
            return errores;           
        }
        public int Buscar_inicio_recta(List<int> coordenadas_rectas, int x, int y, int coordenada_final)
        {
            int posicion_inicial = 0;
            for (int i = coordenada_final; i < coordenadas_rectas.Count; i += 2)
            {
                if (coordenadas_rectas[i] == x && coordenadas_rectas[i + 1] == y)
                {
                    posicion_inicial = i;
                    break;
                }
            }
            if (posicion_inicial == 0)
            {
                for (int i = 0; i < coordenada_final; i += 2)
                {
                    if (coordenadas_rectas[i] == x && coordenadas_rectas[i + 1] == y)
                    {
                        posicion_inicial = i;
                        break;
                    }
                }
            }
            return posicion_inicial;
        }
        public int Buscar_fin_recta(List<int> coordenadas_rectas, int x, int y, int posicion_inicial)
        {
            if (posicion_inicial == coordenadas_rectas.Count)
                posicion_inicial = 0;
            int posicion_final = 0;
            bool entre = false;
            for (int i = posicion_inicial; i < coordenadas_rectas.Count; i += 2)
            {
                if (coordenadas_rectas[i] == x && coordenadas_rectas[i + 1] == y)
                {
                    posicion_final = i;
                    break;
                }
            }
            if (posicion_final == 0)
            {
                for (int i = 0; i <= posicion_inicial; i += 2)
                {
                    if (coordenadas_rectas[i] == x && coordenadas_rectas[i + 1] == y)
                    {
                        posicion_final = i;
                        entre = true;
                        break;
                    }
                }
            }
            if (posicion_final == 0 && !entre)
                posicion_final = posicion_inicial;
            return posicion_final;
        }
    }
}
