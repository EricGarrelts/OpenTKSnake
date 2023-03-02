using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace OpenGLSnake.render
{
    public class Window : GameWindow
    {

        public static Vector2 size; // window size
        public static int shaderProgram;
        public static GridManager gm = new GridManager();
        public static TickManager tm = new TickManager();
        public static Snake? snake;
        public static Apple? apple;

        public Window(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws)
        {
        }

        protected override void OnLoad()
        {
            size = new Vector2(Size.X, Size.Y);
            shaderProgram = ShaderUtils.createProgram();
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.PatchParameter(PatchParameterInt.PatchVertices, 3);
            GL.Enable(EnableCap.DepthTest);
            gm.init(); // initialize grid manager,

            // Game specific entities initialization
            snake = new Snake();
            apple = new Apple();

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            tm.increment((float)args.Time); // increment time.
            Color4 backColor;
            backColor.A = 1.0f;
            backColor.R = 0.1f;
            backColor.G = 0.1f;
            backColor.B = 0.1f;
            GL.ClearColor(backColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.UseProgram(Window.shaderProgram);
            gm.render(); // render objs
            SwapBuffers();
        }

        // todo update this input system, it is buggy and weird.
        public void setInput()
        {
            if (snake == null) return;
            if (IsKeyDown(Keys.W) && snake.direction != Snake.Direction.Up)
            {
                if (snake.direction != Snake.Direction.Down)
                {
                    snake.direction = Snake.Direction.Up;
                    return;
                }
            }
            if (IsKeyDown(Keys.S) && snake.direction != Snake.Direction.Down)
            {
                if (snake.direction != Snake.Direction.Up)
                {
                    snake.direction = Snake.Direction.Down;
                    return;
                }
            }
            if (IsKeyDown(Keys.D))
            {
                if (snake.direction != Snake.Direction.Left)
                {
                    snake.direction = Snake.Direction.Right;
                    return;
                }
            }
            if (IsKeyDown(Keys.A))
            {
                if (snake.direction != Snake.Direction.Right)
                {
                    snake.direction = Snake.Direction.Left;
                    return;
                }
            }
        }
    }
}

