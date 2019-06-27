using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace sticker_man
{
    public abstract class GameObject
    {

        private string name;
        private Tag tag;

        private BoundBox boundBox;
        private Transformacao4D transform;
        private List<Ponto4D> vertices;

        public GameObject() {
            transform = new Transformacao4D();
            boundBox = new BoundBox();
            this.vertices = new List<Ponto4D>();
        }

        public GameObject(List<Ponto4D> vertices) {
            transform = new Transformacao4D();
            boundBox = new BoundBox();
            this.vertices = vertices;
        }

        public List<Ponto4D> GetVertices() => vertices;

        public void SetVertices(List<Ponto4D> vertices) {
            this.vertices = vertices;
            boundBox.AtualizarBBox(vertices);
        }

        public virtual void Translate(double tx, double ty, double tz)
        {
            Transformacao4D newTransform = new Transformacao4D();
            newTransform.AtribuirTranslacao(tx, ty, tz);

            transform = newTransform.TransformMatrix(transform);
            boundBox.AtualizarBBox(GetVertices());
        }

        public virtual void Scale(double factor)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Rotate(double factor)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Draw()
        {
            GL.PushMatrix();
            GL.MultMatrix(transform.GetData());

            GL.Color3(Color.Black);
            GL.LineWidth(3);
            GL.Begin(PrimitiveType.Lines);
                foreach(Ponto4D vertex in vertices)
                    GL.Vertex2(vertex.X, vertex.Y);
            GL.End();

            boundBox.DesenharBBox();
            
            GL.PopMatrix();
        }
    }
}