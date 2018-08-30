using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyHtmlBuilder
{
	class Navbar
	{
		static string t = "\t";
		static string n = "\n";

		public static string Create(string line)
		{
			string title = StringUtils.SubString(line,"navBarStart(text:",",home:");
			string headerLink = StringUtils.SubString(line,",home:",");");
			string result = "";
			result += HTML.StartTag("nav" + HTML.Attribute("class","navbar navbar-inverse navbar-fixed-top"))+n;
			result += t+HTML.StartTag("div" + HTML.Attribute("class","container-fluid blackBack"))+n;

			result += _NavBarHeader(title, headerLink);

			result += HTML.StartTag("div" + HTML.Attribute("class","collapse navbar-collapse blueText") + HTML.Attribute("id","myNavbar")) +n;
			result += HTML.StartTag("ul" + HTML.Attribute("class","nav navbar-nav"));

			return result;
		}

		public static string Link(string line)
		{
			string name = StringUtils.SubString(line,"navLink(text:",",link:");
			string link = StringUtils.SubString(line,",link:",");");
			string html = "";
			html += HTML.StartTag("li");
			html += HTML.Link(name,link,HTML.Attribute("class","navLink"));
			html += HTML.EndTag("li");
			return html;
		}

		public static string Dropdown(string line)
		{
			string title = StringUtils.SubString(line,"navDropdown(text:",");");
			string html = "";
			html += HTML.StartTag("li" + HTML.Attribute("class","dropdown"));
			string innerHtml = title + HTML.StartTag("span" + HTML.Attribute("class","caret")) + HTML.EndTag("span");
			html += HTML.Link(innerHtml,"#",HTML.Attribute("class","navLink") + HTML.Attribute("data-toggle","dropdown"));
			html += HTML.StartTag("ul" + HTML.Attribute("class","dropdown-menu"));
			
			return html;
		}

		public static string SubmenuDropdown(string line)
		{
			string title = StringUtils.SubString(line,"navSubmenuDropdownStart(text:",");");
			string html = "";
			html += HTML.StartTag("li" + HTML.Attribute("class","dropdown-submenu"));
			string innerHtml = title + HTML.StartTag("span" + HTML.Attribute("class","caret")) + HTML.EndTag("span");
			html += HTML.Link(innerHtml,"",HTML.Attribute("class","submenu navLink"));
			html += HTML.StartTag("ul" + HTML.Attribute("class","dropdown-menu"));
			return html;
		}
		

		private static string _NavBarHeader(string brand,string headerLink)
		{
			string result = "";

			result += t + t + HTML.StartTag("div" + HTML.Attribute("class","navbar-header")) + n;
			result += t + t + t + HTML.StartTag("button" + HTML.Attribute("type","button") + HTML.Attribute("class","navbar-toggle") + HTML.Attribute("data-toggle","collapse") + HTML.Attribute("data-target","#myNavbar")) + n;
			result += t + t + t + HTML.StartTag("span" + HTML.Attribute("class","icon-bar")) + HTML.EndTag("span")+n;
			result += t + t + t + HTML.StartTag("span" + HTML.Attribute("class","icon-bar")) + HTML.EndTag("span")+n;
			result += t + t + t + HTML.StartTag("span" + HTML.Attribute("class","icon-bar")) + HTML.EndTag("span")+n;
			result += t + t + t + HTML.EndTag("button") + n;
			string innerHtml = HTML.StartTag("div" + HTML.Attribute("class","blueText")) + brand + HTML.EndTag("div");
			result += t + t + t + HTML.Link(innerHtml,headerLink,HTML.Attribute("class","navLink navbar-brand")) + n;
			result += t + t + HTML.EndTag("div")+n;
			

			return result;
		}
	}
}
