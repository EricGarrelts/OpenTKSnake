using System;
using System.Diagnostics;
using OpenTK.Graphics.OpenGL;

namespace OpenGLSnake.render
{

    // Static utility class that handles shader initialization.
	public class ShaderUtils
	{
        public static int createProgram()
        {
            try
            {
                int program = GL.CreateProgram();
                List<int> shaders = new List<int>
                {
                    CompileShader(ShaderType.VertexShader, @"../../../render/shaders/vertexShader.glsl"),
                    CompileShader(ShaderType.FragmentShader, @"../../../render/shaders/fragShader.glsl")
                };

                // Attach the shaders to the program
                foreach (var shader in shaders)
                    GL.AttachShader(program, shader);
                // Link the program
                GL.LinkProgram(program);
                var info = GL.GetProgramInfoLog(program);
                if (!string.IsNullOrWhiteSpace(info))
                    throw new Exception($"CompileShaders ProgramLinking had errors: {info}");

                //Detach and delete the shaders after link
                foreach (var shader in shaders)
                {
                    GL.DetachShader(program, shader);
                    GL.DeleteShader(shader);
                }
                return program;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        private static int CompileShader(ShaderType type, string path)
        {
            var shader = GL.CreateShader(type);
            var src = File.ReadAllText(path);
            GL.ShaderSource(shader, src);
            GL.CompileShader(shader);

            // get info on the shader for any erros
            var info = GL.GetShaderInfoLog(shader);
            if (!string.IsNullOrWhiteSpace(info))
            {
                Console.WriteLine(info);
            }
            return shader;
        }
    }
}

