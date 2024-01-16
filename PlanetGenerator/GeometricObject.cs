using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlanetGenerator
{
    public abstract class GeometricObject
    {
        private VertexPositionColorNormal[] _verts;
        private BasicEffect _effect;
        private VertexBuffer _vertexBuffer;
        private GraphicsDevice graphicsDevice;

        protected void Initialize()
        {

            _effect = new BasicEffect(graphicsDevice);

            //create our vertex buffer

            _vertexBuffer = new VertexBuffer(graphicsDevice, VertexPositionColor.VertexDeclaration, _verts.Length, BufferUsage.WriteOnly);

            //set the vertices
            _vertexBuffer.SetData(_verts);
        }

        protected void Draw()
        {
            //the projection matrix defines the camera of which we see our world - Matt Guerette

            // near plane < how close an object can be

            // far plane < how far an object can be

            _effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, graphicsDevice.Viewport.AspectRatio, 0.001f, 100.0f);

            //view is the origin of the camera, position, and up direction

            _effect.View = Matrix.CreateLookAt(new Vector3(0, 0, -5), Vector3.Forward, Vector3.Up);

            // world defines how the object should be drawn

            _effect.World = Matrix.Identity;
            _effect.VertexColorEnabled = true;

            //going through each pass to apply it on the GPU

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _verts, 0, 1);
            }
        }

    }


}
