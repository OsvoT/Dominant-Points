using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntosDominantesE
{
    public class ReOrdenamientoBPS
    {
        public List<int> NuevosBPs(double[,] distancias_ises, List<int> PDs_ordenados, List<int> coordenadas_contorno_ordenadas_original, int num_vecinos)
        {
            Dijkstra dijkstra = new Dijkstra();

            List<int> nuevosBPs = new List<int>();
            double[,] submatriz = new double[distancias_ises.GetLength(0) / num_vecinos, distancias_ises.GetLength(1)];
            string[,] caminos_minimos = new string[submatriz.GetLength(0) + 1, num_vecinos];
            string[] camino_minimo = new string[submatriz.GetLength(0)];
            double[] menor_distancia = new double[2];
            menor_distancia[0] = double.MaxValue;
            for (int rr = 0; rr < num_vecinos; rr++)
            {
                for (int ii = 0; ii < submatriz.GetLength(0); ii++)
                {
                    for (int j = 0; j < submatriz.GetLength(1); j++)
                    {
                        submatriz[ii, j] = distancias_ises[ii + (rr * submatriz.GetLength(0)), j];
                    }
                }
                camino_minimo = dijkstra.DijkstraAlgoritmo(submatriz, 0, submatriz.GetLength(0), rr);
                for (int a = 0; a < camino_minimo.Length; a++)
                {
                    caminos_minimos[a, rr] = camino_minimo[a];
                }
            }
            //Comparing the 5 results to work with the smallest of them.
            for (int i = 0; i < num_vecinos; i++)
            {
                if (Convert.ToDouble(caminos_minimos[caminos_minimos.GetLength(0) - 1, i]) < menor_distancia[0])
                {
                    menor_distancia[0] = Convert.ToDouble(caminos_minimos[caminos_minimos.GetLength(0) - 1, i]);
                    menor_distancia[1] = i;
                }
            }
            for (int a = 0; a < submatriz.GetLength(0) + 1; a++)
            {
                camino_minimo[a] = caminos_minimos[a, Convert.ToInt32(menor_distancia[1])];
            }
            nuevosBPs = dijkstra.Camino(camino_minimo);
            return Reordenamiento_BPs(nuevosBPs, PDs_ordenados, coordenadas_contorno_ordenadas_original, Convert.ToInt32(menor_distancia[1]), num_vecinos);
        }
        public List<int> Reordenamiento_BPs(List<int> nuevosBPs, List<int> PDs_ordenados, List<int> coordenadas_contorno_ordenadas_original, int BP_inicial, int num_vecinos)
        {
            //Get the path with the shortest route.
            Quitar_BPs quitar_BPs = new Quitar_BPs();
            Calcular_Error calcular_Error = new Calcular_Error();
            List<int> reordenados_BPs = new List<int>();
            List<int> BP = new List<int>();
            int comparador = 3, contador_BPs = nuevosBPs.Count - 2, posicion_DP_act = 0;
            BP_inicial += 1;
            for (int i = 0; i < PDs_ordenados.Count; i += 3)
            {
                if (BP_inicial == comparador)
                {
                    reordenados_BPs.Add(PDs_ordenados[i]);
                    reordenados_BPs.Add(PDs_ordenados[i + 1]);
                    reordenados_BPs.Add(PDs_ordenados[i + 2]);

                    posicion_DP_act = PDs_ordenados[i + 2] * 2;
                }
                else
                {
                    while (BP.Count != 0)
                        BP.RemoveAt(0);
                    BP.Add(PDs_ordenados[i]);
                    BP.Add(PDs_ordenados[i + 1]);
                    BP.Add(PDs_ordenados[i + 2]);

                    posicion_DP_act = PDs_ordenados[i + 2] * 2;

                    BP = quitar_BPs.vecinosBP_N8(BP, coordenadas_contorno_ordenadas_original, posicion_DP_act, num_vecinos);
                    if (BP_inicial == comparador - 2)
                    {
                        reordenados_BPs.Add(BP[0]);
                        reordenados_BPs.Add(BP[1]);
                        reordenados_BPs.Add(BP[2]);
                    }
                    else if (BP_inicial == comparador - 1)
                    {
                        reordenados_BPs.Add(BP[3]);
                        reordenados_BPs.Add(BP[4]);
                        reordenados_BPs.Add(BP[5]);
                    }
                    else if (BP_inicial == comparador + 1)
                    {
                        reordenados_BPs.Add(BP[9]);
                        reordenados_BPs.Add(BP[10]);
                        reordenados_BPs.Add(BP[11]);
                    }
                    else
                    {
                        reordenados_BPs.Add(BP[12]);
                        reordenados_BPs.Add(BP[13]);
                        reordenados_BPs.Add(BP[14]);
                    }
                }
                if (contador_BPs >= 0)
                    BP_inicial = nuevosBPs[contador_BPs] + 5;
                contador_BPs--;
                comparador += 5;                
            }
            return reordenados_BPs;
        }
    }
}
