using System;
using System.Collections.Generic;
using System.Text;

namespace DagbokDataModel
{
	class Row
	{
		public Row()
		{

		}

		public Row(string aString)
		{
			dataString = aString;
		}

		private string dataString;

		private DateTime time;
		private Boat boat;

	}
}
