using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace PuntosDominantesE
{
    public class Metodo_Hungaro
    {
        public double[,] Hungaro(double[,] matriz)
        {
            double[,] matriz_2 = (double[,])matriz.Clone();
            double max, min;
            double[] pi = new double[matriz_2.GetLength(0)],u, v = new double[matriz_2.GetLength(0)];
            int[] scan_t = new int[matriz_2.GetLength(0)], matchg_t = new int[matriz_2.GetLength(0)];
            int[] scan_s = new int[matriz_2.GetLength(0)], matchg_s = new int[matriz_2.GetLength(0)];
            int[] s = new int[matriz_2.GetLength(0)], t = new int[matriz_2.GetLength(0)];

            Stack st = new Stack();

            min = Min_Matriz(matriz);
            max = Max_Matriz(matriz);

            if (min != 0)
            {
                for (int i = 0; i < matriz_2.GetLength(0); i++)
                    for (int j = 0; j < matriz_2.GetLength(1); j++)
                        //matriz_2[i,j] = max + min - matriz[i, j];//tener cuidado
                        matriz_2[i, j] = max + min - matriz[i, j];
            }
            u = Min_por_renglon(matriz_2);

            //inicializando s,t,pi,scant_t,scant_s,matchg_s,matchg_t
            for (int r = 0; r < matriz_2.GetLength(0); r++)
            {
                s[r] = -4; t[r] = -4; pi[r] = 1000000;
                scan_s[r] = -4; scan_t[r] = -4; // para saber si ya fueron escaneados
                matchg_s[r] = -4; matchg_t[r] = -4;
            }
            matchg_s = big_graph_matching_s(matriz_2, matchg_s, u);
            matchg_t = big_graph_matching_t(matriz_2, matchg_t, u);
            int cond, s2, s3, s5;

            cond = 1;
            s3 = 1;
            s5 = 1;
            while (cond == 1)
            {
                s = step1_s(matchg_s, s, matriz_2.GetLength(0));
                t = step1_t(matchg_t, t, pi,matriz_2.GetLength(0));

                while (s5 == 1 || s3 >= 1)
                {
                    s2 = step2(s, scan_s,scan_t, pi, matriz_2.GetLength(0), ref st);
                    //     cout << "s2=" << s2;   // Si empieza a andar mal quita estos coms.
                    if (s2 >= 0)
                    {
                        s3 = step3(ref st, s, t, u, v, pi, scan_s, matriz_2, matchg_s, scan_t, matchg_t, matriz_2.GetLength(0));
;
                        if (s3 >= 0)
                        {
                            step4(s3, matchg_t, matchg_s, t, s, pi, scan_t, scan_s, matriz_2.GetLength(0));
                            cond = 1;
                            break;
                        } // if s3                        
                    }
                    else s5 = step5(matriz_2.GetLength(0), u, pi, s, t, v);
                    if (s5 == 0)
                    {
                        cond = 0;
                        break;
                    }
                } // while s5 || s3
            }// while cond
             //fp << "matchg_s[" << j << "]t[" << j << "]=" << "(" << matchg_s[j] << "," << matchg_t[j] << ")\n";
            for (int j = 0; j < matriz_2.GetLength(0); j++)
            {
                if(matchg_t[j] + 1 == matriz_2.GetLength(0))
                    matchg_t[j] = 0;
                else
                    matchg_t[j] += 1;
                Console.Write(matchg_s[j] + "," + matchg_t[j]);
            }
            string ruta_Errores;
            ruta_Errores = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Hungaro.txt");
            File.Delete(ruta_Errores);

            using (StreamWriter mylogs = File.AppendText(ruta_Errores))
            {
                for (int i = 0; i < matchg_s.Length; i++)
                    mylogs.WriteLine(matchg_s[i] + "," + matchg_t[i]);
            }

            return matriz_2;
        }
        public int[] big_graph_matching_s(double[,] matriz, int[] matchg_s, double[] u)
        {
            int[] markcol = new int[matriz.GetLength(1)];
            for (int r = 0; r < markcol.Length; r++)
                markcol[r] = 0;

            /* Recorre la matriz 2D para compararla */

            for (int r = 0; r < matriz.GetLength(0); r++)
                for (int c = 0; c < matriz.GetLength(1); c++)
                {
                    if (matriz[r,c] == u[r] && markcol[c] == 0)
                    {  // si es la gráfica                        
                        matchg_s[r] = c;                     // columna no esta marcada..
                        markcol[c] = 1;

                        c = matriz.GetLength(1);
                    }
                }
            return matchg_s;
        }
        public int[] big_graph_matching_t(double[,] matriz, int[] matchg_t, double[] u)
        {
            int[] markcol = new int[matriz.GetLength(1)];
            for (int r = 0; r < markcol.Length; r++)
                markcol[r] = 0;

            /* Recorre la matriz 2D para compararla */

            for (int r = 0; r < matriz.GetLength(0); r++)
                for (int c = 0; c < matriz.GetLength(1); c++)
                {
                    if (matriz[r, c] == u[r] && markcol[c] == 0)
                    {  // si es la gráfica y 
                                        // columna no esta marcada..
                        matchg_t[c] = r;
                        markcol[c] = 1;

                        c = matriz.GetLength(1);
                    }
                }
            return matchg_t;
        }
        public double[] Min_por_renglon(double[,] matriz)
        {
            double[] min_renglon = new double[matriz.GetLength(1)];
            int j = 0;
            for(int i =0; i < matriz.GetLength(0); i ++)
            {
                j = 0;
                min_renglon[i] = matriz[i,j];
                for (j = 1; j < matriz.GetLength(1); j++)
                    if (min_renglon[i] > matriz[i, j])
                        min_renglon[i] = matriz[i, j];
            }
            return min_renglon;
        }
        public double Min_Matriz(double[,] matriz)
        {
            double min = 10000000;
            for (int i = 0; i < matriz.GetLength(0); i++)
                for (int j = 0; j < matriz.GetLength(1); j++)
                    if (min > matriz[i, j])
                        min = matriz[i, j];
            return min;
        }
        public double Max_Matriz(double[,] Matriz)
        {
            double max = 10000000;
            for (int i = 0; i < Matriz.GetLength(0); i++)
                for (int j = 0; j < Matriz.GetLength(1); j++)
                    if (max < Matriz[i, j])
                        max = Matriz[i, j];
            return max;
        }

        //Pasos Metodo Hungaro
        //-------------------------------------------------------------------------------------------------
        //PASO 1
        public int[] step1_s(int[] matchg_s, int[] s, int TAM)  // Primer paso del Algoritmo. "Etiquetar cada
        {                       // vértice expuesto de S con un -1.           
            for (int r = 0; r < TAM; r++)
            {
                if (matchg_s[r] < 0)
                { // Si algún vértice s no esta saturado le pongo -1
                    s[r] = -1;
                }
            }// for r
            return s;
        }// step
        public int[] step1_t(int[] matchg_t, int[] t, double[] pi,int TAM)  // Primer paso del Algoritmo. "Etiquetar cada
        {                       // vértice expuesto de S con un -1.           
            for (int r = 0; r < TAM; r++)
            {                
                if (matchg_t[r] < 0 && pi[r] == 0) // Si algún vértice t no esta saturado le pongo -1
                    t[r] = -1;
            }// for r
            return t;
        }// step

        //-------------------------------------------------------------------------------------------------
        //PASO 2
        public int step2(int[]s, int[] scan_s, int[] scan_t, double[] pi, int TAM, ref Stack st)
        {
            //** getchar();

            int j, enc;
            enc = -1;
            for (int i = TAM - 1; i > -1; i--)
            {
                if (s[i] >= -1 && scan_s[i] != -3)
                {// si la etiqueta es>=-1 y no se ha esca
                    scan_s[i] = -2; // lo voy a usar
                    st.Push(i);
                    enc = 1;
                }
            } // for i

            for (int i = TAM - 1; i > -1; i--)
            {
                if (scan_t[i] != -3 && pi[i] == 0)
                { // Si t no esta usado y pi es 0..
                    scan_t[i] = -2; // lo voy a usar
                    st.Push(i);
                    enc = 1;
                }
            } // for i

            return enc;
        }
        //-------------------------------------------------------------------------------------------------
        //PASO 3
        public int step3(ref Stack st, int[] s, int[] t, double[] u, double[] v, double[] pi, int[] scan_s, double[,] mat2D, int[] matchg_s, int[] scan_t, int[] matchg_t , int TAM)
        {
            int i, j, val = 0;
            int exp = 0;
            double w;
            
            i = (int)st.Pop();
            while (i != -1)
            {
                if (scan_s[i] == -2)
                { // Va al caso en el que se escogió una s en el paso 2.
                    scan_s[i] = -3; // Ya se escaneo s[i]
                    exp = 0;

                    for (j = 0; j < TAM; j++)
                    {

                        w = mat2D[i,j];

                        if (matchg_s[i] != j && matchg_t[j] != i)
                        { // Si no hay matching..
                            if ((u[i] + v[j] - w) < pi[j])
                            { // y u+v-w es menor que pi..
                                pi[j] = u[i] + v[j] - w;  // reemplazamos pi por u+v-w.
                                t[j] = i; // Etiqueta a los vértices de T que no estan saturados.
                            } // if u+v-c
                        } // for j
                    } // if matchg
                } // if s[]
                else if (scan_t[i] == -2)
                { // Si se eligió una t porque pi = 0..
                    scan_t[i] = -3; // ya se escaneo
                    exp = 1; // Puede que este expuesto
                    for (j = 0; j < TAM; j++)
                        if (matchg_s[j] == i && matchg_t[i] == j)
                        { // checamos si t no esta expuesto
                            s[j] = i;   // y etiquetamos a s con i.
                            exp = 0; // no esta expuesto
                        } // if
                } // else

                if (exp == 1)
                {
                    val = i; // *********** REGRESA EL VALOR CORRECTO
                    i = -1;
                } // para que se salga del while de una vez.
                else
                {
                    val = -1;
                    if(st.Count > 0)
                        i = (int)st.Pop();
                    //**  cout << "repito i=" << i << " ";
                    //**  getchar();
                }//else
            } // while i

            return val;
        } // step3
        //----------------------------------------------------------------------------------------------------
        //PASO 4
        void step4(int i, int[] matchg_t, int[] matchg_s, int[] t, int[] s, double[] pi , int[] scan_t, int[] scan_s, int TAM)  // Se ha encontrado una trayectoria extendida.
        {
            int k;
            //Construyamos el nuevo matching         

            k = i;
            do
            {
                //**    cout << "Entra al while ";
                matchg_t[k] = t[k];// El nuevo vértice t saturado.
                matchg_s[t[k]] = k; // El nuevo vértice s saturado.
                k = s[t[k]];
            } while (k != -1);

            for (k = 0; k < TAM; k++)
            {
                pi[k] = 1000000;
                s[k] = -4;
                t[k] = -4;
                scan_s[k] = -4;
                scan_t[k] = -4;
            } // for k
        } // step4
          //-------------------------------------------------------------------------
          //PASO 5
        public int step5(int TAM, double[] u, double[] pi, int[] s, int[] t, double[] v)
        {
            int i, j;
            int val = 0;
            double delt = 0, delt1, delt2;
            delt1 = 1000000;
            delt2 = 1000000;


            for (i = 0; i < TAM; i++) //Busquemos el mínimo de las u's
                if (u[i] < delt1)
                    delt1 = u[i];

            for (i = 0; i < TAM; i++) //Busquemos el mínimo de las pi's
                if (pi[i] < delt2 && pi[i] > 0)
                    delt2 = pi[i];

            if (delt1 <= delt2) // Cálculo de delta
                delt = delt1;
            else if (delt2 < delt1)
                delt = delt2;

            for (i = 0; i < TAM; i++)
                if (s[i] >= -1) //Si s esta etiquetado..
                    u[i] = u[i] - delt;  // Le restamos delta a u[i]

            for (i = 0; i < TAM; i++)
                if (pi[i] == 0) //Si pi es cero..
                    v[i] = v[i] + delt;   // le sumamos delta a v[i]

            for (i = 0; i < TAM; i++)
                if (t[i] >= 0 && pi[i] > 0) //Si pi es mayor que cero y t[i] esta etiquetada
                    pi[i] = pi[i] - delt;   // le restamos delta a p[i]

            if (delt == delt2) val = 1;
            else if (delt == delt1) val = 0;

            return val;

        }
        //-------------------------------------------------------------------------
        /*void bi_graph::totalwork()
        {
            int i, j;
            long int sum;
            int w;

            sum = 0;

            for (i = 0; i < TAM; i++)
            {
                for (j = 0; j < TAM; j++)
                {
                    forig >> wch;
                    w = atoi(wch);
                    if (matchg_s[i] == j)
                        sum = sum + w;
                }// for j
            } // for i

        }// totalwork()*/

    }
}