using System;
using System.Collections.Generic;

namespace PuntosDominantesE
{
    public class Quitar_BPs
    {
        public List<int> Eliminar_BPs_version2(List<int> DPs_objeto, List<Int32> coordenadas_contorno_ordenadas_original, List<double> ISES, double T)
        {
            Calcular_Error calcular_Error = new Calcular_Error();
            int BP_intermedio = 0, posicion_ise = -1, contador = 0 ;
            List<Int32> DPs_analizar = new List<int>();
            List<double> ISES_minimos_encontrados = new List<double>();
            List<double> ISES_Nuevos = new List<double>();
            double ise_menor;
            ISES_Nuevos = Calcular_ISES_Nuevos(DPs_objeto, ISES, coordenadas_contorno_ordenadas_original);
            do
            {
                if (contador == 60)
                {
                    System.Threading.Thread.CurrentThread.Join(10);
                    contador = 0;
                }
                posicion_ise = Buscar_ISE_Minimo_version2(ISES_minimos_encontrados, ISES_Nuevos, posicion_ise);
                if (posicion_ise != -2)
                {
                    ise_menor = ISES_Nuevos[posicion_ise];                    

                    if (posicion_ise != -1)
                    {
                        ///--------------------------------------------------------
                        BP_intermedio = posicion_ise * 3;
                        
                        if (posicion_ise != 0)
                        {
                            ISES[ISES.Count - 1] -= ISES[posicion_ise - 1];
                            ISES[ISES.Count - 1] -= ISES[posicion_ise];
                            ISES[ISES.Count - 1] += ISES_Nuevos[posicion_ise];
                        }
                        else
                        {
                            ISES[ISES.Count - 1] -= ISES[ISES.Count - 2];
                            ISES[ISES.Count - 1] -= ISES[posicion_ise];
                            ISES[ISES.Count - 1] += ISES_Nuevos[posicion_ise];
                        }
                        ///--------------------------------------------------------                      
                        if (ISES[ISES.Count - 1] <= T)
                        {
                            contador++;
                            if (posicion_ise == 0)
                            {
                                ISES.RemoveAt(ISES.Count - 2);
                                ISES.RemoveAt(posicion_ise);

                                ISES.Insert(ISES.Count - 1, ISES_Nuevos[posicion_ise]);
                            }
                            else
                            {
                                ISES.RemoveAt(posicion_ise - 1);
                                ISES.RemoveAt(posicion_ise - 1);

                                ISES.Insert(posicion_ise - 1, ISES_Nuevos[posicion_ise]);
                            }
                            for (int k = 0; k < 3; k++)
                                DPs_objeto.RemoveAt(BP_intermedio);
                            ISES_Nuevos = Calcular_ISES_Nuevos(DPs_objeto, ISES, coordenadas_contorno_ordenadas_original);

                            posicion_ise = -1;
                        }
                        else
                        {

                            break;
                        }
                    }
                }
            } while (posicion_ise != -2);
            //Remove equal BPs
            DPs_objeto = Eliminar_DPs_objeto_repetidos(DPs_objeto);
            return DPs_objeto;
        }        
        public List<int> Eliminar_DPs_objeto_repetidos(List<int> DPs_objeto)
        {
            for (int i = 0; i < DPs_objeto.Count - 3; i += 3)
            {
                if (i == 0)
                {
                    if (DPs_objeto[i] == DPs_objeto[i + 3] && DPs_objeto[i + 1] == DPs_objeto[i + 4] && DPs_objeto[i + 2] == DPs_objeto[i + 5])
                    {
                        for (int k = 0; k < 3; k++)
                            DPs_objeto.RemoveAt(i + 3);
                        i -= 3;
                    }
                }                
            }
            return DPs_objeto;
        }
        public List<double> Calcular_ISES_Nuevos(List<int> DPs_objeto, List<double> ISES, List<int> coordenadas_contorno_ordenadas_original)
        {
            Calcular_Error calcular_Error = new Calcular_Error();
            List<double> ISE_actual = new List<double>();
            List<double> ISES_Nuevos = new List<double>();
            List<Int32> DPs_analizar = new List<int>();
            int posicion_DP = 0;
            for (int posicion_ise = 0; posicion_ise < DPs_objeto.Count; posicion_ise += 3)
            {
                if (posicion_ise == 0)
                {
                    DPs_analizar.Add(DPs_objeto[DPs_objeto.Count - 3]);
                    DPs_analizar.Add(DPs_objeto[DPs_objeto.Count - 2]);
                    DPs_analizar.Add(DPs_objeto[DPs_objeto.Count - 1]);
                    DPs_analizar.Add(DPs_objeto[posicion_ise + 3]);
                    DPs_analizar.Add(DPs_objeto[posicion_ise + 4]);
                    DPs_analizar.Add(DPs_objeto[posicion_ise + 5]);
                }
                else if (posicion_ise + 3 != DPs_objeto.Count)
                {
                    DPs_analizar.Add(DPs_objeto[posicion_ise - 3]);
                    DPs_analizar.Add(DPs_objeto[posicion_ise - 2]);
                    DPs_analizar.Add(DPs_objeto[posicion_ise - 1]);
                    DPs_analizar.Add(DPs_objeto[posicion_ise + 3]);
                    DPs_analizar.Add(DPs_objeto[posicion_ise + 4]);
                    DPs_analizar.Add(DPs_objeto[posicion_ise + 5]);
                }
                else
                {

                    DPs_analizar.Add(DPs_objeto[posicion_ise - 3]);
                    DPs_analizar.Add(DPs_objeto[posicion_ise - 2]);
                    DPs_analizar.Add(DPs_objeto[posicion_ise - 1]);
                    DPs_analizar.Add(DPs_objeto[0]);
                    DPs_analizar.Add(DPs_objeto[1]);
                    DPs_analizar.Add(DPs_objeto[2]);
                }
                posicion_DP = DPs_analizar[2];
                ISE_actual = calcular_Error.Obtener_Error(DPs_analizar, coordenadas_contorno_ordenadas_original, posicion_DP, true);
                ISE_actual[0] = Math.Round(ISE_actual[0], 4);
                ISES_Nuevos.Add(ISE_actual[0]);

                while (DPs_analizar.Count != 0)
                    DPs_analizar.RemoveAt(0);
            }
            return ISES_Nuevos;
        }        
        public int Buscar_ISE_Minimo_version2(List<double> ISES_minimos_encontrados, List<double> ISES_Nuevos, int posicion_ise_ant)
        {
            int posicion_ise = -2;
            double ise_menor = 10000000;

            for (int i = posicion_ise_ant + 1; i < ISES_Nuevos.Count; i++)
            {
                if (posicion_ise_ant != -1)
                {
                    if (ise_menor > ISES_Nuevos[i] && ISE_diferente(ISES_minimos_encontrados, ISES_Nuevos[i]))
                    {
                        ise_menor = ISES_Nuevos[i];
                        posicion_ise = i;
                    }
                }
                else
                {
                    if (ise_menor > ISES_Nuevos[i])
                    {
                        ise_menor = ISES_Nuevos[i];
                        posicion_ise = i;
                    }
                }

            }
            return posicion_ise;
        }
        public bool ISE_diferente(List<double> ISES_minimos_encontrados, double ISE_actual)
        {
            bool diferente = false;
            for (int i = 0; i < ISES_minimos_encontrados.Count; i++)
            {
                if (ISES_minimos_encontrados[i] == ISE_actual)
                {
                    diferente = true;
                    break;
                }
            }
            return diferente;
        }
        public double[,] Mejor_acomodo_BPS(List<int> DPs_objeto, List<Int32> coordenadas_contorno_ordenadas_original, double T, int num_vecinos)
        {
            Calcular_Error calcular_Error = new Calcular_Error();            
            int num_BPs = DPs_objeto.Count / 3, contador_e_i = 1, posicion_DP_sig = 0, posicion_DP_act = 0;
            double[,] distancias_ises = new double[(num_vecinos * (num_BPs - 1) + 2) * num_vecinos, (num_vecinos * (num_BPs - 1) + 2)];
            num_BPs = num_vecinos * (num_BPs - 1) + 2;
            List<int> BP_act = new List<int>(), BP_sig = new List<int>();
            double[,] BPs;
            int[] BPs_finales = new int[2];
            int contador_BP = 0;

            for (int i = 0; i < distancias_ises.GetLength(0); i++)
            {
                for (int j = 0; j < distancias_ises.GetLength(1); j++)
                {
                    distancias_ises[i, j] = double.MaxValue;
                }
            }
            for (int i = 0; i < DPs_objeto.Count; i += 3)
            {
                if (i + 3 >= DPs_objeto.Count)
                {
                    BP_sig.Add(DPs_objeto[0]);
                    BP_sig.Add(DPs_objeto[1]);
                    BP_sig.Add(DPs_objeto[2]);
                    posicion_DP_sig = DPs_objeto[2] * 2;
                    BP_sig = vecinosBP_N8(BP_sig, coordenadas_contorno_ordenadas_original, posicion_DP_sig, num_vecinos);
                }
                else
                {
                    BP_sig.Add(DPs_objeto[i + 3]);
                    BP_sig.Add(DPs_objeto[i + 4]);
                    BP_sig.Add(DPs_objeto[i + 5]);
                    posicion_DP_sig = DPs_objeto[i + 5] * 2;
                    BP_sig = vecinosBP_N8(BP_sig, coordenadas_contorno_ordenadas_original, posicion_DP_sig, num_vecinos);
                }
                BP_act.Add(DPs_objeto[i]);
                BP_act.Add(DPs_objeto[i + 1]);
                BP_act.Add(DPs_objeto[i + 2]);
                posicion_DP_act = DPs_objeto[i + 2] * 2;
                BP_act = vecinosBP_N8(BP_act, coordenadas_contorno_ordenadas_original, posicion_DP_act, num_vecinos);
                if (posicion_DP_act - 4 >= 0)
                    BPs = Reacomodar_BPs(BP_act, BP_sig, coordenadas_contorno_ordenadas_original, posicion_DP_act - 4);
                else if (posicion_DP_act - 2 >= 0)
                    BPs = Reacomodar_BPs(BP_act, BP_sig, coordenadas_contorno_ordenadas_original, coordenadas_contorno_ordenadas_original.Count - 2);
                else
                    BPs = Reacomodar_BPs(BP_act, BP_sig, coordenadas_contorno_ordenadas_original, coordenadas_contorno_ordenadas_original.Count - 4);
                //Store the ises of the left, right and current BPs.
                //---------------------------------------------------------------------------------
                if (i + 2 < DPs_objeto.Count - 2)
                {
                    if (contador_BP == 0)
                    {
                        for (int k = 0; k < BPs.GetLength(0); k++)
                        {
                            for (int l = 0; l < BPs.GetLength(1); l++)
                            {
                                distancias_ises[contador_BP + (num_BPs * k), contador_e_i] = BPs[k, l];

                                contador_e_i++;
                            }
                            contador_e_i = 1;
                        }
                        contador_e_i += num_vecinos;

                        contador_BP += 1;
                    }
                    else
                    {
                        for (int r = 0; r < num_vecinos; r++)
                        {
                            for (int k = 0; k < BPs.GetLength(0); k++)
                            {
                                for (int l = 0; l < BPs.GetLength(1); l++)
                                {
                                    distancias_ises[contador_BP + (r * num_BPs), contador_e_i] = BPs[k, l];

                                    contador_e_i++;
                                }
                                contador_e_i -= num_vecinos;
                                contador_BP++;
                            }
                            contador_BP -= num_vecinos;
                        }
                        contador_e_i += num_vecinos;
                        contador_BP += num_vecinos;
                    }
                }
                else
                {
                    contador_e_i -= num_vecinos;
                    for (int k = 0; k < BPs.GetLength(0); k++)
                    {
                        for (int l = 0; l < BPs.GetLength(1); l++)
                        {
                            distancias_ises[(contador_e_i + num_BPs * (l)), num_BPs - 1] = BPs[k, l];
                        }
                        contador_e_i++;
                    }
                }
                //---------------------------------------------------------------------------------
                while (BP_act.Count > 0)
                    BP_act.RemoveAt(0);

                while (BP_sig.Count > 0)
                    BP_sig.RemoveAt(0);
            }

            return distancias_ises;
        }
        public double[,] Reacomodar_BPs(List<int> BP_act, List<int> BP_sig, List<Int32> coordenadas_contorno_ordenadas_original, int inicial)
        {
            Calcular_Error calcular_Error = new Calcular_Error();
            int posicion_BP_i1 = inicial, posicion_BP_i2 = 0, posicion_DP = inicial, posicion_DP_1 = inicial, posicion_temp = 0;
            double[,] BPs_ises = new double[BP_act.Count / 3, BP_act.Count / 3];
            List<double> ISES = new List<double>();
            List<double> ISES_ant = new List<double>();
            List<int> DPs_analizar = new List<int>();
            List<int> DPs_temp = new List<int>();
            for (int j = 0; j < BP_act.Count; j += 3)
            {
                if (posicion_BP_i1 == coordenadas_contorno_ordenadas_original.Count - 3)
                    posicion_BP_i1 = 0;
                DPs_analizar.Add(BP_act[j]);
                DPs_analizar.Add(BP_act[j + 1]);
                DPs_analizar.Add(BP_act[j + 2]);

                posicion_BP_i2 = posicion_DP_1;

                while (DPs_temp.Count != 0)
                {
                    DPs_temp.RemoveAt(0);
                }
                for (int l = 0; l < BP_sig.Count; l += 3)
                {
                    while (ISES.Count != 0)
                        ISES.RemoveAt(0);
                    DPs_analizar.Add(BP_sig[l]);
                    DPs_analizar.Add(BP_sig[l + 1]);
                    DPs_analizar.Add(BP_sig[l + 2]);
                    posicion_BP_i2 = DPs_analizar[5] * 2;
                    if (l == 6)
                    {
                        if (DPs_temp[0] == DPs_analizar[3] && DPs_temp[1] == DPs_analizar[4] && DPs_temp[2] == DPs_analizar[5])
                        {
                            posicion_temp = posicion_DP_1;
                            posicion_DP_1 = posicion_BP_i2;
                            posicion_BP_i2 += 2;
                        }
                    }
                    if (posicion_BP_i2 > posicion_DP_1 || Math.Abs(posicion_BP_i2 - posicion_DP_1) >= coordenadas_contorno_ordenadas_original.Count / 2)
                    {
                        ISES = calcular_Error.Obtener_Error(DPs_analizar, coordenadas_contorno_ordenadas_original, posicion_DP_1, true);
                    }
                    else
                    {
                        while (ISES_ant.Count != 0)
                            ISES_ant.RemoveAt(0);
                        for (int i = 0; i < ISES.Count; i++)
                            ISES_ant.Add(ISES[i]);
                        for (int r = 0; r < 3; r++)
                            DPs_analizar.RemoveAt(0);

                        DPs_analizar.Add(BP_act[j]);
                        DPs_analizar.Add(BP_act[j + 1]);
                        DPs_analizar.Add(BP_act[j + 2]);
                        ISES = calcular_Error.Obtener_Error(DPs_analizar, coordenadas_contorno_ordenadas_original, posicion_DP_1, true);

                        for (int r = 0; r < 3; r++)
                            DPs_analizar.RemoveAt(0);
                        DPs_analizar.Add(BP_sig[l]);
                        DPs_analizar.Add(BP_sig[l + 1]);
                        DPs_analizar.Add(BP_sig[l + 2]);

                    }
                    if (ISES_ant.Count > 0)
                    {
                        if (ISES_ant[0] < ISES[0])
                            BPs_ises[j / 3, l / 3] = ISES_ant[0];
                        else
                            BPs_ises[j / 3, l / 3] = ISES[0];
                    }
                    else
                        BPs_ises[j / 3, l / 3] = ISES[0];
                    if (l == 0)
                    {
                        for (int k = 3; k < 6; k++)
                            DPs_temp.Add(DPs_analizar[k]);
                    }
                    for (int k = 0; k < 3; k++)
                        DPs_analizar.RemoveAt(DPs_analizar.Count - 1);
                    if (posicion_temp != 0)
                    {
                        posicion_DP_1 = posicion_temp;
                        posicion_temp = 0;
                    }
                }
                for (int k = 0; k < 3; k++)
                    DPs_analizar.RemoveAt(DPs_analizar.Count - 1);
            }

            return BPs_ises;
        }
        public List<int> vecinosBP_N8(List<int> BP, List<Int32> coordenadas_contorno_ordenadas_original, int inicial, int num_vecinos)
        {

            Calcular_Error calcular_Error = new Calcular_Error();

            List<int> vecinos = new List<int>();
            int coordenada_final;

            coordenada_final = inicial;
            if (coordenada_final == 0)
                coordenada_final = coordenadas_contorno_ordenadas_original.Count - 4;
            else if (coordenada_final == 2)
                coordenada_final = coordenadas_contorno_ordenadas_original.Count - 2;
            else
                coordenada_final -= 4;
            for (int i = 0; i < num_vecinos; i++)
            {

                for (int k = 0; k < 3; k++)
                {
                    if (k == 2)
                        vecinos.Add(coordenada_final / 2);
                    else
                        vecinos.Add(coordenadas_contorno_ordenadas_original[coordenada_final + k]);
                }
                if (coordenada_final + 2 == coordenadas_contorno_ordenadas_original.Count)
                {
                    coordenada_final = 0;
                }
                else
                    coordenada_final += 2;
            }
            return vecinos;
        }        
    }
}
