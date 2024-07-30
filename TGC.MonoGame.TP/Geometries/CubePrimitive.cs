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

namespace TGC.MonoGame.TP.Geometries
{
    /// <summary>
    ///     Geometric primitive class for drawing cubes.
    /// </summary>
    public class CubePrimitive : GeometricPrimitive
    {

        /// <summary>
        ///     Constructs a new cube primitive.
        /// </summary>
        /// 
        public OrientedBoundingBox BoundingCube { get; set; }


        public override void Draw(Matrix view, Matrix projection)
        {
            // Set Effect parameters.
            /*Effect.Parameters["World"].SetValue(World);
            Effect.Parameters["View"].SetValue(view);
            Effect.Parameters["Projection"].SetValue(projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.ToVector3());

            if (surfaceTexture == null)
            {
                throw new ArgumentNullException(nameof(surfaceTexture), "surfaceTexture no puede ser nulo.");
            }

            // Verificación de que el parámetro ColorTexture exista
            var colorTextureParameter = Effect.Parameters["ColorSampler+ColorTexture"]; //no preguntes, tampoco tengo idea
            if (colorTextureParameter == null)
            {
                throw new InvalidOperationException("No se encontró el parámetro 'ColorTexture' en el shader.");
            }

            Effect.Parameters["ColorSampler+ColorTexture"].SetValue(surfaceTexture);
            Effect.Parameters["NormalMap"].SetValue(normalTexture);*/

            Effect.Parameters["matWorld"].SetValue(World);
            Effect.Parameters["matWorldViewProj"].SetValue(World * view * projection);
            Effect.Parameters["matInverseTransposeWorld"].SetValue(Matrix.Transpose(Matrix.Invert(World)));
            Effect.Parameters["lightPosition"].SetValue(Position + new Vector3(0, 60, 0));
            Effect.Parameters["lightColor"].SetValue(new Vector3(253, 251, 211));
            Effect.Parameters["normalTexture"]?.SetValue(normalTexture);
            Effect.Parameters["albedoTexture"]?.SetValue(surfaceTexture);
            // Draw the model.
            Draw(Effect);
        }
        public CubePrimitive(
                GraphicsDevice graphicsDevice,
                ContentManager content,
                Color color,
                float size = 25f,
                Vector3? coordinates = null,
                Vector3? scale = null,
                Matrix? rotation = null,
                string? Text = ""
            )
        {

            Color = color;
            Position = (Vector3)coordinates;
            

            // A cube has six faces, each one pointing in a different direction.
            Vector3[] normals =
            {
                // front normal
                Vector3.UnitZ,
                // back normal
                -Vector3.UnitZ,
                // right normal
                Vector3.UnitX,
                // left normal
                -Vector3.UnitX,
                // top normal
                Vector3.UnitY,
                // bottom normal
                -Vector3.UnitY
            };

            

            var i = 0;
            // Create each face in turn.
            foreach (var normal in normals)
            {
                // Get two vectors perpendicular to the face normal and to each other.
                var side1 = new Vector3(normal.Y, normal.Z, normal.X);
                var side2 = Vector3.Cross(normal, side1);

                // Six indices (two triangles) per face.
                AddIndex(CurrentVertex + 0);
                AddIndex(CurrentVertex + 1);
                AddIndex(CurrentVertex + 2);

                AddIndex(CurrentVertex + 0);
                AddIndex(CurrentVertex + 2);
                AddIndex(CurrentVertex + 3);

                // Four vertices per face.
                AddVertex((normal - side1 - side2) * size / 2, color, normal, new Vector2(0,0));
                AddVertex((normal - side1 + side2) * size / 2, color, normal, new Vector2(0,1));
                AddVertex((normal + side1 + side2) * size / 2, color, normal, new Vector2(1,1));
                AddVertex((normal + side1 - side2) * size / 2, color, normal, new Vector2(1,0));

                i++;
            }

            World = Matrix.CreateScale(scale ?? Vector3.One) * (rotation ?? Matrix.Identity) * Matrix.CreateTranslation(coordinates ?? Vector3.Zero);
            
            BoundingCube = new OrientedBoundingBox(coordinates ?? Vector3.Zero, (scale ?? Vector3.One) * 25 / 2);
            BoundingCube.Rotate(rotation ?? Matrix.Identity);
                
            InitializePrimitive(graphicsDevice, content);
            Effect = content.Load<Effect>(ContentFolderEffects + "PBR_superficie");
            surfaceTexture = content.Load<Texture2D>(ContentFolderTextures + "materials/gold but wood/color");
            normalTexture = content.Load<Texture2D>(ContentFolderTextures + "materials/gold but wood/normal");
        }
    }
}