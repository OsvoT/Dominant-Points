using System;
using System.Collections.Generic;

namespace PuntosDominantesE
{
    public class BuscarCoordenadas
    {
        public int[] Buscar_Primerpixel(int[,] matriz, int n)
        {
            int[] coordenadas = new int[3];
            int i, j = 0;
            for (i = (matriz.GetLength(0) * (1 - n)); i < matriz.GetLength(0) * n; i++)
            {
                for (j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] == 1)
                    {
                        coordenadas[0] = i;
                        coordenadas[1] = j;
                        i = matriz.GetLength(0);
                        break;
                    }
                }
            }
            return coordenadas;
        }
        public int[] Buscar_Penultimopixel(int[,] matriz, int[] coordenadas)
        {
            int[] coordenadas_penultimo = new int[3];
            bool bandera = false;
            int i = coordenadas[0], j = coordenadas[1], op = 0;
            List<int> coordenadas_contorno_componente = new List<int>();
            if (i - 1 >= 0 && j - 1 >= 0)
            {
                if (matriz[i - 1, j] == 0 && matriz[i, j - 1] != 0)
                {
                    op = 2;
                }
            }
            else if (i + 1 < matriz.GetLength(0) && i - 1 >= 0 && j + 1 < matriz.GetLength(1))
            {
                if (((matriz[i, j + 1] == 0 && matriz[i - 1, j] == 0) || matriz[i, j + 1] == 0))
                {
                    op = 0;
                }
            }
            else if (i + 1 < matriz.GetLength(0) && j - 1 >= 0 && j + 1 < matriz.GetLength(1))
            {
                if (((matriz[i + 1, j] == 0 && matriz[i, j + 1] == 0) || matriz[i + 1, j] == 0) && matriz[i, j - 1] != 0)
                {
                    op = 6;
                }
            }
            else
            {
                op = 4;
            }

            do
            {
                switch (op)
                {
                    case 0:
                        if (i - 1 >= 0 && j + 1 < matriz.GetLength(1))
                        {
                            if (matriz[i - 1, j + 1] != 0)
                            {
                                coordenadas_penultimo[0] = i - 1;
                                coordenadas_penultimo[1] = j + 1;
                                coordenadas_penultimo[2] = 1;
                                bandera = true;
                            }
                            else
                                op++;
                        }
                        else
                            op++;
                        break;
                    case 1:
                        if (i - 1 >= 0)
                        {
                            if (matriz[i - 1, j] != 0)
                            {
                                coordenadas_penultimo[0] = i - 1;
                                coordenadas_penultimo[1] = j;
                                bandera = true;
                            }
                            else
                                op++;
                        }
                        else
                            op++;
                        break;
                    case 2:
                        if (i - 1 >= 0 && j - 1 >= 0)
                        {
                            if (matriz[i - 1, j - 1] != 0)
                            {
                                coordenadas_penultimo[0] = i - 1;
                                coordenadas_penultimo[1] = j - 1;
                                coordenadas_penultimo[2] = 1;
                                bandera = true;
                            }
                            else
                                op++;
                        }
                        else
                            op++;
                        break;
                    case 3:
                        if (j - 1 >= 0)
                        {
                            if (matriz[i, j - 1] != 0)
                            {
                                coordenadas_penultimo[0] = i;
                                coordenadas_penultimo[1] = j - 1;
                                bandera = true;
                            }
                            else
                                op++;
                        }
                        else
                            op++;
                        break;
                    case 4:
                        if (i + 1 < matriz.GetLength(0) && j - 1 >= 0)
                        {
                            if (matriz[i + 1, j - 1] != 0)
                            {
                                coordenadas_penultimo[0] = i + 1;
                                coordenadas_penultimo[1] = j - 1;
                                coordenadas_penultimo[2] = 1;
                                bandera = true;
                            }
                            else
                                op++;
                        }
                        else
                            op++;
                        break;
                    case 5:
                        if (i + 1 < matriz.GetLength(0))
                        {
                            if (matriz[i + 1, j] != 0)
                            {
                                coordenadas_penultimo[0] = i + 1;
                                coordenadas_penultimo[1] = j;
                                bandera = true;
                            }
                            else
                                op++;
                        }
                        else
                            op++;
                        break;
                    case 6:
                        if (i + 1 < matriz.GetLength(0) && j + 1 < matriz.GetLength(1))
                        {
                            if (matriz[i + 1, j + 1] !=0)
                            {
                                coordenadas_penultimo[0] = i + 1;
                                coordenadas_penultimo[1] = j + 1;
                                coordenadas_penultimo[2] = 1;
                                bandera = true;
                            }
                            else
                                op++;
                        }
                        else
                            op++;
                        break;
                    case 7:
                        if (j + 1 < matriz.GetLength(1))
                        {
                            if (matriz[i, j + 1] != 0)
                            {
                                coordenadas_penultimo[0] = i;
                                coordenadas_penultimo[1] = j + 1;
                                bandera = true;
                            }
                        }
                        else
                            op = 0;
                        break;
                }
            } while (!bandera);
            return coordenadas_penultimo;
        } 
    }
}
