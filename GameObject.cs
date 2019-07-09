using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace stick_man
{
    public abstract class GameObject
    {

        private string name;
        private Tag tag;
        private Color color;

        private BoundBox boundBox;
        private Transformacao4D transform;
        private List<Ponto4D> vertices;
        private PrimitiveType primitive;

        public GameObject(Tag tag = Tag.UNTTAGED) {
            transform = new Transformacao4D();
            boundBox = new BoundBox();
            primitive = PrimitiveType.Lines;
            this.vertices = new List<Ponto4D>();
        }

        public GameObject(List<Ponto4D> vertices, Tag tag = Tag.UNTTAGED) {
            transform = new Transformacao4D();
            boundBox = new BoundBox();
            primitive = PrimitiveType.Lines;
            SetVertices(vertices);
        }

        public void SetColor(Color color) => this.color = color;
        public Color GetColor() => this.color != null ? this.color : Color.Black;

        public List<Ponto4D> GetVertices() => this.vertices;

        public void SetVertices(List<Ponto4D> vertices) {
            this.vertices = new List<Ponto4D>();
            this.vertices.AddRange(vertices);
            boundBox.AtualizarBBox(this.vertices);
        }

        public void SetPrimitive(PrimitiveType primitive) => this.primitive = primitive;

        public void AddVertice(Ponto4D newVertice)
        {
            List<Ponto4D> vertices = GetVertices();
            vertices.Add(newVertice);
            SetVertices(vertices);
        }

        public void FinishObject()
        {
            AddVertice(vertices[0]);  
            SetPrimitive(PrimitiveType.Polygon);
        }

        public void UpdateVertice(Ponto4D vertice, int position)
        {
            List<Ponto4D> vertices = GetVertices();
            vertices[position] = vertice;
            SetVertices(vertices);
        }

        public void SetTag(Tag tag) => this.tag = tag;

        public Tag GetTag() => tag;

        public BoundBox GetBoundBox() => this.boundBox;

        public Transformacao4D GetTransform() => transform;

        public virtual void Translate(double tx, double ty, double tz)
        {
            Transformacao4D newTransform = new Transformacao4D();
            newTransform.AtribuirTranslacao(tx, ty, tz);

            transform = newTransform.TransformMatrix(transform);
            boundBox.AtualizarBBox(this.vertices, transform);
        }

        public virtual void Scale(double factor)
        {
            Ponto4D center = boundBox.ObterCentro;

            Transformacao4D globalTransform = new Transformacao4D();
            Transformacao4D translationTransform = new Transformacao4D();
            Transformacao4D translationTransformInverse = new Transformacao4D();
            Transformacao4D scaleTransform = new Transformacao4D();

            translationTransform.AtribuirTranslacao(center.X, center.Y, center.Z);
            globalTransform = translationTransform.TransformMatrix(globalTransform);
            
            scaleTransform.AtribuirEscala(factor, 1.0, 1.0);
            globalTransform = scaleTransform.TransformMatrix(globalTransform);

            center.InvertSignals();
            translationTransformInverse.AtribuirTranslacao(center.X, center.Y, center.Z);
            globalTransform = translationTransformInverse.TransformMatrix(globalTransform);

            transform = transform.TransformMatrix(globalTransform);
        }

        public virtual void Rotate(double factor)
        {
            Ponto4D center = boundBox.ObterCentro;

            Transformacao4D globalTransform = new Transformacao4D();
            Transformacao4D translationTransform = new Transformacao4D();
            Transformacao4D translationTransformInverse = new Transformacao4D();
            Transformacao4D rotationTransform = new Transformacao4D();

            translationTransform.AtribuirTranslacao(center.X, center.Y, center.Z);
            globalTransform = translationTransform.TransformMatrix(globalTransform);

            rotationTransform.AtribuirRotacaoZ(Transformacao4D.DEG_TO_RAD * factor);
            globalTransform = rotationTransform.TransformMatrix(globalTransform);

            center.InvertSignals();
            translationTransformInverse.AtribuirTranslacao(center.X, center.Y, center.Z);
            globalTransform = translationTransformInverse.TransformMatrix(globalTransform);

            transform = transform.TransformMatrix(globalTransform);
        }

        public virtual void Draw()
        {
            GL.PushMatrix();
            GL.MultMatrix(transform.GetData());
            GL.Color3(GetColor());
            GL.LineWidth(3);
            GL.Begin(primitive);
                foreach(Ponto4D vertex in this.vertices)
                    GL.Vertex3(vertex.X, vertex.Y, vertex.Z);
            GL.End();

            GL.PopMatrix();

            boundBox.DesenharBBox();
        }

        public bool IsColliding(GameObject other) {
            return boundBox.IsColliding(this, other);
        }
    }
}