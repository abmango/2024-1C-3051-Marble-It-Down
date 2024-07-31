using BepuPhysics.Trees;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TGC.MonoGame.TP.Camera;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP
{
    internal class LightManager
    {
        // dependencias
        private ContentManager Content;
        private GraphicsDevice Graphics;

        private const int ShadowmapSize = 2048;
        private RenderTarget2D ShadowMapRenderTarget;
        private Effect ShadowMapEffect { get; set; }

        // cámara
        private TargetCamera TargetLightCamera { get; set; }
        private readonly float LightCameraFarPlaneDistance = 3000f;
        private readonly float LightCameraNearPlaneDistance = 5f;

        // posición

        private Vector3 LightPosition = Vector3.One * 500f;
        private float Timer;

        public LightManager (ContentManager content, GraphicsDevice graphicsDevice) 
        {
            Content = content;
            Graphics = graphicsDevice;
        }

        public void Initialize()
        {
            TargetLightCamera = new TargetCamera(1f, LightPosition, Vector3.Zero);
            TargetLightCamera.BuildProjection(1f, LightCameraNearPlaneDistance, LightCameraFarPlaneDistance, MathHelper.PiOver2);
        }

        public void LoadContent()
        {
            // Load the shadowmap effect
            ShadowMapEffect = Content.Load<Effect>(TGCGame.ContentFolderEffects + "ShadowMap");
        }

        public void Update(GameTime gameTime)
        {
            UpdateLightPosition((float)gameTime.ElapsedGameTime.TotalSeconds);

            TargetLightCamera.Position = LightPosition;
            TargetLightCamera.BuildView();
        }
        private void UpdateLightPosition(float elapsedTime)
        {
            LightPosition = new Vector3(MathF.Cos(Timer) * 2000f, MathF.Sin(Timer) * 2000f, 0f);
            Timer += elapsedTime * 0.025f;
        }

        public void DrawShadows(Matrix view, Matrix projection, List<GeometricPrimitive> models)
        {
            #region Pass 1

            Graphics.DepthStencilState = DepthStencilState.Default;
            // Set the render target as our shadow map, we are drawing the depth into this texture
            Graphics.SetRenderTarget(ShadowMapRenderTarget);
            Graphics.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);

            ShadowMapEffect.CurrentTechnique = ShadowMapEffect.Techniques["DepthPass"];
  
            // We get the base transform for each mesh
            foreach (var model in models)
            {
                //model.Effect = ShadowMapEffect;

                // We set the main matrices for each mesh to draw
                var worldMatrix = model.World;

                // WorldViewProjection is used to transform from model space to clip space
                ShadowMapEffect.Parameters["WorldViewProjection"].SetValue(worldMatrix * TargetLightCamera.View * TargetLightCamera.Projection);

                // Once we set these matrices we draw
                //model.Draw(TargetLightCamera.View, TargetLightCamera.Projection);
                model.Draw(ShadowMapEffect);
            }

            #endregion

            #region Pass 2

            // Set the render target as null, we are drawing on the screen!
            Graphics.SetRenderTarget(null);
            Graphics.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.CornflowerBlue, 1f, 0);

            ShadowMapEffect.CurrentTechnique = ShadowMapEffect.Techniques["DrawShadowedPCF"];
            //ShadowMapEffect.Parameters["baseTexture"].SetValue(BasicEffect.Texture);
            ShadowMapEffect.Parameters["shadowMap"].SetValue(ShadowMapRenderTarget);
            ShadowMapEffect.Parameters["lightPosition"].SetValue(LightPosition);
            ShadowMapEffect.Parameters["shadowMapSize"].SetValue(Vector2.One * ShadowmapSize);
            ShadowMapEffect.Parameters["LightViewProjection"].SetValue(TargetLightCamera.View * TargetLightCamera.Projection);
            foreach (var model in models)
            {
                //model.Effect = ShadowMapEffect;

                // We set the main matrices for each mesh to draw
                var worldMatrix = model.World;

                // WorldViewProjection is used to transform from model space to clip space
                ShadowMapEffect.Parameters["WorldViewProjection"].SetValue(worldMatrix * view * projection);
                ShadowMapEffect.Parameters["World"].SetValue(worldMatrix);
                ShadowMapEffect.Parameters["InverseTransposeWorld"].SetValue(Matrix.Transpose(Matrix.Invert(worldMatrix)));

                // Once we set these matrices we draw
                //model.Draw(view, projection);
                model.Draw(ShadowMapEffect);
            }



            #endregion
        }


    }
}
