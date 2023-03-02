using System;
using System.Drawing;
using OpenTK.Mathematics;

namespace OpenGLSnake.render
{
	public class Pixel: Renderable
	{
		private float translateX;
		private float translateY;
		public static float size;
		private float[] vertices;
		public Color4 color;

        private uint[] indices =
			{
				0,3,2,
				0,1,3
			};

		public Pixel(float translateX, float translateY)
		{
			// converts Pixel dimensions into screen space dimensions
            size = ((Window.size.X / GridManager.Span) / Window.size.X)*2;
            float[] verticesTemplate =
			{
				-1.0f, 1.0f, 0f, // top left
				-1.0f + size, 1.0f, 0f, // top right
				-1.0f, 1.0f - size, 0f, // bottom left
				-1.0f + size, 1.0f-size, 0f // bottom right				
			};

			// sets the coords of the pixel's vertices
			vertices = verticesTemplate;
            this.translateX = translateX;
			this.translateY = translateY;
			vertices[0] += translateX;
			vertices[3] += translateX;
			vertices[6] += translateX;
			vertices[9] += translateX;
			vertices[1] -= translateY;
			vertices[4] -= translateY;
			vertices[7] -= translateY;
			vertices[10] -= translateY;
			init();
		}

		public void init()
		{
            load(vertices, indices); // Runs Renderable.load(), initializes VBO with vertices data.
        }

		// Toggles conditional in Renderable.render()
		public void toggle()
		{
			this.on = !this.on;
		}
	}
}

