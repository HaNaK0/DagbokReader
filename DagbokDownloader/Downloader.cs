using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DagbokDownloader
{
	public class Downloader : IDisposable
	{
		// If modifying these scopes, delete your previously saved credentials
		// at ~/.credentials/drive-dotnet-quickstart.json
		static string[] Scopes = { DriveService.Scope.DriveReadonly };
		static string ApplicationName = "Drive API .NET Quickstart";
		readonly DriveService driveService;

		public Downloader()
		{
			UserCredential credential;

			using (var stream =
				new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
			{
				// The file token.json stores the user's access and refresh tokens, and is created
				// automatically when the authorization flow completes for the first time.
				string credPath = "token.json";
				credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
					GoogleClientSecrets.Load(stream).Secrets,
					Scopes,
					"user",
					CancellationToken.None,
					new FileDataStore(credPath, true)).Result;
				Console.WriteLine("Credential file saved to: " + credPath);
			}

			// Create Drive API service.
			driveService = new DriveService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = credential,
				ApplicationName = ApplicationName,
			});
		}

		public struct FileIdentifier
		{
			public FileIdentifier(string aName, string id)
			{
				name = aName;
				fileId = id;
			}

			public new string ToString => "File(name: " + name + ", id:" + fileId + ")";

			public readonly string name;
			public readonly string fileId;
		}

		public bool IsValid => driveService != null;

		public IEnumerable<FileIdentifier> QueryForFiles(string aName)
		{
			string pageToken = null;

			do
			{
				FilesResource.ListRequest listRequest = driveService.Files.List();
				listRequest.Q = "name = '" + aName + "'";
				listRequest.Spaces = "drive";
				listRequest.Fields = "nextPageToken, files(id, name)";
				listRequest.PageToken = pageToken;


				FileList result = listRequest.Execute();
				foreach (Google.Apis.Drive.v3.Data.File file in result.Files)
				{
					yield return new FileIdentifier(file.Name, file.Id);
				}
				pageToken = result.NextPageToken;
			} while (pageToken != null);

			yield break;
		}

		public void DownloadFile(string fileId, ref Stream stream)
		{
			Google.Apis.Download.IDownloadProgress result = driveService.Files.Export(fileId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet").DownloadWithStatus(stream);

			Console.WriteLine(result);
		}

		public void Dispose()
		{
			driveService.Dispose();
		}
	}
}
