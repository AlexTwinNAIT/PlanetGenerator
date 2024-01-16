using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PlanetGenerator
{
    public class SpherePrimitive : GeometricObject
    {
        public SpherePrimitive(GraphicsDevice graphicsDevice) : this(graphicsDevice,1,16){}

        public SpherePrimitive(GraphicsDevice device, float diameter, int tessellation)
        {
            if (tessellation < 3) // tessellation must be greater than 3 to generate a sphere.
            {
                throw new ArgumentOutOfRangeException("tessellation");
            }

            int vertSegments = tessellation;
            int horiSegments = tessellation * 2;

            float radius = diameter / 2;
            // incomplete.

        }
    }

}
