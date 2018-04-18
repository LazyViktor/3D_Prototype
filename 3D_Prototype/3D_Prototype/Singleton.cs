using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3D_Prototype
{
    class Singleton
    {
        /* Used for sharing of global variables between classes
         * accesable through Singleton.Instance.[variable name] */



        // Global Variables

        // Camera
        public Camera camera;

        // Graphics Default Stuff
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        // GameObjects
        public Ground ground;
        public PlayerCube playerCube;
        public ObstaclePyramid obstaclePyramid;

        // Controlls
        public KeyboardState keyboardState;

        // Physics
        public float G_Force { get; } = 10f;


        //Singleton Stuff
        private static Singleton instance;

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }
}
