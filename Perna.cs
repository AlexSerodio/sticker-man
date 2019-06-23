using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace sticker_man
{
    class Perna : Membro
    {
        
        public Ponto4D inicio;
        public Ponto4D fim;

        public Perna(Ponto4D inicio, Ponto4D fim)
        {
            this.inicio = inicio;
            this.fim = fim;
        }

        public Ponto4D Inicio { set => inicio = value; }
        public Ponto4D Fim { set => fim = value; }
        public Ponto4D GetFinal() => fim;
        public Ponto4D GetOrigem() => inicio;

        public void Desenhar()
        {
            GL.Color3(Color.Black);
            GL.PointSize(5);
            GL.Begin(PrimitiveType.Lines);
                GL.Vertex2(inicio.X, inicio.Y);
                GL.Vertex2(fim.X, fim.Y);
            GL.End();
        }

        public void Mover(double tx, double ty, double tz)
        {
            throw new System.NotImplementedException();
        }

        public List<Ponto4D> GetPontos()
        {
            List<Ponto4D> points = new List<Ponto4D>();
            points.Add(inicio);
            points.Add(fim);

            return points;
        }
    }

}