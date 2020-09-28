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
    public partial class Form_BPs_Detection : Form
    {
        private ImageList images_Elimination = new ImageList();
        private List<double> ISE_Elimination = new List<double>();
        private List<int> n_DPs_Elimination = new List<int>();
        private ImageList images_Rearrangement = new ImageList();
        private List<double> ISE_Rearrangement = new List<double>();
        private List<int> n_DPs_Rearrangement = new List<int>();
        private string path;
        public Form_BPs_Detection()
        {
            InitializeComponent();
        }
        public void Mostrar_informacion(string filepath, List<double> ISE, List<int> n_DPs, int r, List<double> list_ISE_Elimination, List<int> list_n_DPs_Elimination, List<double> list_ISE_Rearrangement, List<int> list_n_DPs_Rearrangement)
        {
            path = filepath;
            //images_Elimination = BPs_Elimination;
            ISE_Elimination = list_ISE_Elimination;
            n_DPs_Elimination = list_n_DPs_Elimination;
            //images_Rearrangement = BPs_Rearrangement;
            ISE_Rearrangement = list_ISE_Rearrangement;
            n_DPs_Rearrangement = list_n_DPs_Rearrangement;
            int i = 1;
            bool entre = false;
            Bitmap bitmap = new Bitmap(filepath + "\\Detection" + 0 + ".jpeg");
            PB.Image = bitmap;
            textBoxISE.Text = ISE[0].ToString();
            textBoxn_DPs.Text = n_DPs[0].ToString();
            textBoxr.Text = r.ToString();

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
                    while (i < ISE.Count)
                    {
                        now = DateTime.Now;
                        if ((now - t).TotalSeconds >= 2)
                        {
                            if (r > 1)
                            {
                                r = Convert.ToInt32(Math.Round(Convert.ToDouble(r) / 2, 1));
                            }
                            else if (r == 1)
                                r = 0;
                            bitmap = new Bitmap(filepath + "\\Detection" + i + ".jpeg");
                            PB.Image = bitmap;
                            textBoxISE.Text = ISE[i].ToString();
                            textBoxn_DPs.Text = n_DPs[i].ToString();
                            textBoxr.Text = r.ToString();
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

        public void buttonRegresar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form_BPs_Elimination form_BPs_Elimination = new Form_BPs_Elimination();
            form_BPs_Elimination.Show(this.Owner);
            form_BPs_Elimination.Mostrar_informacion(path, ISE_Elimination, n_DPs_Elimination,ISE_Rearrangement,n_DPs_Rearrangement);

            this.Close();

        }
    }
}
