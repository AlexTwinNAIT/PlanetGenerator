using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetGenerator
{
    public class SpherePrimitive
    {
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
