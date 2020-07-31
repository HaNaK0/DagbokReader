using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DagbokDataModel
{
	class DataContainer
	{
		private static DataContainer staticData;

		/// <summary>
		/// Static data that is shared across devices
		/// </summary>
		/// <exception cref="Exception">Throws an exception if the static data is not loaded</exception>
		public DataContainer StaticData
		{
			get
			{
				if (staticData == null)
				{
					throw new Exception("Static data was not loaded before access");
				}

				return staticData;
			}
		}

		/// <summary>
		/// Load static data
		/// </summary>
		/// <param name="path" default="">path to the static data</param>
		public static void LoadStaticData(string path = "StaticData.xml")
		{
			if (staticData == null)
			{
				staticData = new DataContainer("0.0");
				staticData.LoadFromXml(path);
			}
		}

		/// <summary>
		/// Create a new DataContainer
		/// </summary>
		/// <param name="versionString">A version</param>
		public DataContainer(string versionString)
		{
			Version = new Version(versionString);
		}

		/// <summary>
		/// Load data from a xml document
		/// </summary>
		/// <param name="path">path to the xml file to load</param>
		public void LoadFromXml(string path)
		{
			XElement rootElement = XElement.Load(path);

			Version = new Version(rootElement.Attribute("version").Value);

			XElement boatsElement = rootElement.Element("Boats");
			if (boatsElement != null)
			{
				MapDataString(ref boats, ref boatMap, boatsElement.Value);
			}

			XElement destinationElement = rootElement.Element("Destinations");
			if (destinationElement != null)
			{
				MapDataString(ref destinations, ref destinationMap, destinationElement.Value);
			}

			XElement crewElement = rootElement.Element("Crew");
			if (crewElement != null)
			{
				MapDataString(ref crew, ref crewMap, crewElement.Value);
			}

			XElement stringsElement = rootElement.Element("Strings");
			if (stringsElement != null)
			{
				MapDataString(ref strings, ref stringMap, stringsElement.Value);
			}
		}

		private static void MapDataString(ref List<List<string>> targetList, ref Dictionary<string, int> targetMap, string sourceString)
		{
			foreach (string row in sourceString.Split(';'))
			{
				int index = targetList.Count;
				List<string> rowList = new List<string>(row.Split(','));
				targetList.Add(rowList);

				foreach (string entry in rowList)
				{
					targetMap.Add(entry, index);
				}
			}
		}

		private static int MapNewData(ref List<List<string>> targetList, ref Dictionary<string, int> targetMap, IList<string> data)
		{
			int index = targetList.Count;
			targetList.Add(data.ToList());

			foreach (string entry in data)
			{
				targetMap.Add(entry, index);
			}

			return index;
		}

		/// <summary>
		/// Version of this data
		/// </summary>
		public Version Version { get; set; }

		private List<List<string>> boats;
		private Dictionary<string, int> boatMap;

		/// <summary>
		/// Get the Id of a boat from any of it's name
		/// </summary>
		/// <param name="boatName">A name of a boat</param>
		/// <returns>Id</returns>
		public int GetBoatId(string boatName) => boatMap[boatName];

		/// <summary>
		/// Get all the names for a boat from an Id
		/// </summary>
		/// <param name="id">the Id of a boat</param>
		/// <returns>returns a list with all the names</returns>
		public IList<string> GetBoatNames(int id) => boats[id];

		/// <summary>
		/// Add a boat
		/// </summary>
		/// <param name="boat">A string with the names of the boats separated by a comma</param>
		/// <returns>Id of the new boat</returns>
		public int AddBoat(string boat) => MapNewData(ref boats, ref boatMap, boat.Split(','));


		private List<List<string>> destinations;
		private Dictionary<string, int> destinationMap;

		/// <summary>
		/// Get the Id of a destination from its name
		/// </summary>
		/// <param name="DestinationName">A destination name</param>
		/// <returns>the id of the destination</returns>
		public int GetDestinationId(string DestinationName) => destinationMap[DestinationName];

		/// <summary>
		/// Get all the names for a destination from an Id
		/// </summary>
		/// <param name="id">the Id of a destination</param>
		/// <returns>returns a list with all the names</returns>
		public IList<string> GetDestinationNames(int id) => destinations[id];

		/// <summary>
		/// Add a destination
		/// </summary>
		/// <param name="destination">A string with the names and aliases of the destination separated by a comma</param>
		/// <returns>Id of the new boat</returns>
		public int AddDestination(string destination) => MapNewData(ref boats, ref boatMap, destination.Split(','));


		private List<List<string>> crew;
		private Dictionary<string, int> crewMap;

		/// <summary>
		/// Get the Id of a crew from their name
		/// </summary>
		/// <param name="CrewName">A crew name</param>
		/// <returns>The id of the destination</returns>
		public int GetCrewId(string CrewName) => crewMap[CrewName];

		/// <summary>
		/// Get all the names for a Crew from an Id
		/// </summary>
		/// <param name="id">the Id of a Crew</param>
		/// <returns>returns a list with all the names</returns>
		public IList<string> GetCrewNames(int id) => crew[id];

		/// <summary>
		/// Add a crew
		/// </summary>
		/// <param name="crewString">A string with the names and aliases of the crew separated by a comma</param>
		/// <returns>Id of the new crew</returns>
		public int AddCrew(string crewString) => MapNewData(ref crew, ref crewMap, crewString.Split(','));


		private List<List<string>> strings;
		private Dictionary<string, int> stringMap;

		/// <summary>
		/// Get the Id of a string
		/// </summary>
		/// <param name="aString">A string</param>
		/// <returns>The id of the string</returns>
		public int GetStringId(string aString) => stringMap[aString];

		/// <summary>
		/// Get a string and all aliases
		/// </summary>
		/// <param name="id">the Id of a string</param>
		/// <returns>returns a list with all the strings and aliases</returns>
		public IList<string> GetStrings(int id) => strings[id];

		/// <summary>
		/// Add a string
		/// </summary>
		/// <param name="stringData">Asociated strings separated with a comma</param>
		/// <returns>Id of the new string</returns>
		public int Addstring(string stringData) => MapNewData(ref strings, ref stringMap, stringData.Split(','));
	}
}
