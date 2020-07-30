using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace DagbokDataModel
{
	class DataContainer
	{
		private static DataContainer staticData;

		public static DataContainer GetStaticData()
		{
			if(staticData == null)
			{
				staticData = new DataContainer("0.0");
				staticData.LoadFromXml("StaticData.xml");
			}

			return staticData;
		}

		public DataContainer(string versionString)
		{
			Version = new Version(versionString);
		}

		public void LoadFromXml(string path)
		{
			XElement rootElement = XElement.Load(path);

			Version = new Version(rootElement.Attribute("version").Value);

			XElement boatsElement = rootElement.Element("Boats");
			if(boatsElement != null)
			{
				MapDataString(ref boats, ref boatMap, boatsElement.Value);
			}
		}

		private static void MapDataString(ref List<List<string>> targetList, ref Dictionary<string, int> targetMap, string sourceString)
		{
			foreach (string row in sourceString.Split(';'))
			{
				targetList.Add(new List<string>(row.Split(',')));
			}


		}

		public Version Version { get; set; }

		private List<List<string>> boats;
		private Dictionary<string, int> boatMap;

		private List<List<string>> destinations;
		private Dictionary<string, int> destinationMap;

		private List<List<string>> crew;
		private Dictionary<string, int> crewMap;

		private List<List<string>> strings;
		private Dictionary<string, int> stringtMap;
	}
}
