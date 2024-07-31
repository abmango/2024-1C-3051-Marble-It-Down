#region File Description

//-----------------------------------------------------------------------------
// GeometricPrimitive.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion Using Statements

namespace TGC.MonoGame.TP.Geometries
{
    public struct VertexPositionColorNormalTexture : IVertexType
    {
        public Vector3 Position;
        public Color Color;
        public Vector3 Normal;
        public Vector2 TextureCoordinate;

        public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration
        (
            new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
            new VertexElement(sizeof(float) * 3, VertexElementFormat.Color, VertexElementUsage.Color, 0),
            new VertexElement(sizeof(float) * 3 + 4, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
            new VertexElement(sizeof(float) * 6 + 4, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
        );

        VertexDeclaration IVertexType.VertexDeclaration => VertexDeclaration;

        public VertexPositionColorNormalTexture(Vector3 position, Color color, Vector3 normal, Vector2 textureCoordinate)
        {
            Position = position;
            Color = color;
            Normal = normal;
            TextureCoordinate = textureCoordinate;
        }
    }



    /// <summary>
    ///     Base class for simple geometric primitive models. This provides a vertex buffer, an index buffer, plus methods for
    ///     drawing the model. Classes for specific types of primitive (CubePrimitive, SpherePrimitive, etc.) are derived from
    ///     this common base, and use the AddVertex and AddIndex methods to specify their geometry.
    /// </summary>
    /// 
    public abstract class GeometricPrimitive : Entity
    {
        #region Fields

        public const string ContentFolderEffects = "Effects/";

        public const string ContentFolderTextures = "Textures/";

        // During the process of constructing a primitive model, vertex and index data is stored on the CPU in these managed lists.
        //public List<VertexPositionColorNormal> Vertices { get; } = new List<VertexPositionColorNormal>();

        List<VertexPositionColorNormalTexture> Vertices = new List<VertexPositionColorNormalTexture>();


        public List<ushort> Indices { get; } = new List<ushort>();

        // Once all the geometry has been specified, the InitializePrimitive method copies the vertex and index data into these buffers,
        // which store it on the GPU ready for efficient rendering.
        private VertexBuffer VertexBuffer { get; set; }

        private IndexBuffer IndexBuffer { get; set; }
        public Effect Effect { get; set; }

        public Effect Effect2{get;set;}

        public Matrix World { get; set; }
        public Color Color { get; set; }


        #endregion Fields

        #region Initialization

        /// <summary>
        ///     Adds a new vertex to the primitive model. This should only be called during the initialization process, before
        ///     InitializePrimitive.
        /// </summary>
        protected void AddVertex(Vector3 position, Color color, Vector3 normal, Vector2 texCoord)
        {
            Vertices.Add(new VertexPositionColorNormalTexture(position, color, normal, texCoord));
        }

        /// <summary>
        ///     Adds a new index to the primitive model. This should only be called during the initialization process, before
        ///     InitializePrimitive.
        /// </summary>
        protected void AddIndex(int index)
        {
            if (index > ushort.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(index));

            Indices.Add((ushort)index);
        }

        /// <summary>
        ///     Queries the index of the current vertex. This starts at zero, and increments every time AddVertex is called.
        /// </summary>
        protected int CurrentVertex => Vertices.Count;

        /// <summary>
        ///     Once all the geometry has been specified by calling AddVertex and AddIndex, this method copies the vertex and index
        ///     data into GPU format buffers, ready for efficient rendering.
        /// </summary>
        /// 

        public Texture2D surfaceTexture;
        public Texture2D normalTexture;

        protected virtual void InitializePrimitive(GraphicsDevice graphicsDevice, ContentManager content, Effect? primitiveEffect = null)
        {
            // Create a vertex declaration, describing the format of our vertex data.

            // Create a vertex buffer, and copy our vertex data into it.
            VertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColorNormalTexture), Vertices.Count, BufferUsage.None);
            VertexBuffer.SetData(Vertices.ToArray());

            // Create an index buffer, and copy our index data into it.
            IndexBuffer = new IndexBuffer(graphicsDevice, typeof(ushort), Indices.Count, BufferUsage.None);
            IndexBuffer.SetData(Indices.ToArray());

            //Effect = content.Load<Effect>(ContentFolderEffects + "PBR");
            Effect = content.Load<Effect>(ContentFolderEffects+"PBR_superficie");
            Effect.CurrentTechnique = Effect.Techniques["PBR"];

            //surfaceTexture = content.Load<Texture2D>(ContentFolderTextures + "materials/marble/color");
            //normalTexture = content.Load<Texture2D>(ContentFolderTextures + "materials/marble/normal");
        }


        /// <summary>
        ///     Finalizer.
        /// </summary>
        ~GeometricPrimitive()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Frees resources used by this object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Frees resources used by this object.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            VertexBuffer?.Dispose();
            IndexBuffer?.Dispose();
            Effect?.Dispose();
        }

        #endregion Initialization

        #region Draw

        /// <summary>
        ///     Draws the primitive model, using the specified effect. Unlike the other Draw overload where you just specify the
        ///     world/view/projection matrices and color, this method does not set any render states, so you must make sure all
        ///     states are set to sensible values before you call it.
        /// </summary>
        /// <param name="effect">Used to set and query effects, and to choose techniques.</param>
        public void Draw(Effect effect)
        {
            var graphicsDevice = effect.GraphicsDevice;

            // Set our vertex declaration, vertex buffer, and index buffer.
            graphicsDevice.SetVertexBuffer(VertexBuffer);

            graphicsDevice.Indices = IndexBuffer;

            foreach (var effectPass in effect.CurrentTechnique.Passes)
            {
                effectPass.Apply();

                var primitiveCount = Indices.Count / 3;

                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, primitiveCount);
            }
        }

        public virtual void Draw(Matrix view, Matrix projection)
        {
            // Set Effect parameters.
            /*Effect.Parameters["World"].SetValue(World);
            Effect.Parameters["View"].SetValue(view);
            Effect.Parameters["Projection"].SetValue(projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.ToVector3());*/
            /*
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
            //Effect.Parameters["lightPosition"].SetValue(Position + new Vector3(0, 30, 0));
            //Effect.Parameters["lightPosition"].SetValue( new Vector3(25, 140, -800));
            Effect.Parameters["lightColor"].SetValue(new Vector3(253, 251, 211));
            Effect.Parameters["normalTexture"]?.SetValue(normalTexture);
            Effect.Parameters["albedoTexture"]?.SetValue(surfaceTexture);
            // Draw the model.
            Draw(Effect);
        }

        #endregion Draw
    }
}