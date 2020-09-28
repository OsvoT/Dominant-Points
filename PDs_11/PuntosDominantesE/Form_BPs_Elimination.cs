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
    public partial class Form_BPs_Elimination : Form
    {

        private List<double> ISE_Elimination = new List<double>();
        private List<int> n_DPs_Elimination = new List<int>();
        private List<double> ISE_Rearrangement = new List<double>();
        private List<int> n_DPs_Rearrangement = new List<int>();
        private string path;
        public Form_BPs_Elimination()
        {
            InitializeComponent();
        }
        public void Mostrar_informacion(string filepath, List<double> ISE, List<int> n_DPs, List<double> list_ISE_Rearrangement, List<int> list_n_DPs_Rearrangement)
        {
            path = filepath;
            ISE_Elimination = ISE;
            n_DPs_Elimination = n_DPs;
            ISE_Rearrangement = list_ISE_Rearrangement;
            n_DPs_Rearrangement = list_n_DPs_Rearrangement;
            int i = 1;
            bool entre = false;
            Bitmap bitmap = new Bitmap(filepath + "\\Elimination" + i + ".jpeg");

            PB.Image = bitmap;
            textBoxISE.Text = ISE[0].ToString();
            textBoxn_DPs.Text = n_DPs[0].ToString();

            Application.DoEvents();
            bitmap.Dispose();
            DateTime now = DateTime.Now;

            DateTime t = DateTime.Now;
            while (!entre)
            {
                if ((now - t).TotalSeconds >= 2)
                {
                    entre = true;
                    t = now;
                    while (i < ISE.Count - 1)
                    {
                        now = DateTime.Now;
                        if ((now - t).TotalSeconds >= 2)
                        {
                            bitmap = new Bitmap(filepath + "\\Elimination" + (i + 1) + ".jpeg");
                            PB.Image = bitmap;
                            textBoxISE.Text = ISE[i].ToString();
                            textBoxn_DPs.Text = n_DPs[i].ToString();
                            t = now;
                            i++;
                        }
                        Application.DoEvents();
                        bitmap.Dispose();

                    }
                }
                else
                    now = DateTime.Now;
            }

            buttonRegresar.Visible = true;
        }

        private void buttonRegresar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form_BPs_Rearrangement form_BPs_Rearrangement = new Form_BPs_Rearrangement();
            form_BPs_Rearrangement.Show(this.Owner);
            form_BPs_Rearrangement.Mostrar_informacion(path, ISE_Rearrangement, n_DPs_Rearrangement, ISE_Elimination, n_DPs_Elimination);

            this.Close();

        }
    }
}
