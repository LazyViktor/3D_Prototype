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
    class DeathScreen
    {
        // Background stats
        Vector2 backgroundTopLeftPosition;
        int backgroundWidth;
        int backgroundHeight;
        Texture2D backgroundPixel = new Texture2D(
            Singleton.Instance.graphics.GraphicsDevice, 1, 1);
        Color[] backgroundColor = { Color.Red };

        // Fail
        // Load in Game1 Content.Load<SpriteFont<>
        public SpriteFont FailFont { private get; set; }
        Color failFontColor = Color.Black;

        // Normal font for descriptions etc
        // Load in Game1 Content.Load<SpriteFont<>
        public SpriteFont NormalFont { private get; set; }
        Color normalFontColor = Color.White;

        // Subheadings font        
        // Load in Game1 Content.Load<SpriteFont<>
        public SpriteFont HeadingsFont { private get; set; }
        Color headingsFontColor = Color.White;

        //// Death Screen Text
        // FAIL
        Vector2 failPosition;
        Vector2 failOrigin;
        string failText = "FAIL";
        // game description
        string explanationText
            = "Your Score: " + "000000"
            + Environment.NewLine +"Dont touch the obstacles!";
        Vector2 explanationPosition;
        Vector2 explanationOrigin;
        

        // restart promt
        string restartPromtText
            = "Press ENTER to try again";
        Vector2 restartPromtPosition;
        Vector2 restartPromtOrigin;





        public DeathScreen()
        {
            Viewport viewport =
                Singleton.Instance.graphics.GraphicsDevice.Viewport;


            // background alinement 1/10 from the boarders
            backgroundTopLeftPosition = new Vector2(
                viewport.X + viewport.Width / 10,
                viewport.Y + viewport.Height / 10);

            // background is 8/10 of screen in width and height
            backgroundWidth = viewport.Width / 10 * 8;
            backgroundHeight = viewport.Height / 10 * 8;

            backgroundPixel.SetData<Color>(backgroundColor);


            // text positions
            failPosition = new Vector2(
                viewport.X + viewport.Width / 2,
                viewport.Y + viewport.Height / 10 * 3);

            explanationPosition = new Vector2(
                viewport.X + viewport.Width / 2,
                viewport.Y + viewport.Height / 10 * 5);

            restartPromtPosition = new Vector2(
                viewport.X + viewport.Width / 2,
                viewport.Y + viewport.Height / 10 * 7);

        }


        public void Update()
        {
            explanationText
            = "Your Score: " + Singleton.Instance.highScoreSystem.currScoreText
            + Environment.NewLine + "Dont touch the obstacles!";
        }


        public void AssigneTextOrigins()
        {
            // assigne textOrigin for centered drawing

            failOrigin = FailFont.MeasureString(failText) / 2;

            explanationOrigin = HeadingsFont.MeasureString(explanationText) / 2;

            restartPromtOrigin = NormalFont.MeasureString(restartPromtText) / 2;
        }

        public void Draw()
        {
            // Background
            Singleton.Instance.spriteBatch.Draw(backgroundPixel,
                new Rectangle((int)backgroundTopLeftPosition.X,
                (int)backgroundTopLeftPosition.Y, backgroundWidth,
                backgroundHeight), Color.White);

            // FAIL
            Singleton.Instance.spriteBatch.DrawString(FailFont,
                failText, failPosition, failFontColor, 0,
                failOrigin, 1.0f, SpriteEffects.None, 0.5f);

            // explanation
            Singleton.Instance.spriteBatch.DrawString(HeadingsFont,
                explanationText, explanationPosition, headingsFontColor, 0,
                explanationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            
            // restart promt
            Singleton.Instance.spriteBatch.DrawString(NormalFont,
                restartPromtText, restartPromtPosition, normalFontColor, 0,
                restartPromtOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }
    }
}
