using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TGC.MonoGame.TP.MainCharacter;
using TGC.MonoGame.TP.Geometries;
using Microsoft.Xna.Framework.Audio;

namespace TGC.MonoGame.TP.Stages.Items
{
    internal class SpeedBoost : Pickup
    {
        public SpeedBoost(GraphicsDevice graphicsDevice, ContentManager content, Color color, float size, Vector3 coordinates, Vector3 scale, Matrix rotation) : base(graphicsDevice, content, color, size, coordinates, scale, rotation)
        {
            ItemSoundEffect = content.Load<SoundEffect>(TGCGame.ContentFolderSounds + "powerup");
            ItemSoundEffectInstance = ItemSoundEffect.CreateInstance();
        }

        protected override CustomPrimitive CreateModel(GraphicsDevice graphicsDevice, ContentManager content, Color color, float size, Vector3 coordinates, Vector3 scale, Matrix rotation)
        {
            return new LightningPrimitive(graphicsDevice, content, Color.Yellow, size, coordinates, scale, rotation);
        }

        public override void ModifyCharacterStats(Character sphere)
        {
            sphere.SpeedBoost = 5f; 
        }

    }
}
