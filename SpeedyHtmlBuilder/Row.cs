using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyHtmlBuilder
{
	public class Row
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


			start = t + HTML.StartTag("div" + HTML.Attribute("class", "row"));
			this.content = content;
			end = t + HTML.EndTag("div")+ "<!--End of Row-->" + n;
		}

		public string GetData()
		{

			return start + t + t + EdgeColumn() + n + t + t + t +  CenterColumn() + t + t+ n + EdgeColumn() + n + end;
		}

		private string CenterColumn()
		{
			string center;

			center = HTML.StartTag("div" + HTML.Attribute("class", mProperties + " col-lg-8 col-md-8 col-sm-12 col-xs-12"));

			string tx4 = t + t + t + t;

			return center + content + HTML.EndTag("div");
		}


		private string EdgeColumn()
		{
			return HTML.StartTag("div" + HTML.Attribute("class", "col-lg-2 col-md-2")) + HTML.EndTag("div");
		}


	}
}
