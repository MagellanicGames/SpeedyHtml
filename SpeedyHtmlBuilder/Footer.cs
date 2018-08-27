using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyHtmlBuilder
{
	class Footer
	{
		static string n = "\n";
		public static string Create(string line)
		{
			string email = StringUtils.SubString(line, "email:", ",date:");
			string date = StringUtils.SubString(line, ",date:", ",copy:");
			string copy = StringUtils.SubString(line, ",copy:", ",class:");
			string css = StringUtils.SubString(line, "class:", ");");

			string footer = HTML.StartTag("footer") + HTML.StartTag("h6" + HTML.Attribute("class",css)) + "Email: " + email + " " + HTML.AddMailIcon() + HTML.StartTag("br") + n;
			footer += "Last Updated: " + date + HTML.StartTag("br") + n;
			footer += "Copyright: " + copy + HTML.EndTag("h6") + n;
			footer += HTML.EndTag("footer") + n;

			return footer;
		}
	}
}
