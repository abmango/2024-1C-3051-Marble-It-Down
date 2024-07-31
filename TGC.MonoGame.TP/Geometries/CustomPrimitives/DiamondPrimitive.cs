#region File Description

//-----------------------------------------------------------------------------
// CubePrimitive.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TGC.MonoGame.TP.Collisions;

#endregion Using Statements

namespace TGC.MonoGame.TP.Geometries {

    public class DiamondPrimitive : CustomPrimitive {
        
        public DiamondPrimitive(GraphicsDevice graphicsDevice, ContentManager content, Color color, float size = 25f, Vector3? coordinates = null, Vector3? scale = null, Matrix? rotation = null) {
            
            Effect = content.Load<Effect>(ContentFolderEffects + "PBR_superficie");
            surfaceTexture = content.Load<Texture2D>(ContentFolderTextures + "materials/ruby/color");
            normalTexture = content.Load<Texture2D>(ContentFolderTextures + "materials/ruby/normal");

            Color = color;

            Vector3[] vertexList =
            {
                new Vector3(-0.25f, 1f, (float) -Math.Sqrt(3)/4),
                new Vector3(-0.5f, 1f, 0f),
                new Vector3(-0.25f, 1f,  (float) Math.Sqrt(3)/4),
                new Vector3(0.25f, 1f,  (float) Math.Sqrt(3)/4),
                new Vector3(0.5f, 1f, 0f),
                new Vector3(0.25f, 1f,  (float) -Math.Sqrt(3)/4),

                new Vector3(-0.5f, 0.4f, (float) -Math.Sqrt(3)/2),
                new Vector3(-1f, 0.4f, 0f),
                new Vector3(-0.5f, 0.4f, (float) Math.Sqrt(3)/2),
                new Vector3(0.5f, 0.4f, (float) Math.Sqrt(3)/2),
                new Vector3(1f, 0.4f, 0f),
                new Vector3(0.5f, 0.4f, (float) -Math.Sqrt(3)/2),

                new Vector3(0f, -1f, 0f)
            };

            // top
            AddTriangle(vertexList[0], vertexList[1], vertexList[2], size, color);
            AddTriangle(vertexList[0], vertexList[2], vertexList[5], size, color);
            AddTriangle(vertexList[3], vertexList[5], vertexList[2], size, color);
            AddTriangle(vertexList[4], vertexList[5], vertexList[3], size, color);

            // middle
            AddTriangle(vertexList[0], vertexList[6], vertexList[1], size, color);
            AddTriangle(vertexList[7], vertexList[1], vertexList[6], size, color);
            AddTriangle(vertexList[1], vertexList[7], vertexList[2], size, color);
            AddTriangle(vertexList[8], vertexList[2], vertexList[7], size, color);
            AddTriangle(vertexList[2], vertexList[8], vertexList[3], size, color);
            AddTriangle(vertexList[9], vertexList[3], vertexList[8], size, color);
            AddTriangle(vertexList[3], vertexList[9], vertexList[4], size, color);
            AddTriangle(vertexList[10], vertexList[4], vertexList[9], size, color);
            AddTriangle(vertexList[4], vertexList[10], vertexList[5], size, color);
            AddTriangle(vertexList[11], vertexList[5], vertexList[10], size, color);
            AddTriangle(vertexList[5], vertexList[11], vertexList[0], size, color);
            AddTriangle(vertexList[6], vertexList[0], vertexList[11], size, color);

            //// bottom
            AddTriangle(vertexList[12], vertexList[11], vertexList[10], size, color);
            AddTriangle(vertexList[12], vertexList[10], vertexList[9], size, color);
            AddTriangle(vertexList[12], vertexList[9], vertexList[8], size, color);
            AddTriangle(vertexList[12], vertexList[8], vertexList[7], size, color);
            AddTriangle(vertexList[12], vertexList[7], vertexList[6], size, color);
            AddTriangle(vertexList[12], vertexList[6], vertexList[11], size, color);

            World = Matrix.CreateScale(scale ?? Vector3.One) * (rotation ?? Matrix.Identity) * Matrix.CreateTranslation(coordinates ?? Vector3.Zero);

            BoundingCube = new OrientedBoundingBox(coordinates ?? Vector3.Zero, (scale ?? Vector3.One) * 25 / 2);
            BoundingCube.Rotate(rotation ?? Matrix.Identity);

            InitializePrimitive(graphicsDevice, content);
        }

    }
}