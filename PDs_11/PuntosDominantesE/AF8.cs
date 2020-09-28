using System;
using System.Collections.Generic;

namespace PuntosDominantesE
{
    public class AF8
    {
        public string ObtenerAF8(string cf8)
        {
            char[] cadenaf8 = cf8.ToCharArray();
            string codigoAF8 = System.String.Empty, AF8acomodado = System.String.Empty;
            int i = 1, longitud, val_i, val_i_1;
            longitud = cf8.Length;
            while (i <= longitud)
            {
                if (i == longitud)
                {
                    val_i = cadenaf8[0] - 97;
                    val_i_1 = cadenaf8[longitud - 1] - 97;

                }
                else
                {
                    val_i = cadenaf8[i] - 97;
                    val_i_1 = cadenaf8[i - 1] - 97;
                }
                for (int r = 0; r < 8; r++)
                {
                    if (val_i == ((val_i_1) + r) % 8)
                    {
                        codigoAF8 += Convert.ToChar(r + 97);
                        break;
                    }
                }
                i++;
            }
            AF8acomodado = codigoAF8.Substring(codigoAF8.Length - 1, 1);
            codigoAF8 = codigoAF8.Remove(codigoAF8.Length - 1);
            AF8acomodado += codigoAF8 + " ";
            return AF8acomodado;            
        }

        public string CC_AF8(int[,] Mresolucion, int n, List<string> PDs_alfa_siguiente, bool It_is_file, List<Int32> coordenadas_contorno_archivo)
        {
            BuscarCoordenadas buscar_coordenadas = new BuscarCoordenadas();
            List<List<Int32>> coordenadas_contorno_ordenadas = new List<List<Int32>>();
            Ordenar_Contorno ordenar_Contorno = new Ordenar_Contorno();
            F8 codigof8 = new F8();

            string cadenaAF8 = System.String.Empty, cadenaF8 = System.String.Empty;
            int[] coor_pixel_inicial = new int[3];
            bool encontre_vector = false;
            string[] vectorBase = new string[2];

            if (!It_is_file)
            {
                coor_pixel_inicial = buscar_coordenadas.Buscar_Primerpixel(Mresolucion, n);
                coordenadas_contorno_ordenadas = ordenar_Contorno.Ordenar(Mresolucion, coor_pixel_inicial);
                if (PDs_alfa_siguiente.Count == 0)
                {
                    PDs_alfa_siguiente.Add(coor_pixel_inicial[0].ToString());
                    PDs_alfa_siguiente.Add(coor_pixel_inicial[1].ToString());
                    coor_pixel_inicial = buscar_coordenadas.Buscar_Penultimopixel(Mresolucion, coor_pixel_inicial);
                    PDs_alfa_siguiente.Add(coor_pixel_inicial[0].ToString());
                    PDs_alfa_siguiente.Add(coor_pixel_inicial[1].ToString());
                    coor_pixel_inicial[0] = Convert.ToInt32(PDs_alfa_siguiente[0]);
                    coor_pixel_inicial[1] = Convert.ToInt32(PDs_alfa_siguiente[1]);

                }

                encontre_vector = false;
                if (coor_pixel_inicial[1] + 1 < Mresolucion.GetLength(1))
                {
                    if (Mresolucion[coor_pixel_inicial[0], coor_pixel_inicial[1] + 1] != 0)
                    {
                        vectorBase[0] = " 0";
                        vectorBase[1] = " 1";
                        encontre_vector = true;
                    }
                }
                if (coor_pixel_inicial[0] + 1 < Mresolucion.GetLength(0) && coor_pixel_inicial[1] + 1 < Mresolucion.GetLength(1) && !encontre_vector)
                {
                    if (Mresolucion[coor_pixel_inicial[0] + 1, coor_pixel_inicial[1] + 1] != 0)
                    {
                        vectorBase[0] = " 1";
                        vectorBase[1] = " 1";
                        encontre_vector = true;
                    }
                }
                if (coor_pixel_inicial[0] + 1 < Mresolucion.GetLength(0) && !encontre_vector)
                {
                    if (Mresolucion[coor_pixel_inicial[0] + 1, coor_pixel_inicial[1]] != 0)
                    {
                        vectorBase[0] = " 1";
                        vectorBase[1] = " 0";
                        encontre_vector = true;
                    }
                }
                if (coor_pixel_inicial[0] + 1 < Mresolucion.GetLength(1) && coor_pixel_inicial[1] - 1 >= 0 && !encontre_vector)
                {
                    if (Mresolucion[coor_pixel_inicial[0] + 1, coor_pixel_inicial[1] - 1] != 0)
                    {
                        vectorBase[0] = " 1";
                        vectorBase[1] = " 2";
                    }
                }
            }
            else
            {

                coordenadas_contorno_ordenadas.Add(coordenadas_contorno_archivo);
                coor_pixel_inicial[0] = coordenadas_contorno_archivo[0];
                coor_pixel_inicial[1] = coordenadas_contorno_archivo[1];


                PDs_alfa_siguiente.Add(coor_pixel_inicial[0].ToString());
                PDs_alfa_siguiente.Add(coor_pixel_inicial[1].ToString());
                
                PDs_alfa_siguiente.Add(coordenadas_contorno_archivo[coordenadas_contorno_archivo.Count - 2].ToString());
                PDs_alfa_siguiente.Add(coordenadas_contorno_archivo[coordenadas_contorno_archivo.Count - 1].ToString());
                

                encontre_vector = false;
                if (coor_pixel_inicial[0] == coordenadas_contorno_archivo[2] && coor_pixel_inicial[1] + 1 == coordenadas_contorno_archivo[3])
                {
                    vectorBase[0] = " 0";
                    vectorBase[1] = " 1";
                    encontre_vector = true;
                }

                if (coor_pixel_inicial[0] + 1 == coordenadas_contorno_archivo[2] && coor_pixel_inicial[1] + 1 == coordenadas_contorno_archivo[3])
                {
                    vectorBase[0] = " 1";
                    vectorBase[1] = " 1";
                    encontre_vector = true;
                }

                if (coor_pixel_inicial[0] + 1 == coordenadas_contorno_archivo[2] && coor_pixel_inicial[1] == coordenadas_contorno_archivo[3])
                {
                    vectorBase[0] = " 1";
                    vectorBase[1] = " 0";
                    encontre_vector = true;
                }

                if (coor_pixel_inicial[0] + 1 == coordenadas_contorno_archivo[2] && coor_pixel_inicial[1] - 1 == coordenadas_contorno_archivo[3])
                {
                    vectorBase[0] = " 1";
                    vectorBase[1] = " 2";
                }

            }
            cadenaF8 = codigof8.Obtener_F8(coordenadas_contorno_ordenadas[0], It_is_file);

            cadenaAF8 += ObtenerAF8(cadenaF8);
            return cadenaAF8;
        }
    }
}
