using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TGC.MonoGame.TP.MainCharacter;
using TGC.MonoGame.TP.Geometries;
using Microsoft.Xna.Framework.Audio;

namespace TGC.MonoGame.TP.Stages.Items
{
    internal class Rupee : Pickup
    {
        public Rupee(GraphicsDevice graphicsDevice, ContentManager content, Color color, float size, Vector3 coordinates, Vector3 scale, Matrix rotation) : base(graphicsDevice, content, color, size, coordinates, scale, rotation)
        {
            ItemSoundEffect = content.Load<SoundEffect>(TGCGame.ContentFolderSounds + "coin");
            ItemSoundEffectInstance = ItemSoundEffect.CreateInstance();
        }

        protected override CustomPrimitive CreateModel(GraphicsDevice graphicsDevice, ContentManager content, Color color, float size, Vector3 coordinates, Vector3 scale, Matrix rotation)
        {
            return new RupeePrimitive(graphicsDevice, content, Color.Green, size, coordinates, scale, rotation);
        }

        public override void ModifyCharacterStats(Character sphere)
        {
            sphere.Money += 1;
        }

    }
}

