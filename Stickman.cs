using System;
using System.Collections.Generic;

namespace sticker_man
{
    public class Stickman : GameObject
    {

        private Ponto4D root;
        public List<GameObject> limbs;
        private double sizeFactor = 0.2;

        public Stickman(Ponto4D root = null)
        {
            this.root = (root != null) ? root : new Ponto4D();
            limbs = new List<GameObject>();

            base.SetTag(Tag.MAN);
            CreateBody(root);
        }

        public Ponto4D GetRoot() => root;

        private void CreateBody(Ponto4D raiz)
        {
            Ponto4D topoTronco = new Ponto4D(raiz.X, raiz.Y+130) * sizeFactor;
            Ponto4D origemBraco = new Ponto4D(raiz.X, raiz.Y+100) * sizeFactor;

            limbs.Add(new Limb(raiz*sizeFactor, topoTronco));
            limbs.Add(new Limb(raiz*sizeFactor, new Ponto4D(raiz.X-80, raiz.Y-100)*sizeFactor));
            limbs.Add(new Limb(raiz*sizeFactor, new Ponto4D(raiz.X+80, raiz.Y-100)*sizeFactor));
            limbs.Add(new Limb(origemBraco, new Ponto4D(raiz.X-80, raiz.Y)*sizeFactor));
            limbs.Add(new Limb(origemBraco, new Ponto4D(raiz.X+80, raiz.Y)*sizeFactor));
            limbs.Add(new Head(topoTronco, 10));

            SetVertices();
        }

        private void SetVertices()
        {
            List<Ponto4D> vertices = new List<Ponto4D>();
            foreach(var limb in limbs)
                vertices.AddRange(limb.GetVertices());

            base.SetVertices(vertices);
        }

        public bool Collided()
        {
            foreach(GameObject obj in World.objects) {
                if(obj.IsColliding(this))
                    return true;
            }

            return false;
        }

    }

}