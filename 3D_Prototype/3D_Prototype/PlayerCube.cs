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
    class PlayerCube
    {
        public Vector3 PlayerPosition { get; private set; }

        public Model PlayerModel { private get; set; }

        public int Size { get; } = 100; // in x,y,z direction

        float currMoveSpeed = 0f;    // for automated forward movement
        float maxMoveSpeed = 2f;     // speed cap
        float accelMoveSpeed = 0.1f;   // acceleration for a slow start

        // for jump logic
        bool isJumping = false;
        float jumpSpeed = 20f;
        float maxJumpHeightFactored = 2f;


        public PlayerCube(Vector3 _playerPosition)
        {
            // Dont forget to load model manuelly in Game1.LoadConten()!

            // Adjusted to make the ground total 0 and
            // to stop model clipping halfway through ground.
            PlayerPosition = _playerPosition + new Vector3(0, Size/2, 0);

            maxJumpHeightFactored *= Size;
        }
        

        public void Update()
        {
            // Using 2D logic for movement.
            Vector2 moveVector = Vector2.Zero;


            // calculating forward movement
                // updating speed through acceleration
            if(currMoveSpeed < maxMoveSpeed)
            {
                currMoveSpeed += accelMoveSpeed;

                // check and adjust for movement cap
                if(currMoveSpeed > maxMoveSpeed)
                {
                    currMoveSpeed = maxMoveSpeed;
                }
            }

            // applying forward movement
            moveVector.X += currMoveSpeed;


            // applying gravity
            moveVector.Y -= Singleton.Instance.G_Force;


            // jumping trigger: Space
            if ((Singleton.Instance.keyboardState.IsKeyDown(Keys.Space)
                && PlayerPosition.Y == Size / 2)
                || isJumping)
            {
                moveVector = this.Jump(moveVector);
            }


            // appling total movement
            PlayerPosition += new Vector3(moveVector, 0);


            // adjusting for groud clipping and maxJumpHeight
            if (PlayerPosition.Y < Size / 2)
            {
                PlayerPosition = new Vector3(PlayerPosition.X, Size / 2, PlayerPosition.Z);
            }
            else if(PlayerPosition.Y > maxJumpHeightFactored)
            {
                isJumping = false;
            }

        }


        public Vector2 Jump(Vector2 _moveVector)
        {
            isJumping = true;
            
            return _moveVector + new Vector2(0, jumpSpeed);
        }


        public void Draw()
        {
            Singleton.Instance.camera.DrawModel(PlayerModel, PlayerPosition);
        }
    }
}
