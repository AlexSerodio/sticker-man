using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace stick_man
{
    public class GeometricObject : GameObject
    {

        public GeometricObject() {
            base.SetPrimitive(PrimitiveType.LineStrip);
        }

        public GeometricObject(Ponto4D vertice) {
            base.SetPrimitive(PrimitiveType.LineStrip);
            base.AddVertice(vertice);
        }
        
        public GeometricObject(List<Ponto4D> vertices) : base(vertices) {
            base.SetPrimitive(PrimitiveType.LineStrip);
        }

    }
}