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
            string rowEnd = "rowEnd;";
            string padding = "padding;";
            string addContainer = "addContainer;";
            string htmlStart = "htmlStart;";
            string htmlEnd = "htmlEnd;";
            string classProperties = "{class:";
            string addFooter = "addFooter;";
            string addImage = "addImage(";


            for (int i = 1; i < source.Count; i++)
            {
                var line = source[i];

                if (String.IsNullOrWhiteSpace(line))
                    continue;

                if (line.Contains(addContainer))
                {

                    if (line.Contains("{class:"))
                    {
                        string props = StringUtils.SubString(line, "{class:", "}");
                        page.AddContainer("container-fluid " + props);
                    }
                    else
                        page.AddContainer();

                    continue;
                }


                if (line.Contains(rowStart))
                {
                    string properties = "";
                    if (line.Contains(classProperties))
                    {
                        properties = StringUtils.SubString(line, "{class:", "}");
                    }
                    i++;
                    string content = "";
                    while (source[i].Contains(rowEnd) == false)
                    {

                        content += source[i] + "\n";
                        i++;

                        if (i > source.Count - 1)
                        {
                            Console.WriteLine("Hanging rowStart, no rowEnd found.");
                            break;
                        }
                    }

                    page.AddRowToLastAddedContainer(content, properties);
                    continue;
                }

                if (line == padding)
                {
                    page.Padding();
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

                if (source[i].Contains(addFooter))
                {
                    string email = StringUtils.SubString(source[i], "email:", ",date:");
                    string date = StringUtils.SubString(source[i], ",date:", ",copy:");
                    string copy = StringUtils.SubString(source[i], ",copy:", "}");

                    page.AddFooter(email, date, copy);
                    continue;
                }

                if(line.Contains(addImage))
                {
                    string imageName = StringUtils.SubString(line, "addImage(", ");");
                    page.AddImage(imageName);
                    continue;

                }
            }



            page.SaveToFile("index");

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
