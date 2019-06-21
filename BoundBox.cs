using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace sticker_man
{
    internal class BoundBox {
        
        private double menorX, menorY, menorZ, maiorX, maiorY, maiorZ;
        private Ponto4D centro = new Ponto4D();

        public double MenorX { get => menorX; }
        public double MenorY { get => menorY; }
        public double MenorZ { get => menorZ; }
        public double MaiorX { get => maiorX; }
        public double MaiorY { get => maiorY; }
        public double MaiorZ { get => maiorZ; }
        public Ponto4D Centro { get => centro; }

        public BoundBox(double menorX=0, double menorY=0, double menorZ=0, double maiorX=0, double maiorY=0, double maiorZ=0) 
        {
            this.menorX = menorX; 
            this.menorY = menorY; 
            this.menorZ = menorZ; 
            this.maiorX = maiorX; 
            this.maiorY = maiorY; 
            this.maiorZ = maiorZ;
        }

        public void AtribuirBBox(Ponto4D pto) 
        {
            this.menorX = pto.X; this.menorY = pto.Y; this.menorZ = pto.Z;
            this.maiorX = pto.X; this.maiorY = pto.Y; this.maiorZ = pto.Z;
            ProcessarCentroBBox();
        }
            
        public void AtualizarBBox(Ponto4D pto) 
        {
            AtualizarBBox(pto.X, pto.Y, pto.Z);
        }

        public void AtualizarBBox(double x, double y, double z) 
        {
            if (x < menorX) {
                menorX = x;
            } else {
                if (x > maiorX) 
                    maiorX = x;
            }
            if (y < menorY) {
                menorY = y;
            } else {
                if (y > maiorY) 
                    maiorY = y;
            }
            if (z < menorZ) {
                menorZ = z;
            } else {
                if (z > maiorZ) 
                    maiorZ = z;
            }
        }
        
        public void ProcessarCentroBBox() 
        {
            centro.X = (maiorX + menorX)/2;
            centro.Y = (maiorY + menorY)/2;
            centro.Z = (maiorZ + menorZ)/2;
        }

        public bool EstaDentro(Ponto4D ponto)
        {
            if(ponto.X < maiorX) {
                if(ponto.X > menorX) {
                    if(ponto.Y < maiorY) {
                        if(ponto.Y > menorY) {
                            if(ponto.Z < maiorZ) {
                                if(ponto.Z > menorZ)
                                    return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public void DesenharBBox() {
            GL.Color3(Color.Brown);

            GL.PointSize(5);
            GL.Begin(PrimitiveType.Points);
            GL.Vertex2(centro.X,centro.Y);
            GL.End();

            GL.LineWidth(1);
                GL.Begin(PrimitiveType.LineLoop);
                    GL.Vertex3(menorX, maiorY, menorZ);
                    GL.Vertex3(maiorX, maiorY, menorZ);
                    GL.Vertex3(maiorX, menorY, menorZ);
                    GL.Vertex3(menorX, menorY, menorZ);
            GL.End();
            GL.Begin(PrimitiveType.LineLoop);
                GL.Vertex3(menorX, menorY, menorZ);
                GL.Vertex3(menorX, menorY, maiorZ);
                GL.Vertex3(menorX, maiorY, maiorZ);
                GL.Vertex3(menorX, maiorY, menorZ);
            GL.End();
            GL.Begin(PrimitiveType.LineLoop);
                GL.Vertex3(maiorX, maiorY, maiorZ);
                GL.Vertex3(menorX, maiorY, maiorZ);
                GL.Vertex3(menorX, menorY, maiorZ);
                GL.Vertex3(maiorX, menorY, maiorZ);
            GL.End();
            GL.Begin(PrimitiveType.LineLoop);
                GL.Vertex3(maiorX, menorY, menorZ);
                GL.Vertex3(maiorX, maiorY, menorZ);
                GL.Vertex3(maiorX, maiorY, maiorZ);
                GL.Vertex3(maiorX, menorY, maiorZ);
            GL.End();
        }

    }
}