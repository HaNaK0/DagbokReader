using System;
using System.Collections.Generic;
using System.Text;

namespace DagbokDataModel
{
	interface IDataMember <IDType>
	{
		IDType Id
		{
			get;
			set;
		}

		string ToString();
	}
}
