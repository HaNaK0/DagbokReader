using System;
using DagbokDownloader;

namespace DagbokReader
{
	class Program
	{
		static void Main(string[] args)
		{
			Downloader downloader = new Downloader();

			foreach (Downloader.FileIdentifier file in downloader.QueryForFiles("20-26 22 juni - 28 juni"))
			{
				Console.WriteLine("File: (" + file.name + ", " + file.fileId +")");
			}
		}
	}
}
