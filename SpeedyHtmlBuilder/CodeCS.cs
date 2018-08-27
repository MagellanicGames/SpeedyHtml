using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyHtmlBuilder
{
	class CodeCS
	{

		static string basic_span = "<span class=code-basic>";
		static string class_span = "<span class=code-class>";
		static string string_span = "<span class=code-string>";
		static string number_span = "<span class=code-num>";

		public static string Parse(string line)
		{
			line = line.Replace(" new ",basic_span + " new " + HTML.EndTag("span"));
			line = line.Replace("float ",basic_span + "float " + HTML.EndTag("span"));
			line = line.Replace("int ",basic_span + "int " + HTML.EndTag("span"));
			line = line.Replace("var ",basic_span + "var " + HTML.EndTag("span"));
			line = line.Replace(" out ",basic_span + " out " + HTML.EndTag("span"));
			line = line.Replace(" base",basic_span + "base" + HTML.EndTag("span"));
			line = line.Replace("typeof",basic_span + "typeof" + HTML.EndTag("span"));
			line = line.Replace("BufferUsage",basic_span + "BufferUsage" + HTML.EndTag("span"));
			line = line.Replace(" in ",basic_span + " in " + HTML.EndTag("span"));
			line = line.Replace("protected",basic_span + "protected" + HTML.EndTag("span"));
			line = line.Replace("override",basic_span + "override" + HTML.EndTag("span"));
			line = line.Replace("private",basic_span + "private" + HTML.EndTag("span"));
			line = line.Replace("public",basic_span + "public" + HTML.EndTag("span"));
			line = line.Replace("foreach",basic_span + "foreach" + HTML.EndTag("span"));
			line = line.Replace("void ",basic_span + "void " + HTML.EndTag("span"));
			line = line.Replace(" this ",basic_span + "this" + HTML.EndTag("span"));
			line = line.Replace("namespace",basic_span + "namespace" + HTML.EndTag("span"));
			line = line.Replace(" class ",basic_span + " class " + HTML.EndTag("span"));
			line = line.Replace("float4x4",basic_span + "float4x4" + HTML.EndTag("span"));
			line = line.Replace("float4 ",basic_span + "float4 " + HTML.EndTag("span"));
			line = line.Replace("float2 ",basic_span + "float2 " + HTML.EndTag("span"));
			line = line.Replace("struct ",basic_span + "struct " + HTML.EndTag("span"));
			line = line.Replace("return ",basic_span + "return " + HTML.EndTag("span"));
			line = line.Replace("technique ",basic_span + "technique " + HTML.EndTag("span"));
			line = line.Replace("basic ",basic_span + "basic " + HTML.EndTag("span"));
			line = line.Replace(" compile ",basic_span + " compile " + HTML.EndTag("span"));
			line = line.Replace(" VertexShader ",basic_span + " VertexShader " + HTML.EndTag("span"));
			line = line.Replace(" PixelShader ",basic_span + " PixelShader " + HTML.EndTag("span"));
			line = line.Replace("SamplerState ",basic_span + "SamplerState " + HTML.EndTag("span"));
			line = line.Replace(" if ",basic_span + "if " + HTML.EndTag("span"));
			line = line.Replace(" pass ",basic_span + " pass " + HTML.EndTag("span"));
			line = line.Replace(" bool ",basic_span + "bool" + HTML.EndTag("span"));
			line = line.Replace(" static ",basic_span + "static" + HTML.EndTag("span"));
			line = line.Replace(" for ",basic_span + "for" + HTML.EndTag("span"));
			line = line.Replace("true",basic_span + "true" + HTML.EndTag("span"));
			line = line.Replace("false",basic_span + "false" + HTML.EndTag("span"));

			line = line.Replace("Vector2",class_span + "Vector2" + HTML.EndTag("span"));
			line = line.Replace("Vector3",class_span + "Vector3" + HTML.EndTag("span"));
			line = line.Replace("VertexPositionNormalTexture",class_span + "VertexPositionNormalTexture" + HTML.EndTag("span"));
			line = line.Replace("Matrix",class_span + "Matrix" + HTML.EndTag("span"));
			line = line.Replace("Texture2D",class_span + "Texture2D" + HTML.EndTag("span"));
			line = line.Replace(" VertexBuffer",class_span + "VertexBuffer" + HTML.EndTag("span"));
			line = line.Replace(" GameTime",class_span + "GameTime" + HTML.EndTag("span"));
			line = line.Replace("Color",class_span + "Color" + HTML.EndTag("span"));
			line = line.Replace(" Effect",class_span + "Effect" + HTML.EndTag("span"));
			line = line.Replace("EffectPass",class_span + "EffectPass" + HTML.EndTag("span"));
			line = line.Replace("GraphicsDeviceManager",class_span + "GraphicsDeviceManager" + HTML.EndTag("span"));
			line = line.Replace("GraphicsDevice ",class_span + "GraphicsDevice" + HTML.EndTag("span"));
			line = line.Replace(" Content",class_span + "Content" + HTML.EndTag("span"));
			line = line.Replace(" Game ",class_span + "Game" + HTML.EndTag("span"));
			line = line.Replace(" Game1",class_span + "Game1" + HTML.EndTag("span"));
			line = line.Replace("KeyboardState",class_span + "KeyboardState" + HTML.EndTag("span"));
			line = line.Replace("Keyboard",class_span + "Keyboard" + HTML.EndTag("span"));
			line = line.Replace("KeyboardState",class_span + "KeyboardState" + HTML.EndTag("span"));
			line = line.Replace("SoundEffect ",class_span + "SoundEffect" + HTML.EndTag("span"));
			line = line.Replace("SoundEffectInstance",class_span + "SoundEffectInstance" + HTML.EndTag("span"));
			line = line.Replace("SpriteBatch ",class_span + "SpriteBatch " + HTML.EndTag("span"));
			line = line.Replace(" List ",class_span + "List" + HTML.EndTag("span"));
			line = line.Replace(" Controls ",class_span + "Controls" + HTML.EndTag("span"));

			if(line.Contains("\""))
			{
				int start = line.IndexOf('\"');
				int end = line.LastIndexOf('\"') + 1;
				string strToColor = line.Substring(start,end - start);
				line = line.Replace(strToColor,string_span + strToColor + HTML.EndTag("span"));
			}

			return line +"\n";
		}
	}
}
