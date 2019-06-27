using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace sticker_man
{
    public class GeometricObject : GameObject
    {

        public GeometricObject(List<Ponto4D> vertices) : base(vertices) {
            
        }

    }
}