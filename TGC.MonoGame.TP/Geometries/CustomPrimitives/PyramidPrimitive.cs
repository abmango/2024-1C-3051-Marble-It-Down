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

    public class PyramidPrimitive : CustomPrimitive {
        
        public PyramidPrimitive(GraphicsDevice graphicsDevice, ContentManager content, Color color, float size = 25f, Vector3? coordinates = null, Vector3? scale = null, Matrix? rotation = null) {

            Color = color;

            Vector3[] vertexList =
            {
                new Vector3(0f, 0.5f, 0f),
                new Vector3(-1f, -1f, -1f),
                new Vector3(1f, -1f, -1f),
                new Vector3(-1f, -1f, 1),
                new Vector3(1f, -1f, 1f)
            };

            // bottom
            AddTriangle(vertexList[1], vertexList[2], vertexList[3], size, color);
            AddTriangle(vertexList[4], vertexList[3], vertexList[2], size, color);

            // sides
            AddTriangle(vertexList[0], vertexList[1], vertexList[3], size, color);
            AddTriangle(vertexList[0], vertexList[3], vertexList[4], size, color);
            AddTriangle(vertexList[0], vertexList[4], vertexList[2], size, color);
            AddTriangle(vertexList[0], vertexList[2], vertexList[1], size, color);

            World = Matrix.CreateScale(scale ?? Vector3.One) * (rotation ?? Matrix.Identity) * Matrix.CreateTranslation(coordinates ?? Vector3.Zero);

            BoundingCube = new OrientedBoundingBox(coordinates ?? Vector3.Zero, (scale ?? Vector3.One) * 25 / 2);
            BoundingCube.Rotate(rotation ?? Matrix.Identity);

            InitializePrimitive(graphicsDevice, content);
        }
    }
}