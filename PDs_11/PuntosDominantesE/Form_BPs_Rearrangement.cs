using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntosDominantesE
{
    public partial class Form_BPs_Rearrangement : Form
    {
        string path;
        public Form_BPs_Rearrangement()
        {
            InitializeComponent();
        }
        public void Mostrar_informacion(string filepath, List<double> ISE, List<int> n_DPs, List<double> ISE_E, List<int> n_DPs_E)
        {
            path = filepath;
            int i = 1;
            bool entre = false;
            //Rearrangement
            Bitmap bitmap = new Bitmap(filepath + "\\Rearrangement" + i + ".jpeg");
            PB.Image = bitmap;
            textBoxISE.Text = ISE[0].ToString();
            textBoxn_DPs.Text = n_DPs[0].ToString();

            //Eliminacion
            Bitmap bitmap_E = new Bitmap(filepath + "\\Elimination" + i + ".jpeg");
            PB_E.Image = bitmap_E;
            textBoxISE_E.Text = ISE_E[0].ToString();
            textBoxn_DPs_E.Text = n_DPs_E[0].ToString();

            Application.DoEvents();
            bitmap.Dispose();
            bitmap_E.Dispose();

            DateTime now = DateTime.Now;

            DateTime t = DateTime.Now;
            while (!entre)
            {
                if ((now - t).TotalSeconds >= 2)
                {
                    entre = true;
                    t = now;
                    while (i < ISE.Count)
                    {
                        now = DateTime.Now;
                        if ((now - t).TotalSeconds >= 2)
                        {
                            //Rearrangement
                            bitmap = new Bitmap(filepath + "\\Rearrangement" + (i + 1) + ".jpeg");
                            PB.Image = bitmap;
                            textBoxISE.Text = ISE[i].ToString();
                            textBoxn_DPs.Text = n_DPs[i].ToString();

                            //Eliminacion
                            bitmap_E = new Bitmap(filepath + "\\Elimination" + (i + 1) + ".jpeg");
                            PB_E.Image = bitmap_E;
                            textBoxISE_E.Text = ISE_E[i].ToString();
                            textBoxn_DPs_E.Text = n_DPs_E[i].ToString();

                            t = now;
                            i++;
                        }
                        Application.DoEvents();
                        bitmap.Dispose();
                        bitmap_E.Dispose();

                    }
                }
                else
                    now = DateTime.Now;
            }

            buttonRegresar.Visible = true;
        }

        private void buttonRegresar_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
            int i = 0;

            while (System.IO.File.Exists(path + "\\Detection" + i + ".jpeg"))
            {
                System.IO.File.Delete(path + "\\Detection" + i + ".jpeg");
                i++;
            }
            i = 1;
            while (System.IO.File.Exists(path + "\\Elimination" + i + ".jpeg"))
            {
                System.IO.File.Delete(path + "\\Elimination" + i + ".jpeg"); 
                System.IO.File.Delete(path + "\\Rearrangement" + i + ".jpeg");

                i++;
            }
        }

    }
}
