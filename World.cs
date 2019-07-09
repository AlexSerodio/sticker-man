using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace stick_man
{
    public class World
    {
        private Ground ground;
        private Camera camera;

        private bool instantiationMode = false;
        private bool creationMode = false;
        private bool creatingObject = false;
        private List<GameObject> objects = new List<GameObject>();
        private GameObject selectedObject;
        private bool scaling = false;

        public GameObject GetSelectedObject() 
        {
            return selectedObject;
        }

        public void SetSelectedObject(GameObject obj) {
            if(obj != null)
                obj.SetColor(Color.Green);
            if(selectedObject != null)
                selectedObject.SetColor(Color.Black);

            selectedObject = obj;
        }

        public bool HasSelectedObject() => this.selectedObject != null;

        public World(int width, int height)
        {
            camera = new Camera(0, width, 0, height, -1, 1);

            CraeteGround(width/2, height/2);
        }

        public void UpdateCamera() => camera.Update();

        public GameObject SelectObject(Ponto4D pontoClicado)
        {
            foreach(GameObject obj in objects) {
                if(obj.GetBoundBox().IsColliding(pontoClicado, obj))
                    return obj;
            }

            return null;
        }

        public void DrawObjects()
        {
            foreach(GameObject obj in objects)
                obj.Draw();
        }

        public bool IsInstantiationModeOn() => instantiationMode;
        public void SetInstantiationMode(bool active) => instantiationMode = active;
        public void SwitchInstantiationMode() => instantiationMode = !instantiationMode;

        public bool IsCreationModeOn() => creationMode;
        public void SetCreationMode(bool active) => creationMode = active;
        public void SwitchCreationMode() => creationMode = !creationMode;

        public bool IsCreatingObject() => creatingObject;
        public void SetCreatingObject(bool active) => creatingObject = active;
        
        private void CraeteGround(int width, int height)
        {
            Ground ground = new Ground(new List<Ponto4D> { 
                new Ponto4D(width, -height+50),
                new Ponto4D(-width, -height+50),
                new Ponto4D(-width, -height),
                new Ponto4D(width, -height),
            });
            objects.Add(ground);
        }

        public void AddObject(GameObject newObject) => objects.Add(newObject);
        public GameObject GetObject(int index) => objects[index];
        public GameObject GetLastObject() => objects[objects.Count-1];
        public List<GameObject> GetObjects() => objects;

        public void SetPrimitive(PrimitiveType primitive) 
        {
            foreach(GameObject obj in objects)
                obj.SetPrimitive(primitive);
        }

        public void CreateRectangle(Ponto4D center)
        {
            GameObject rectangle = new Rectangle(new List<Ponto4D> { 
                new Ponto4D(center.X+50, center.Y+10, 50),
                new Ponto4D(center.X-50, center.Y+10, 50),
                new Ponto4D(center.X-50, center.Y-10, 50),
                new Ponto4D(center.X+50, center.Y-10, 50)
            });

            objects.Add(rectangle);
            SetSelectedObject(GetLastObject());
        }

        public void SwitchScalingMode() => this.scaling = !this.scaling;

        public bool IsScalingModeOn() => this.scaling;
    }
}
