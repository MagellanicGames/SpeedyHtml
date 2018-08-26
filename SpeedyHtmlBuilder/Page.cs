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

		public void  NavBar(string line)
		{
			
			string title = StringUtils.SubString(line,"navBarStart(text:",",home:");
			string headerLink = StringUtils.SubString(line,",home:",");");
			string result = "";
			result += StartTag("nav" + Attribute("class","navbar navbar-inverse navbar-fixed-top"))+n;
			result += t+StartTag("div" + Attribute("class","container-fluid blackBack"))+n;

			result += _NavBarHeader(title, headerLink);

			result += StartTag("div" + Attribute("class","collapse navbar-collapse blueText") + Attribute("id","myNavbar")) +n;
			result += StartTag("ul" + Attribute("class","nav navbar-nav"));
			AddHtml(result);
		}

		public void NavLink(string line)
		{
			string name = StringUtils.SubString(line,"navLink(text:",",link:");
			string link = StringUtils.SubString(line,",link:",");");
			string html = "";
			html += StartTag("li");
			html += Link(name,link,Attribute("class","navLink"));
			html += EndTag("li");
			AddHtml(html);
		}

		public void NavLinkDropdown(string line)
		{
			string title = StringUtils.SubString(line,"navDropdown(text:",");");
			string html = "";
			html += StartTag("li" + Attribute("class","dropdown"));
			string innerHtml = title + StartTag("span" + Attribute("class","caret")) + EndTag("span");
			html += Link(innerHtml,"#",Attribute("class","navLink") + Attribute("data-toggle","dropdown"));
			html += StartTag("ul" + Attribute("class","dropdown-menu"));
			AddHtml(html);
		}

		public void NavLinkDropdownEnd()
		{
			AddHtml(EndTag("ul") + EndTag("li"));
		}

		public void NavBarEnd()
		{
			AddHtml(EndTag("ul") + EndTag("div") + EndTag("nav"));
		}

		private string _NavBarHeader(string brand,string headerLink)
		{
			string result = "";

			result += t + t + StartTag("div" + Attribute("class","navbar-header")) + n;
			result += t + t + t + StartTag("button" + Attribute("type","button") + Attribute("class","navbar-toggle") + Attribute("data-toggle","collapse") + Attribute("data-target","#myNavbar")) + n;
			result += t + t + t + StartTag("span" + Attribute("class","icon-bar")) + EndTag("span")+n;
			result += t + t + t + StartTag("span" + Attribute("class","icon-bar")) + EndTag("span")+n;
			result += t + t + t + StartTag("span" + Attribute("class","icon-bar")) + EndTag("span")+n;
			result += t + t + t + EndTag("button") + n;
			result += t + t + t + A(StartTag("div" + Attribute("class","blueText")) + brand + EndTag("div"), headerLink,"navLink navbar-brand")+n;
			result += t + t + EndTag("div")+n;
			

			return result;
		}

		public void CodeStart(List<string> code)
		{
			string html = "";
			html += "<div class=\"container-fluid\" >	<div class=\"row\" ><div class=\"col-lg-2 col-md-2\" ></div><div class=\" col-lg-8 col-md-8 col-sm-12 col-xs-12\" >";
			html += "<pre class=\"code\"> <code class=\"csharp whiteText\">";

			foreach(var line in code)
			{
				if(line.Contains("codeBasic(type:"))
					html += CodeBasic(line);
				else if(line.Contains("codeBasicNonNum(type:"))
					html += CodeBasicNonNum(line);
				else if(line.Contains("codeClass(type:"))
					html += CodeClass(line);
				else if(line.Contains("codeClassNew(type:"))
					html += CodeClassNew(line);
				else if(line.Contains("codeString(name:"))
					html += CodeString(line);
				else if(String.IsNullOrWhiteSpace(line))
					html += "\n";
				else
					html += ParseCode(line);
			}

			html += "</code> </pre> </div> </div>";
			AddHtml(html);
		}

		static string basic_span = "<span class=code-basic>";
		static string class_span = "<span class=code-class>";
		static string string_span = "<span class=code-string>";
		private string ParseCode(string line)
		{
			line = line.Replace(" new ",basic_span + " new " + EndTag("span"));
			line = line.Replace("float ",basic_span + "float " + EndTag("span"));
			line = line.Replace("int ",basic_span + "int " + EndTag("span"));
			line = line.Replace("var ",basic_span + "var " + EndTag("span"));
			line = line.Replace(" out ",basic_span + " out " + EndTag("span"));
			line = line.Replace(" base",basic_span + "base" + EndTag("span"));
			line = line.Replace("typeof",basic_span + "typeof" + EndTag("span"));
			line = line.Replace("BufferUsage",basic_span + "BufferUsage" + EndTag("span"));
			line = line.Replace(" in ",basic_span + " in " + EndTag("span"));
			line = line.Replace("protected",basic_span + "protected" + EndTag("span"));
			line = line.Replace("override",basic_span + "override" + EndTag("span"));
			line = line.Replace("private",basic_span + "private" + EndTag("span"));
			line = line.Replace("public",basic_span + "public" + EndTag("span"));
			line = line.Replace("foreach",basic_span + "foreach" + EndTag("span"));
			line = line.Replace("void ",basic_span + "void " + EndTag("span"));
			line = line.Replace(" this",basic_span + "this " + EndTag("span"));
			line = line.Replace("namespace",basic_span + "namespace" + EndTag("span"));
			line = line.Replace(" class ",basic_span + " class " + EndTag("span"));
			line = line.Replace("float4x4",basic_span + "float4x4" + EndTag("span"));
			line = line.Replace("float4 ",basic_span + "float4 " + EndTag("span"));
			line = line.Replace("float2 ",basic_span + "float2 " + EndTag("span"));
			line = line.Replace("struct ",basic_span + "struct " + EndTag("span"));
			line = line.Replace("return ",basic_span + "return " + EndTag("span"));
			line = line.Replace("technique ",basic_span + "technique " + EndTag("span"));
			line = line.Replace("basic ",basic_span + "basic " + EndTag("span"));
			line = line.Replace(" compile ",basic_span + " compile " + EndTag("span"));
			line = line.Replace(" VertexShader ",basic_span + " VertexShader " + EndTag("span"));
			line = line.Replace(" PixelShader ",basic_span + " PixelShader " + EndTag("span"));
			line = line.Replace("SamplerState ",basic_span + "SamplerState " + EndTag("span"));
			line = line.Replace(" if ",basic_span + " if " + EndTag("span"));
			line = line.Replace(" pass ",basic_span + " pass " + EndTag("span"));

			line = line.Replace("Vector2",class_span + "Vector2" + EndTag("span"));
			line = line.Replace("Vector3",class_span + "Vector3" + EndTag("span"));
			line = line.Replace("VertexPositionNormalTexture",class_span + "VertexPositionNormalTexture" + EndTag("span"));
			line = line.Replace("Matrix",class_span + "Matrix" + EndTag("span"));
			line = line.Replace("Texture2D",class_span + "Texture2D" + EndTag("span"));
			line = line.Replace(" VertexBuffer",class_span + "VertexBuffer" + EndTag("span"));
			line = line.Replace(" GameTime",class_span + "GameTime" + EndTag("span"));
			line = line.Replace("Color",class_span + "Color" + EndTag("span"));
			line = line.Replace(" Effect",class_span + "Effect" + EndTag("span"));
			line = line.Replace("EffectPass",class_span + "EffectPass" + EndTag("span"));
			line = line.Replace("GraphicsDeviceManager",class_span + "GraphicsDeviceManager" + EndTag("span"));
			line = line.Replace(" Content",class_span + "Content" + EndTag("span"));
			line = line.Replace(" Game ",class_span + "Game" + EndTag("span"));
			line = line.Replace(" Game1",class_span + "Game1" + EndTag("span"));
			line = line.Replace("Keyboard",class_span + "Keyboard" + EndTag("span"));
			line = line.Replace("SoundEffect ",class_span + "SoundEffect" + EndTag("span"));
			line = line.Replace("SoundEffectInstance",class_span + "SoundEffectInstance" + EndTag("span"));
			line = line.Replace("SpriteBatch ",class_span + "SpriteBatch " + EndTag("span"));

			if(line.Contains("\""))
			{
				int start = line.IndexOf('\"');
				int end = line.LastIndexOf('\"') + 1;
				string strToColor = line.Substring(start,end - start);
				line = line.Replace(strToColor,string_span + strToColor + EndTag("span"));
			}

			return line +"\n";
		}

		private string CodeBasic(string line)
		{
			string type = StringUtils.SubString(line,"codeBasic(type:",",name:");
			string name = StringUtils.SubString(line,"name:",",value:");
			string value = StringUtils.SubString(line,"value:",");");

			string code = "";
			if (type != "")
				code = Span(type,"code-basic") + " ";
			code += name;
			if(value != "")
				code += " = " + Span(value,"code-num");
			code += ";\n";
			return code;
		}	
		
			private string CodeBasicNonNum(string line)
		{
			string type = StringUtils.SubString(line,"codeBasicNonNum(type:",",name:");
			string name = StringUtils.SubString(line,"name:",",value:");
			string value = StringUtils.SubString(line,"value:",");");

			string code = "";
			if (type != "")
				code = Span(type,"code-basic") + " ";
			code += name;
			if(value != "")
				code += " = " + value;
			code += ";\n";
			return code;
		}		

		private string CodeClass(string line)
		{
			string type = StringUtils.SubString(line,"codeClass(type:",",name:");
			string name = StringUtils.SubString(line,"name:",",value:");
			string value = StringUtils.SubString(line,"value:",");");

			string code = "";
			if (type != "")
				code = Span(type,"code-class") + " ";
			code += name;
			if(value != "")
				code += " = " + value;
			code += ";\n";
			return code;
		}

		private string CodeClassNew(string line)
		{
			string type = StringUtils.SubString(line,"codeClassNew(type:",",name:");
			string name = StringUtils.SubString(line,"name:",",value:");
			string value = StringUtils.SubString(line,"value:",");");

			string code = "";
			if (type != "")
				code = Span(type,"code-class") + " ";
			code += name;
			if(value != "")
				code += " = " + Span("new","code-basic") + " " + Span(type,"code-class") + value;
			code += ";\n";
			return code;
		}

		private string CodeString(string line)
		{
			string name = StringUtils.SubString(line,"name:",",value:");
			string value = StringUtils.SubString(line,"value:",");");

			string code = Span("string","code-basic") + " " + name;
			if(value != "")
				code += " = " + Span("\"" + value + "\"","code-string");
			code += ";\n";
			return code;
		}

		public string Span(string content, string cssClass)
		{
			return "<span class=" + cssClass + ">" + content + "</span>";
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

		public static string Link(string innerHtml,string url,string attributes)
		{
			return StartTag("a" + attributes + Attribute("href",url)) + innerHtml + EndTag("a");
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
