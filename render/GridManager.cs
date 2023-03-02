using System;
using System.Drawing;
using OpenTK.Graphics.ES20;
using OpenTK.Mathematics;

namespace OpenGLSnake.render
{
	public class GridManager
	{
        public static int Span = 12; // How many pixels in a row and column
        private static List<Pixel> pixelList = new List<Pixel>();

		// Initializes the grid adds all values to pixelList
        public void init()
		{
			for (int yi = 0; yi < Span; yi++) // Y value of pixel
            {
				float translateY = yi * Pixel.size; 
				for (int xi = 0; xi < Span; xi++) // X value of pixel
				{
					float translateX = xi * Pixel.size;
					pixelList.Add(new Pixel(translateX, translateY));
				}
			}
		}

		// iterates and calls the .render method of Pixel.cs to draw to screen
		public void render()
		{
			for (int i = 0; i < pixelList.Count; i++)
			{
				pixelList[i].render(pixelList[i].color);
			}
		}

		// render method using index. runs Pixel.toggle() and sets Pixel.color
		public void toggle(int index, Color4 color)
		{
			if (index < 0) index = 0;
			if (index > Span * Span - 1) index = Span * Span - 1;
            pixelList[index].color = color;
            pixelList[index].toggle();
		}

		// render method using 2d grid, top left is 0,0 bottom right is GridManager.Span-1
		public void toggle(int x, int y, Color4 color)
		{
            int index = y * Span + x; // translates X,Y coords to int
			pixelList[index].color = color;
            pixelList[index].toggle();
        }
	}
}

