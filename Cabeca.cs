using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace sticker_man
{
    class Cabeca : Membro
    {
        
        private Ponto4D centro;
        private double raio;

        public Cabeca(Ponto4D centro, double raio)
        {
            this.raio = raio;
            this.centro = new Ponto4D(centro.X, centro.Y + raio);
        }

        public double Raio { get => raio; set => raio = value; }
        public Ponto4D GetFinal() => new Ponto4D(centro.X + raio, centro.Y + raio);
        public Ponto4D GetOrigem() => centro;

        public void Desenhar()
        {
            Ponto4D point;
            int qtdPontos = 200;
            int intervaloAngulo = 360 / qtdPontos;

            GL.PointSize(3);
            GL.Color3(Color.Yellow);
            GL.Begin(PrimitiveType.Points);
                for(int angulo = 0; angulo < 360; angulo += intervaloAngulo) {
                    point = centro + Matematica.ptoCirculo(angulo, Raio);
                    GL.Vertex2(point.X, point.Y);
                }
            GL.End();
        }

        public void Mover(double tx, double ty, double tz)
        {
            throw new System.NotImplementedException();
        }

        public List<Ponto4D> GetPontos()
        {
            List<Ponto4D> points = new List<Ponto4D>();
            points.Add(new Ponto4D(centro.X + raio, centro.Y));
            points.Add(new Ponto4D(centro.X - raio, centro.Y));
            points.Add(new Ponto4D(centro.X, centro.Y + raio));
            points.Add(new Ponto4D(centro.X, centro.Y - raio));

            return points;
        }
    }

}