using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace SpeedyHtmlBuilder
{
	class Program
	{
		static void Main(string[] args)
		{

			List<string> args_list = new List<string>(args);

			if (args_list.Count < 1)
			{
				Console.WriteLine("No filename given.");
				return;
			}


			if (!File.Exists(args_list[0]))
			{
				Console.WriteLine("Could not find " + "\"" + args_list[0] + "\"");
				return;
			}
		   
			if(!args_list[0].Contains(".shb"))
			{
				Console.WriteLine("Filename needs .shb extension. ie myscript.shb");
				return;
			}
			List<string> source = new List<string>(File.ReadAllLines(args_list[0]));

			if(source.Count < 1)
			{
				Console.WriteLine("Script file is empty.");
				return;
			}
			string title;
			string cssStyle;

			if (source[0].Contains(','))
			{
				title = source[0].Split(',')[0];
				cssStyle = source[0].Split(',')[1];
			}
			else
			{
				title = source[0];
				cssStyle = "style.css";
			}

			Page page = new Page(title, cssStyle);

			string rowStart = "rowStart;";        
			string padding = "padding;";
			string addContainer = "addContainer;";
			string htmlStart = "htmlStart;";
			string htmlEnd = "htmlEnd;";
		 
			string addFooter = "addFooter(";
			string addImage = "addImage(";
			string heading = "heading(";
			string subHeading = "subHeading(";
			string addImageCentered = "addImageCentered(";
			string addScript = "addScript(";
			string navBarStart = "navBarStart(";
			string navBarEnd = "navBarEnd(";
			string navLink = "navLink(";

			for(int i = 0; i < source.Count;i++)
			{
				if (String.IsNullOrWhiteSpace(source[i]))
				{
					source.RemoveAt(i);
					i--;
				}   
			}

			for (int i = 1; i < source.Count; i++)
			{
				var line = source[i];            

				if (line.Contains(addContainer))
				{
					page.AddContainer(line);    
					continue;
				}                

				if (line == padding)
				{
					page.Padding();
					continue;
				}
			   

				if (line.Contains(addFooter))
				{          
					page.AddFooter(line);
					continue;
				}

				if(line.Contains(addImage))
				{                   
					page.AddImage(line);
					continue;
				}

				if(line.Contains(heading))
				{                   
					page.AddHeading(line);
					continue;
				}

				if(line.Contains(subHeading))
				{                   
					page.AddSubHeading(line);
					continue;
				}

				if(line.Contains(addImageCentered))
				{                   
					page.AddImageCentered(line);
					continue;
				}

				if (line.Contains(rowStart))
				{
					page.RowStart(source, i);
					continue;
				}

				if(line.Contains(addScript))
				{
					page.AddScript(line);
					continue;
				}

				if(line.Contains(navBarStart))
				{					
					page.NavBar(line);
					continue;
				}

				if(line.Contains(navLink))
				{
					page.NavLink(line);
					continue;
				}


				if(line.Contains(navBarEnd))
				{
					page.NavBarEnd();
					continue;
				}

				if (line == htmlStart)
				{
					i++;
					string content = "";
					while (source[i].Contains(htmlEnd) == false)
					{
						content += source[i] + "\n";
						i++;
						if (i > source.Count - 1)
						{
							Console.WriteLine("Hanging htmlStart, no htmlEnd found.");
							break;
						}
					}

					page.AddHtml(content);
					continue;
				}
			}



			page.SaveToFile(args_list[0].Split('.')[0]);

			string css = "div.padding{\npadding-top:30px;\n}";
			if (!File.Exists("style.css"))
				File.WriteAllText("style.css",css);
		}


	}



	public struct StringPos
	{
		public int start;
		public int length;

		public StringPos(int s, int l)
		{
			start = s;
			length = l;
		}
	}

	public class StringUtils
	{
		private static StringBuilder sb = new StringBuilder();

		public static StringPos DataPosition(string text, string subStr1, string subStr2)
		{
			StringPos result;

			result.start = text.IndexOf(subStr1) + subStr1.Length;
			result.length = text.IndexOf(subStr2) - result.start;
			return result;
		}

		public static string SubString(string original, string str1, string str2)
		{
			StringPos s = DataPosition(original, str1, str2);
			return SubString(original, s);
		}

		public static string SubString(string s, StringPos pos)
		{
			return s.Substring(pos.start, pos.length);
		}

	}
}
