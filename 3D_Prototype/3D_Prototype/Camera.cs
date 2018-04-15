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
        public Vector3 CameraPosition { get; set; }

        // Camera target in world space
        public Vector3 TargetPosition { private get; set; }

        // Matrices used for 3D Camera
        public Matrix ViewMatrix { get; private set; }
        public Matrix ProjectionMatrix { get; private set; }

        // Camera stats
        float fieldOfView = 90f;
        float nearPlaneDistance = 0.1f;
        float farPlaneDistance = 100f;


        public Camera(Viewport _viewport, Vector3 _cameraPosition, Vector3 _targetPosition)
        {
            CameraPosition = _cameraPosition;
            TargetPosition = _targetPosition;

            viewport = _viewport;

            ViewMatrix = Matrix.CreateLookAt(CameraPosition, TargetPosition, Vector3.Up)
                //* Matrix.CreateRotationY(MathHelper.ToRadians(30f))
                //* Matrix.CreateRotationX(MathHelper.ToRadians(30f))
                * Matrix.CreateTranslation(-2, -3, 0);

            ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(fieldOfView),
                viewport.AspectRatio,
                nearPlaneDistance, farPlaneDistance);
        }

        public void Update(Vector3 _posWorldTarget)
        {
            TargetPosition = _posWorldTarget;

            // Move camera in x and y direction corresponding to target
            CameraPosition = new Vector3(TargetPosition.X, TargetPosition.Y, CameraPosition.Z);

            // Update viewMatrix accordingly
            ViewMatrix = Matrix.CreateLookAt(CameraPosition, TargetPosition, Vector3.Up);
        }

        public void DrawModel(Model _model, Vector3 _modelPosition)
        {
            foreach(ModelMesh mesh in _model.Meshes)
            {
                foreach(BasicEffect effect in mesh.Effects)
                {
                    effect.World = Matrix.CreateWorld(_modelPosition, Vector3.Forward, Vector3.Up);
                    effect.View = ViewMatrix;
                    effect.Projection = ProjectionMatrix;
                }

                mesh.Draw();
            }
        }

    }
}
