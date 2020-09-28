
using System;
using System.Collections.Generic;

namespace PuntosDominantesE
{
    public class Ordenar_Contorno
    {
        public List<List<int>> Ordenar(int[,] matriz_binaria, int[] coordenada_inicial)
        {
            List<List<int>> coordenadas_resultantes = new List<List<int>>();
            List<int> coordenadas_pendientes = new List<int>();
            List<int> coordenadas = new List<int> { }, vecino_siguiente = new List<int> { }, vecinos_no_contorno, vecinos = new List<int> { };
            List<int> coordenadas_repetidas = new List<int> { };
            coordenadas.Add(coordenada_inicial[0]);
            coordenadas.Add(coordenada_inicial[1]);
            int contador_direcciones = 0, direccion = 0, i = coordenada_inicial[0], j = coordenada_inicial[1];
            bool entre = false, es_contorno = true, encontre_siguiente = false; //false =  no es contorno
            BuscarCoordenadas buscar_coordenadas = new BuscarCoordenadas();
            int[] penultimo = new int[2];
            //int[,] vecinos = new int[3,3];
            //0-.- der.
            //1\.- abajo-der.
            //2|.- abajo.
            //3/.- abajo-izq.
            //4-.- izq.
            //5\.- arriba-izq.
            //6|.- arriba.
            //7/.- arriba-der.
            //Aqui es donde debemos de organizar los diferentes contornos de la imagen.
            matriz_binaria[coordenada_inicial[0], coordenada_inicial[1]] = 5;
            penultimo = buscar_coordenadas.Buscar_Penultimopixel(matriz_binaria, coordenada_inicial);
            do
            {
                if (vecinos.Count > 0)
                {
                    if (matriz_binaria[i, j] != 1 && penultimo[0] == i && penultimo[1] == j)
                        entre = true;
                    matriz_binaria[i, j] = 6;
                    coordenadas.Add(i);
                    coordenadas.Add(j);
                    if ((penultimo[0] == i && penultimo[1] == j && coordenadas_repetidas.Count > 0) || (penultimo[0] == i && penultimo[1] == j && coordenadas.Count > 4))
                    {
                        if (entre)
                            if (coordenadas.Count > 4)
                                coordenadas.Add(-1);
                        break;
                    }
                    while (coordenadas_repetidas.Count != 0)
                        coordenadas_repetidas.RemoveAt(0);
                }
                while (vecinos.Count != 0)
                    vecinos.RemoveAt(0);
                encontre_siguiente = false;
                switch (direccion)
                {
                    case 0:
                        if (j + 1 < matriz_binaria.GetLength(1))
                        {
                            if (i - 1 >= 0)
                            {
                                if (matriz_binaria[i - 1, j + 1] == 1 || matriz_binaria[i - 1, j + 1] == 5)
                                {

                                    contador_direcciones = 0;
                                    vecinos.Add(i - 1);
                                    vecinos.Add(j + 1);
                                    vecinos.Add(7);
                                    encontre_siguiente = true;

                                    while (coordenadas_repetidas.Count != 0)
                                        coordenadas_repetidas.RemoveAt(0);
                                }
                                else
                                {
                                    if (matriz_binaria[i - 1, j + 1] == 6 && coordenadas_repetidas.Count == 0)
                                    {
                                        coordenadas_repetidas.Add(i - 1);
                                        coordenadas_repetidas.Add(j + 1);
                                        coordenadas_repetidas.Add(7);
                                    }
                                }
                            }
                            if ((matriz_binaria[i, j + 1] == 1 || matriz_binaria[i, j + 1] == 5) && !encontre_siguiente)
                            {
                                contador_direcciones = 0;
                                vecinos.Add(i);
                                vecinos.Add(j + 1);
                                vecinos.Add(0);
                                encontre_siguiente = true;
                                while (coordenadas_repetidas.Count != 0)
                                    coordenadas_repetidas.RemoveAt(0);
                            }
                            if (matriz_binaria[i, j + 1] == 6 && coordenadas_repetidas.Count == 0)
                            {
                                coordenadas_repetidas.Add(i);
                                coordenadas_repetidas.Add(j + 1);
                                coordenadas_repetidas.Add(0);
                            }
                            if (i + 1 < matriz_binaria.GetLength(0) && !encontre_siguiente)
                            {
                                if (matriz_binaria[i + 1, j + 1] == 1 || matriz_binaria[i + 1, j + 1] == 5)
                                {
                                    contador_direcciones = 0;
                                    vecinos.Add(i + 1);
                                    vecinos.Add(j + 1);
                                    vecinos.Add(1);
                                    encontre_siguiente = true;
                                    while (coordenadas_repetidas.Count != 0)
                                        coordenadas_repetidas.RemoveAt(0);
                                }
                                else
                                {
                                    if (matriz_binaria[i + 1, j + 1] == 6 && coordenadas_repetidas.Count == 0)
                                    {
                                        coordenadas_repetidas.Add(i + 1);
                                        coordenadas_repetidas.Add(j + 1);
                                        coordenadas_repetidas.Add(1);
                                    }
                                }
                            }
                        }
                        break;
                    case 1:
                        if (j + 1 < matriz_binaria.GetLength(1))
                        {
                            if (matriz_binaria[i, j + 1] == 1 || matriz_binaria[i, j + 1] == 5)
                            {
                                contador_direcciones = 0;
                                vecinos.Add(i);
                                vecinos.Add(j + 1);
                                vecinos.Add(0);
                                encontre_siguiente = true;
                                while (coordenadas_repetidas.Count != 0)
                                    coordenadas_repetidas.RemoveAt(0);
                            }
                            if (matriz_binaria[i, j + 1] == 6 && coordenadas_repetidas.Count == 0)
                            {
                                coordenadas_repetidas.Add(i);
                                coordenadas_repetidas.Add(j + 1);
                                coordenadas_repetidas.Add(0);
                            }
                            if (i + 1 < matriz_binaria.GetLength(0) && !encontre_siguiente)
                            {
                                if (matriz_binaria[i + 1, j + 1] == 1 || matriz_binaria[i + 1, j + 1] == 5)
                                {
                                    contador_direcciones = 0;
                                    vecinos.Add(i + 1);
                                    vecinos.Add(j + 1);
                                    vecinos.Add(1);
                                    encontre_siguiente = true;
                                    while (coordenadas_repetidas.Count != 0)
                                        coordenadas_repetidas.RemoveAt(0);
                                }
                                else
                                {
                                    if (matriz_binaria[i + 1, j + 1] == 6 && coordenadas_repetidas.Count == 0)
                                    {
                                        coordenadas_repetidas.Add(i + 1);
                                        coordenadas_repetidas.Add(j + 1);
                                        coordenadas_repetidas.Add(1);
                                    }
                                }
                            }
                        }
                        if (i + 1 < matriz_binaria.GetLength(0) && !encontre_siguiente)
                        {
                            if (matriz_binaria[i + 1, j] == 1 || matriz_binaria[i + 1, j] == 5)
                            {
                                contador_direcciones = 0;
                                vecinos.Add(i + 1);
                                vecinos.Add(j);
                                vecinos.Add(2);
                                encontre_siguiente = true;
                                while (coordenadas_repetidas.Count != 0)
                                    coordenadas_repetidas.RemoveAt(0);
                            }
                            else
                            {
                                if (matriz_binaria[i + 1, j] == 6 && coordenadas_repetidas.Count == 0)
                                {
                                    coordenadas_repetidas.Add(i + 1);
                                    coordenadas_repetidas.Add(j);
                                    coordenadas_repetidas.Add(2);
                                }
                            }
                        }
                        break;
                    case 2:
                        if (i + 1 < matriz_binaria.GetLength(0))
                        {
                            if (j + 1 < matriz_binaria.GetLength(1))
                            {
                                if (matriz_binaria[i + 1, j + 1] == 1 || matriz_binaria[i + 1, j + 1] == 5)
                                {
                                    contador_direcciones = 0;
                                    vecinos.Add(i + 1);
                                    vecinos.Add(j + 1);
                                    vecinos.Add(1);
                                    encontre_siguiente = true;
                                    while (coordenadas_repetidas.Count != 0)
                                        coordenadas_repetidas.RemoveAt(0);
                                }
                                else
                                {
                                    if (matriz_binaria[i + 1, j + 1] == 6 && coordenadas_repetidas.Count == 0)
                                    {
                                        coordenadas_repetidas.Add(i + 1);
                                        coordenadas_repetidas.Add(j + 1);
                                        coordenadas_repetidas.Add(1);
                                    }
                                }
                            }
                            if ((matriz_binaria[i + 1, j] == 1 || matriz_binaria[i + 1, j] == 5) && !encontre_siguiente)
                            {
                                contador_direcciones = 0;
                                vecinos.Add(i + 1);
                                vecinos.Add(j);
                                vecinos.Add(2);
                                encontre_siguiente = true;
                                while (coordenadas_repetidas.Count != 0)
                                    coordenadas_repetidas.RemoveAt(0);
                            }
                            if (matriz_binaria[i + 1, j] == 6 && coordenadas_repetidas.Count == 0)
                            {
                                coordenadas_repetidas.Add(i + 1);
                                coordenadas_repetidas.Add(j);
                                coordenadas_repetidas.Add(2);
                            }
                            if (j - 1 >= 0 && !encontre_siguiente)
                            {
                                if (matriz_binaria[i + 1, j - 1] == 1 || matriz_binaria[i + 1, j - 1] == 5)
                                {
                                    contador_direcciones = 0;
                                    vecinos.Add(i + 1);
                                    vecinos.Add(j - 1);
                                    vecinos.Add(3);
                                    encontre_siguiente = true;
                                    while (coordenadas_repetidas.Count != 0)
                                        coordenadas_repetidas.RemoveAt(0);
                                }
                                else
                                {
                                    if (matriz_binaria[i + 1, j - 1] == 6 && coordenadas_repetidas.Count == 0)
                                    {
                                        coordenadas_repetidas.Add(i + 1);
                                        coordenadas_repetidas.Add(j - 1);
                                        coordenadas_repetidas.Add(3);
                                    }
                                }
                            }
                        }
                        break;
                    case 3:
                        if (i + 1 < matriz_binaria.GetLength(0))
                        {
                            if (matriz_binaria[i + 1, j] == 1 || matriz_binaria[i + 1, j] == 5)
                            {
                                contador_direcciones = 0;
                                vecinos.Add(i + 1);
                                vecinos.Add(j);
                                vecinos.Add(2);
                                encontre_siguiente = true;
                                while (coordenadas_repetidas.Count != 0)
                                    coordenadas_repetidas.RemoveAt(0);
                            }
                            if (matriz_binaria[i + 1, j] == 6 && coordenadas_repetidas.Count == 0)
                            {
                                coordenadas_repetidas.Add(i + 1);
                                coordenadas_repetidas.Add(j);
                                coordenadas_repetidas.Add(2);
                            }
                            if (j - 1 >= 0 && !encontre_siguiente)
                            {
                                if (matriz_binaria[i + 1, j - 1] == 1 || matriz_binaria[i + 1, j - 1] == 5)
                                {
                                    contador_direcciones = 0;
                                    vecinos.Add(i + 1);
                                    vecinos.Add(j - 1);
                                    vecinos.Add(3);
                                    encontre_siguiente = true;
                                    while (coordenadas_repetidas.Count != 0)
                                        coordenadas_repetidas.RemoveAt(0);
                                }
                                else
                                {
                                    if (matriz_binaria[i + 1, j - 1] == 6 && coordenadas_repetidas.Count == 0)
                                    {
                                        coordenadas_repetidas.Add(i + 1);
                                        coordenadas_repetidas.Add(j - 1);
                                        coordenadas_repetidas.Add(3);
                                    }
                                }
                            }
                        }
                        if (j - 1 >= 0 && !encontre_siguiente)
                        {
                            if (matriz_binaria[i, j - 1] == 1 || matriz_binaria[i, j - 1] == 5)
                            {
                                contador_direcciones = 0;
                                vecinos.Add(i);
                                vecinos.Add(j - 1);
                                vecinos.Add(4);
                                encontre_siguiente = true;
                                while (coordenadas_repetidas.Count != 0)
                                    coordenadas_repetidas.RemoveAt(0);                           
                            }
                            else
                            {
                                if (matriz_binaria[i, j - 1] == 6 && coordenadas_repetidas.Count == 0)
                                {
                                    coordenadas_repetidas.Add(i);
                                    coordenadas_repetidas.Add(j - 1);
                                    coordenadas_repetidas.Add(4);
                                }
                            }
                        }
                        break;
                    case 4:
                        if (j - 1 >= 0)
                        {
                            if (i + 1 < matriz_binaria.GetLength(0))
                            {
                                if (matriz_binaria[i + 1, j - 1] == 1 || matriz_binaria[i + 1, j - 1] == 5)
                                {
                                    contador_direcciones = 0;
                                    vecinos.Add(i + 1);
                                    vecinos.Add(j - 1);
                                    vecinos.Add(3);
                                    encontre_siguiente = true;
                                    while (coordenadas_repetidas.Count != 0)
                                        coordenadas_repetidas.RemoveAt(0);
                                }
                                else
                                {
                                    if (matriz_binaria[i + 1, j - 1] == 6 && coordenadas_repetidas.Count == 0)
                                    {
                                        coordenadas_repetidas.Add(i + 1);
                                        coordenadas_repetidas.Add(j - 1);
                                        coordenadas_repetidas.Add(3);
                                    }
                                }
                            }
                            if ((matriz_binaria[i, j - 1] == 1 || matriz_binaria[i, j - 1] == 5) && !encontre_siguiente)
                            {
                                contador_direcciones = 0;
                                vecinos.Add(i);
                                vecinos.Add(j - 1);
                                vecinos.Add(4);
                                encontre_siguiente = true;
                                while (coordenadas_repetidas.Count != 0)
                                    coordenadas_repetidas.RemoveAt(0);                                
                            }
                            if (matriz_binaria[i + 1, j - 1] == 6 && coordenadas_repetidas.Count == 0)
                            {
                                coordenadas_repetidas.Add(i);
                                coordenadas_repetidas.Add(j - 1);
                                coordenadas_repetidas.Add(4);
                            }
                            if (i - 1 >= 0 && !encontre_siguiente)
                            {
                                if (matriz_binaria[i - 1, j - 1] == 1 || matriz_binaria[i - 1, j - 1] == 5)
                                {contador_direcciones = 0;
                                    vecinos.Add(i - 1);
                                    vecinos.Add(j - 1);
                                    vecinos.Add(5);
                                    encontre_siguiente = true;
                                    while (coordenadas_repetidas.Count != 0)
                                        coordenadas_repetidas.RemoveAt(0);
                                }
                                else
                                {
                                    if (matriz_binaria[i - 1, j - 1] == 6 && coordenadas_repetidas.Count == 0)
                                    {
                                        coordenadas_repetidas.Add(i - 1);
                                        coordenadas_repetidas.Add(j - 1);
                                        coordenadas_repetidas.Add(5);
                                    }
                                }
                            }
                        }
                        break;
                    case 5:
                        if (j - 1 >= 0)
                        {
                            if (matriz_binaria[i, j - 1] == 1 || matriz_binaria[i, j - 1] == 5)
                            {
                                contador_direcciones = 0;
                                vecinos.Add(i);
                                vecinos.Add(j - 1);
                                vecinos.Add(4);
                                encontre_siguiente = true;
                                while (coordenadas_repetidas.Count != 0)
                                    coordenadas_repetidas.RemoveAt(0);

                            }
                            if (matriz_binaria[i, j - 1] == 6 && coordenadas_repetidas.Count == 0)
                            {
                                coordenadas_repetidas.Add(i);
                                coordenadas_repetidas.Add(j - 1);
                                coordenadas_repetidas.Add(4);
                            }
                            if (i - 1 >= 0 && !encontre_siguiente)
                            {
                                if (matriz_binaria[i - 1, j - 1] == 1 || matriz_binaria[i - 1, j - 1] == 5)
                                {
                                    contador_direcciones = 0;
                                    vecinos.Add(i - 1);
                                    vecinos.Add(j - 1);
                                    vecinos.Add(5);
                                    encontre_siguiente = true;
                                    while (coordenadas_repetidas.Count != 0)
                                        coordenadas_repetidas.RemoveAt(0);
                                }
                                else
                                {
                                    if (matriz_binaria[i - 1, j - 1] == 6 && coordenadas_repetidas.Count == 0)
                                    {
                                        coordenadas_repetidas.Add(i - 1);
                                        coordenadas_repetidas.Add(j - 1);
                                        coordenadas_repetidas.Add(5);
                                    }
                                }
                            }
                        }
                        if (i - 1 >= 0 && !encontre_siguiente)
                        {
                            if (matriz_binaria[i - 1, j] == 1 || matriz_binaria[i - 1, j] == 5)
                            {
                                contador_direcciones = 0;
                                vecinos.Add(i - 1);
                                vecinos.Add(j);
                                vecinos.Add(6);
                                encontre_siguiente = true;
                                while (coordenadas_repetidas.Count != 0)
                                    coordenadas_repetidas.RemoveAt(0);                                
                            }
                            else
                            {
                                if (matriz_binaria[i - 1, j] == 6 && coordenadas_repetidas.Count == 0)
                                {
                                    coordenadas_repetidas.Add(i - 1);
                                    coordenadas_repetidas.Add(j);
                                    coordenadas_repetidas.Add(6);
                                }
                            }
                        }
                        break;
                    case 6:
                        if (i - 1 >= 0)
                        {
                            if (j - 1 >= 0)
                            {
                                if (matriz_binaria[i - 1, j - 1] == 1 || matriz_binaria[i - 1, j - 1] == 5)
                                {
                                    contador_direcciones = 0;
                                    vecinos.Add(i - 1);
                                    vecinos.Add(j - 1);
                                    vecinos.Add(5);
                                    encontre_siguiente = true;
                                    while (coordenadas_repetidas.Count != 0)
                                        coordenadas_repetidas.RemoveAt(0);

                                }
                                else
                                {
                                    if (matriz_binaria[i - 1, j - 1] == 6 && coordenadas_repetidas.Count == 0)
                                    {
                                        coordenadas_repetidas.Add(i - 1);
                                        coordenadas_repetidas.Add(j - 1);
                                        coordenadas_repetidas.Add(5);
                                    }
                                }
                            }
                            if ((matriz_binaria[i - 1, j] == 1 || matriz_binaria[i - 1, j] == 5) && !encontre_siguiente)
                            {
                                contador_direcciones = 0;
                                vecinos.Add(i - 1);
                                vecinos.Add(j);
                                vecinos.Add(6);
                                encontre_siguiente = true;
                                while (coordenadas_repetidas.Count != 0)
                                    coordenadas_repetidas.RemoveAt(0);
                            }
                            if (matriz_binaria[i - 1, j] == 6 && coordenadas_repetidas.Count == 0)
                            {
                                coordenadas_repetidas.Add(i - 1);
                                coordenadas_repetidas.Add(j);
                                coordenadas_repetidas.Add(6);
                            }
                            if (j + 1 < matriz_binaria.GetLength(1) && !encontre_siguiente)
                            {
                                if (matriz_binaria[i - 1, j + 1] == 1 || matriz_binaria[i - 1, j + 1] == 5)
                                {
                                    contador_direcciones = 0;
                                    vecinos.Add(i - 1);
                                    vecinos.Add(j + 1);
                                    vecinos.Add(7);
                                    encontre_siguiente = true;
                                    while (coordenadas_repetidas.Count != 0)
                                        coordenadas_repetidas.RemoveAt(0);                                    
                                }
                                else
                                {
                                    if (matriz_binaria[i - 1, j + 1] == 6 && coordenadas_repetidas.Count == 0)
                                    {
                                        coordenadas_repetidas.Add(i - 1);
                                        coordenadas_repetidas.Add(j + 1);
                                        coordenadas_repetidas.Add(7);
                                    }
                                }
                            }
                        }
                        break;
                    case 7:
                        if (i - 1 >= 0)
                        {
                            if (matriz_binaria[i - 1, j] == 1 || matriz_binaria[i - 1, j] == 5)
                            {
                                contador_direcciones = 0;
                                vecinos.Add(i - 1);
                                vecinos.Add(j);
                                vecinos.Add(6);
                                encontre_siguiente = true;
                                while (coordenadas_repetidas.Count != 0)
                                    coordenadas_repetidas.RemoveAt(0);

                            }
                            if (matriz_binaria[i - 1, j] == 6 && coordenadas_repetidas.Count == 0)
                            {
                                coordenadas_repetidas.Add(i - 1);
                                coordenadas_repetidas.Add(j);
                                coordenadas_repetidas.Add(6);
                            }
                            if (j + 1 < matriz_binaria.GetLength(1) && !encontre_siguiente)
                            {
                                if (matriz_binaria[i - 1, j + 1] == 1 || matriz_binaria[i - 1, j + 1] == 5)
                                {
                                    contador_direcciones = 0;
                                    vecinos.Add(i - 1);
                                    vecinos.Add(j + 1);
                                    vecinos.Add(7);
                                    encontre_siguiente = true;
                                    while (coordenadas_repetidas.Count != 0)
                                        coordenadas_repetidas.RemoveAt(0);                                    
                                }
                                else
                                {
                                    if (matriz_binaria[i - 1, j + 1] == 6 && coordenadas_repetidas.Count == 0)
                                    {
                                        coordenadas_repetidas.Add(i - 1);
                                        coordenadas_repetidas.Add(j + 1);
                                        coordenadas_repetidas.Add(7);
                                    }
                                }
                            }
                        }
                        if (j + 1 < matriz_binaria.GetLength(1) && !encontre_siguiente)
                        {
                            if (matriz_binaria[i, j + 1] == 1 || matriz_binaria[i, j + 1] == 5)
                            {
                                contador_direcciones = 0;
                                vecinos.Add(i);
                                vecinos.Add(j + 1);
                                vecinos.Add(0);
                                encontre_siguiente = true;
                                while (coordenadas_repetidas.Count != 0)
                                    coordenadas_repetidas.RemoveAt(0);                                
                            }
                            else
                            {
                                if (matriz_binaria[i, j + 1] == 6 && coordenadas_repetidas.Count == 0)
                                {
                                    coordenadas_repetidas.Add(i);
                                    coordenadas_repetidas.Add(j + 1);
                                    coordenadas_repetidas.Add(0);
                                }
                            }
                        }
                        break;
                }
                if (vecinos.Count == 0)
                {
                    if (es_contorno)
                    {
                        if (direccion + 1 > 7)
                            direccion = 0;
                        else
                            direccion++;
                    }
                    else
                    {
                        if (direccion - 1 < 0)
                            direccion = 7;
                        else
                            direccion--;
                    }
                    contador_direcciones++;
                }
                else
                {
                    vecinos_no_contorno = Validar_vecinos(vecinos, matriz_binaria);
                    if (vecinos_no_contorno.Count > 0)
                    {
                        if (vecinos.Count == 4)
                        {
                            //ubicando pixeles no contorno de la lista vecinos
                            for (int k = 0; k < vecinos.Count; k += 3)
                            {
                                if (vecinos[k] != vecinos_no_contorno[0] || vecinos[k + 1] != vecinos_no_contorno[1])
                                {
                                    if (N8(matriz_binaria, vecinos[k], vecinos[k + 1]) == 2)
                                    {
                                        matriz_binaria[vecinos_no_contorno[0], vecinos_no_contorno[1]] = 5;
                                        coordenadas.Add(vecinos_no_contorno[0]);
                                        coordenadas.Add(vecinos_no_contorno[1]);
                                        i = vecinos_no_contorno[vecinos[k]];
                                        j = vecinos_no_contorno[vecinos[k + 1]];
                                        direccion = vecinos_no_contorno[vecinos[k + 2]];
                                        es_contorno = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            i = vecinos_no_contorno[0];
                            j = vecinos_no_contorno[1];
                            direccion = vecinos_no_contorno[2];
                            es_contorno = false;
                        }
                    }
                    else
                    {
                        i = vecinos[0];
                        j = vecinos[1];
                        if (vecinos[2] - 2 >= 0)
                            direccion = vecinos[2] - 2;
                        else
                            direccion = 8 + (vecinos[2] - 2);
                        entre = false;
                        es_contorno = true;
                    }
                }
                if (contador_direcciones == 8 && coordenadas_repetidas.Count > 0)
                {
                    i = coordenadas_repetidas[0];
                    j = coordenadas_repetidas[1];
                    if (coordenadas_repetidas[2] - 1 >= 0)
                        direccion = coordenadas_repetidas[2] - 1;
                    else
                        direccion = 8 + (coordenadas_repetidas[2] - 1);
                    entre = false;
                    es_contorno = true;
                    contador_direcciones = 0;
                    for (int k = 0; k < coordenadas_repetidas.Count; k++)
                        vecinos.Add(coordenadas_repetidas[k]);
                }

            } while (contador_direcciones < 8);
            if (coordenadas[coordenadas.Count - 1] > 0)
                coordenadas.Add(0);
            coordenadas_resultantes.Add(coordenadas);

            coordenadas_resultantes.Add(coordenadas_pendientes);
            return coordenadas_resultantes;
        }
        public int N8(int[,] matriz, int i, int j)
        {
            int vecinos = 0;
            if (i - 1 >= 0 && j - 1 >= 0)
            {
                if (matriz[i - 1, j - 1] == 1)
                    vecinos++;
            }
            if (i - 1 >= 0)
            {
                if (matriz[i - 1, j] == 1)
                    vecinos++;
            }
            if (i - 1 >= 0 && j + 1 < matriz.GetLength(1))
            {
                if (matriz[i - 1, j + 1] == 1)
                    vecinos++;
            }
            if (j + 1 < matriz.GetLength(1))
            {
                if (matriz[i, j + 1] == 1)
                    vecinos++;
            }
            if (i + 1 < matriz.GetLength(0) && j + 1 < matriz.GetLength(1))
            {
                if (matriz[i + 1, j + 1] == 1)
                    vecinos++;
            }
            if (i + 1 < matriz.GetLength(0))
            {
                if (matriz[i + 1, j] == 1)
                    vecinos++;
            }
            if (i + 1 < matriz.GetLength(0) && j - 1 >= 0)
            {
                if (matriz[i + 1, j - 1] == 1)
                    vecinos++;
            }
            if (j - 1 >= 0)
            {
                if (matriz[i, j - 1] == 1)
                    vecinos++;
            }
            return vecinos;
        }
        public List<int> Validar_vecinos(List<int> vecinos, int[,] matriz)
        {
            List<int> vecinos_no_contorno = new List<int> { };
            for (int i = vecinos.Count - 1; i >= 0; i -= 3)
            {
                if (!Es_contorno(vecinos[i - 2], vecinos[i - 1], matriz))
                {
                    vecinos_no_contorno.Add(vecinos[i - 2]);
                    vecinos_no_contorno.Add(vecinos[i - 1]);
                    vecinos_no_contorno.Add(vecinos[i]);
                    break;
                }
            }
            return vecinos_no_contorno;
        }
        public bool Es_contorno(int i, int j, int[,] matriz)
        {
            bool contorno = false;
            if (i + 1 < matriz.GetLength(0))
                if (matriz[i + 1, j] == 0)
                    contorno = true;
            if (i - 1 >= 0)
                if (matriz[i - 1, j] == 0)
                    contorno = true;
            if (j + 1 < matriz.GetLength(1))
                if (matriz[i, j + 1] == 0)
                    contorno = true;
            if (j - 1 >= 0)
                if (matriz[i, j - 1] == 0)
                    contorno = true;
            return contorno;
        }
    }
}
