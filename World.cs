using System.Collections.Generic;

namespace stick_man
{
    public class World
    {
        private Camera camera;
        public static List<GameObject> objects = new List<GameObject>();

        public World() 
        {
            camera = new Camera(-400, 400, -400, 400, -1, 1);
        }

    }
}
