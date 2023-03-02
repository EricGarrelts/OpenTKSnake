using OpenGLSnake.render;
using OpenTK.Mathematics;

namespace OpenGLSnake
{
	public class Snake
	{
		public int score = 0;
		private List<Body> bodyList = new List<Body>();
		public Direction direction = Direction.Up;

		public Snake()
		{
			Vector2i startingPos = new Vector2i(GridManager.Span / 2);
			bodyList.Add(new Body(startingPos));
            bodyList[0].hook(Color4.Green);
        }

		public void update()
		{
            move();
            switch (checkCollision())
            {
                case (Collision.Self):
                    Window.tm.stopped = true;
                    break;
                case (Collision.Apple):
                    Apple.randomize();
                    addBody();
                    break;
            }
		}

        public bool containsSnake(Vector2i pos)
        {
            for (int i = 0; i < bodyList.Count; i++)
            {
                if (bodyList[i].position == pos) return true;
            }
            return false;
        }

        private void addBody()
        {
            Vector2i lastPos = bodyList[bodyList.Count - 1].lastPosition;
            bodyList.Add(new Body(lastPos));
            bodyList[bodyList.Count - 1].hook(Color4.White);

        }

        private Collision checkCollision()
        {
            for (int i = 1; i < bodyList.Count; i++)
            {
                if (bodyList[i].position == bodyList[0].position)
                {
                    return Collision.Self;
                }
            }
            if (bodyList[0].position == Apple.position)
            {
                return Collision.Apple;
            }
            return Collision.Nothing;
        }

        private void move()
		{
            void setHead(Vector2i pos)
            {
                if (pos.X < 0) pos.X = GridManager.Span-1;
                if (pos.X >= GridManager.Span) pos.X = 0;
                if (pos.Y < 0) pos.Y = GridManager.Span - 1;
                if (pos.Y >= GridManager.Span) pos.Y = 0;
                bodyList[0].hook(Color4.Green);
                bodyList[0].lastPosition = bodyList[0].position;
                bodyList[0].position = pos;
                bodyList[0].hook(Color4.Green);
            }
            switch (direction)
            {
                case Direction.Up:
                    setHead(new Vector2i(bodyList[0].position.X, bodyList[0].position.Y-1));
                    break;
                case Direction.Right:
                    setHead(new Vector2i(bodyList[0].position.X + 1, bodyList[0].position.Y));
                    break;
                case Direction.Down:
                    setHead(new Vector2i(bodyList[0].position.X, bodyList[0].position.Y + 1));
                    break;
                case Direction.Left:
                    setHead(new Vector2i(bodyList[0].position.X - 1, bodyList[0].position.Y));
                    break;
            }
            for (int i = 1; i < bodyList.Count; i++)
            {
                bodyList[i].lastPosition = bodyList[i].position;
                bodyList[i].hook(Color4.White);
                bodyList[i].position = bodyList[i - 1].lastPosition;
                bodyList[i].hook(Color4.White);

            }
        }

		public enum Direction
		{
			Up,
			Right,
			Down,
			Left
		}

        public enum Collision
        {
            Self,
            Apple,
            Nothing
        }

        private class Body
        {
            public Vector2i position;
            public Vector2i lastPosition;

            public Body(Vector2i position)
            {
                this.position = position;
            }

            public void hook(Color4 color)
            {
                Window.gm.toggle(position.X, position.Y, color);
            }
        }
    }
}

