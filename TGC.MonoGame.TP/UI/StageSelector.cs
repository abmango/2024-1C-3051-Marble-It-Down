using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGC.MonoGame.TP.UI
{
    internal class StageSelector
    {
        const float LineSpacing = 25f;

        public static void Update()
        {

        }

        public static void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, SpriteFont font, int stageSelected)
        {

            spriteBatch.Begin();

            var position = TextHelper.CenterText(graphicsDevice, font, "Stage X", 1f);
            spriteBatch.DrawString(font, "Stage 1", position, stageSelected == 1 ? Color.White : Color.Gray);

            position.Y += LineSpacing;
            spriteBatch.DrawString(font, "Stage 2", position, stageSelected == 2 ? Color.White : Color.Gray);

            spriteBatch.End();

        }
    }
}
