﻿//System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinecraftClone.Scripts.Graphics;
using MinecraftClone.Scripts.Player;
using MinecraftClone.Scripts.World;


//Open TK
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

//STB
using StbImageSharp;

namespace MinecraftClone.Scripts.World
{
	internal class TextureData
	{
		public static Dictionary<BlockType, Dictionary<Faces, Vector2>> blockTypeUVCoord = new Dictionary<BlockType, Dictionary<Faces, Vector2>>()
		{
			{BlockType.dirt, new Dictionary<Faces, Vector2>()
				{
					{Faces.top, new Vector2(2, 15) },
					{Faces.bottom, new Vector2(2, 15) },
					{Faces.front, new Vector2(2, 15) },
					{Faces.back, new Vector2(2, 15) },
					{Faces.left, new Vector2(2, 15) },
					{Faces.right, new Vector2(2, 15) },
				}
			},
			{BlockType.grass, new Dictionary<Faces, Vector2>()
				{
					{Faces.top, new Vector2(7, 13) },
					{Faces.bottom, new Vector2(2, 15) },
					{Faces.front, new Vector2(3, 15) },
					{Faces.back, new Vector2(3, 15) },
					{Faces.left, new Vector2(3, 15) },
					{Faces.right, new Vector2(3, 15) },
				}
			},
			{BlockType.stone, new Dictionary<Faces, Vector2>()
				{
					{Faces.top, new Vector2(1, 15) },
					{Faces.bottom, new Vector2(1, 15) },
					{Faces.front, new Vector2(1, 15) },
					{Faces.back, new Vector2(1, 15) },
					{Faces.left, new Vector2(1, 15) },
					{Faces.right, new Vector2(1, 15) },
				}
			},
		};
	}
}
