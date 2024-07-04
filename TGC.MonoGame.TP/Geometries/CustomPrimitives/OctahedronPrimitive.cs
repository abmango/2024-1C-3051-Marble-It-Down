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

    public class OctahedronPrimitive : CustomPrimitive {
        
        public OctahedronPrimitive(GraphicsDevice graphicsDevice, ContentManager content, Color color, float size = 25f, Vector3? coordinates = null, Vector3? scale = null, Matrix? rotation = null) {

            Color = color;

            Vector3[] vertexList =
            {
                new Vector3(0f, 1f, 0f),
                new Vector3(-1f, 0, -1f),
                new Vector3(1f, 0, -1f),
                new Vector3(-1f, 0, 1),
                new Vector3(1f, 0, 1f),
                new Vector3(0f, -1f, 0f)
            };

            // top
            AddTriangle(vertexList[0], vertexList[1], vertexList[3], size, color);
            AddTriangle(vertexList[0], vertexList[3], vertexList[4], size, color);
            AddTriangle(vertexList[0], vertexList[4], vertexList[2], size, color);
            AddTriangle(vertexList[0], vertexList[2], vertexList[1], size, color);

            // bottom
            AddTriangle(vertexList[5], vertexList[3], vertexList[1], size, color);
            AddTriangle(vertexList[5], vertexList[4], vertexList[3], size, color);
            AddTriangle(vertexList[5], vertexList[2], vertexList[4], size, color);
            AddTriangle(vertexList[5], vertexList[1], vertexList[2], size, color);

            World = Matrix.CreateScale(scale ?? Vector3.One) * (rotation ?? Matrix.Identity) * Matrix.CreateTranslation(coordinates ?? Vector3.Zero);

            BoundingCube = new OrientedBoundingBox(coordinates ?? Vector3.Zero, (scale ?? Vector3.One) * 25 / 2);
            BoundingCube.Rotate(rotation ?? Matrix.Identity);

            InitializePrimitive(graphicsDevice, content);
        }

    }
}