using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyHtmlBuilder
{
	class HTML
	{

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

		/// <summary>
		/// Attribute example: class, id.  Used the HTML.Attribute method
		/// </summary>
		/// <param name="innerHtml"></param>
		/// <param name="url"></param>
		/// <param name="attributes"></param>
		/// <returns></returns>
		public static string Link(string innerHtml, string url, string attributes)
		{
			return StartTag("a" + Attribute("href",url) + attributes) + innerHtml + EndTag("a");
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

		

		public static string SelfClosingTag(string name)
		{
			return "<" + name + "/>";
		}

		public static string AddMailIcon()
		{
			return StartTag("span", "glyphicon glyphicon-envelope") + EndTag("span");
		}
	}
}
