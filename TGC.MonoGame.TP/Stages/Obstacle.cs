using System;
using BepuPhysics.Constraints;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TGC.MonoGame.TP.Collisions;
using TGC.MonoGame.TP.Geometries;
using TGC.MonoGame.TP.Stages.Items;
using static System.Formats.Asn1.AsnWriter;

namespace TGC.MonoGame.TP.Stages
{
    public class Obstacle
    {
        protected Vector3? InitialPosition;
        protected Vector3? InitialScale;
        protected Matrix? InitialRotation;
        protected Vector3? CurrentPosition;
        protected Vector3 Movement;
        public CubePrimitive Model;
        protected float Time;
        private const float Speed = 0.5f;


        public Obstacle(GraphicsDevice graphicsDevice, ContentManager content, Color color, float size = 25f, Vector3? coordinates = null, Vector3? scale = null, Matrix? rotation = null, Vector3? movement = null)
        {
            Model = new CubePrimitive(graphicsDevice, content, color, size, coordinates, scale, rotation);
            InitialPosition = coordinates;
            InitialScale = scale;
            InitialRotation = rotation;
            Movement = (Vector3) movement;
        }

        public void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Time += elapsedTime * Speed;

            CurrentPosition = InitialPosition + (float) Math.Sin(Time) * Movement;
            Model.World = Matrix.CreateScale(InitialScale ?? Vector3.One) * (InitialRotation ?? Matrix.Identity) * Matrix.CreateTranslation(CurrentPosition ?? Vector3.Zero);
            Model.BoundingCube = new OrientedBoundingBox(CurrentPosition ?? Vector3.Zero, (InitialScale ?? Vector3.One) * 25 / 2);
            Model.BoundingCube.Rotate(InitialRotation ?? Matrix.Identity);
        }

        public void Draw(Matrix view, Matrix projection)
        {
            Model.Draw(view, projection);
        }
    }
}
