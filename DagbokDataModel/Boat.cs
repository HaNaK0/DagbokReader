using System;
using System.Collections.Generic;
using System.Text;

namespace DagbokDataModel
{
	class Boat : IDataMember<sbyte>
	{

		public Boat()
		{

		}

		public Boat(string aString)
		{
			StringData = aString;
		}

		private void DecodeString(string boatString = null)
		{
			string stringToDecode = boatString ?? stringData;


		}

		sbyte IDataMember<sbyte>.Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		string IDataMember<sbyte>.ToString()
		{
			return stringData;
		}

		private string stringData;
		public string StringData
		{
			get => stringData;
			set
			{
				stringData = value;
				DecodeString();
			}
		}
	}
}
