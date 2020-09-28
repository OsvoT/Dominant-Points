using System;
using System.Collections.Generic;

namespace PuntosDominantesE
{
    public class F8
    {
        public string Obtener_F8(List<int> coordenadas_contorno, bool It_is_File)
        {
            int coor_anterior_i, coor_anterior_j;
            string cf8 = System.String.Empty, coordenadas_cf8 = System.String.Empty;
            int i_cont = 0;
            if (!It_is_File)
            {
                for (int i = 0; i < coordenadas_contorno.Count - 1; i += 2)
                {
                    if (i + 2 < coordenadas_contorno.Count - 1)
                    {
                        coor_anterior_i = coordenadas_contorno[i];
                        coor_anterior_j = coordenadas_contorno[i + 1];
                        i_cont = i;
                    }
                    else
                    {
                        coor_anterior_i = coordenadas_contorno[i];
                        coor_anterior_j = coordenadas_contorno[i + 1];
                        i_cont = -2;
                    }
                    //Symbol F8 "a"
                    if (coor_anterior_i == coordenadas_contorno[i_cont + 2] && coor_anterior_j + 1 == coordenadas_contorno[i_cont + 3])
                        cf8 += "a";
                    //Symbol F8 "b"
                    else if (coor_anterior_i + 1 == coordenadas_contorno[i_cont + 2] && coor_anterior_j + 1 == coordenadas_contorno[i_cont + 3])
                        cf8 += "b";
                    //Symbol F8 "c"
                    else if (coor_anterior_i + 1 == coordenadas_contorno[i_cont + 2] && coor_anterior_j == coordenadas_contorno[i_cont + 3])
                        cf8 += "c";
                    //Symbol F8 "d"
                    else if (coor_anterior_i + 1 == coordenadas_contorno[i_cont + 2] && coor_anterior_j - 1 == (coordenadas_contorno[i_cont + 3]))
                        cf8 += "d";
                    //Symbol F8 "e"
                    else if (coor_anterior_i == coordenadas_contorno[i_cont + 2] && coor_anterior_j - 1 == coordenadas_contorno[i_cont + 3])
                        cf8 += "e";
                    //Symbol F8 "f"
                    else if (coor_anterior_i - 1 == coordenadas_contorno[i_cont + 2] && coor_anterior_j - 1 == coordenadas_contorno[i_cont + 3])
                        cf8 += "f";
                    //Symbol F8 "g"
                    else if (coor_anterior_i - 1 == coordenadas_contorno[i_cont + 2] && coor_anterior_j == coordenadas_contorno[i_cont + 3])
                        cf8 += "g";
                    //Symbol F8 "h"
                    else if (coor_anterior_i - 1 == coordenadas_contorno[i_cont + 2] && coor_anterior_j + 1 == coordenadas_contorno[i_cont + 3])
                        cf8 += "h";
                }
            }
            else
            {
                for (int i = 2; i < coordenadas_contorno.Count; i += 2)
                {
                    coor_anterior_i = coordenadas_contorno[i - 2];
                    coor_anterior_j = coordenadas_contorno[i - 1];
                    //Symbol F8 "a"
                    if (coor_anterior_i == coordenadas_contorno[i] && coor_anterior_j + 1 == coordenadas_contorno[i + 1])
                        cf8 += "a";
                    //Symbol F8 "b"
                    else if (coor_anterior_i + 1 == coordenadas_contorno[i] && coor_anterior_j + 1 == coordenadas_contorno[i + 1])
                        cf8 += "b";
                    //Symbol F8 "c"
                    else if (coor_anterior_i + 1 == coordenadas_contorno[i] && coor_anterior_j == coordenadas_contorno[i + 1])
                        cf8 += "c";
                    //Symbol F8 "d"
                    else if (coor_anterior_i + 1 == coordenadas_contorno[i] && coor_anterior_j - 1 == (coordenadas_contorno[i + 1]))
                        cf8 += "d";
                    //Symbol F8 "e"
                    else if (coor_anterior_i == coordenadas_contorno[i] && coor_anterior_j - 1 == coordenadas_contorno[i + 1])
                        cf8 += "e";
                    //Symbol F8 "f"
                    else if (coor_anterior_i - 1 == coordenadas_contorno[i] && coor_anterior_j - 1 == coordenadas_contorno[i + 1])
                        cf8 += "f";
                    //Symbol F8 "g"
                    else if (coor_anterior_i - 1 == coordenadas_contorno[i] && coor_anterior_j == coordenadas_contorno[i + 1])
                        cf8 += "g";
                    //Symbol F8 "h"
                    else if (coor_anterior_i - 1 == coordenadas_contorno[i] && coor_anterior_j + 1 == coordenadas_contorno[i + 1])
                        cf8 += "h";
                }
                coor_anterior_i = coordenadas_contorno[coordenadas_contorno.Count - 2];
                coor_anterior_j = coordenadas_contorno[coordenadas_contorno.Count - 1];
                //Symbol F8 "a"
                if (coor_anterior_i == coordenadas_contorno[0] && coor_anterior_j + 1 == coordenadas_contorno[1])
                    cf8 += "a";
                //Symbol F8 "b"
                else if (coor_anterior_i + 1 == coordenadas_contorno[0] && coor_anterior_j + 1 == coordenadas_contorno[1])
                    cf8 += "b";
                //Symbol F8 "c"
                else if (coor_anterior_i + 1 == coordenadas_contorno[0] && coor_anterior_j == coordenadas_contorno[1])
                    cf8 += "c";
                //Symbol F8 "d"
                else if (coor_anterior_i + 1 == coordenadas_contorno[0] && coor_anterior_j - 1 == (coordenadas_contorno[1]))
                    cf8 += "d";
                //Symbol F8 "e"
                else if (coor_anterior_i == coordenadas_contorno[0] && coor_anterior_j - 1 == coordenadas_contorno[1])
                    cf8 += "e";
                //Symbol F8 "f"
                else if (coor_anterior_i - 1 == coordenadas_contorno[0] && coor_anterior_j - 1 == coordenadas_contorno[1])
                    cf8 += "f";
                //Symbol F8 "g"
                else if (coor_anterior_i - 1 == coordenadas_contorno[0] && coor_anterior_j == coordenadas_contorno[1])
                    cf8 += "g";
                //Symbol F8 "h"
                else if (coor_anterior_i - 1 == coordenadas_contorno[0] && coor_anterior_j + 1 == coordenadas_contorno[1])
                    cf8 += "h";
            }
            return cf8;
        }
    }
}
