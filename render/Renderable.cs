using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace OpenGLSnake.render
{
	public class Renderable
	{
        private int VAO;
        private int VBO;
        private int EBO;
        private int vertexCount;
        private int indicesCount;
        protected bool on = false; // render conditional

        public void load(float[] vertices, uint[] indices)
        {
            vertexCount = vertices.Length;
            indicesCount = indices.Length;
            VBO = GL.GenBuffer();
            VAO = GL.GenVertexArray();
            EBO = GL.GenBuffer();
            GL.BindVertexArray(VAO);
            // Position & Color
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, (vertexCount * 4), vertices, BufferUsageHint.StaticDraw);

            // Indices
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * indicesCount, indices, BufferUsageHint.StaticDraw);

            // Position attribute layout
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * 4, 0);
            GL.EnableVertexAttribArray(0);
            //GL.BindAttribLocation(Window.shaderProgram, 0, "aPos");

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        // renders the VAO
        public void render(Color4 color)
        {
            if (!this.on) return; // if "on" is true, no render DEFAULT:False
            int colorLocation = GL.GetUniformLocation(Window.shaderProgram, "color");
            GL.Uniform4(colorLocation, color);
            GL.BindVertexArray(VAO);
            GL.DrawElements(PrimitiveType.Triangles, indicesCount, DrawElementsType.UnsignedInt, 0);
        }

    }
}

