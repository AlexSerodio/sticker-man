using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace sticker_man
{
    class SrPalito : Desenho, Objeto
    {

        private Ponto4D raiz;
        private Dictionary<TipoDeMembros, Membro> membros;
        private BoundBox boundBox= new BoundBox();
        private Transformacao4D transformacao;

        public Ponto4D Raiz { get => raiz; set => raiz = value; }

        public SrPalito(Ponto4D raiz = null)
        {
            this.raiz = (raiz != null) ? raiz : new Ponto4D();
            membros = new Dictionary<TipoDeMembros, Membro>();
            transformacao = new Transformacao4D();

            ConstruirCorpo(raiz);
            DefinirBoundBox();
        }

        private void ConstruirCorpo(Ponto4D raiz)
        {
            Ponto4D topoTronco = new Ponto4D(raiz.X, raiz.Y+130);
            Ponto4D origemBraco = new Ponto4D(raiz.X, raiz.Y+100);

            membros[TipoDeMembros.TRONCO] = new Tronco(raiz, topoTronco);
            membros[TipoDeMembros.PERNA_ESQUERDA] = new Perna(raiz, new Ponto4D(raiz.X-80, raiz.Y-100));
            membros[TipoDeMembros.PERNA_DIREITA] = new Perna(raiz, new Ponto4D(raiz.X+80, raiz.Y-100));
            membros[TipoDeMembros.BRACO_ESQUERDO] = new Perna(origemBraco, new Ponto4D(raiz.X-80, raiz.Y));
            membros[TipoDeMembros.BRACO_DIREITO] = new Perna(origemBraco, new Ponto4D(raiz.X+80, raiz.Y));
            membros[TipoDeMembros.CABECA] = new Cabeca(topoTronco, 50);
        }

        private void DefinirBoundBox()
        {
            boundBox.AtribuirBBox(raiz);
            boundBox.AtualizarBBox(membros[TipoDeMembros.TRONCO].GetFinal());
            boundBox.AtualizarBBox(membros[TipoDeMembros.TRONCO].GetOrigem());
            boundBox.AtualizarBBox(membros[TipoDeMembros.PERNA_ESQUERDA].GetFinal());
            boundBox.AtualizarBBox(membros[TipoDeMembros.PERNA_ESQUERDA].GetOrigem());
            boundBox.AtualizarBBox(membros[TipoDeMembros.PERNA_DIREITA].GetFinal());
            boundBox.AtualizarBBox(membros[TipoDeMembros.PERNA_DIREITA].GetOrigem());
            boundBox.AtualizarBBox(membros[TipoDeMembros.BRACO_ESQUERDO].GetFinal());
            boundBox.AtualizarBBox(membros[TipoDeMembros.BRACO_ESQUERDO].GetOrigem());
            boundBox.AtualizarBBox(membros[TipoDeMembros.BRACO_DIREITO].GetFinal());
            boundBox.AtualizarBBox(membros[TipoDeMembros.BRACO_DIREITO].GetOrigem());
            boundBox.AtualizarBBox(membros[TipoDeMembros.CABECA].GetFinal());
        }

        /// <summary>
        /// Altera a translação do polígono e todos os seus filhos com base nos valores especificados.
        /// </summary>
        /// <param name="tx">Nova posição X.</param>
        /// <param name="ty">Nova posição Y.</param>
        /// <param name="tz">Nova posição Z (0 por padrão).</param>
        public void Mover(double tx, double ty, double tz = 0)
        {
            Transformacao4D novaTransformacao = new Transformacao4D();
            novaTransformacao.AtribuirTranslacao(tx, ty, tz);

            transformacao = novaTransformacao.TransformMatrix(transformacao);
            boundBox.AtualizarBBox(GetPontos());
        }

        public void Desenhar()
        {
            GL.PushMatrix();
            GL.MultMatrix(transformacao.GetData());

            foreach(var membro in membros)
                membro.Value.Desenhar();

            boundBox.DesenharBBox();
            
            GL.PopMatrix();
        }

        public List<Ponto4D> GetPontos()
        {
            List<Ponto4D> points = new List<Ponto4D>();
            foreach(var membro in membros)
                points.AddRange(membro.Value.GetPontos());

            return points;
        }

    }

}