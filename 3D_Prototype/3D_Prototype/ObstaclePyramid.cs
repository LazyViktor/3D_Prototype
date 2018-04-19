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
    class ObstaclePyramid
    {
        public Vector3 ObstaclePosition { get; private set; }

        public Model ObstacleModel { private get; set; }

        float size = 100f;


        public ObstaclePyramid(Vector3 _obstaclePosition, Model _obstacleModel)
        {
            // Dont forget to load model into Singleton manuelly in Game1.LoadConten()!
            ObstacleModel = _obstacleModel;

            // Adjusted to make the ground total 0 and
            // to stop model clipping halfway through ground.
            ObstaclePosition = new Vector3(_obstaclePosition.X,
                _obstaclePosition.Y + size / 2, _obstaclePosition.Z);
        }


        public void Update()
        {
            Vector3 playerPosition = Singleton.Instance.playerCube.PlayerPosition;
            float playerSize = Singleton.Instance.playerCube.Size;

            // check for player Collision
            if(playerPosition.X + playerSize / 2 >= ObstaclePosition.X - size / 2 
                && playerPosition.X - playerSize / 2 <= ObstaclePosition.X + size / 2
                && playerPosition.Y - playerSize / 2 <= ObstaclePosition.Y + size / 2)
            {
                // ObstaclePyramid is treated like a cube for Collison
                this.OnCollision();
            }
        }


        public void OnCollision()
        {
            // TODO: call death method to reset level and show death screen
            Singleton.Instance.currGameState = Singleton.Gamestates.death;

            //// Reset GameObjects

            // Player
            Singleton.Instance.playerCube.Reset();

            // ObstacleMap
            Singleton.Instance.currObstacleMap.Reset();

            // Ground
            Singleton.Instance.ground.Reset();

            // HighscoreSystem
            Singleton.Instance.highScoreSystem.Reset();
        }


        public void Draw()
        {
            // call method for drawing 3D models
            Singleton.Instance.camera.DrawModel(ObstacleModel ,ObstaclePosition);
        }
    }
}
