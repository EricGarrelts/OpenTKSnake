using System;
using System.Security.Cryptography.X509Certificates;
using OpenGLSnake.render;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace OpenGLSnake
{
	public class Program
	{
		public static Window? window;
		
		public static void Main(string[] args)
		{
			GameWindowSettings gws = GameWindowSettings.Default;
			NativeWindowSettings nws = NativeWindowSettings.Default;
			nws.WindowBorder = OpenTK.Windowing.Common.WindowBorder.Fixed;
			nws.Size = new Vector2i(500, 500); // must be square for grid system to work properly.
			window = new Window(gws, nws);
			window.Run();
		}
	}
}

