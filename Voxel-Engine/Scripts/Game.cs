﻿//System
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxelEngine.Scripts.Graphics;
using VoxelEngine.Scripts.Json;
using VoxelEngine.Scripts.Player;
using VoxelEngine.Scripts.UI;
using VoxelEngine.Scripts.World;


//Open TK
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

//STB
using StbImageSharp;
using static System.Net.Mime.MediaTypeNames;


namespace VoxelEngine.Scripts
{
    internal class Game : GameWindow
    {
        ShaderProgram program;

		public World.WorldManager worldManager;
		public Camera camera;
        public BiomeJsonManager biomeJsonManager;

		//tranformation variables
		float yRot = 0f;


		//width and height of screen
		int width, height;

        //game variables
        public bool isPaused;
        public int renderDistance = 16;



        //constructor that sets the width, height, and calls the base constructor (GameWindow's Constructor) with default args
        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Title = title })
        {
            this.width = width;
            this.height = height;

            //center window
            CenterWindow(new Vector2i(width, height));
        }
        //called whenever window is resized
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            width = e.Width;
            height = e.Height;
        }

        //called once when game is started
        protected override void OnLoad()
        {
            base.OnLoad();

            //creates shaders
            program = new ShaderProgram("Default.vert", "Default.frag");

            //creates biome json manager
            biomeJsonManager = new BiomeJsonManager();

            //creates world manager and generates the world
            worldManager = new WorldManager(renderDistance);
            worldManager.GenerateWorld();

            //spawns camera
            camera = new Camera(width, height, new Vector3(0, 25, 0));

			//locks the cursor
			CursorState = CursorState.Grabbed;


			RenderShaders();
		}



		public void RenderShaders()
        {
            program = new ShaderProgram("Default.vert", "Default.frag");

            GL.Enable(EnableCap.DepthTest);
            GL.FrontFace(FrontFaceDirection.Cw);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
        }


		//called once when game is closed
		protected override void OnUnload()
        {
            base.OnUnload();

            worldManager.DeleteWorld();
            program.Delete();
        }
        //called every frame. All rendering happens here
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            //Set the color to fill the screen with
            GL.ClearColor(0.3f, 0.3f, 1f, 1f);
            //Fill the screen with the color
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


            //transformation mat    rices
            Matrix4 model = Matrix4.Identity;
            Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix();

            //sets the variables in the shaders
            int modelLocation = GL.GetUniformLocation(program.ID, "model");
            int viewLocation = GL.GetUniformLocation(program.ID, "view");
            int projectionLocation = GL.GetUniformLocation(program.ID, "projection");

            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref projection);

			//swap the buffers
            Context.SwapBuffers();

			worldManager.RenderWorld();

			base.OnRenderFrame(args);
        }
        //called every frame. All updating happens here
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            MouseState mouse = MouseState;
            KeyboardState input = KeyboardState;

            base.OnUpdateFrame(args);
            camera.Update(input, mouse, args);
            InputController(input, mouse, args);
        }


        public void InputController(KeyboardState input, MouseState mouse, FrameEventArgs e)
		{
			if (input.IsKeyDown(Keys.Escape))
			{
				TogglePause();
			}
		}

        public void TogglePause()
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                CursorState = CursorState.Normal;
            }
            else
            {
                CursorState = CursorState.Grabbed;
            }
        }

        //Function to load a text file and return its contents as a string
        public static string LoadShaderSource(string filePath)
        {
            string shaderSource = "";

            try
            {
                using (StreamReader reader = new StreamReader("../../../Shaders/" + filePath))
                {
                    shaderSource = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to load shader source file: " + e.Message);
            }

            return shaderSource;
        }
    }
}
