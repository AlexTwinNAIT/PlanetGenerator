# Planet Generator
 In this repository I aim to recreate David Linssen's Planetarium: https://managore.itch.io/planetarium in MonoGame.


# The Plan

The plan I've taken is to create a very simple 3D engine, capable of drawing a sphere to the screen. After that I will look into drawing a texture onto that sphere, and rotating it. I've indulged into [Matt Geurette's Video](https://www.youtube.com/watch?v=zsnZ8YqPY6o) on 3d Primitive in Monogame to teach me the absolute basics on how to render 3d objects in MonoGame.

Then I'll have to look into creating pseudo heightmaps (for water, sand, glass and rock) procedurally, and applying it to the sphere.

After I will need to figure out how to draw some cloud meshes procedurally.

lastly, the orbital bodies: Satellites and rings, and figuring out how to make them orbit at their own speeds.

I should also mention capabilities for users to generate, and customize their own planet (perhaps sliders for customization?)

# Features

If checked down, it's been implemented.

- [X] Test Draw Triangle (fixed perspective)
- [ ] Draw 3D Sphere (fixed perspective)
- [ ] Display Static Texture
- [ ] Rotate Texture
- [ ] Procedurally Generated Textures
- [ ] Procedurally Generated Clouds
- [ ] Satellites
- [ ] Rings
- [ ] Orbit (per body)
- [ ] User Customization
- [ ] Clipboard for sharing?

# My Understandings of This Project

### Disclaimer: My knowledge of these topics are limited and may be incorrect.

## Initializing and running the test version of the 3D Renderer

After following Matt Guerrette's Video, I got code which taught me how to do a couple of the items on my list. However I haven't implemtented rotating. Let's get into it.

We first start by defnining a vew variables

1. A VertexPositionColor array, which stores vertices.
2. A BasicEffect, which applies effects to vertices while rendering? (unsure) 
3. A VertexBuffer, which handles sending predefined vertex information to the GPU.

```c#
private VertexPositionColor[] _verts;
private BasicEffect _effect;
private VertexBuffer _vertexBuffer;
```

---

Second we initialize all of this to prepare for the runtime phase of the application.

We go and set up the predefined vertices, right now it's just a triangle.

We initialize the effect.

We initialize the bugger by sending it information, such as how many vertices we have.

```cs
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
```
---

**Now onto the update function.**

We start by creating our projection, *think of it as going into a digital camera's settings and configuring it for our photoshoot.*

We start by using `Matrix.CreatePersppectiveFieldOFView()` and defining the field of view as the first parameter, because it uses radians, we return PiOver4 (0.78...), we define the aspect ratio, using the viewport's information, then the viewplanes.

The far viewplane is how far the camera can see, anything further than that and it won't be rendered. The near viewplane is how close an object can be without getting cut off.

The reason we're doing this is because we must prioritize performance when programming visuals. by limiting the distance we can see, we limit the processing power used to render the camera's perspective.

The view is the origin of the camera, and it's sense of direction. *Think of it as the camera being set into a tri-pod.*

Using `Matrix.CreateLookAt()`, we'll get information that the view can use to define the camera's position,  then it's direction (where it's facing) and lastly, the upwards direction, relative to the camera.

Finally we must define the world, and how the object should be drawn. *Think of it as the subject we're capturing with our camera.*

Using `Matrix.Identity` we get the sort of deafault value. I'm still a bit unsure.

Now we draw apply the effect passes and apply it. Then we draw the primitives/triangles 

```cs
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
```

and just like that we've rendered...

![First Render](https://i.imgur.com/sId4j2Y.png)

**A red triangle.**

## (sort of) Definitions

### Mesh

A mesh is a group of vertices, edges and faces that make up a 3D object, typically every object is formed through a collection of triangles (1)

A triangle is an object that has the minimum amount of verticies that can have a face.

### Vertexes/Vertices

Vertices are points in 3D space that define a model's shape. (2)

If you dont know, every object in a game or really just in computer generated graphics are just a bunch of points in 3D space. One of those points is a Vertex. 

Vertices usually have these attributes: (2)

- Vector (Position)
- Color
- Reflectance
- Texture Coordinates
- Normal Vectors
- Tangent Vectors

and a couple other we aren't too worried about.

### Vertex Buffer

To my understanding a Vertex Buffer is a class that handles taking a vertex's information (Normals,Texture Coords,...) and sending it to the GPU to be handled further. (3)



# References

## Unlisted
Commit names are references to Chris Christodoulou tracks.

## Listed
1. [Introduction to Computer Graphics, Harold Serrano](https://www.haroldserrano.com/blog/before-using-metal-computer-graphics-basics)
2. [Vertex (Computer Graphics), Wikipedia](https://en.wikipedia.org/wiki/Vertex_(computer_graphics)) -- No references, take with grain of salt.
3. [Vertex Buffer Object](https://relativity.net.au/gaming/java/VertexBufferObject.html)