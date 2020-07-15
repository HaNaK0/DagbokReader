using System;
using System.Linq;
using System.Collections.Generic;

using DagbokDownloader;
using System.IO;

namespace DagbokReader
{
	class Program
	{
		static void Main(string[] args)
		{
			Downloader downloader = new Downloader();

			Console.WriteLine("Which file name should be used?");
			string fileName = Console.ReadLine();

			IList<Downloader.FileIdentifier> files = downloader.QueryForFiles(fileName).ToList();

			int index = 0;
			if (files.Count == 0)
			{
				Console.WriteLine("No file found");
				return;
			}
			else if (files.Count == 1)
			{
				Console.WriteLine("Found 1 file");
			}
			else
			{
				Console.WriteLine("Found " + files.Count + " files");
				for (int i = 0; i < files.Count; i++)
				{
					Console.WriteLine(i.ToString() + ": " + files[i]);
				}
				index = ConsoleReadInt(0, files.Count);
			}

			Downloader.FileIdentifier file = files[index];
			Console.WriteLine("Will use file " + file.ToString);
			string tagetFilePath = file.name + ".xlsx";

			
			using (FileStream fileStream = File.Open(tagetFilePath, FileMode.Create))
			{
				Stream stream = fileStream;
				downloader.DownloadFile(file.fileId, ref stream);
			}
		}

		private static int ConsoleReadInt(int min, int max)
		{
			int result;
			string input;
			do
			{
				Console.WriteLine("Please input a number between " + min + "and " + (max - 1));
				input = Console.ReadLine();
			} while (!int.TryParse(input, out result) || result < min || result >= max);

			return result;
		}
	}
}
