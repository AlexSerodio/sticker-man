using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace sticker_man
{
    public class Head : GameObject
    {
        
        private Ponto4D center;
        private double radius;

        public Head(Ponto4D center, double radius)
        {
            this.radius = radius;
            this.center = new Ponto4D(center.X, center.Y + radius);
            SetVertices(250);
        }

        public Ponto4D GetCenter() => center;
        public Ponto4D GetRadius() => new Ponto4D(center.X + radius, center.Y + radius);

        public void SetVertices(int vertQuantity) 
        {
            List<Ponto4D> vertices = new List<Ponto4D>();
            int angleStep = 360 / vertQuantity;
            for(int angle = 0; angle < 360; angle += angleStep)
                vertices.Add(center + Matematica.ptoCirculo(angle, radius));

            base.SetVertices(vertices);
        }
    }

}