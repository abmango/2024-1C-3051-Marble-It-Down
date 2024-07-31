using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using TGC.MonoGame.TP.Geometries;
using TGC.MonoGame.TP.Stages;
using TGC.MonoGame.TP.Stages.Items;

class Stage_02 : Stage
{

    public Stage_02(GraphicsDevice graphicsDevice, ContentManager content) :
        base(graphicsDevice, content, characterPosition: new Vector3(300f, 25f, 0f))
    {
        BackgroundMusic = Content.Load<Song>(ContentFolderMusic + "stage_02");
        MediaPlayer.Play(BackgroundMusic);
        MediaPlayer.IsRepeating = true;
    }

    protected override void LoadColliders()
    {
        for (int i = 0; i < Track.Count; i++)
        {
            GeometricPrimitive objetoActual = Track[i];
            if (objetoActual is CubePrimitive)
            {
                CubePrimitive aux = (CubePrimitive)objetoActual;
                Colliders.Add(aux.BoundingCube);
            }
            if (objetoActual is RampPrimitive)
            {
                RampPrimitive aux = (RampPrimitive)objetoActual;
                foreach (var boundingRamp in aux.BoundingRamps)
                {
                    Colliders.Add(boundingRamp);
                }
            }
        }

        for (int i = 0; i < Obstacles.Count; i++)
        {
            Obstacle cuboActual = (Obstacle)Obstacles[i];
            Colliders.Add(cuboActual.Model.BoundingCube);
        }

        for (int i = 0; i < Signs.Count; i++)
        {
            CubePrimitive cuboActual = (CubePrimitive)Signs[i];
            Colliders.Add(cuboActual.BoundingCube);
        }

        PickupColliders = Pickups;

        for (int i = 0; i < Checkpoints.Count; i++)
        {
            CubePrimitive cuboActual = (CubePrimitive)Checkpoints[i];
            CheckpointColliders.Add(cuboActual.BoundingCube);
        }
    }

    protected override void LoadTrack()
    {
        Track = new List<GeometricPrimitive>()
            {
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(0, 0, 0), scale: new Vector3(30, 1, 10), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(87.5f, 50, -112.5f), scale: new Vector3(1, 3, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(87.5f, 50, 112.5f), scale: new Vector3(1, 3, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(87.5f, 112.5f, 0), scale: new Vector3(1, 2, 10), rotation: Matrix.CreateFromYawPitchRoll(3.1415927f, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-625, 0, 0), scale: new Vector3(20, 1, 8), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1000, 0, 0), scale: new Vector3(10, 1, 6), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1212.5f, 0, 0), scale: new Vector3(7, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                //new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1250, 25, 0), scale: new Vector3(1, 4, 4), rotation: Matrix.CreateFromYawPitchRoll(-1.5707964f, 3.1415927f, 1.5707964f)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1250, 0, -175), scale: new Vector3(4, 1, 10), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1275, -24.5f, -362.5f), scale: new Vector3(2, 1, 6), rotation: Matrix.CreateFromYawPitchRoll(0, (float)System.Math.PI / -9, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1225, 0, -312.5f), scale: new Vector3(2, 1, 2), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1225, -24.5f, -400), scale: new Vector3(2, 1, 6), rotation: Matrix.CreateFromYawPitchRoll(0, (float)System.Math.PI / -9, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1250, -50, -537.5f), scale: new Vector3(10, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(1.5707964f, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1250, -50, -662.5f), scale: new Vector3(1, 3, 3), rotation: Matrix.CreateFromYawPitchRoll(3.926991f, 0, 1.5707964f)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1150, -50, -687.5f), scale: new Vector3(4, 1, 10), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1350, -50, -687.5f), scale: new Vector3(4, 1, 10), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1250, -50, -812.5f), scale: new Vector3(1, 7, 7), rotation: Matrix.CreateFromYawPitchRoll(0.7853983f, 0, 1.5707964f)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1237.5f, -75, -925), scale: new Vector3(6, 1, 6), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1237.5f, -100, -1012.5f), scale: new Vector3(4, 1, 6), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1237.5f, -125, -1162.5f), scale: new Vector3(3, 1, 7), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1225, -125, -1300), scale: new Vector3(1, 4, 4), rotation: Matrix.CreateFromYawPitchRoll(4.712389f, 0, 1.5707964f)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1025, -125, -1312.5f), scale: new Vector3(12, 1, 3), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-775, -125, -1312.5f), scale: new Vector3(4, 1, 3), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-625, -125, -1312.5f), scale: new Vector3(4, 1, 3), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-475, -125, -1312.5f), scale: new Vector3(4, 1, 3), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-375, -150, -1275), scale: new Vector3(2, 1, 6), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-287.5f, -175, -1250), scale: new Vector3(3, 1, 8), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-200, -200, -1225), scale: new Vector3(2, 1, 10), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(12.5f, -200, -1287.5f), scale: new Vector3(15, 1, 5), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-25, -200, -1125), scale: new Vector3(12, 1, 2), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(237.5f, -225, -1162.5f), scale: new Vector3(5, 1, 15), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(287.5f, -237.5f, -850), scale: new Vector3(3, 1, 10), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(262.5f, -250, -675), scale: new Vector3(3, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(250, -262.5f, -575), scale: new Vector3(1, 4, 4), rotation: Matrix.CreateFromYawPitchRoll(-4.712389f, 0, 1.5707964f)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-175, -262.5f, -537.5f), scale: new Vector3(30, 1, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-175, -262.5f, -600), scale: new Vector3(30, 1, 2), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-600, -287.5f, -575), scale: new Vector3(4, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-700, -312.5f, -575), scale: new Vector3(4, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(-1.5707964f, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-762.5f, -337.5f, -575), scale: new Vector3(1, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-787.5f, -362.5f, -575), scale: new Vector3(1, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-812.5f, -387.5f, -575), scale: new Vector3(1, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-875, -412.5f, -575), scale: new Vector3(4, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-875, -412.5f, -725), scale: new Vector3(4, 1, 6), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-875, -412.5f, -1500), scale: new Vector3(6, 1, 6), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-762.5f, -362.5f, -1737.5f), scale: new Vector3(1, 3, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-987.5f, -362.5f, -1737.5f), scale: new Vector3(1, 3, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-875, -312.5f, -1737.5f), scale: new Vector3(10, 2, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-875f, -412.5f, -1825f), scale: new Vector3(10f, 1f, 20f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-875, -412.5f, -875), scale: new Vector3(4, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-875, -412.5f, -975), scale: new Vector3(4, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-875, -412.5f, -1075), scale: new Vector3(4, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-875, -412.5f, -1175), scale: new Vector3(4, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-875, -412.5f, -1275), scale: new Vector3(4, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-875, -412.5f, -1375), scale: new Vector3(4, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-875f, -412.5f, -1825f), scale: new Vector3(10f, 1f, 20f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f))
            };
    }

    protected override void LoadObstacles()
    {
        Obstacles = new List<Obstacle>()
            {
            new Obstacle(GraphicsDevice, Content, Color.Blue, coordinates: new Vector3(-887.5f, 37.5f, 37.5f), scale: new Vector3(1, 2, 3), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0), movement: new Vector3(0f, 0f, -100)),
            new Obstacle(GraphicsDevice, Content, Color.Red, coordinates: new Vector3(-962.5f, 37.5f, -37.5f), scale: new Vector3(1, 2, 3), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0), movement: new Vector3(0f, 0f, 100)),
            new Obstacle(GraphicsDevice, Content, Color.Yellow, coordinates: new Vector3(-1037.5f, 37.5f, 37.5f), scale: new Vector3(1, 2, 3), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0), movement: new Vector3(0f, 0f, -100)),
            new Obstacle(GraphicsDevice, Content, Color.Blue, coordinates: new Vector3(-1250, -12.5f, -825), scale: new Vector3(2, 2, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0), movement: new Vector3(50f, 0f, 0f)),
            new Obstacle(GraphicsDevice, Content, Color.Blue, coordinates: new Vector3(-1200f, -87.5f, -1312.5f), scale: new Vector3(1.5f, 2, 0.5f), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0), movement: new Vector3(-50f, 0f, 25f)),
            new Obstacle(GraphicsDevice, Content, Color.Red, coordinates: new Vector3(-1075f, -87.5f, -1312.5f), scale: new Vector3(1.5f, 2, 0.5f), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0), movement: new Vector3(-50f, 0f, -25f)),
            new Obstacle(GraphicsDevice, Content, Color.Yellow, coordinates: new Vector3(-950f, -87.5f, -1312.5f), scale: new Vector3(1.5f, 2, 0.5f), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0), movement: new Vector3(-50f, 0f, 25f)),
            new Obstacle(GraphicsDevice, Content, Color.Blue, coordinates: new Vector3(-525, -225, -512.5f), scale: new Vector3(2, 2, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0), movement: new Vector3(0f, 0f, 100f)),
            new Obstacle(GraphicsDevice, Content, Color.Red, coordinates: new Vector3(-400, -225, -512.5f), scale: new Vector3(2, 2, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0), movement: new Vector3(0f, 0f, 50f)),
            new Obstacle(GraphicsDevice, Content, Color.Yellow, coordinates: new Vector3(-275, -225, -512.5f), scale: new Vector3(2, 2, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0), movement: new Vector3(0f, 0f, 50f)),
            new Obstacle(GraphicsDevice, Content, Color.Blue, coordinates: new Vector3(-875, -375f, -875), scale: new Vector3(2, 2, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0), movement: new Vector3(-75f, 0f, 0f)),
            new Obstacle(GraphicsDevice, Content, Color.Blue, coordinates: new Vector3(-875, -375f, -975), scale: new Vector3(2, 2, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0), movement: new Vector3(75f, 0f, 0f)),
            new Obstacle(GraphicsDevice, Content, Color.Red, coordinates: new Vector3(-875, -375f, -1075), scale: new Vector3(2, 2, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0), movement: new Vector3(-75f, 0f, 0f)),
            new Obstacle(GraphicsDevice, Content, Color.Red, coordinates: new Vector3(-875, -375f, -1175), scale: new Vector3(2, 2, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0), movement: new Vector3(75f, 0f, 0f)),
            new Obstacle(GraphicsDevice, Content, Color.Yellow, coordinates: new Vector3(-875, -375f, -1275), scale: new Vector3(2, 2, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0), movement: new Vector3(-75f, 0f, 0f)),
            new Obstacle(GraphicsDevice, Content, Color.Yellow, coordinates: new Vector3(-875, -375f, -1375), scale: new Vector3(2, 2, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0), movement: new Vector3(75f, 0f, 0f))
        };
    }

    protected override void LoadSigns()
    {
        Signs = new List<GeometricPrimitive>
        {

        };
    }

    protected override void LoadPickups()
    {
        Pickups = new List<Pickup>()
        {
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-387.5f, 25f, 0f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f,coordinates: new Vector3(-412.5f, 25f, 0f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f,coordinates: new Vector3(-437.5f, 25f, 0f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f,coordinates: new Vector3(-462.5f, 25f, 0f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f,coordinates: new Vector3(-487.5f, 25f, 0f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f,coordinates: new Vector3(-562.5f, 25f, 62.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f,coordinates: new Vector3(-587.5f, 25f, 62.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-612.5f, 25f, 62.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-637.5f, 25f, 62.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-687.5f, 25f, -62.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-712.5f, 25f, -62.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-737.5f, 25f, -62.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-762.5f, 25f, -62.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-1250f, 25f, -150f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-1250f, 25f, -175f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-1250f, 25f, -200f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-1250f, -25f, -550f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-1250f, -25f, -525f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-1250f, -25f, -500f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-1150f, -25f, -675f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-1150f, -25f, -700f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Antigravity(GraphicsDevice, Content, Color.Turquoise, 25f, coordinates: new Vector3(-1150f, -25f, -737.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new SpeedBoost(GraphicsDevice, Content, Color.Yellow, 25f, coordinates: new Vector3(-1350f, -25f, -737.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-1350f, -25f, -700f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-1350f, -25f, -675f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-1250f, -25f, -912.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-1250f, -50f, -987.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-1237.5f, -75f, -1050f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-1237.5f, -75f, -1075f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-1237.5f, -100f, -1125f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-1237.5f, -100f, -1150f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-1237.5f, -100f, -1175f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-1237.5f, -100f, -1200f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-937.5f, -100f, -1312.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-912.5f, -100f, -1312.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-787.5f, -100f, -1312.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-762.5f, -100f, -1312.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-637.5f, -100f, -1312.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-612.5f, -100f, -1312.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-487.5f, -100f, -1312.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-462.5f, -100f, -1312.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-162.5f, -175f, -1125f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-137.5f, -175f, -1125f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-112.5f, -175f, -1125f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-87.5f, -175f, -1125f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-62.5f, -175f, -1125f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-37.5f, -175f, -1125f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-12.5f, -175f, -1125f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(12.5f, -175f, -1125f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(37.5f, -175f, -1125f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(62.5f, -175f, -1125f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Ruby(GraphicsDevice, Content, Color.Red, 25f, coordinates: new Vector3(87.5f, -175f, -1125f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(100f, -175f, -1287.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(150f, -175f, -1287.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(50f, -175f, -1287.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(0f, -175f, -1287.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-50f, -175f, -1287.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-100f, -175f, -1287.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-150f, -175f, -1287.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(287.5f, -200f, -987.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(287.5f, -212.5f, -737.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(262.5f, -225f, -650f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(187.5f, -237.5f, -600f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(137.5f, -237.5f, -600f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(87.5f, -237.5f, -600f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(37.5f, -237.5f, -600f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-12.5f, -237.5f, -600f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-62.5f, -237.5f, -600f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-112.5f, -237.5f, -600f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-162.5f, -237.5f, -600f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-212.5f, -237.5f, -600f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-262.5f, -237.5f, -600f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-312.5f, -237.5f, -600f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-362.5f, -237.5f, -600f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-412.5f, -237.5f, -600f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-462.5f, -237.5f, -600f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-512.5f, -237.5f, -600f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Ruby(GraphicsDevice, Content, Color.Red, 25f, coordinates: new Vector3(-487.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Ruby(GraphicsDevice, Content, Color.Red, 25f, coordinates: new Vector3(-450f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Ruby(GraphicsDevice, Content, Color.Red, 25f, coordinates: new Vector3(-362.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Ruby(GraphicsDevice, Content, Color.Red, 25f, coordinates: new Vector3(-325f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-237.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-212.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-187.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-162.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-137.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-112.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-87.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-62.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-37.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-12.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(12.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(37.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(62.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(87.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(112.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(137.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(162.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new SpeedBoost(GraphicsDevice, Content, Color.Yellow, 25f, coordinates: new Vector3(187.5f, -237.5f, -537.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-600f, -262.5f, -575f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-712.5f, -287.5f, -575f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-875f, -387.5f, -575f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(-875f, -387.5f, -712.5f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f))
        };
    }

    protected override void LoadCheckpoints()
    {
        Checkpoints = new List<GeometricPrimitive>()
            {
                new CubePrimitive(GraphicsDevice, Content, Color.White, coordinates: new Vector3(-1250, 12.5f, -550), scale: new Vector3(8, 4, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),  // ESTE ES EL PRIMERO
                new CubePrimitive(GraphicsDevice, Content, Color.White, coordinates: new Vector3(-412.5f, -87.5f, -1250), scale: new Vector3(1, 4, 8), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)), // ESTE ES EL SEGUNDO
                new CubePrimitive(GraphicsDevice, Content, Color.White, coordinates: new Vector3(237.5f, -200, -625), scale: new Vector3(5, 4, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)), // ESTE ES EL TERCERO
                new CubePrimitive(GraphicsDevice, Content, Color.White, coordinates: new Vector3(-812.5f, -300, -575), scale: new Vector3(1, 6, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)), // ESTE ES EL CUARTO
                new CubePrimitive(GraphicsDevice, Content, Color.White, coordinates: new Vector3(-875, -337.5f, -1737.5f), scale: new Vector3(10, 5, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)) // ESTE ES EL QUINTO
                //new CubePrimitive(GraphicsDevice, Content, Color.DarkGray, coordinates: new Vector3(-875, -412.5f, -1762.5f), scale: new Vector3(10, 1, 15), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)) // ESTE ES EL ÚLTIMO
            };
    }

    public override void Update(GameTime gameTime)
    {
        foreach (Pickup pickup in Pickups)
        {
            pickup.Update(gameTime);
        }

        foreach (Obstacle obstacle in Obstacles)
        {
            Colliders.Remove(obstacle.Model.BoundingCube);
            obstacle.Update(gameTime);
            Colliders.Add(obstacle.Model.BoundingCube);
        }
    }

    public override List<GeometricPrimitive> GetModelListForShadowmapping()
    {
        List<GeometricPrimitive> models = new List<GeometricPrimitive>();

        models.AddRange(Track);
        models.AddRange(Signs);
        for (int i = 0; i < Obstacles.Count; i++)
        {
            models.Add(Obstacles[i].Model);
        }
        for (int i = 0; i < Pickups.Count; i++)
        {
            models.Add(Pickups[i].Model);
        }

        return models;
    }
}
