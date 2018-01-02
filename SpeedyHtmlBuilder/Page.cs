using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpeedyHtmlBuilder
{
    class Container
    {
        string classProperty;
        List<Row> mRows;

        string htmlData;

        bool rawHtml;

        static string n = "\n";
        static string t = "\t";

        public Container(string classProperty = "container-fluid")
        {
            this.classProperty = classProperty;
            mRows = new List<Row>();

            rawHtml = false;
        }

        public Container(string data, bool rawHtml = true)
        {
            this.rawHtml = true;
            htmlData = data;
        }

        public void AddRow(string content, string classProperties)
        {
            mRows.Add(new Row(content, classProperties));
        }

        public string GetData()
        {
            string data = "";
            if (!rawHtml)
            {
                data = Page.StartTag("div" + Page.Attribute("class", classProperty)) + n;
                foreach (var row in mRows)
                {
                    data += row.GetData();
                }

                data += Page.EndTag("div");
            }
            else
            {
                data += htmlData;
            }


            return data + "<!--End of container-->" +n;
        }


    }


    class Row
    {


        string content;

        static string n = "\n";
        static string t = "\t";

        string start;
        string end;



        string mProperties;

        public Row(string content, string classProperties)
        {
            mProperties = classProperties;


            start = t + Page.StartTag("div" + Page.Attribute("class", "row"));
            this.content = content;
            end = t + Page.EndTag("div")+ "<!--End of Row-->" + n;
        }

        public string GetData()
        {

            return start + t + t + EdgeColumn() + n + t + t + t +  CenterColumn() + t + t+ n + EdgeColumn() + n + end;
        }

        private string CenterColumn()
        {
            string center;

            center = Page.StartTag("div" + Page.Attribute("class", mProperties + " col-lg-8 col-md-8 col-sm-12 col-xs-12"));

            string tx4 = t + t + t + t;

            return center + content + Page.EndTag("div");
        }


        private string EdgeColumn()
        {
            return Page.StartTag("div" + Page.Attribute("class", "col-lg-2 col-md-2")) + Page.EndTag("div");
        }


    }


    class Page
    {
        string data;

        string head;
        static string n = "\n";
        static string t = "\t";

        List<Container> mContainers;
        List<string> mScripts;

        /// <summary>
        /// creates page up to title in body
        /// closes when gotten.
        /// </summary>
        /// <param name="title"></param>
        public Page(string title, string styleSheet)
        {
            mContainers = new List<Container>();
            mScripts = new List<string>();

            data = "";
            data += StartTag("!DOCTYPE html") + n;
            data += StartTag("html" + Attribute("lang", "en")) + n;

            GenerateHead(title, styleSheet);

            data += head + n;
            data += StartTag("body" + Attribute("class", "bg")) + n;

        }

        private void GenerateHead(string title, string styleSheet)
        {
            string tx2 = t + t;
            head += t + StartTag("head") + n;
            head += tx2 + Title(title) + n;
            head += tx2 + IconLink() + n;
            head += tx2 + StartTag("meta" + Attribute("charset", "utf-8")) + n;
            head += tx2 + StartTag("meta" + Attribute("name", "viewport") + Attribute("content", "width=device-width, initial-scale=1 maximum-scale=1 user-scalable=no")) + n;
            head += tx2 + link("stylesheet", styleSheet) + n;
            head += tx2 + link("stylesheet", "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css") + n;
            head += tx2 + Script("https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js") + n;
            head += tx2 + Script("https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js") + n;
            head += EndTag("head") + n;
        }

        public void AddRowToLastAddedContainer(string content, string classProperties = "")
        {
            mContainers[mContainers.Count - 1].AddRow(content, classProperties);
        }

        public void AddScript(string line)
        {
            string src = StringUtils.SubString(line, "addScript(", ");");
            mScripts.Add(Script(src));
        }

        public void AddHeading(string line)
        {
            string text = StringUtils.SubString(line, "(text:", ",class:");
            string cssClass = StringUtils.SubString(line, ",class:", ",id:");
            string id = StringUtils.SubString(line, ",id:", ");");
            AddRowToLastAddedContainer(StartTag("center") + StartTag("h1" + Attribute("id", id), cssClass) + text + EndTag("h1") + EndTag("center"));
        }

        public void AddSubHeading(string line)
        {
            string text = StringUtils.SubString(line, "(text:", ",class:");
            string cssClass = StringUtils.SubString(line, ",class:", ",id:");
            string id = StringUtils.SubString(line, ",id:", ");");
            AddRowToLastAddedContainer(StartTag("h3" + Attribute("id",id), cssClass) + text + EndTag("h3") );
        }

        public void AddImage(string line)
        {
            string imageName = StringUtils.SubString(line, "addImage(", ");");
            AddRowToLastAddedContainer(StartTag("img" + Attribute("style","max-width:75%") + Attribute("src", imageName)));
        }

        public void AddImageCentered(string line)
        {
            string imageName = StringUtils.SubString(line, "Centered(", ");");
            string html = StartTag("center") + StartTag("img" + Attribute("src", imageName)) + EndTag("center");
            AddRowToLastAddedContainer(html);
        }

        public void AddHtml(string html)
        {
            mContainers.Add(new Container(html, true));
        }

        public void AddFooter(string line)
        {
            string email = StringUtils.SubString(line, "email:", ",date:");
            string date = StringUtils.SubString(line, ",date:", ",copy:");
            string copy = StringUtils.SubString(line, ",copy:", ",class:");
            string css = StringUtils.SubString(line, "class:", ");");

            string footer = StartTag("footer") + StartTag("h6" + Attribute("class",css)) + "Email: " + email + " " +AddMailIcon() + StartTag("br") + n;
            footer += "Last Updated: " + date + StartTag("br") + n;
            footer += "Copyright: " + copy + EndTag("h6") + n;
            footer += EndTag("footer") + n;

            AddRowToLastAddedContainer(footer);
        }

        public void AddContainer(string line)
        {
            if (line.Contains("{class:"))
            {
               string props = StringUtils.SubString(line, "{class:", "}");
               mContainers.Add(new Container("container-fluid " + props));
            }
            else
                mContainers.Add(new Container());
        }
       

        public void Padding()
        {
            AddRowToLastAddedContainer(StartTag("div", "row padding") + EndTag("div"));
        }

        public void RowStart(List<string> script, int rowStartPos)
        {
            string properties = "";
            string rowStartLine = script[rowStartPos];
            if (rowStartLine.Contains("{class:"))
            {
                properties = StringUtils.SubString(rowStartLine, "{class:", "}");
            }
            int rowEndPos = FindRowEnd(script, rowStartPos);

            rowStartPos++;
            if(rowEndPos == 0)
            {
                Console.WriteLine("Hanging rowStart, no rowEnd found.");
                return;
            }
            string content = "";

            rowEndPos++;
            for(int i = rowStartPos;i<rowEndPos - 1;i++)
            {
                content += script[i] + "\n";
            } 

            AddRowToLastAddedContainer(content, properties);
        }

        private int FindRowEnd(List<string> script, int currentPosition)
        {
            if (currentPosition > script.Count - 1)
                return 0;

            for(int i = currentPosition; i < script.Count;i++)
            {
                if (script[i].Contains("rowEnd;"))
                    return i;
            }
            return 0;
        }

        private string AddMailIcon()
        {
            return StartTag("span", "glyphicon glyphicon-envelope") + EndTag("span");
        }

        public static string H1(string innerHTML, string id = "")
        {
            string result;

            if (id != "")
                result = StartTag("center") + StartTag("h1" + Attribute("id", id), "red");
            else
                result = StartTag("center") + StartTag("h1", "red");

            result += innerHTML + EndTag("h1") + EndTag("center");

            return result;
        }

        public static string P(string innerHTML)
        {
            string result = "";
            result += StartTag("p") + innerHTML + EndTag("p");
            return result;
        }

        public static string B(string innerHTML, string classProperty = "")
        {
            string result = "";
            result += StartTag("b", classProperty) + innerHTML + EndTag("b");

            return result;
        }

        public static string A(string innerHtml, string url, string classProperty = "")
        {
            string result = "";

            result += StartTag("a" + Attribute("href", url), classProperty) + innerHtml + EndTag("a");
            return result;
        }       

        private void CloseBodyHtml()
        {         
            data += EndTag("body") + n;
            data += EndTag("html");
        }


        public static string Script(string src)
        {
            return StartTag("script" + Attribute("src", src)) + EndTag("script");
        }
        public static string link(string rel, string href)
        {
            return StartTag("link" + Attribute("rel", rel) + Attribute("href", href));
        }
        public static string Attribute(string name, string value)
        {
            return " " + name + "=" + QuotedString(value) + " ";
        }
        public static string QuotedString(string betweenQuotes)
        {
            return "\"" + betweenQuotes + "\"";
        }

        public static string IconLink()
        {
            return SelfClosingTag("link rel=" + QuotedString("shortcut icon") + ("type=") + QuotedString("image/x-icon") + "href=" + QuotedString("favicon.ico"));
        }

        public static string Title(string title)
        {
            return StartTag("title") + title + EndTag("title");
        }

        public static string DocType()
        {
            return StartTag("!DOCTYPE html");
        }

        public static string StartTag(string name, string classProperty = "")
        {
            string result = "";
            if (classProperty == "")
                result = "<" + name + ">";
            else
                result = "<" + name + Attribute("class", classProperty) + ">";

            return result;
        }

        public static string EndTag(string name)
        {
            return "</" + name + ">";
        }

        public static string SelfClosingTag(string name)
        {
            return "<" + name + "/>";
        }

        public static string GetPageData()
        {
            string result = "";
            return result;
        }

        public void SaveToFile(string pageName)
        {
            mContainers.Add(new Container());
            string content = "";

            content += StartTag("p" + Attribute("style", "font-size:50%"));
            content += "Generated with SpeedyHtml.";
            content += EndTag("p");
            AddRowToLastAddedContainer(content);

            foreach (var container in mContainers)
            {
                data += container.GetData();
            }

            foreach(var s in mScripts)
            {
                data += s + n;
            }
            CloseBodyHtml();
            File.WriteAllText(pageName + ".html", data);
        }
    }
}
