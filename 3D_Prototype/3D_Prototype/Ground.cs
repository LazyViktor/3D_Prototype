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
    class Ground
    {
        VertexPositionNormalTexture[] floorVerts;
        int repetitions;

        int endlessCounter = 1;

        public Texture2D CheckerboardTexture { private get; set; }
        BasicEffect effect;

        public Ground(int _repetitions)
        {
            floorVerts = new VertexPositionNormalTexture[6];

            // wrong orientation (z axis up)
            //floorVerts[0].Position = new Vector3(-20, -20, 0);
            //floorVerts[1].Position = new Vector3(-20, 20, 0);
            //floorVerts[2].Position = new Vector3(20, -20, 0);

            //floorVerts[3].Position = floorVerts[1].Position;
            //floorVerts[4].Position = new Vector3(20, 20, 0);
            //floorVerts[5].Position = floorVerts[2].Position;


            //floorVerts[0].Position = new Vector3(-20, 0, -20);
            //floorVerts[1].Position = new Vector3(-20, 0, 20);
            //floorVerts[2].Position = new Vector3(20, 0, -20);

            //floorVerts[3].Position = floorVerts[1].Position;
            //floorVerts[4].Position = new Vector3(20, 0, 20);
            //floorVerts[5].Position = floorVerts[2].Position;
                        
            //repetitions = _repetitions;
            //floorVerts[0].TextureCoordinate = new Vector2(0, 0);
            //floorVerts[1].TextureCoordinate = new Vector2(0, repetitions);
            //floorVerts[2].TextureCoordinate = new Vector2(repetitions, 0);

            //floorVerts[3].TextureCoordinate = floorVerts[1].TextureCoordinate;
            //floorVerts[4].TextureCoordinate = new Vector2(repetitions, repetitions);
            //floorVerts[5].TextureCoordinate = floorVerts[2].TextureCoordinate;


            repetitions = _repetitions;
            floorVerts[0].Position = new Vector3(-repetitions, 0, -repetitions)
                * Singleton.Instance.playerCube.Size;
            floorVerts[1].Position = new Vector3(-repetitions, 0, repetitions)
                * Singleton.Instance.playerCube.Size;
            floorVerts[2].Position = new Vector3(repetitions, 0, -repetitions)
                * Singleton.Instance.playerCube.Size;

            floorVerts[3].Position = floorVerts[1].Position;
            floorVerts[4].Position = new Vector3(repetitions, 0, repetitions)
                * Singleton.Instance.playerCube.Size;
            floorVerts[5].Position = floorVerts[2].Position;
            
            floorVerts[0].TextureCoordinate = new Vector2(0, 0);
            floorVerts[1].TextureCoordinate = new Vector2(0, repetitions);
            floorVerts[2].TextureCoordinate = new Vector2(repetitions, 0);

            floorVerts[3].TextureCoordinate = floorVerts[1].TextureCoordinate;
            floorVerts[4].TextureCoordinate = new Vector2(repetitions, repetitions);
            floorVerts[5].TextureCoordinate = floorVerts[2].TextureCoordinate;
            
            effect = new BasicEffect(Singleton.Instance.graphics.GraphicsDevice);
        }

        public void Update()
        {
            // check if vertices need to be updated for endless scroll
            if(Singleton.Instance.playerCube.PlayerPosition.X > repetitions
                * Singleton.Instance.playerCube.Size * endlessCounter
                - repetitions * Singleton.Instance.playerCube.Size / 2 )
            {
                // update vertices positions
                floorVerts[0].Position = new Vector3(-repetitions, 0, -repetitions)
                    * Singleton.Instance.playerCube.Size
                    + new Vector3(repetitions * Singleton.Instance.playerCube.Size * endlessCounter, 0, 0);
                floorVerts[1].Position = new Vector3(-repetitions, 0, repetitions)
                    * Singleton.Instance.playerCube.Size
                     + new Vector3(repetitions * Singleton.Instance.playerCube.Size * endlessCounter, 0, 0);
                floorVerts[2].Position = new Vector3(repetitions, 0, -repetitions)
                    * Singleton.Instance.playerCube.Size
                     + new Vector3(repetitions * Singleton.Instance.playerCube.Size * endlessCounter, 0, 0);

                floorVerts[3].Position = floorVerts[1].Position;
                floorVerts[4].Position = new Vector3(repetitions, 0, repetitions)
                    * Singleton.Instance.playerCube.Size
                     + new Vector3(repetitions * Singleton.Instance.playerCube.Size * endlessCounter, 0, 0);
                floorVerts[5].Position = floorVerts[2].Position;

                floorVerts[0].TextureCoordinate = new Vector2(0, 0);
                floorVerts[1].TextureCoordinate = new Vector2(0, repetitions);
                floorVerts[2].TextureCoordinate = new Vector2(repetitions, 0);

                floorVerts[3].TextureCoordinate = floorVerts[1].TextureCoordinate;
                floorVerts[4].TextureCoordinate = new Vector2(repetitions, repetitions);
                floorVerts[5].TextureCoordinate = floorVerts[2].TextureCoordinate;

                endlessCounter++;
            }

            // update vertices positions
        }

        public void Draw()
        {

            effect.View = Singleton.Instance.camera.ViewMatrix;

            effect.Projection = Singleton.Instance.camera.ProjectionMatrix;

            effect.TextureEnabled = true;
            effect.Texture = CheckerboardTexture;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                Singleton.Instance.graphics.GraphicsDevice.DrawUserPrimitives(
                            PrimitiveType.TriangleList,
                    floorVerts,
                    0,
                    2);
            }
        }
    }
}
