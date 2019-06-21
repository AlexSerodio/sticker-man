using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace sticker_man
{
    class Tronco : Membro
    {
        
        private Ponto4D inicio;
        private Ponto4D fim;

        public Tronco(Ponto4D inicio, Ponto4D fim)
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
            GL.LineWidth(3);
            GL.Begin(PrimitiveType.Lines);
                GL.Vertex2(inicio.X, inicio.Y);
                GL.Vertex2(fim.X, fim.Y);
            GL.End();
        }

    }

}