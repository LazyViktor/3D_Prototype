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

        Line leftSide = new Line();
        Line rightSide = new Line();

        Line playerBottomSide = new Line();


        public ObstaclePyramid(Vector3 _obstaclePosition, Model _obstacleModel)
        {
            // Dont forget to load model into Singleton manuelly in Game1.LoadConten()!
            ObstacleModel = _obstacleModel;

            // Adjusted to make the ground total 0 and
            // to stop model clipping halfway through ground.
            ObstaclePosition = new Vector3(_obstaclePosition.X,
                _obstaclePosition.Y + size / 2, _obstaclePosition.Z);

            //// edit side lines
            // bottom left point
            leftSide.x1 = ObstaclePosition.X - size / 2;
            leftSide.y1 = ObstaclePosition.Y - size / 2;

            // bottom right point
            rightSide.x1 = ObstaclePosition.X + size / 2;
            rightSide.y1 = ObstaclePosition.Y - size / 2;

            // top points
            leftSide.x2 = ObstaclePosition.X;
            leftSide.y2 = ObstaclePosition.Y + size / 2;

            rightSide.x2 = ObstaclePosition.X;
            rightSide.y2 = ObstaclePosition.Y + size / 2;
        }


        public void Update()
        {
            Vector3 playerPosition = Singleton.Instance.playerCube.PlayerPosition;
            float playerSize = Singleton.Instance.playerCube.Size;

            //// edit player bottom side line
            // bottom left point
            playerBottomSide.x1 = playerPosition.X - playerSize / 2;
            playerBottomSide.y1 = playerPosition.Y - playerSize / 2;
            // bottom right point
            playerBottomSide.x2 = playerPosition.X + playerSize / 2;
            playerBottomSide.y2 = playerPosition.Y - playerSize / 2;

            //// check for simple player Collision
            //if(playerPosition.X + playerSize / 2 >= ObstaclePosition.X - size / 2 
            //    && playerPosition.X - playerSize / 2 <= ObstaclePosition.X + size / 2
            //    && playerPosition.Y - playerSize / 2 <= ObstaclePosition.Y + size / 2)
            //{
            //    // ObstaclePyramid is treated like a cube for Collison
            //    this.OnCollision();
            //}

            // check for player optimised Collision
            if (playerPosition.X + playerSize / 2 >= ObstaclePosition.X - size / 2
                && playerPosition.X - playerSize / 2 <= ObstaclePosition.X + size / 2
                && playerPosition.Y - playerSize / 2 <= ObstaclePosition.Y + size / 2)
            {

                // find intersection point
                Point intersectionPoint;
                if(playerPosition.X > ObstaclePosition.X)
                {
                    // right side
                    intersectionPoint = LineIntersection.FindIntersection(rightSide, playerBottomSide);
                }
                else
                {
                    // left side
                    intersectionPoint = LineIntersection.FindIntersection(leftSide, playerBottomSide);
                }

                // check if intersectionPoint in on actual player model
                if(intersectionPoint.x >= playerBottomSide.x1 
                    && intersectionPoint.x <= playerBottomSide.x2)
                {
                    this.OnCollision();
                }
                
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
