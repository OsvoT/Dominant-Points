using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntosDominantesE
{
    class Puntos_Dominantes2
    {
        public string Coordenates_BPs(string chaincode, int p, int q, int r, List<int> coordenadas, List<string> Lista_PDs, bool primer_alfa)
        {
            string BPs = System.String.Empty;
            int amountA = 0, amountR = 0, cont_BP = 0;
            bool is_hb = false, is_bh = false, between_BPs = false, is_begin_end;
            for (int i = 0; i < chaincode.Length - 1; i++)
            {
                is_begin_end = false;
                if (i < chaincode.Length - 3)
                    if (Convert.ToInt32(Lista_PDs[cont_BP]) == coordenadas[i * 2] && Convert.ToInt32(Lista_PDs[cont_BP + 1]) == coordenadas[i * 2 + 1] || Convert.ToInt32(Lista_PDs[cont_BP]) == coordenadas[(i + 1) * 2] && Convert.ToInt32(Lista_PDs[cont_BP + 1]) == coordenadas[(i + 1) * 2 + 1] || Convert.ToInt32(Lista_PDs[cont_BP + 2]) == coordenadas[i * 2] && Convert.ToInt32(Lista_PDs[cont_BP + 3]) == coordenadas[i * 2 + 1] || Convert.ToInt32(Lista_PDs[cont_BP + 2]) == coordenadas[(i + 1) * 2] && Convert.ToInt32(Lista_PDs[cont_BP + 3]) == coordenadas[(i + 1) * 2 + 1])
                        is_begin_end = true;
                //Know if actual coordenate is BPs.
                if (!between_BPs)
                    if (Convert.ToInt32(Lista_PDs[cont_BP]) == coordenadas[i * 2] && Convert.ToInt32(Lista_PDs[cont_BP + 1]) == coordenadas[i * 2 + 1])
                    {
                        between_BPs = true;
                        BPs += ".";
                    }
                if (chaincode[i] == 'a' && amountA <= p && amountA <= q)
                {
                    amountA++;
                }
                else if (chaincode[i] == 'b' && chaincode[i + 1] == 'h' && amountR < r && !is_hb && !is_begin_end)
                {

                    is_bh = true;
                    i = i + 1;
                    amountA = 0;
                    amountR++;
                }
                else if (chaincode[i] == 'h' && chaincode[i + 1] == 'b' && amountR < r && !is_bh && !is_begin_end)
                {
                    is_hb = true;
                    i = i + 1;
                    amountA = 0;
                    amountR++;
                }
                else
                {
                    amountA = 0;
                    amountR = 0;
                    if (between_BPs)
                    {
                        BPs += coordenadas[i * 2] + "," + coordenadas[i * 2 + 1] + "," + "0" + "," + i + "-";
                        if (Lista_PDs[cont_BP + 2] == coordenadas[i * 2].ToString() && Lista_PDs[cont_BP + 3] == coordenadas[i * 2 + 1].ToString())
                        {
                            cont_BP += 4;
                            if (cont_BP == Lista_PDs.Count)
                                cont_BP = 0;
                            between_BPs = false;
                        }
                        if (!between_BPs)
                        {
                            if (Convert.ToInt32(Lista_PDs[cont_BP]) == coordenadas[i * 2] && Convert.ToInt32(Lista_PDs[cont_BP + 1]) == coordenadas[i * 2 + 1])
                            {
                                between_BPs = true;
                                BPs += ".";
                                BPs += coordenadas[i * 2] + "," + coordenadas[i * 2 + 1] + "," + "0" + "," + i + "-";
                            }
                        }
                        if ((chaincode[i] == 'b' && chaincode[i + 1] == 'h') || (chaincode[i] == 'h' && chaincode[i + 1] == 'b') && !primer_alfa)
                        {
                            if (Lista_PDs[cont_BP + 2] == coordenadas[(i + 1) * 2].ToString() && Lista_PDs[cont_BP + 3] == coordenadas[(i + 1) * 2 + 1].ToString())
                            {
                                BPs += coordenadas[(i + 1) * 2] + "," + coordenadas[(i + 1) * 2 + 1] + "," + "0" + "," + (i + 1) + "-";
                                cont_BP += 4;
                                if (cont_BP == Lista_PDs.Count)
                                    cont_BP = 0;
                                between_BPs = false;
                            }
                            if (Convert.ToInt32(Lista_PDs[cont_BP]) == coordenadas[(i + 1) * 2] && Convert.ToInt32(Lista_PDs[cont_BP + 1]) == coordenadas[(i + 1) * 2 + 1])
                            {
                                between_BPs = true;
                                BPs += ".";
                                BPs += coordenadas[(i + 1) * 2] + "," + coordenadas[(i + 1) * 2 + 1] + "," + "0" + "," + (i + 1) + "-";
                            }
                            if ((Lista_PDs[cont_BP] != coordenadas[(i + 1) * 2].ToString() || Lista_PDs[cont_BP + 1] != coordenadas[(i + 1) * 2 + 1].ToString()) && (Lista_PDs[cont_BP + 2] != coordenadas[(i + 1) * 2].ToString() || Lista_PDs[cont_BP + 3] != coordenadas[(i + 1) * 2 + 1].ToString()))
                                BPs += coordenadas[(i + 1) * 2] + "," + coordenadas[(i + 1) * 2 + 1] + "," + "0" + "," + (i + 1) + "-";
                            i = i + 1;
                        }
                    }
                    else
                    {
                        if ((chaincode[i] == 'b' && chaincode[i + 1] == 'h') || (chaincode[i] == 'h' && chaincode[i + 1] == 'b') && !primer_alfa)
                        {
                            if (Convert.ToInt32(Lista_PDs[cont_BP]) == coordenadas[(i + 1) * 2] && Convert.ToInt32(Lista_PDs[cont_BP + 1]) == coordenadas[(i + 1) * 2 + 1])
                            {
                                between_BPs = true;
                                BPs += ".";
                                BPs += coordenadas[(i + 1) * 2] + "," + coordenadas[(i + 1) * 2 + 1] + "," + "0" + "," + (i + 1) + "-";
                            }
                            i += 1;
                        }
                    }
                    is_hb = false;
                    is_bh = false;
                }
            }
            //Save information of first BP when the last BP is selected to add more BPs.
            if (between_BPs)
                BPs += coordenadas[0] + "," + coordenadas[1] + "," + "0" + "," + 0 + "-";
            return BPs;
        }
    }
}
