using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace PuntosDominantesE
{
    public partial class Form1 : Form
    {
        private double alfa = 1;
        private string nombre = System.String.Empty;
        private Procesamiento BLProcesador;
        private bool It_is_file = false;
        List<Int32> coordenadas_contorno_ordenadas_original = new List<Int32>();
        List<List<Int32>> coordenadas_contorno_ordenadas = new List<List<Int32>>();
        List<Int32> coordenadas_contorno_ordenadas_sig = new List<Int32>();

        List<int> list_n_DPs = new List<int>();
        List<double> list_ISE = new List<double>();
        List<int> list_n_DPs_Elimination = new List<int>();
        List<double> list_ISE_Elimination = new List<double>();
        List<int> list_n_DPs_Rearrangement = new List<int>();
        List<double> list_ISE_Rearrangement = new List<double>();

        private int[,] Moriginal;
        private string cadenaAF8;

        private int p = 0, q = 0, r = 0;

        string filepath;

        public Form1()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OFCargarImagen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(OFCargarImagen.FileName);
                PDprincipal.Image = bitmap ;
                string fileName = OFCargarImagen.FileName;
                filepath = Path.GetDirectoryName(fileName);
                label1.Visible = true;
                nombre = OFCargarImagen.FileName;
                string[] substring = nombre.Split('\\');
                string[] substring2 = substring[substring.Length - 1].Split('.');
                nombre = substring2[0];
                Contorno contorno = new Contorno();
                Cargar_Imagen_PDResultante();
            }

        }
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxT.TextLength > 0)
            {
                button1.Visible = false;
                MegaMatriz megaMatriz = new MegaMatriz();
                F8 codigof8 = new F8();
                AF8 codigoaf8 = new AF8();
                Ordenar_Contorno ordenar_Contorno = new Ordenar_Contorno();
                Contorno contorno = new Contorno();
                BuscarCoordenadas buscar_coordenadas = new BuscarCoordenadas();
                Calcular_Error calcular_Error = new Calcular_Error();
                Seleccion_Segmentos seleccion_Segmentos = new Seleccion_Segmentos();
                Procesar_PQR procesar_PQR = new Procesar_PQR();
                Quitar_BPs quitar_BPs = new Quitar_BPs();
                ReOrdenamientoBPS reOrdenamientoBPS = new ReOrdenamientoBPS();
                Puntos_Dominantes2 puntos_Dominantes2 = new Puntos_Dominantes2();
                Obtener_subcadenasAF8 obtener_SubcadenasAF8 = new Obtener_subcadenasAF8();

                List<string> PDs_alfa_siguiente = new List<string>();
                List<Int32> PDs_alpha_original = new List<Int32>();
                List<int> PDs_mantener_actual = new List<int>();
                List<Int32> PDs_alpha_actual = new List<Int32>();
                List<string> PDs_objeto = new List<string>();
                List<double> ISES = new List<double>();
                List<List<string>> Subcadenas_AF8 = new List<List<string>>();
                List<int> PDs_ordenados = new List<int>();
                List<int> coordenadas_PDs_alfa_actual = new List<int>();
                List<int> coordenadas_BP_reordenadas = new List<int>();
                List<int> PDs_ordenados_ant = new List<int>();

                List<int> posiciones = new List<int>();
                List<int> posiciones_almacenadas = new List<int>();
                List<int> posiciones_ant_act = new List<int>();

                Convertir_a_Imagen Pintar_PDs = new Convertir_a_Imagen();

                string subcadena;
                


                double[,] distancias_ises = new double[5, 5];
                int[] coor_pixel_inicial = new int[3];
                int[] coor_pixel_inicial_sig = new int[3];
                int[] dimensiones = new int[3];
                int[] pqr = new int[3];
                string[] vectorBase = new string[2];
                string[] rectas_discretas_actuales = new string[4];
                int n, p_temporal, q_temporal, r_temporal, contador_BPs = 0;
                double ISE, CR, WE, FOM, WE2, n_1, nDP;
                int num_vecinos = 5;
                bool continuar = false, repetir_penultimo = false;
                string rectas_discretas2;

                double T;
                T = Convert.ToDouble(textBoxT.Text);
                textBoxT.Enabled = false;
                bool cambiar_resolucion = false, primer_alfa = true;

                dimensiones[0] = Moriginal.GetLength(0);
                dimensiones[1] = Moriginal.GetLength(1);
                dimensiones[2] = 1;

                n = 1;

                string subcadenas = Path.Combine(filepath, "Substrings of " + nombre + ".txt");
                File.Delete(subcadenas);

                do
                {
                    if (primer_alfa)
                    {
                        coor_pixel_inicial = buscar_coordenadas.Buscar_Primerpixel(Moriginal, n);
                        if (!It_is_file)
                        {
                            //Traverse the boundary of the 2D shape to obtain its coordinates.
                            coordenadas_contorno_ordenadas_original = ordenar_Contorno.Ordenar(Moriginal, coor_pixel_inicial)[0];
                        }
                        //Obtain the CC to f8 for the boundary of the original shape to know the maximum of p, q and r.
                        Restablecer(Moriginal);
                        cadenaAF8 = codigoaf8.CC_AF8(Moriginal, n, PDs_alfa_siguiente, It_is_file, coordenadas_contorno_ordenadas_original);
                        Restablecer(Moriginal);

                        //Function to obtain maximum p, q, r.
                        while (PDs_alfa_siguiente.Count != 0)
                            PDs_alfa_siguiente.RemoveAt(0);
                        pqr = procesar_PQR.Calcular_PQR(cadenaAF8);
                        p = pqr[0];
                        q = pqr[1];
                        r = pqr[2];

                        label3.Visible = true;
                        textBox2.Visible = true;
                        label6.Visible = true;
                        textBox3.Visible = true;
                        label5.Visible = true;
                        label4.Visible = true;
                        textBox4.Visible = true;

                        textBox2.Text = p.ToString();
                        textBox3.Text = q.ToString();
                        textBox4.Text = r.ToString();
                    }
                    cadenaAF8 = codigoaf8.CC_AF8(Moriginal, n, PDs_alfa_siguiente, It_is_file, coordenadas_contorno_ordenadas_original);

                    if (coordenadas_contorno_ordenadas_original[coordenadas_contorno_ordenadas_original.Count - 1] < 0)
                        repetir_penultimo = true;
                    if (!It_is_file)
                        coordenadas_contorno_ordenadas_original.RemoveAt(coordenadas_contorno_ordenadas_original.Count - 1);
                    char[] delimiter = { '0', '1', '2', '3', '4', '6', '7', '8', '9' };
                    string[] aAF8 = cadenaAF8.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                    p_temporal = p;
                    q_temporal = q;
                    r_temporal = r;
                    do
                    {
                        //Store the BPs from the previous iteration to know which are the BPs detected after each change of r.
                        while (posiciones_ant_act.Count > 0)
                            posiciones_ant_act.RemoveAt(0);
                        for (int i = 0; i < posiciones_almacenadas.Count; i++)
                            posiciones_ant_act.Add(posiciones_almacenadas[i]);
                        while (posiciones.Count > 0)
                            posiciones.RemoveAt(0);
                        //Application of the language for detection of BPs.
                        rectas_discretas2 = puntos_Dominantes2.Coordenates_BPs(cadenaAF8, p_temporal, q_temporal, r_temporal, coordenadas_contorno_ordenadas_original, PDs_alfa_siguiente, primer_alfa);
                        label14.Visible = true;

                        //Divide the chain into substrings according to the position where PDs are.
                        PDs_alpha_actual = Obtener_subcadenasAF8.Almacenar_PD_Lista(rectas_discretas2, primer_alfa);

                        //1000000000 This value allows to separate the DPs of different Segments of Discrete Straight Lines
                        Restablecer(Moriginal);
                        PDs_alpha_original = seleccion_Segmentos.Obtener_Coordenadas_PDs_reales(PDs_alpha_actual, Moriginal, alfa, true);
                        //-------------------------------------Error ISE ------------------------------------------------------------------------- 
                        PDs_alpha_actual = contorno.LimpiarDPs2(PDs_alpha_original, coor_pixel_inicial, posiciones);

                        for (int i = 0; i < posiciones.Count; i++)
                            posiciones_ant_act.Add(posiciones[i]);
                        //Create and store the images of the sets of BPs detected by L
                        Pintar_PDs.getDP(PDs_alpha_actual, new Bitmap(PDResultante.Image)).Save(filepath + "\\Detection" + list_ISE.Count + ".jpeg");
                        //------------------------------------------------------------------------------           

                        //Calculation of the error made between each pair of BPs.
                        ISES = calcular_Error.Obtener_Error(PDs_alpha_actual, coordenadas_contorno_ordenadas_original, 0, repetir_penultimo);

                        //Store the different substrings in a file.
                        using (StreamWriter mylogs = File.AppendText(subcadenas))
                        {
                            List<int> sortedPosiciones = posiciones_ant_act.OrderBy(number => number).ToList();
                            int index = 0;
                            while (index < sortedPosiciones.Count - 1)
                            {
                                if (sortedPosiciones[index] == sortedPosiciones[index + 1])
                                    sortedPosiciones.RemoveAt(index);
                                else
                                    index++;
                            }

                            if (list_ISE.Count > 0)
                            {
                                List<int> PD_temp = new List<int>();
                                List<double> ISE_temp = new List<double>();
                                for (int k = 0; k < sortedPosiciones.Count; k++)
                                {
                                    PD_temp.Add(coordenadas_contorno_ordenadas_original[(sortedPosiciones[k] * 2)]);
                                    PD_temp.Add(coordenadas_contorno_ordenadas_original[(sortedPosiciones[k] * 2) + 1]);
                                    PD_temp.Add(sortedPosiciones[k]);
                                }
                                ISE_temp = calcular_Error.Obtener_Error(PD_temp, coordenadas_contorno_ordenadas_original, 0, true);
                                list_ISE.Add(ISE_temp[ISE_temp.Count - 1]);
                            }
                            else
                                list_ISE.Add(ISES[ISES.Count - 1]);

                            list_n_DPs.Add(sortedPosiciones.Count);

                            mylogs.WriteLine("-------------------------------------------------------------------------------");
                            mylogs.WriteLine("                                     r = " + r_temporal);
                            mylogs.WriteLine("-------------------------------------------------------------------------------");
                            contador_BPs = 0;
                            subcadena = System.String.Empty;
                            string[] Substring = cadenaAF8.Split(' ');
                            for (int i = 0; i < Substring[Substring.Length - 2].Length; i++)
                            {
                                if (contador_BPs < sortedPosiciones.Count)
                                {
                                    if (sortedPosiciones[contador_BPs] != 0)
                                    {
                                        if (i == (sortedPosiciones[contador_BPs]))
                                        {
                                            mylogs.WriteLine(sortedPosiciones[contador_BPs - 1] + "," + subcadena);
                                            contador_BPs++;
                                            subcadena = System.String.Empty;
                                            subcadena += Substring[Substring.Length - 2].Substring(i, 1);
                                        }
                                        else
                                        {
                                            subcadena += Substring[Substring.Length - 2].Substring(i, 1);
                                        }
                                    }
                                    else
                                    {
                                        contador_BPs++;
                                        subcadena += Substring[Substring.Length - 2].Substring(i, 1);
                                    }
                                }
                                else
                                    subcadena += Substring[Substring.Length - 2].Substring(i, 1);
                            }

                            mylogs.WriteLine((sortedPosiciones[sortedPosiciones.Count - 1]) + "," + subcadena);
                        }

                        //------------------------------------------------------------------------------


                        //----------- Obtain segments with errors greater than those allowed (T / sd). --------------
                        if (alfa >= 1)
                        {
                            cambiar_resolucion = false;
                            PDs_mantener_actual = seleccion_Segmentos.Obtener_Segmentos(ISES, PDs_alpha_original, T, PDs_objeto, coordenadas_contorno_ordenadas_original, r_temporal, nombre, list_ISE);

                            if (PDs_mantener_actual.Count > 0)
                            {
                                int i_anterior = 0;
                                //Remove 1000000000 from the list of BPs
                                //-----------------------------------------------------------------------------------------------------------
                                for (int i = 0; i < PDs_alpha_original.Count; i++)
                                {
                                    if (PDs_alpha_original[i] == 1000000000)
                                    {
                                        PDs_alpha_original.RemoveAt(i);
                                        if (i == i_anterior + 3)
                                        {
                                            PDs_alpha_original.Insert(i, 0);
                                            PDs_alpha_original.Insert(i, 0);
                                        }
                                        i_anterior = i;
                                        i--;
                                    }
                                }
                                //-----------------------------------------------------------------------------------------------------------

                                for (int i = 0; i < PDs_mantener_actual.Count; i++)
                                {
                                    for (int j = 0; j < 6; j++)
                                    {
                                        if (j == 0 || j == 3)
                                        {
                                            if (!megaMatriz.Es_PD(PDs_objeto, PDs_alpha_original[(PDs_mantener_actual[i] * 6) - i * 6], PDs_alpha_original[(PDs_mantener_actual[i] * 6) - i * 6 + 1], PDs_alpha_original[(PDs_mantener_actual[i] * 6) - i * 6 + 2]))
                                            {
                                                //Store the coordinates of the BPs that generate an allowed error.
                                                //------------------------------------------------------------------------------------
                                                PDs_objeto.Add(PDs_alpha_original[(PDs_mantener_actual[i] * 6) - i * 6].ToString());
                                                PDs_objeto.Add(PDs_alpha_original[(PDs_mantener_actual[i] * 6) - i * 6 + 1].ToString());
                                                PDs_objeto.Add(PDs_alpha_original[(PDs_mantener_actual[i] * 6) - i * 6 + 2].ToString());
                                                posiciones_almacenadas.Add(PDs_alpha_original[(PDs_mantener_actual[i] * 6) - i * 6 + 2]);
                                                //------------------------------------------------------------------------------------                                               
                                            }
                                        }
                                        PDs_alpha_original.RemoveAt((PDs_mantener_actual[i] * 6) - i * 6);
                                    }
                                    if (PDs_alpha_original.Count == 0)
                                        break;
                                }
                            }
                        }
                        else
                        {
                            while (PDs_alpha_original.Count > 0)
                            {
                                if (PDs_alpha_original[0] != 1000000000)
                                    PDs_objeto.Add(PDs_alpha_original[0].ToString());
                                PDs_alpha_original.RemoveAt(0);
                            }
                        }
                        //Decrease r, while p and q maintain their initial values.

                        while (PDs_alfa_siguiente.Count > 0)
                        {
                            PDs_alfa_siguiente.RemoveAt(0);
                        }
                        if (r_temporal > 0)
                        {
                            if (PDs_alpha_original.Count > 0)
                                repetir_penultimo = false;
                            for (int i = 0; i < PDs_alpha_original.Count; i++)
                            {
                                if (PDs_alpha_original[i] != 1000000000)
                                {
                                    if (i % 6 == 0 && i + 3 < PDs_alpha_original.Count)
                                    {
                                        //The coordinates of the BPs that do not meet the condition allow finding new BPs contained between them in those contour segments.
                                        if (PDs_alpha_original[i] != PDs_alpha_original[i + 3] || PDs_alpha_original[i + 1] != PDs_alpha_original[i + 4])
                                            PDs_alfa_siguiente.Add(PDs_alpha_original[i].ToString());
                                        else
                                            i += 3;
                                    }
                                    else
                                    {
                                        if ((i + 1) % 3 != 0)
                                            PDs_alfa_siguiente.Add(PDs_alpha_original[i].ToString());
                                    }
                                }
                            }
                        }
                        if (r_temporal > 1)
                        {
                            r_temporal = Convert.ToInt32(Math.Round(Convert.ToDouble(r_temporal) / 2, 1));
                        }
                        else if (r_temporal == 1)
                            r_temporal = 0;
                        else
                        {
                            r_temporal = -1;
                            while (PDs_alfa_siguiente.Count > 0)
                            {
                                if (PDs_alfa_siguiente[0] != "1000000000")
                                    PDs_objeto.Add(PDs_alfa_siguiente[0].ToString());
                                PDs_alfa_siguiente.RemoveAt(0);
                            }

                        }
                        primer_alfa = false;
                    } while (r_temporal != -1 && PDs_alfa_siguiente.Count != 0);
                } while (cambiar_resolucion && PDs_alfa_siguiente.Count > 0 && alfa > 1);
                //Eliminate repeated BPs to know the total number of them.
                PDs_objeto = contorno.LimpiarDPs(PDs_objeto);
                PDs_ordenados = contorno.Ordenar_PDs_2(PDs_objeto, coordenadas_contorno_ordenadas_original);

                megaMatriz = null;
                posiciones_almacenadas.Clear();
                posiciones_ant_act.Clear();
                repetir_penultimo = true;
                int contador_eliminados = 0;

                do
                {
                    ISES = calcular_Error.Obtener_Error(PDs_ordenados, coordenadas_contorno_ordenadas_original, 0, repetir_penultimo);
                    if (list_ISE_Elimination.Count == 0)
                    {
                        list_ISE_Elimination.Add(ISES[ISES.Count - 1]);
                        list_n_DPs_Elimination.Add(PDs_ordenados.Count / 3);
                    }
                    list_ISE_Rearrangement.Add(ISES[ISES.Count - 1]);
                    list_n_DPs_Rearrangement.Add(PDs_ordenados.Count / 3);

                    Pintar_PDs.getDP(PDs_ordenados, new Bitmap(PDResultante.Image)).Save(filepath + "\\Elimination" + list_ISE_Elimination.Count + ".jpeg");
                    //Use of our novel method for the elimination of BPs.
                    PDs_ordenados = quitar_BPs.Eliminar_BPs_version2(PDs_ordenados, coordenadas_contorno_ordenadas_original, ISES, T);

                    ISES = calcular_Error.Obtener_Error(PDs_ordenados, coordenadas_contorno_ordenadas_original, 0, repetir_penultimo);

                    list_ISE_Elimination.Add(ISES[ISES.Count - 1]);
                    list_n_DPs_Elimination.Add(PDs_ordenados.Count / 3);

                    while (PDs_ordenados_ant.Count > 0)
                        PDs_ordenados_ant.RemoveAt(0);
                    for (int ii = 0; ii < PDs_ordenados.Count; ii++)
                    {
                        PDs_ordenados_ant.Add(PDs_ordenados[ii]);
                    }
                    //Create 5 graphs to use the Dijkstra algorithm.
                    distancias_ises = quitar_BPs.Mejor_acomodo_BPS(PDs_ordenados, coordenadas_contorno_ordenadas_original, T, num_vecinos);

                    //Use of Dijkstra to get the best fit of current BPs.
                    PDs_ordenados = reOrdenamientoBPS.NuevosBPs(distancias_ises, PDs_ordenados, coordenadas_contorno_ordenadas_original, num_vecinos);


                    //Storage of the rearrangement information.
                    Pintar_PDs.getDP(PDs_ordenados, new Bitmap(PDResultante.Image)).Save(filepath + "\\Rearrangement" + list_ISE_Rearrangement.Count + ".jpeg");

                    //Comparison of Sets of BPs of the previous and current iteration to know if there was any change, if not, end the loop.
                    continuar = Comparar_Listas_DPs(PDs_ordenados, PDs_ordenados_ant);
                    contador_eliminados++;
                } while (continuar);

                //Updating of errors made between each pair of BPs.
                ISES = calcular_Error.Obtener_Error(PDs_ordenados, coordenadas_contorno_ordenadas_original, 0, true);

                //----------------------------------------ISE ----------------------------------------------------------------------------

                ISE = Math.Round(ISES[ISES.Count - 1], 4);
                textBox5.Text = ISE.ToString();

                //----------------------------------------n ----------------------------------------------------------------------------

                n_1 = coordenadas_contorno_ordenadas_original.Count / 2;
                nDP = ((PDs_ordenados.Count) / 3);
                //----------------------------------------CR ----------------------------------------------------------------------------

                CR = Math.Round(n_1 / nDP, 4);

                textBox6.Text = n_1.ToString();

                //----------------------------------------FOM ---------------------------------------------------------------------------
                FOM = Math.Round(n_1 / (ISE * nDP), 4);
                textBox8.Text = FOM.ToString();

                //----------------------------------------WE ---------------------------------------------------------------------------
                WE = Math.Round(ISE / CR, 4);
                textBox7.Text = WE.ToString();

                //----------------------------------------WE2 ---------------------------------------------------------------------------
                WE2 = Math.Round(ISE / Math.Pow(CR, 2), 4);
                textBox9.Text = WE2.ToString();

                textBox1.Text = nDP.ToString();


                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                labelT.Visible = true;
                label13.Visible = true;
                textBox1.Visible = true;
                textBox5.Visible = true;
                textBox6.Visible = true;
                textBox7.Visible = true;
                textBox8.Visible = true;
                textBox9.Visible = true;

                Clean.Visible = true;
                Restart.Visible = true;
                label2.Text = "Result";
                It_is_file = false;

                //----------------------------------------------------------------------------------------------------------------------------




                PDResultante.Image = Pintar_PDs.Pintar_lineas(PDResultante, PDs_ordenados);
                PDResultante.Image = Pintar_PDs.getDP(PDs_ordenados, new Bitmap(PDResultante.Image));

                button2.Visible = true;
                //Storage of the generated polygon, its BPs and the original boundary on the hard disk.
                PDResultante.Image.Save(filepath + "\\Resultante_PDs" + nombre + ".jpeg");

                //Show the method information on the screen.
                this.Hide();
                Form_BPs_Detection bPs_Detection = new Form_BPs_Detection();
                bPs_Detection.Show(this);
                bPs_Detection.Mostrar_informacion(filepath,list_ISE, list_n_DPs, r, list_ISE_Elimination, list_n_DPs_Elimination, list_ISE_Rearrangement, list_n_DPs_Rearrangement);                
            }
            else
            {
                string message = "Assign a value to T to continue";
                string caption = "Error Detected in Input";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    // Closes the parent form.
                    this.Close();
                }

            }
        }
        public int[,] Restablecer(int[,] Matriz)
        {
            for (int i = 0; i < Matriz.GetLength(0); i++)
            {
                for (int j = 0; j < Matriz.GetLength(1); j++)
                {
                    if (Matriz[i, j] != 0)
                        Matriz[i, j] = 1;
                }
            }
            return Matriz;
        }

        public bool Comparar_Listas_DPs(List<int> PDs_ordenados, List<int> PDs_ordenados_ant)
        {
            bool continuar = true, encontre_diferente_DP = false;
            if (PDs_ordenados.Count == PDs_ordenados_ant.Count)
            {
                for (int i = 0; i < PDs_ordenados.Count; i += 3)
                {
                    if (PDs_ordenados_ant[i] != PDs_ordenados[i] || PDs_ordenados_ant[i + 1] != PDs_ordenados[i + 1] || PDs_ordenados_ant[i + 2] != PDs_ordenados[i + 2])
                    {
                        encontre_diferente_DP = true;
                        break;
                    }
                }
                if (!encontre_diferente_DP)
                    continuar = false;
            }
            return continuar;
        }
        private void textBoxT_MouseHover(object sender, EventArgs e)
        {
            ttmensaje.SetToolTip(textBoxT, "T is the tolerable error, that share it to the errors committed by each pair of break points.");
            ttmensaje.ToolTipTitle = "Assing T";
            ttmensaje.ToolTipIcon = ToolTipIcon.Info;
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox4.Text = textBox4.Text.Remove(textBox4.Text.Length - 1);
            }
            else
            {
                button1.Visible = true;
            }
        }

        private void Clean_Click(object sender, EventArgs e)
        {
            //Hide textboxes
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox10.Visible = false;
            textBox9.Visible = false;
            textBox8.Visible = false;


            //Clean textbox
            textBoxT.Clear();

            //Hide labels
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label7.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label8.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label11.Visible = false;
            label12.Visible = false;

            //Hide buttons
            Clean.Visible = false;
            Restart.Visible = false;
            button2.Visible = false;

            Cargar_Imagen_PDResultante();
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        public void Cargar_Imagen_PDResultante()
        {
            Contorno contorno = new Contorno();

            BLProcesador = new Procesamiento((Bitmap)PDprincipal.Image);
            if (!It_is_file)
            {
                Moriginal = BLProcesador.DescomponerRGB();
                Moriginal = BLProcesador.Convertir(Moriginal);
                Moriginal = contorno.Obtener_Contorno(Moriginal, 1);
            }

            for (int i = 0; i < Moriginal.GetLength(0); i++)
                for (int j = 0; j < Moriginal.GetLength(1); j++)
                    if (Moriginal[i, j] != 0)
                        Moriginal[i, j] = 1;

            //-------------------------------------Convert matrix to image---------------------------------------------------------- 
            Convertir_a_Imagen convertir_A_Imagen = new Convertir_a_Imagen();
            Color[][] Imagen_contorno;

            if (!It_is_file)
                Imagen_contorno = convertir_A_Imagen.Convertir_inverso(Moriginal);
            else
                Imagen_contorno = convertir_A_Imagen.Convertir_inverso(Moriginal);

            PDResultante.Image = convertir_A_Imagen.GenerarImagen(Imagen_contorno);
            Imagen_contorno = null;

            PDResultante.Visible = true;
            label2.Visible = true;

            label6.Visible = true;
            button1.Visible = true;
            textBoxT.Visible = true;
            labelT.Visible = true;
            textBoxT.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox10.Visible = true;
            string[] substrings;
            substrings = cadenaAF8.Split(' ');
            textBox10.Text = substrings[substrings.Length - 2];
            button2.Enabled = true;
        }
    }
}

