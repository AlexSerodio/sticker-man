using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace stick_man
{
    public class World
    {

        private Ground ground;
        private Camera camera;

        private bool creatingPlataform = false;
        private bool creatingRampRight = false;
        private bool creatingRampLeft = false;
        private bool creatingBox = false;
        
        private GameObject selectedObject;
        private bool scaling = false;

        public GameObject GetSelectedObject() => selectedObject;

        public void SetSelectedObject(GameObject obj) {
            if(obj != null)
                obj.SetColor(Color.Green);
            if(selectedObject != null)
                selectedObject.SetColor(Color.YellowGreen);

            selectedObject = obj;
        }

        public bool HasSelectedObject() => this.selectedObject != null;

        public World(int width, int height)
        {
            camera = new Camera(0, width, 0, height, -1, 1);

            CraeteGround(width/2, height/2);
            CraeteGround(width/2, -height/2);
        }

        public void UpdateCamera() => camera.Update();

        public GameObject SelectObject(Ponto4D pontoClicado)
        {
            foreach(GameObject obj in Global.objects) {
                if(obj.GetBoundBox().IsColliding(pontoClicado, obj))
                    return obj;
            }

            return null;
        }

        public void HandleObjects()
        {
            foreach(GameObject obj in Global.objects) {
                obj.Draw();
                if(obj.HasGravity())
                    obj.Gravity();
            }
        }

        public bool IsCreatingPlataform() => creatingPlataform;
        public void SetCreatingPlataform(bool active) => creatingPlataform = active;

        public bool IsCreatingRampRight() => creatingRampRight;
        public void SetCreatingRampRight(bool active) => creatingRampRight = active;

        public bool IsCreatingRampLeft() => creatingRampLeft;
        public void SetCreatingRampLeft(bool active) => creatingRampLeft = active;

        public bool IsCreatingBox() => creatingBox;
        public void SetCreatingBox(bool active) => creatingBox = active;

        public void AddObject(GameObject newObject) => Global.objects.Add(newObject);
        public GameObject GetObject(int index) => Global.objects[index];
        public GameObject GetLastObject() => Global.objects[Global.objects.Count-1];
        public List<GameObject> GetObjects() => Global.objects;

        public void SwitchScalingMode() => this.scaling = !this.scaling;
        public bool IsScalingModeOn() => this.scaling;

        public void SetPrimitive(PrimitiveType primitive) 
        {
            foreach(GameObject obj in Global.objects)
                obj.SetPrimitive(primitive);
        }

        private void CraeteGround(int width, int height)
        {
            Ground ground = new Ground(new List<Ponto4D> { 
                new Ponto4D(width+100, -height+50),
                new Ponto4D(-width-100, -height+50),
                new Ponto4D(-width-100, -height),
                new Ponto4D(width+100, -height)
            });
            Global.objects.Add(ground);
        }

        private void CraeteCeilling(int width, int height)
        {
            Ground ground = new Ground(new List<Ponto4D> { 
                new Ponto4D(width+100, height-50),
                new Ponto4D(-width-100, height-50),
                new Ponto4D(-width-100, height),
                new Ponto4D(width+100, height)
            });
            Global.objects.Add(ground);
        }

        public void CreatePlataform(Ponto4D center)
        {
            GameObject rectangle = new Rectangle(new List<Ponto4D> { 
                new Ponto4D(center.X+50, center.Y+10, 50),
                new Ponto4D(center.X-50, center.Y+10, 50),
                new Ponto4D(center.X-50, center.Y-10, 50),
                new Ponto4D(center.X+50, center.Y-10, 50)
            });

            Global.objects.Add(rectangle);
            SetSelectedObject(GetLastObject());
        }

        public void CreateRampRight(Ponto4D center) {
            GameObject rectangle = new Rectangle(new List<Ponto4D> { 
                new Ponto4D(center.X+50, center.Y+50, 50),
                new Ponto4D(center.X+25, center.Y+50, 50),
                new Ponto4D(center.X-100, center.Y-50, 50),
                new Ponto4D(center.X-75, center.Y-50, 50)
            });

            Global.objects.Add(rectangle);
            SetSelectedObject(GetLastObject());
        }

        public void CreateRampLeft(Ponto4D center) {
            GameObject rectangle = new Rectangle(new List<Ponto4D> { 
                new Ponto4D(center.X+100, center.Y+50, 50),
                new Ponto4D(center.X+75, center.Y+50, 50),
                new Ponto4D(center.X-50, center.Y-50, 50),
                new Ponto4D(center.X-25, center.Y-50, 50)
            });

            Global.objects.Add(rectangle);
            SetSelectedObject(GetLastObject());
        }

        public void CreateBox(Ponto4D center)
        {
            GameObject box = new Box(new List<Ponto4D> {
                new Ponto4D(center.X+10, center.Y+10, 10),
                new Ponto4D(center.X-10, center.Y+10, 10),
                new Ponto4D(center.X-10, center.Y-10, 10),
                new Ponto4D(center.X+10, center.Y-10, 10)
            }, 10);

            Global.objects.Add(box);
        }

        public void RemoveBoxes()
        {
            for (int i = 0; i < Global.objects.Count; i++) {
                if(Global.objects[i] is Box)
                    Global.objects.Remove(Global.objects[i]);
            }
        }

        public void RemovePlataforms()
        {
            
        }
    }
}
