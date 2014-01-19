using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using SDL2;

namespace Program
{
	class Test
	{
		public static void Main()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			IntPtr window = SDL.CreateWindow("SDL2 Window",
				WindowPosition.Centered, WindowPosition.Centered,
				640, 480,
				WindowFlags.OpenGL);

			SDL.GL.SetAttribute(ContextAttribute.AcceleratedVisual, 1);
			SDL.GL.SetAttribute(ContextAttribute.AlphaSize, 8);
			SDL.GL.SetAttribute(ContextAttribute.BlueSize, 8);
			SDL.GL.SetAttribute(ContextAttribute.GreenSize, 8);
			SDL.GL.SetAttribute(ContextAttribute.RedSize, 8);
			SDL.GL.SetAttribute(ContextAttribute.DepthSize, 16);
			SDL.GL.SetAttribute(ContextAttribute.Doublebuffer, 1);
			IntPtr context = SDL.GL.CreateContext(window);
			SDL.GL.MakeCurrent(window, context);
			SDL.GL.SetSwapInterval(0);

			GraphicsContext dummy = new GraphicsContext(
				new ContextHandle(context),
				(name) => SDL.GL.GetAddress(name),
				() => new ContextHandle(SDL.GL.GetCurrentContext()));

			sw.Stop();
			Console.WriteLine("Loaded in {0} ms", Math.Round(sw.Elapsed.TotalMilliseconds));
			Console.WriteLine(GL.GetString(StringName.Vendor));
			Console.WriteLine(GL.GetString(StringName.Renderer));
			Console.WriteLine(GL.GetString(StringName.Version));

			SDL.PumpEvents();

			GL.ClearColor(0, 0, 0, 0);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.Begin(PrimitiveType.Triangles);
			const int count = 1 << 10;
			for (int i = 0; i < count; i++)
			{
				GL.Vertex3(0.0f, 0.0f, -1.0f);
				GL.Vertex3(0.001f, 0.0f, -1.0f);
				GL.Vertex3(0.001f, 0.001f, -1.0f);
			}
			GL.End();

			SDL.GL.SwapWindow(window);

			dummy.Dispose();
			SDL.GL.DeleteContext(context);
			SDL.DestroyWindow(window);
		}
	}
}

namespace SDL2
{
	using System.Security;

	public enum WindowFlags
	{
		Default = 0,
		OpenGL = 0x00000002,
		AllowHighDpi = 0x00002000,
	}

	public enum WindowPosition
	{
		Centered = 0x2FFF0000,
		CenteredDisplay0 = Centered,
		CenteredDisplay1 = Centered | 1,
		CenteredDisplay2 = Centered | 2,
		CenteredDisplay3 = Centered | 3,
		CenteredDisplay4 = Centered | 4,
		CenteredDisplay5 = Centered | 5,
	}

	public enum ContextAttribute
	{
		RedSize,
		GreenSize,
		BlueSize,
		AlphaSize,
		BufferSize,
		Doublebuffer,
		DepthSize,
		StencilSize,
		AccumulatorRedSize,
		AccumulatorGreenSize,
		AccumulatorBlueSize,
		Stereo,
		MultisampleBuffers,
		MultisampleSamples,
		AcceleratedVisual,
		RetainedBacking,
		ContextMajorVersion,
		ContextMinorVersion,
		ContextEgl,
		ContextFlags,
		ContextProfileMask,
		ShareWithCurrentContext
	}

	static class SDL
	{
		const string lib = "SDL2";

		[SuppressUnmanagedCodeSecurity]
		[DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_CreateWindow", ExactSpelling = true)]
		static extern IntPtr CreateWindow(byte[] title, int x, int y, int w, int h, WindowFlags flags);

		public static IntPtr CreateWindow(string title, int x, int y, int w, int h, WindowFlags flags)
		{
			byte[] title_utf8 = System.Text.Encoding.ASCII.GetBytes(title);
			return CreateWindow(title_utf8, x, y, w, h, flags);
		}

		[SuppressUnmanagedCodeSecurity]
		[DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_CreateWindow", ExactSpelling = true)]
		public static extern IntPtr CreateWindow(string title, WindowPosition x, WindowPosition y, int w, int h, WindowFlags flags);

		[SuppressUnmanagedCodeSecurity]
		[DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_DestroyWindow", ExactSpelling = true)]
		public static extern void DestroyWindow(IntPtr window);

		[SuppressUnmanagedCodeSecurity]
		[DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_PumpEvents", ExactSpelling = true)]
		public static extern void PumpEvents();

		public static class GL
		{
			[SuppressUnmanagedCodeSecurity]
			[DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_CreateContext", ExactSpelling = true)]
			public static extern IntPtr CreateContext(IntPtr window);

			[SuppressUnmanagedCodeSecurity]
			[DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_DeleteContext", ExactSpelling = true)]
			public static extern void DeleteContext(IntPtr context);

			[SuppressUnmanagedCodeSecurity]
			[DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_GetCurrentContext", ExactSpelling = true)]
			public static extern IntPtr GetCurrentContext();

			[SuppressUnmanagedCodeSecurity]
			[DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_GetProcAddress", ExactSpelling = true)]
			public static extern IntPtr GetAddress(string name);

			[SuppressUnmanagedCodeSecurity]
			[DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_MakeCurrent", ExactSpelling = true)]
			public static extern int MakeCurrent(IntPtr window, IntPtr context);

			[SuppressUnmanagedCodeSecurity]
			[DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_SetAttribute", ExactSpelling = true)]
			public static extern int SetAttribute(ContextAttribute attr, int value);

			[SuppressUnmanagedCodeSecurity]
			[DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_SetSwapInterval", ExactSpelling = true)]
			public static extern int SetSwapInterval(int interval);

			[SuppressUnmanagedCodeSecurity]
			[DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_SwapWindow", ExactSpelling = true)]
			public static extern void SwapWindow(IntPtr window);
		}
	}
}
