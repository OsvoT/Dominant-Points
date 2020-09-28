using System;
using System.Collections.Generic;
using System.IO;


namespace PuntosDominantesE
{
    public class Seleccion_Segmentos
    {
        public List<int> Obtener_Segmentos(List<double> ISES, List<int> PDs, double T, List<string> PDs_objeto, List<int> coordenadas_contorno_ordenadas_original, int r, string nombre, List<double> list_ISE)
        {
            //Obteniendo la distancia euclidiana entre 2DPs.
            Contorno contorno = new Contorno();
            double kappa;
            List<int> PDs_segmentos_deseados = new List<int>();
            string posiciones_cumplen = System.String.Empty;
            int contados_PDs = 0;
            for (int i = 0; i < PDs.Count - 1; i += 6)
            {
                if (PDs[i] != 1000000000 && PDs[i + 3] != 1000000000)
                {
                    kappa = Calcular_Kappa(PDs, coordenadas_contorno_ordenadas_original, i);
                    if (ISES[contados_PDs] <= (1 / kappa) * T)
                    {
                        PDs_segmentos_deseados.Add(contados_PDs);
                       

                    }                    
                    contados_PDs++;
                }
                else
                {
                    if (PDs[i] == 1000000000)
                        i -= 5;
                    else
                    {
                        PDs_segmentos_deseados.Add(contados_PDs);
                        contados_PDs++;
                        i--;
                    }

                }
            }
            return PDs_segmentos_deseados;
        }
        public List<int> Obtener_Coordenadas_PDs_reales(List<int> PDs, int[,] Moriginal, double alfa, bool reales)
        {
            List<int> PDs_resultado = new List<int>();
            bool No = false;
            int[] Coordenadas = new int[PDs.Count / 2];
            int[] Coordenadas_actuales = new int[2];
            string subcadena = System.String.Empty, cadena_coordenadas_actuales = System.String.Empty;
                for (int i = 0; i < PDs.Count; i += 3)
                {
                    No = false;
                    if (reales)
                    {
                        if (PDs[i] != 1000000000)
                            Coordenadas_actuales = Coordenas_reales(PDs[i], PDs[i + 1], Moriginal, alfa);
                        else
                        {
                            No = true;
                            i -= 2;
                        }
                    }
                    else
                    {
                        if (PDs[i] != 1000000000)
                            Coordenadas_actuales = Coordenas_siguient_alfa(PDs[i], PDs[i + 1], alfa);
                        else
                        {
                            No = true;
                            i -= 2;
                        }
                    }
                    if (!No)
                    {
                        PDs_resultado.Add(Coordenadas_actuales[0]);
                        PDs_resultado.Add(Coordenadas_actuales[1]);
                        PDs_resultado.Add(PDs[i + 2]);
                    }
                    else if (reales)
                        PDs_resultado.Add(1000000000);
                }
                return PDs_resultado;
            //}
        }
        public int[] Coordenas_siguient_alfa(int x, int y, double alfa)
        {
            int[] Coordenadas = new int[2];
            Coordenadas[0] = Convert.ToInt32(Math.Floor(Convert.ToDouble(x) / alfa));
            Coordenadas[1] = Convert.ToInt32(Math.Floor(Convert.ToDouble(y) / alfa));
            return Coordenadas;
        }
        public int[] Coordenas_reales(int x, int y, int[,] Moriginal, double alfa)
        {
            int[] Coordenadas = new int[2];
            double[] centro_masa_megapixel = new double[2];
            double distancia_menor = 100000000, distancia_actual, i_megam_actual, j_megam_actual, i_megam_siguiente, j_megam_siguiente;
            int contador_i = 0, contador_j = 0;
            i_megam_actual = ((x + contador_i) * alfa);
            j_megam_actual = ((y + contador_j) * alfa);
            i_megam_siguiente = ((x + contador_i + 1) * alfa);
            j_megam_siguiente = ((y + contador_j + 1) * alfa);
            centro_masa_megapixel[0] = i_megam_actual + (alfa / 2) - 1;
            centro_masa_megapixel[1] = j_megam_actual + (alfa / 2) - 1;

            //Ya estan los bordes de los pixeles a analizar solo falta crear un contador para recorrer 
            //aquellos pixeles dentro de los bordes del actual.
            for (int i = Convert.ToInt32(Math.Ceiling(i_megam_actual)); i < Convert.ToInt32(Math.Floor(i_megam_siguiente)); i++)
            {
                if (i >= 0)
                {
                    for (int j = Convert.ToInt32(Math.Ceiling(j_megam_actual)); j < Convert.ToInt32(Math.Floor(j_megam_siguiente)); j++)
                    {
                        if (j >= 0)
                        {
                            if (Moriginal[i, j] == 1)
                            {
                                //Calcular la distancia con respecto al centro de masa del mega pixel.
                                distancia_actual = Obtener_distancia(i, j, centro_masa_megapixel[0], centro_masa_megapixel[1]);
                                if (distancia_menor >= distancia_actual)
                                {
                                    distancia_menor = distancia_actual;
                                    Coordenadas[0] = i;
                                    Coordenadas[1] = j;
                                }
                            }
                        }
                    }
                }
            }
            return Coordenadas;
        }
        public double Cantidad_pixeles_entre_BPs(List<int> coordenadas, int PD_k_x_act, int PD_k_y_act, int PD_k_x_sig, int PD_k_y_sig)
        {
            Calcular_Error calcular_Error = new Calcular_Error();
            double S = 0;
            int coordenada_inicial, coordenada_final;
            coordenada_inicial = calcular_Error.Buscar_inicio_recta(coordenadas, PD_k_x_act, PD_k_y_act, 0);
            coordenada_final = calcular_Error.Buscar_fin_recta(coordenadas, PD_k_x_sig, PD_k_y_sig, coordenada_inicial);
            //Para no comenzar a contar pixeles desde el BP.
            //------------------------
            if (coordenada_inicial != coordenada_final)
            {
                if (coordenada_inicial <= coordenada_final)
                {
                    while (coordenada_inicial <= coordenada_final)
                    {
                        coordenada_inicial += 2;
                        S++;
                    }
                }
                else
                {
                    while (coordenada_inicial <= coordenadas.Count - 1)
                    {
                        coordenada_inicial += 2;
                        S++;
                    }
                    coordenada_inicial = 0;
                    while (coordenada_inicial <= coordenada_final)
                    {
                        coordenada_inicial += 2;
                        S++;
                    }
                }
            }
            return S;
        }
        public double Calcular_Kappa(List<int> PDs, List<int> coordenadas_contorno_ordenadas_original, int i)
        {
            double distancia_eucliniana = 0, kappa, S = 0;
            S = Cantidad_pixeles_entre_BPs(coordenadas_contorno_ordenadas_original, PDs[i], PDs[i + 1], PDs[i + 3], PDs[i + 4]);
            distancia_eucliniana = Math.Sqrt(Math.Pow(PDs[i] - PDs[i + 3], 2) + Math.Pow(PDs[i + 1] - PDs[i + 4], 2));
            kappa = S * distancia_eucliniana;
            return kappa;
        }
        public double Obtener_distancia(int i, int j, double x_cm, double y_cm)
        {
            double distancia;
            distancia = Math.Sqrt(Math.Pow(i - x_cm, 2) + Math.Pow(j - y_cm, 2));
            return distancia;
        }
    }
}
