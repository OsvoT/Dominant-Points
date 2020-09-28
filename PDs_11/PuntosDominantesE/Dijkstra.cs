using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace PuntosDominantesE
{
    public class Dijkstra
    {
        private int MinimumDistance(double[] distance, bool[] shortestPathTreeSet, int verticesCount)
        {
            double min = double.MaxValue;
            int minIndex = 0;

            for (int v = 0; v < verticesCount; ++v)
            {
                if (shortestPathTreeSet[v] == false && distance[v] <= min)
                {
                    min = distance[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }        
        public string[] DijkstraAlgoritmo(double[,] graph, int source, int verticesCount, int r)
        {
            string[] Camino = new string[graph.GetLength(0) + 1];
            double[] distance = new double[verticesCount];
            bool[] shortestPathTreeSet = new bool[verticesCount];

            for (int i = 0; i < verticesCount; ++i)
            {
                distance[i] = double.MaxValue;
                shortestPathTreeSet[i] = false;
            }

            distance[source] = 0;
            Camino[source] = source.ToString();

            for (int count = 0; count < verticesCount - 1; ++count)
            {
                int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
                shortestPathTreeSet[u] = true;

                for (int v = 0; v < 10; ++v)
                {
                    if (u + v < graph.GetLength(0))
                    {
                        if (!shortestPathTreeSet[v + u] && graph[u, v + u] != double.MaxValue && distance[u] + graph[u, v + u] < distance[v + u])
                        {
                            distance[v + u] = distance[u] + graph[u, v + u];
                            Camino[v + u] = Camino[u] + "," + (v + u).ToString();
                        }
                    }                  
                }
            }
            Camino[distance.Length] = distance[distance.Length - 1].ToString();
            return Camino;
        }

        public List<int> Camino(string[] distancia)
        {
            List<int> camino = new List<int>();
            string[] substring = distancia[distancia.Length - 2].Split(',');
            for(int i = substring.Length - 1; i >= 0 ; i--)
            {
                camino.Add(Convert.ToInt32(substring[i]));
            }
            return camino;
        }
    }
}