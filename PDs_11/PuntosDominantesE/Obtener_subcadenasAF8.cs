using System;
using System.Collections.Generic;

namespace PuntosDominantesE
{
    public class Obtener_subcadenasAF8
    {
        public static List<string> Dividir_cadenas(string cadenaAF8, List<int> posiciones, List<int> PDs_mantener_actual)
        {
            List<string> subcadenas = new List<string>();
            int segmento;
            string subcadena;
            for (int i = 0; i < posiciones.Count; i++)
            {
                if (PDs_mantener_actual.Contains(i))
                {
                    subcadenas.Add("*");
                }
                else
                {
                    if (i < posiciones.Count - 1)
                    {
                        segmento = posiciones[i + 1] - posiciones[i];
                        subcadena = cadenaAF8.Substring(posiciones[i], segmento);
                        subcadenas.Add(subcadena);
                    }
                    else
                    {
                        segmento = cadenaAF8.Length - posiciones[i] - 1;
                        subcadena = cadenaAF8.Substring(posiciones[i], segmento);
                        if (posiciones[0] != 0)
                        {
                            subcadena += cadenaAF8.Substring(1, posiciones[0] - 1);
                        }
                        subcadenas.Add(subcadena);
                    }
                }
            }
            return subcadenas;
        }
        public static List<int> Almacenar_PD_Lista(string rectas_discretas, bool primer_alfa)
        {
            List<int> PDs = new List<int>();
            String[] substrings3 = rectas_discretas.Split('.');
            bool entre = false, almacene = false, caso_extra = false;
            string[] temporal = new string[4];
            int[] v_caso_extra = new int[4];
            int contador = 0;
            for (int k = 0; k < substrings3.Length; k++)
            {

                String[] substrings = substrings3[k].Split('-');

                for (int j = 0; j < substrings.Length - 1; j++)
                {
                    String[] substrings2 = substrings[j].Split(',');
                    entre = false;
                    for (int i = 0; i < substrings2.Length; i++)
                    {
                        if (j == 0 && !almacene && primer_alfa && substrings3[1].Length > 16)
                        {
                            temporal = substrings[0].Split(',');
                            PDs.Add(Convert.ToInt32(temporal[0]));
                            PDs.Add(Convert.ToInt32(temporal[1]));
                            PDs.Add(Convert.ToInt32(temporal[3]));
                            almacene = true;
                            break;
                        }
                        if (substrings.GetLength(0) == 2)
                        {
                            if (PDs.Count > 0)
                            {
                                if (i != 2)
                                {
                                    PDs.Insert(contador, Convert.ToInt32(substrings2[i]));
                                    contador++;
                                    caso_extra = true;
                                }
                            }
                            else
                            {
                                if (i != 2)
                                    v_caso_extra[i] = Convert.ToInt32(substrings2[i]);
                            }
                        }
                        else
                        {
                            if (i != 2)
                                PDs.Add(Convert.ToInt32(substrings2[i]));
                        }
                        if (substrings.Length - 1 > 2 && j + 1 < substrings.Length - 1 && i + 1 == substrings2.Length)
                        {
                            if (j == 0)
                            {
                                if (primer_alfa)
                                    if (!entre)
                                    {
                                        i = -1;
                                        entre = true;
                                    }
                            }
                            else
                            {
                                if (!entre)
                                {
                                    i = -1;
                                    entre = true;
                                }
                            }

                        }
                        else if (primer_alfa && i + 1 == substrings2.Length && !entre)
                        {
                            i = -1;
                            entre = true;
                        }
                    }
                }
                if (primer_alfa && k > 0)
                {
                    PDs.Add(Convert.ToInt32(temporal[0]));
                    PDs.Add(Convert.ToInt32(temporal[1]));
                    PDs.Add(Convert.ToInt32(temporal[3]));
                }

                if (k > 0 && k < substrings3.Length)
                {
                    if (substrings.Length > 1 && !caso_extra)
                        PDs.Add(1000000000);
                }
            }
            if (v_caso_extra[0] != 0 && v_caso_extra[1] != 0)
            {
                PDs.RemoveAt(PDs.Count - 1);
                PDs.Add(PDs[PDs.Count - 2]);
                PDs.Add(PDs[PDs.Count - 2]);
                PDs.Add(PDs[PDs.Count - 2]);
                for (int i = 0; i < 3; i++)
                    PDs.Add(v_caso_extra[i]);
                PDs.Add(1000000000);
            }
            return PDs;

        }
    }
}
