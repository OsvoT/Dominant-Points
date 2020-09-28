using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PuntosDominantesE
{
    class Procesamiento
    {
        Bitmap MapaFuente;

        int[,] R;
        int[,] G;
        int[,] B;

        //Valores de los colores
        //0xAARRGGBB 0xFF000000 0x00FF0000 0x0000FF00 0x000000FF

        int r = 0x00FF0000;
        int g = 0x0000FF00;
        int b = 0x000000FF;

        public Procesamiento(Bitmap MapaFuente)
        {
            this.MapaFuente = new Bitmap(MapaFuente);

        }
        //-------------------------------------------------------------------------------
        public int[,] DescomponerRGB()
        {
            R = new int[MapaFuente.Width, MapaFuente.Height];
            G = new int[MapaFuente.Width, MapaFuente.Height];
            B = new int[MapaFuente.Width, MapaFuente.Height];

            for (int i = 0; i < MapaFuente.Width; i++)
            {
                for (int j = 0; j < MapaFuente.Height; j++)
                {
                    int X = MapaFuente.GetPixel(i, j).ToArgb();
                    R[i, j] = (r & X) >> 16;
                    G[i, j] = (g & X) >> 8;
                    B[i, j] = (b & X);
                }
            }
            return B;
        }

        public int[,] Convertir(int[,] Pixeles)
        {
            int[,] Matriz_limpia = new int[Pixeles.GetLength(0) + 2, Pixeles.GetLength(1) + 2];

            for (int i = 0; i < Pixeles.GetLength(0); i++)
            {
                for (int j = 0; j < Pixeles.GetLength(1); j++)
                {
                    if (Pixeles[i, j] >= 128)
                    {
                        Matriz_limpia[i + 1, j + 1] = 0;
                    }
                    else
                    {
                        Matriz_limpia[i + 1, j + 1] = 1;
                    }
                }
            }
            return Matriz_limpia;
        }
    }
}