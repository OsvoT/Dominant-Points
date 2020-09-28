using System;
namespace PuntosDominantesE
{
    public class Procesar_PQR
    {
        public int[] Calcular_PQR(string af8)
        {
            int[] pqr = new int[3];
            int caracter = 0, longitud = af8.Length, ascii, p = 0, r = 0, cont_p = 0, cont_r = 0;
            bool b = false, h = false, bh = false, hb = false;

            while (caracter < longitud - 1)
            {
                ascii = Convert.ToChar(af8.Substring(caracter, 1));
                if (ascii == 97)
                {
                    cont_p++;
                    h = false;
                    b = false;
                }
                else
                {
                    if ((ascii == 98 && !b ) || (ascii == 104 && !h))
                    {
                        if (cont_p > p)
                            p = cont_p;
                        cont_p = 0;

                        if (ascii == 98)
                        {
                            if (!h)
                                b = true;
                            else
                            {
                                if (!bh)
                                {
                                    cont_r++;
                                    hb = true;
                                    h = false;
                                }
                                else
                                {
                                    if (cont_r > r)
                                        r = cont_r;
                                    cont_r = 1;
                                    bh = false;
                                    hb = true;
                                }
                            }
                        }
                        else
                        {
                            if (!b)
                                h = true;
                            else
                            {
                                if (!hb)
                                {
                                    cont_r++;
                                    bh = true;
                                    b = false;
                                }
                                else
                                {
                                    if (cont_r > r)
                                        r = cont_r;
                                    cont_r = 1;
                                    hb = false;
                                    bh = true;
                                }
                            }
                        }
                    }
                    else
                    {                    
                        if (cont_p > p)
                            p = cont_p;
                        if (cont_r > r)
                            r = cont_r;
                        cont_p = 0;
                        cont_r = 0;
                        bh = false;
                        hb = false;
                        h = false;
                        b = false;
                        if (ascii == 104)
                            h = true;
                        else
                            b = true;
                    }
                }
                caracter++;
            }
            pqr[0] = p;
            pqr[1] = p;
            pqr[2] = r;
            return pqr;
        }
    }
}
