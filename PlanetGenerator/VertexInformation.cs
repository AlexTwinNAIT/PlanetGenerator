using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlanetGenerator
{
    public struct VertexInformation
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector4 Color;

        public VertexInformation(Vector3 position, Vector3 normal,Vector4 color)
        {
            Position = position;
            Normal = normal;
            Color = color;
        }
    }
}
