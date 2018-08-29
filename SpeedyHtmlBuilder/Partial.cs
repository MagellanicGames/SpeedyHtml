using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyHtmlBuilder
{
	static class Partial
	{
		public static List<string> ImportPartial(string line)
		{
			List<string> result = new List<string>();
			string fileName = StringUtils.SubString(line,"importPartial(",");");

			if(fileName.Contains(".shbp") && File.Exists(fileName))
				result = new List<string>(File.ReadAllLines(fileName));

			return result;
		}
	}
}
