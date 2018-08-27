using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyHtmlBuilder
{
	public class Container
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
				data = HTML.StartTag("div" + HTML.Attribute("class", classProperty)) + n;
				foreach (var row in mRows)
				{
					data += row.GetData();
				}

				data += HTML.EndTag("div");
			}
			else
			{
				data += htmlData;
			}


			return data + "<!--End of container-->" +n;
		}


	}
}
