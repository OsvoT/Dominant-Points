using System;
using System.Collections.Generic;

namespace PuntosDominantesE
{
    public class Contorno
    {
        public List<int> LimpiarDPs2(List<int> PDs_objeto, int[] coor_pixel_inicial, List<int> posiciones)
        {
            MegaMatriz megaMatriz = new MegaMatriz();
            List<int> PDs_limpios = new List<int>();
            List<int> PDs_limpios_segmento = new List<int>();
            bool dentro = false;
            for (int i = 0; i < PDs_objeto.Count - 1; i += 3)
            {
                if (PDs_objeto[i] == 1000000000)
                {
                    PDs_limpios.Add(PDs_objeto[i]);
                    while (PDs_limpios_segmento.Count != 0)
                        PDs_limpios_segmento.RemoveAt(0);
                    i -= 2;
                }
                else
                {
                    dentro = Validar_BP_Actual(PDs_limpios_segmento, PDs_objeto[i], PDs_objeto[i + 1], PDs_objeto[i + 2], coor_pixel_inicial);
                    if (!dentro)
                    {
                        PDs_limpios.Add(PDs_objeto[i]);
                        PDs_limpios.Add(PDs_objeto[i + 1]);
                        PDs_limpios.Add(PDs_objeto[i + 2]);
                        if (!megaMatriz.Es_PD2(posiciones, PDs_objeto[i + 2]))
                            posiciones.Add(PDs_objeto[i + 2]);

                        PDs_limpios_segmento.Add(PDs_objeto[i]);
                        PDs_limpios_segmento.Add(PDs_objeto[i + 1]);
                        PDs_limpios_segmento.Add(PDs_objeto[i + 2]);
                    }
                }
            }
            return PDs_limpios;
        }
        public bool Validar_BP_Actual(List<int> PDs_limpios, int x, int y, int pos, int[] coor_pixel_inicial)
        {
            bool dentro = false;            
            for (int i = 0; i < PDs_limpios.Count; i += 3)
            {
                if (PDs_limpios[i] == 1000000000)
                    i -= 2;
                else
                {
                    if (PDs_limpios[i] == x && PDs_limpios[i + 1] == y &&( coor_pixel_inicial[0] != x || coor_pixel_inicial[1] != y))
                        dentro = true;
                }
            }
            return dentro;
        }
        public List<string> LimpiarDPs(List<string> PDs_objeto)
        {
            MegaMatriz megaMatriz = new MegaMatriz();
            List<string> PDs_limpios = new List<string>();
            for (int i = 0; i < PDs_objeto.Count; i += 3)
            {
                if (!megaMatriz.Es_PD(PDs_limpios, Convert.ToInt32(PDs_objeto[i]), Convert.ToInt32(PDs_objeto[i + 1]), Convert.ToInt32(PDs_objeto[i + 2])))
                {
                    PDs_limpios.Add(PDs_objeto[i]);
                    PDs_limpios.Add(PDs_objeto[i + 1]);
                    PDs_limpios.Add(PDs_objeto[i + 2]);
                }
            }
            return PDs_limpios;
        }
        public List<int> Ordenar_PDs_2(List<string> PDs_objeto, List<int> coordenadas_contorno)
        {
            List<int> PDs_ordenados = new List<int>();
            List<int> coordenadas = new List<int>();
            int posicion;
            for (int k = 0; k < coordenadas_contorno.Count; k++)
            {
                coordenadas.Add(coordenadas_contorno[k]);
            }
            coordenadas.Add(coordenadas_contorno[0]);
            coordenadas.Add(coordenadas_contorno[1]);
            for (int i = 0; i < coordenadas.Count - 2; i += 2)
            {
                posicion = -1;
                for (int j = 2; j < PDs_objeto.Count; j += 3)
                {
                    if (Convert.ToInt32(PDs_objeto[j]) == i / 2)
                    {
                        PDs_ordenados.Add(Convert.ToInt32(PDs_objeto[j - 2]));
                        PDs_ordenados.Add(Convert.ToInt32(PDs_objeto[j - 1]));
                        PDs_ordenados.Add(Convert.ToInt32(PDs_objeto[j]));
                        posicion = j - 2;
                    }
                }
                if (posicion >= 0)
                {
                    for (int k = 0; k < 3; k++)
                        PDs_objeto.RemoveAt(posicion);
                }
            }
            return PDs_ordenados;
        }
        public int[,] Obtener_Contorno(int[,] matriz, int n)
        {
            int[,] matriz_r = new int[matriz.GetLength(0), matriz.GetLength(1)];
            for (int i = (matriz.GetLength(0) * (n - 1)) + 1; i < (matriz.GetLength(0) * n) - 1; i++)
            {
                for (int j = 1; j < matriz.GetLength(1) - 1; j++)
                {
                    if (Es_contorno(i, j, matriz) && matriz[i,j] == 1)
                        matriz_r[i, j] = 1;
                }
            }
                    return matriz_r;
        }
        public bool Es_contorno(int i, int j, int[,] matriz)
        {
            bool contorno = false;
            if (matriz[i + 1, j] == 0 || matriz[i - 1, j] == 0 || matriz[i, j + 1] == 0 || matriz[i, j - 1] == 0)
                contorno = true;
            return contorno;
        }
    }
}
