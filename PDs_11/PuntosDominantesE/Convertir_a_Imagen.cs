using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PuntosDominantesE
{
    public class Convertir_a_Imagen
    {
        public Color[][] Convertir_inverso(int[,] matriz_fucion)
        {
            Color[][] imagen = new Color[matriz_fucion.GetLength(0)][];
            for (int i = 0; i < matriz_fucion.GetLength(0); i++)
            {
                imagen[i] = new Color[matriz_fucion.GetLength(1)];
                for (int j = 0; j < matriz_fucion.GetLength(1); j++)
                {
                    if (matriz_fucion[i, j] == 1)
                    {
                        imagen[i][j] = new Color();
                        imagen[i][j] = Color.Black;
                    }
                    else
                    {
                        imagen[i][j] = new Color();
                        imagen[i][j] = Color.White;
                    }
                }
            }
            return imagen;
        }

        public Bitmap GenerarImagen(Color[][] original)
        {
            Bitmap myBitmap = new Bitmap(original.GetLength(0), original[0].GetLength(0));

            for (int Xcount = 0; Xcount < myBitmap.Width; Xcount++)
            {
                for (int Ycount = 0; Ycount < myBitmap.Height; Ycount++)
                {
                    myBitmap.SetPixel(Xcount, Ycount, original[Xcount][Ycount]);
                }
            }
            return myBitmap;
        }

        public Image Pintar_lineas(PictureBox image1PB, List<int> coordenadas_PDs)
        {
            Graphics grap_puntos = Graphics.FromImage(image1PB.Image);
            Pen Lapiz = new Pen(Color.Green, 1);
            int x_1 = 0;
            int y_1 = 0;
            int x_2 = 0;
            int y_2 = 0;

            for (int i = 0; i < coordenadas_PDs.Count - 3; i += 3)
            {
                x_1 = coordenadas_PDs[i];
                y_1 = coordenadas_PDs[i + 1];
                x_2 = coordenadas_PDs[i + 3];
                y_2 = coordenadas_PDs[i + 4];

                grap_puntos.DrawLine(Lapiz, x_1, y_1, x_2, y_2);
            }

            x_1 = coordenadas_PDs[coordenadas_PDs.Count - 3];
            y_1 = coordenadas_PDs[coordenadas_PDs.Count - 2];
            x_2 = coordenadas_PDs[0];
            y_2 = coordenadas_PDs[1];
            grap_puntos.DrawLine(Lapiz, x_1, y_1, x_2, y_2);       
            return image1PB.Image;
        }
        public Bitmap getDP(List<int> PuntosDominantes, Bitmap reference)
        {
            Bitmap nuevo = new Bitmap(reference);

            for (int i = 0; i < PuntosDominantes.Count; i += 3)
            {
                if (PuntosDominantes[i] != 1000000000)
                {
                    nuevo.SetPixel(PuntosDominantes[i], PuntosDominantes[i + 1], Color.Red);
                }
                else
                    i -= 2;
            }

            return nuevo;

        }
    }
}
