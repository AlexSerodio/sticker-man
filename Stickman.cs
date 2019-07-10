using System.Collections.Generic;
using System.Threading;
using OpenTK.Graphics.OpenGL;

namespace stick_man
{
    public class Stickman : GameObject
    {

        private Ponto4D root;
        public List<GameObject> limbs;
        private double sizeFactor = 0.2;
        private double speed = 2.0;
        private World world;
        private Animator animator;
        private Thread walkThread;

        private bool walking;

        public void SetWalking(bool value) => walking = value;
        public bool GetWalking() => walking;

        public Stickman() : base()
        {
            this.root = new Ponto4D();
            this.sizeFactor = 0.2;
            limbs = new List<GameObject>();
            animator = new Animator();

            base.SetTag(Tag.MAN);
            CreateBody(root);
            PrepareThread();
        }

        public Stickman(Ponto4D root, double sizeFactor, World world) : base()
        {
            this.root = root;
            this.sizeFactor = sizeFactor;
            this.world = world;
            limbs = new List<GameObject>();
            animator = new Animator();

            base.SetTag(Tag.MAN);
            CreateBody(root);
            PrepareThread();
        }

        private void PrepareThread() {
            walkThread = new Thread(Animation);
            walkThread.IsBackground = true;
            walkThread.Priority = ThreadPriority.BelowNormal;
            walkThread.Start();
        }

        private void Animation() {
            while(true) {
                if(walking)
                    Walk();
            }
        }

        public void SetSpeed(double speed) => this.speed = speed;
        public double GetSpeed() => this.speed;

        public Ponto4D GetRoot() => root;

        private void CreateBody(Ponto4D raiz)
        {
            Ponto4D topoTronco = new Ponto4D(raiz.X, raiz.Y+130) * sizeFactor;
            Ponto4D origemBraco = new Ponto4D(raiz.X, raiz.Y+100) * sizeFactor;

            Head head = new Head(topoTronco, 10);
            Limb belly = new Limb(raiz*sizeFactor, topoTronco, BodyPart.BELLY);

            Ponto4D leftKnee = new Ponto4D(raiz.X-80+30, raiz.Y-100+40)*sizeFactor;
            Limb leftUpperLeg = new Limb(raiz*sizeFactor, leftKnee, BodyPart.LEFT_UPPER_LEG);
            Limb leftLowerLeg = new Limb(leftKnee, new Ponto4D(raiz.X-80, raiz.Y-100)*sizeFactor, BodyPart.LEFT_LOWER_LEG);

            Ponto4D rightKnee = new Ponto4D(raiz.X+80-30, raiz.Y-100+40)*sizeFactor;
            Limb rightUpperLeg = new Limb(raiz*sizeFactor, rightKnee, BodyPart.RIGHT_UPPER_LEG);
            Limb rightLowerLeg = new Limb(rightKnee, new Ponto4D(raiz.X+80, raiz.Y-100)*sizeFactor, BodyPart.RIGHT_LOWER_LEG);

            Ponto4D leftElbow = new Ponto4D(raiz.X-80+30, raiz.Y+40)*sizeFactor;
            Limb leftUpperArm = new Limb(origemBraco, leftElbow, BodyPart.LEFT_UPPER_ARM);
            Limb leftLowerArm = new Limb(leftElbow, new Ponto4D(raiz.X-80, raiz.Y)*sizeFactor, BodyPart.LEFT_LOWER_ARM);
            
            Ponto4D rightElbow = new Ponto4D(raiz.X+80-30, raiz.Y+40)*sizeFactor;
            Limb rightUpperArm = new Limb(origemBraco, rightElbow, BodyPart.RIGHT_UPPER_ARM);
            Limb rightLowerArm = new Limb(rightElbow, new Ponto4D(raiz.X+80, raiz.Y)*sizeFactor, BodyPart.RIGHT_LOWER_ARM);

            limbs.Add(belly);

            limbs.Add(leftUpperLeg);
            limbs.Add(leftLowerLeg);
            limbs.Add(rightUpperLeg);
            limbs.Add(rightLowerLeg);

            limbs.Add(leftUpperArm);
            limbs.Add(leftLowerArm);
            limbs.Add(rightUpperArm);
            limbs.Add(rightLowerArm);

            limbs.Add(head);

            SetVertices();
        }

        public void Walk(int frameRate = 200) {
            animator.Walk_Front();
            Thread.Sleep(frameRate);

            animator.Walk_Normal();
            Thread.Sleep(frameRate);
        }

        private void SetVertices()
        {
            List<Ponto4D> vertices = new List<Ponto4D>();
            foreach(var limb in limbs)
                vertices.AddRange(limb.GetVertices());

            base.SetVertices(vertices);
        }

        public void MoveLeft() {
            SetWalking(true);
            base.Translate(-speed, 0, 0);

            if(this.Collided())
                base.Translate(speed, 0, 0);
        }

        public void MoveRight() {
            SetWalking(true);
            base.Translate(speed, 0, 0);

            if(this.Collided())
                base.Translate(-speed, 0, 0);
        }

        public void MoveUp() {
            SetWalking(true);
            base.Translate(0, speed, 0);

            if(this.Collided())
                base.Translate(0, -speed, 0);
        }

        public void MoveDown() {
            SetWalking(true);
            base.Translate(0, -speed, 0);

            if(this.Collided())
                base.Translate(0, speed, 0);
        }

        public override void Draw() {
            GL.PushMatrix();
            GL.MultMatrix(GetTransform().GetData()); 
            GL.Color3(GetColor());
            GL.LineWidth(3);
            GL.Begin(GetPrimitive());
                foreach(GameObject limb in limbs) {
                    if(limb is Limb) {
                        Ponto4D[] p = animator.Animate((Limb)limb);
                        GL.Vertex3(p[0].X, p[0].Y, p[0].Z);
                        GL.Vertex3(p[1].X, p[1].Y, p[1].Z);
                    } else if(limb is Head) {
                        foreach(Ponto4D vertex in limb.GetVertices())
                            GL.Vertex3(vertex.X, vertex.Y, vertex.Z);
                    }
                }
            GL.End();

            GL.PopMatrix();

            GetBoundBox().DesenharBBox();
        }

    }

}