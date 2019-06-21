using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace sticker_man
{
    class Poligono
    {
        private List<Ponto4D> pontos;
        private PrimitiveType primitiva;
        private Color cor;

        public Poligono()
        {
            primitiva = PrimitiveType.LineStrip;
            pontos = new List<Ponto4D>();
            Cor = Color.Red;
        }

        public PrimitiveType Primitiva { get => primitiva; }
        public List<Ponto4D> Pontos { get => pontos; }
        public Color Cor { get => cor; set => cor = value; }

        public void FecharPoligono()
        {
            primitiva = PrimitiveType.LineLoop;
        }

        public void AdicionarPonto(Ponto4D novoPonto)
        {
            pontos.Add(novoPonto);
        }

        public void RemoverPonto(Ponto4D ponto)
        {
            pontos.Remove(ponto);
        }

        public void Desenhar()
        {
            GL.Color3(Color.Black);
            GL.PointSize(5);
            GL.Begin(PrimitiveType.Points);
                foreach(Ponto4D ponto in pontos)
                    GL.Vertex2(ponto.X, ponto.Y);
            GL.End();

            GL.Color3(Cor);
            GL.LineWidth(3);
            GL.Begin(primitiva);
                foreach(Ponto4D ponto in pontos)
                    GL.Vertex2(ponto.X, ponto.Y);
            GL.End();
        }

    }
}
