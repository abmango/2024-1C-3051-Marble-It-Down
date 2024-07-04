using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TGC.MonoGame.TP.MainCharacter;
using TGC.MonoGame.TP.Geometries;
using Microsoft.Xna.Framework.Audio;

namespace TGC.MonoGame.TP.Stages.Items
{
    internal class Antigravity : Pickup
    {
        public Antigravity(GraphicsDevice graphicsDevice, ContentManager content, Color color, float size, Vector3 coordinates, Vector3 scale, Matrix rotation) : base(graphicsDevice, content, color, size, coordinates, scale, rotation)
        {
            ItemSoundEffect = content.Load<SoundEffect>(TGCGame.ContentFolderSounds + "powerup");
            ItemSoundEffectInstance = ItemSoundEffect.CreateInstance();
        }

        protected override CustomPrimitive CreateModel(GraphicsDevice graphicsDevice, ContentManager content, Color color, float size, Vector3 coordinates, Vector3 scale, Matrix rotation)
        {
            return new OctahedronPrimitive(graphicsDevice, content, Color.White, size, coordinates, scale, rotation);
        }

        public override void ModifyCharacterStats(Character sphere)
        {
            sphere.GravityBoost = 0.2f;
        }

    }
}