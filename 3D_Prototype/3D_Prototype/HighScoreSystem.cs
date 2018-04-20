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
    class HighScoreSystem
    {
        int currScoreNum = 0;
        public string currScoreText = "0";

        int scorePerObstacle = 200;

        public SpriteFont Font { private get; set; }    // Load in Game1 Content.Load<SpriteFont<>
        Vector2 textPosition;
        float distanceToScreenBoarder;
        Color fontColor = Color.Purple;

        
        public HighScoreSystem()
        {
            // TODO calculate textPosition upper right corner

            Viewport viewport = Singleton.Instance.graphics.GraphicsDevice.Viewport;

            distanceToScreenBoarder = viewport.Width / 20;

            textPosition = new Vector2(viewport.X  + distanceToScreenBoarder,
                viewport.Y  + distanceToScreenBoarder);

            // test textPosition middle of screen
            //textPosition = new Vector2(viewport.X + viewport.Width / 2,
            //    viewport.Y + viewport.Height / 2);
        }


        public void Reset()
        {
            // call on death

            currScoreNum = 0;
            currScoreText = "0";
        }


        public void AddPoints()
        {
            // add points for passing one obstacle
            currScoreNum += scorePerObstacle;
            currScoreText = currScoreNum.ToString();
        }


        public void Draw()
        {
            Singleton.Instance.spriteBatch.DrawString(Font, "Score: " + currScoreText,
                textPosition, fontColor);
        }
    }
}
