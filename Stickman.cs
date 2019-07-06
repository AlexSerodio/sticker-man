using System.Collections.Generic;

namespace stick_man
{
    public class Stickman : GameObject
    {

        private Ponto4D root;
        public List<GameObject> limbs;
        private double sizeFactor = 0.2;
        private double speed = 2.0;
        private World world;

        public Stickman()
        {
            this.root = new Ponto4D();
            this.sizeFactor = 0.2;
            limbs = new List<GameObject>();

            base.SetTag(Tag.MAN);
            CreateBody(root);
        }

        public Stickman(Ponto4D root, double sizeFactor, World world)
        {
            this.root = root;
            this.sizeFactor = sizeFactor;
            this.world = world;
            limbs = new List<GameObject>();

            base.SetTag(Tag.MAN);
            CreateBody(root);
        }

        public void SetSpeed(double speed) => this.speed = speed;
        public double GetSpeed() => this.speed;

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
            foreach(GameObject obj in world.GetObjects()) {
                if(obj.IsColliding(this))
                    return true;
            }

            return false;
        }

        public void MoveLeft() {
            base.Translate(-speed, 0, 0);

            if(this.Collided())
                base.Translate(speed, 0, 0);
        }

        public void MoveRight() {
            base.Translate(speed, 0, 0);

            if(this.Collided())
                base.Translate(-speed, 0, 0);
        }

        public void MoveUp() {
            base.Translate(0, speed, 0);

            if(this.Collided())
                base.Translate(0, -speed, 0);
        }

        public void MoveDown() {
            base.Translate(0, -speed, 0);

            if(this.Collided())
                base.Translate(0, speed, 0);
        }

    }

}