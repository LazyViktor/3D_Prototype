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
    class StartScreen
    {
        // Background stats
        Vector2 backgroundTopLeftPosition;
        int backgroundWidth;
        int backgroundHeight;
        Texture2D backgroundPixel = new Texture2D(
            Singleton.Instance.graphics.GraphicsDevice, 1, 1);
        Color[] backgroundColor = { Color.Blue };

        // GameTitle
            // Load in Game1 Content.Load<SpriteFont<>
        public SpriteFont GameTitleFont { private get; set; }        
        Vector2 gameTitlePosition;
        Vector2 gameTitleOrigin;
        Color gameTitleFontColor = Color.OrangeRed;
        string gameTitleText = "GameTitle";

        // Normal font for descriptions etc
            // Load in Game1 Content.Load<SpriteFont<>
        public SpriteFont NormalFont { private get; set; }
        Color normalFontColor = Color.White;

        // Subheadings font        
            // Load in Game1 Content.Load<SpriteFont<>
        public SpriteFont HeadingsFont { private get; set; }
        Color headingsFontColor = Color.OrangeRed;

        //// Start screen text
        // game description
        string descriptionText
            = "Jump over the obstacles" + Environment.NewLine
            + "without touching them" + Environment.NewLine
            + "to gain score";
        Vector2 descriptionPosition;
        Vector2 descriptionOrigin;

        // controls heading
        string controlsHeadingText = "Controls:";
        Vector2 controlsHeadingPosition;
        Vector2 controlsHeadingOrigin;

        // controls
        string controlsText = "SPACEBAR = Jump";
        Vector2 controlsPosition;
        Vector2 controlsOrigin;

        // start promt
        string startPromtText
            = "Press ENTER to start";
        Vector2 startPromtPosition;
        Vector2 startPromtOrigin;





        public StartScreen()
        {
            Viewport viewport = 
                Singleton.Instance.graphics.GraphicsDevice.Viewport;

            
            // background alinement 1/10 from the boarders
            backgroundTopLeftPosition = new Vector2(
                viewport.X + viewport.Width / 10,
                viewport.Y + viewport.Height / 10);

            // background is 8/10 of screen in width and height
            backgroundWidth = viewport.Width / 10 * 8;
            backgroundHeight = viewport.Height /10 * 8;

            backgroundPixel.SetData<Color>(backgroundColor);


            // text positions
            gameTitlePosition = new Vector2(
                viewport.X + viewport.Width / 2,
                viewport.Y + viewport.Height / 10 * 2);

            descriptionPosition = new Vector2(
                viewport.X + viewport.Width / 2,
                viewport.Y + viewport.Height / 10 * 4);

            controlsHeadingPosition = new Vector2(
                viewport.X + viewport.Width / 2,
                viewport.Y + viewport.Height / 10 * 6);

            controlsPosition = new Vector2(
                viewport.X + viewport.Width / 2,
                viewport.Y + viewport.Height / 10 * 7);

            startPromtPosition = new Vector2(
                viewport.X + viewport.Width / 2,
                viewport.Y + viewport.Height / 10 * 8);

        }

        public void AssigneTextOrigins()
        {
            // assigne textOrigin for centered drawing

            gameTitleOrigin = GameTitleFont.MeasureString(gameTitleText) / 2;

            descriptionOrigin = NormalFont.MeasureString(descriptionText) / 2;

            controlsHeadingOrigin = HeadingsFont.MeasureString(controlsHeadingText) / 2;

            controlsOrigin = NormalFont.MeasureString(controlsText) / 2;

            startPromtOrigin = NormalFont.MeasureString(startPromtText) / 2;
        }

        public void Draw()
        {
            // Background
            Singleton.Instance.spriteBatch.Draw(backgroundPixel,
                new Rectangle((int)backgroundTopLeftPosition.X,
                (int)backgroundTopLeftPosition.Y, backgroundWidth,
                backgroundHeight), Color.White);

            // GameTitle
            Singleton.Instance.spriteBatch.DrawString(GameTitleFont,
                gameTitleText, gameTitlePosition, gameTitleFontColor, 0, 
                gameTitleOrigin, 1.0f, SpriteEffects.None, 0.5f);

            // description
            Singleton.Instance.spriteBatch.DrawString(NormalFont,
                descriptionText, descriptionPosition, normalFontColor, 0, 
                descriptionOrigin, 1.0f, SpriteEffects.None, 0.5f);

            // controlsHeading
            Singleton.Instance.spriteBatch.DrawString(HeadingsFont,
                controlsHeadingText, controlsHeadingPosition, headingsFontColor, 0, 
                controlsHeadingOrigin, 1.0f, SpriteEffects.None, 0.5f);

            // controls
            Singleton.Instance.spriteBatch.DrawString(NormalFont,
                controlsText, controlsPosition, normalFontColor, 0, controlsOrigin,
                1.0f, SpriteEffects.None, 0.5f);

            // start promt
            Singleton.Instance.spriteBatch.DrawString(NormalFont,
                startPromtText, startPromtPosition, normalFontColor, 0, startPromtOrigin,
                1.0f, SpriteEffects.None, 0.5f);
        }
    }
}
