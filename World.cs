using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace stick_man
{
    public class World
    {
        private Ground ground;
        private Camera camera;

        private bool creationMode = false;
        private bool creatingObject = false;
        private List<GameObject> objects = new List<GameObject>();

        public World(int width, int height) 
        {
            camera = new Camera(0, width, 0, height, -1, 1);

            CraeteGround(width/2, height/2);
        }

        public void UpdateCamera() => camera.Update();

        public void DrawObjects()
        {
            foreach(GameObject obj in objects)
                obj.Draw();
        }

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
    }
}
