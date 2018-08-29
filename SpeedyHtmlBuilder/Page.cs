using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpeedyHtmlBuilder
{

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
			data += HTML.StartTag("!DOCTYPE html") + n;
			data += HTML.StartTag("html" + HTML.Attribute("lang", "en")) + n;

			GenerateHead(title, styleSheet);

			data += head + n;
			data += HTML.StartTag("body" + HTML.Attribute("class", "bg")) + n;

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

		public void RowStart(List<string> script, int rowStartPos,bool removeEmptyLines = true)
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

		private void GenerateHead(string title, string styleSheet)
		{
			string tx2 = t + t;
			head += t + HTML.StartTag("head") + n;
			head += tx2 + HTML.Title(title) + n;
			head += tx2 + HTML.IconLink() + n;
			head += tx2 + HTML.StartTag("meta" + HTML.Attribute("charset", "utf-8")) + n;
			head += tx2 + HTML.StartTag("meta" + HTML.Attribute("name", "viewport") + HTML.Attribute("content", "width=device-width, initial-scale=1 maximum-scale=1 user-scalable=no")) + n;
			head += tx2 + HTML.link("stylesheet", styleSheet) + n;
			head += tx2 + HTML.link("stylesheet", "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css") + n;
			head += tx2 + HTML.Script("https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js") + n;
			head += tx2 + HTML.Script("https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js") + n;
			head += HTML.EndTag(("head")) + n;
		}

		public void AddRowToLastAddedContainer(string content, string classProperties = "")
		{
			mContainers[mContainers.Count - 1].AddRow(content, classProperties);
		}

		public void AddScript(string line)
		{
			string src = StringUtils.SubString(line, "addScript(", ");");
			mScripts.Add(HTML.Script(src));
		}

		public void AddHeading(string line)
		{
			string text = StringUtils.SubString(line, "(text:", ",class:");
			string cssClass = StringUtils.SubString(line, ",class:", ",id:");
			string id = StringUtils.SubString(line, ",id:", ");");
			AddRowToLastAddedContainer(HTML.StartTag("center") + HTML.StartTag("h1" + HTML.Attribute("id", id), cssClass) + text + HTML.EndTag("h1") + HTML.EndTag("center"));
		}

		public void AddSubHeading(string line)
		{
			string text = StringUtils.SubString(line, "(text:", ",class:");
			string cssClass = StringUtils.SubString(line, ",class:", ",id:");
			string id = StringUtils.SubString(line, ",id:", ");");
			AddRowToLastAddedContainer(HTML.StartTag("h3" + HTML.Attribute("id",id), cssClass) + text + HTML.EndTag("h3" ));
		}

		public void AddImage(string line)
		{
			string imageName = StringUtils.SubString(line, "addImage(", ");");
			AddRowToLastAddedContainer(HTML.StartTag("img" + HTML.Attribute("style","max-width:75%") + HTML.Attribute("src", imageName)));
		}

		public void AddImageCentered(string line)
		{
			string imageName = StringUtils.SubString(line, "Centered(", ");");
			string html = HTML.StartTag("center") + HTML.StartTag("img" + HTML.Attribute("src", imageName)) + HTML.EndTag("center") ;
			AddRowToLastAddedContainer(html);
		}

		public void  NavigationBar(string line)
		{					
			AddHtml(Navbar.Create(line));
		}

		public void NavLink(string line)
		{			
			AddHtml(Navbar.Link(line));
		}

		public void NavLinkDropdown(string line)
		{
		
			AddHtml(Navbar.Dropdown(line));
		}

		public void NavLinkDropdownEnd()
		{
			AddHtml(HTML.EndTag("ul") + HTML.EndTag("li"));
		}

		public void NavBarEnd()
		{
			AddHtml(HTML.EndTag("ul") + n + HTML.EndTag("div") + n + HTML.EndTag("nav"));
		}	

		public void Code(List<string> code)
		{
			string html = "";
			html += "<div class=\"container-fluid\">" +n;
			html += "\t<div class=\"row\" ><div class=\"col-lg-2 col-md-2\" ></div>" +n;
			html += "\t\t<div class=\" col-lg-8 col-md-8 col-sm-12 col-xs-12\" >" + n;
			html += "\t\t\t<pre class=\"code\"> \n\t\t\t\t <code class=\"csharp whiteText\">\n";

			foreach(var line in code)
			{
				if(String.IsNullOrWhiteSpace(line))
					html += "\n";
				else
					html += CodeCS.Parse(line);
			}

			html += "\t\t\t\t</code>\n\t\t\t</pre>\n\t\t</div>\n\t</div>\n</div>";
			AddHtml(html);
		}	

		public static string Span(string content, string cssClass)
		{
			return "<span class=" + cssClass + ">" + content + "</span>";
		}

		public void AddHtml(string html)
		{
			mContainers.Add(new Container(html, true));
		}

		public void AddFooter(string line)
		{
			AddRowToLastAddedContainer(Footer.Create(line));
		}

		public void Padding()
		{
			AddRowToLastAddedContainer(HTML.StartTag("div", "row padding") + HTML.EndTag("div"));
		}

		private void CloseBodyHtml()
		{         
			data += HTML.EndTag("body") + n;
			data += HTML.EndTag("html");
		}		

		public void SaveToFile(string pageName)
		{
			mContainers.Add(new Container());
			string content = "";

			content += HTML.StartTag("p" + HTML.Attribute("style", "font-size:50%"));
			content += "Generated with SpeedyHtml.";
			content += HTML.EndTag("p");
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
