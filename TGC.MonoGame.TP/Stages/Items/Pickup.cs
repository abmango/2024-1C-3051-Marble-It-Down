using Microsoft.Xna.Framework;
using TGC.MonoGame.TP.MainCharacter;
using TGC.MonoGame.TP.Geometries;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TGC.MonoGame.TP.Collisions;
using TGC.MonoGame.TP.Stages.Items;
using Microsoft.Xna.Framework.Audio;

public abstract class Pickup
{
    protected Vector3 Position;
    protected float Yaw;
    public ItemStatus Status;
    public CustomPrimitive Model;
    public OrientedBoundingBox BoundingCube
    {
        get 
        {
            return Model.BoundingCube;
        }
    }

    protected SoundEffect ItemSoundEffect;
    protected SoundEffectInstance ItemSoundEffectInstance;

    public Pickup(GraphicsDevice graphicsDevice, ContentManager content, Color color, float size, Vector3 coordinates, Vector3 scale, Matrix rotation)
    {
        Status = ItemStatus.Collectible; 
        Yaw = 0;
        Position = coordinates;
        Model = CreateModel(graphicsDevice, content, color, size, coordinates, scale, rotation);
    }

    public void Update(GameTime gameTime)
    {
        float elapsedTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
        Yaw += elapsedTime * MathHelper.Pi;

        Model.World = Matrix.CreateFromYawPitchRoll(Yaw, 0, 0) * Matrix.CreateTranslation(Position);
    }

    public void Draw(Matrix view, Matrix projection)
    {
        Model.Draw(view, projection);
    }

    protected abstract CustomPrimitive CreateModel(GraphicsDevice graphicsDevice, ContentManager content, Color color, float size, Vector3 coordinates, Vector3 scale, Matrix rotation);

    public abstract void ModifyCharacterStats(Character sphere);

    public void PlaySoundEffect()
    {
        ItemSoundEffectInstance.Play();
    }

}