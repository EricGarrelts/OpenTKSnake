using System;
namespace OpenGLSnake.render
{
	public class TickManager
	{
		private float tickInterval = 0.1f; // update interval in seconds;
		public float intervalSum = 0.0f;
		public bool stopped = false;


		// Increments the game clock upon intervalSum >= tickInterval, the gameloop updates.
		// TL:DR - Turns constant time into discrete time.
		public void increment(float time)
		{
			if (stopped) return;
			intervalSum += time;
			if (intervalSum >= tickInterval)
			{
				if (Program.window == null) return;
				Program.window.setInput();
				intervalSum = 0.0f;
				update();
			}
		}

		// upon a new tick, what functions will be updated?
		// put methods here.
		public void update()
		{
			if (Window.snake != null) Window.snake.update(); 
		}

	}
}

