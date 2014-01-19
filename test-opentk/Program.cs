using System;
using System.Diagnostics;
using System.Threading;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Project
{
	class Test
	{
		[STAThread]
		public static void Main()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			using (Toolkit.Init(new ToolkitOptions { EnableHighResolution = false }))
			using (var gw = new GameWindow())
			{
				gw.VSync = VSyncMode.Off;
				gw.Load += (sender, e) =>
				{
					sw.Stop();
					Console.WriteLine("Loaded in {0} ms", Math.Round(sw.Elapsed.TotalMilliseconds));
					Console.WriteLine(GL.GetString(StringName.Vendor));
					Console.WriteLine(GL.GetString(StringName.Renderer));
					Console.WriteLine(GL.GetString(StringName.Version));
				};
				gw.RenderFrame += (sender, e) =>
				{
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

					gw.SwapBuffers();
					gw.Exit();
				};

				gw.Run();
			}
		}
	}
}
