using System;
using System.Diagnostics;
using System.Threading;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using SDL2;

namespace Project
{
	class Test
	{
		[STAThread]
		public static void Main()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			IntPtr window = SDL.SDL_CreateWindow("SDL2-CS Window",
				SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED,
				640, 480,
				SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL);

			SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_ACCELERATED_VISUAL, 1);
			SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_ALPHA_SIZE, 8);
			SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_BLUE_SIZE, 8);
			SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_GREEN_SIZE, 8);
			SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_RED_SIZE, 8);
			SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_DEPTH_SIZE, 16);
			SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_DOUBLEBUFFER, 1);
			IntPtr context = SDL.SDL_GL_CreateContext(window);
			SDL.SDL_GL_MakeCurrent(window, context);
			SDL.SDL_GL_SetSwapInterval(0);

			GL.LoadAll();

			sw.Stop();
			Console.WriteLine("Loaded in {0} ms", Math.Round(sw.Elapsed.TotalMilliseconds));
			Console.WriteLine(GL.GetString(StringName.Vendor));
			Console.WriteLine(GL.GetString(StringName.Renderer));
			Console.WriteLine(GL.GetString(StringName.Version));

			SDL.SDL_PumpEvents();

			GL.ClearColor(0, 0, 0, 0);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.Begin(BeginMode.Triangles);
			const int count = 1 << 10;
			for (int i = 0; i < count; i++)
			{
				GL.Vertex3(0.0f, 0.0f, -1.0f);
				GL.Vertex3(0.001f, 0.0f, -1.0f);
				GL.Vertex3(0.001f, 0.001f, -1.0f);
			}
			GL.End();

			SDL.SDL_GL_SwapWindow(window);

			SDL.SDL_GL_DeleteContext(context);
			SDL.SDL_DestroyWindow(window);
		}
	}
}
