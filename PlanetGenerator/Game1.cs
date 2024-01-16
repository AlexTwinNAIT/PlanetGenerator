﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text.RegularExpressions;

namespace PlanetGenerator
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private VertexPositionColor[] _verts;
        private BasicEffect _effect;
        private VertexBuffer _vertexBuffer;


        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            //Setup verticies
            _verts = new VertexPositionColor[3]
            {
                new VertexPositionColor(new Vector3(0.0f,1.0f,0.0f), Color.Red),
                new VertexPositionColor(new Vector3(-1.0f,-1.0f,0.0f), Color.Red),
                new VertexPositionColor(new Vector3(1.0f,-1.0f,0.0f), Color.Red)

            };

            // initialize the effect
            _effect = new BasicEffect(GraphicsDevice);

            //create our vertex buffer

            _vertexBuffer = new VertexBuffer(GraphicsDevice, VertexPositionColor.VertexDeclaration, _verts.Length, BufferUsage.WriteOnly);

            //set the vertices
            _vertexBuffer.SetData(_verts);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //the projection matrix defines the camera of which we see our world - Matt Guerette

            // near plane < how close an object can be

            // far plane < how far an object can be

            _effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.001f, 100.0f);

            //view is the origin of the camera, position, and up direction

            _effect.View = Matrix.CreateLookAt(new Vector3(0, 0, -5), Vector3.Forward, Vector3.Up);

            // world defines how the object should be drawn

            _effect.World = Matrix.Identity;
            _effect.VertexColorEnabled = true;

            //going through each pass to apply it on the GPU

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, _verts, 0, 1);
            }

            base.Draw(gameTime);
        }
    }
}
