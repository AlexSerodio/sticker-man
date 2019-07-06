using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Drawing;

namespace stick_man
{
    public class BoundBox {
        
        private double menorX, menorY, menorZ, maiorX, maiorY, maiorZ;

        private Ponto4D centro = new Ponto4D();
        public BoundBox(double menorX=0, double menorY=0, double menorZ=0, double maiorX=0, double maiorY=0, double maiorZ=0) {
            this.menorX = menorX; 
            this.menorY = menorY; 
            this.menorZ = menorZ;
            this.maiorX = maiorX; 
            this.maiorY = maiorY; 
            this.maiorZ = maiorZ;
        }

        public double ObterMenorX => menorX;
        public double ObterMenorY => menorY;
        public double ObterMenorZ => menorZ;
        public double ObterMaiorX => maiorX;
        public double ObterMaiorY => maiorY;
        public double ObterMaiorZ => maiorZ;
        public Ponto4D ObterCentro => centro;

        public void AtribuirBBox(Ponto4D pto) {
            this.menorX = pto.X;
            this.menorY = pto.Y;
            this.menorZ = pto.Z;
            this.maiorX = pto.X;
            this.maiorY = pto.Y;
            this.maiorZ = pto.Z;
            
            ProcessarCentroBBox();
        }
            
        public void AtualizarBBox(Ponto4D pto) {
            AtualizarBBox(pto.X, pto.Y, pto.Z);
        }

        public void AtualizarBBox(double x, double y, double z) {
            if (x < menorX)
                menorX = x;
            else if (x > maiorX)
                maiorX = x;
            
            if (y < menorY)
                menorY = y;
            else  if (y > maiorY)
                maiorY = y;

            ProcessarCentroBBox();
        }

        public void AtualizarBBox(List<Ponto4D> pontos, Transformacao4D transform = null) {
            AtribuirBBox(pontos[0]);

            foreach(Ponto4D ponto in pontos)
                AtualizarBBox(ponto.X, ponto.Y, ponto.Z);

            if(transform != null)
                AtualizarPontos(transform);

            ProcessarCentroBBox();
        }

        public void AtualizarPontos(Transformacao4D transform) {
            maiorX = transform.TransformPoint(new Ponto4D(maiorX, 0, 0)).X;
            menorX = transform.TransformPoint(new Ponto4D(menorX, 0, 0)).X;
            menorY = transform.TransformPoint(new Ponto4D(0, menorY, 0)).Y;
            maiorY = transform.TransformPoint(new Ponto4D(0, maiorY, 0)).Y;
        }

        public void ProcessarCentroBBox() {
            centro.X = (maiorX + menorX) / 2;
            centro.Y = (maiorY + menorY) / 2;
            centro.Z = (maiorZ + menorZ) / 2;
        }

        public bool IsColliding(GameObject collided, GameObject other)
        {
            foreach (Ponto4D vertice in other.GetVertices()) {
                if(IsColliding(other.GetTransform().TransformPoint(vertice), collided))
                    return true;
            }
            return false;
        }

        public bool IsColliding(Ponto4D ponto, GameObject collided)
        {
            if(ponto.X < maiorX) {
                if(ponto.X > menorX) {
                    if(ponto.Y < maiorY) {
                        if(ponto.Y > menorY) {
                            if(collided is GeometricObject)
                                return ScanLine(ponto, collided.GetVertices()) != 0;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private int ScanLine(Ponto4D pontoClicado, List<Ponto4D> vertices)
        {

            int paridade = 0;
            double ti = 0;
            for(int i = 0, j = 1; i < vertices.Count; i++, j++) {

                if(j > vertices.Count - 1)
                    j = 0;

                ti = (pontoClicado.Y - vertices[i].Y) / (vertices[j].Y - vertices[i].Y);

                if(ti > 0 && ti < 1) {
                    double x = vertices[i].X + (vertices[j].X - vertices[i].X) * ti;
                    if(x > pontoClicado.X)
                        paridade++;
                }
            }
            
            return paridade % 2;
        }

        public void DesenharBBox() {
            GL.Color3(Color.Yellow);

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

        public override string ToString() => "{ Xmin: " + menorX + " | Xmax: " + maiorX 
                                            + " }\n{ Ymin: " + menorY + " | Ymax: " + maiorY + " }";

    }
}