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
    class Camera
    {
        // Contains screen bounds etc.
        private Viewport viewport;

        // Camera position in world space
        public Vector3 PosWorldCenter { get; set; }

        // Camera target in world space
        public Vector3 PosWorldTarget { private get; set; }


        public Camera(Viewport _viewport, Vector3 _posWorldCenter, Vector3 _posWorldTarget)
        {
            PosWorldCenter = _posWorldCenter;
            PosWorldTarget = _posWorldTarget;

            viewport = _viewport;
        }

        public void Update(Vector3 _posWorldTarget)
        {
            PosWorldTarget = _posWorldTarget;

            // Move camera in x and y direction corresponding to target
            PosWorldCenter = new Vector3(PosWorldTarget.X, PosWorldTarget.Y, PosWorldCenter.Z);
        }

        public Matrix GetViewMatrix()
        {
            // Create ViewMatrix for Game1 Draw Method
            return Matrix.CreateTranslation(-PosWorldCenter);
        }

    }
}
