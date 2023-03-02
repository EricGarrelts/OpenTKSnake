using System;
using System.Runtime.CompilerServices;
using OpenGLSnake.render;
using OpenTK.Mathematics;

namespace OpenGLSnake
{
	public class Apple
	{

		public static Vector2i position;

		public Apple()
		{
            if (Window.snake == null) return;
            Random random = new Random();
            position.X = random.Next(GridManager.Span);
            position.Y = random.Next(GridManager.Span);
            if (Window.snake.containsSnake(position))
            {
                randomize();
                return;
            }
            draw();
        }

		public static void randomize()
		{
			draw();
            if (Window.snake == null) return;
			Random random = new Random();
			position.X = random.Next(GridManager.Span);
			position.Y = random.Next(GridManager.Span);
			while (Window.snake.containsSnake(position))
			{
                position.X = random.Next(GridManager.Span);
                position.Y = random.Next(GridManager.Span);
            }
			draw();
		}

		private static void draw()
		{
			Window.gm.toggle(position.X, position.Y, Color4.Red);
		}
	}
}

