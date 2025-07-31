using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using CosmosChangeFeed.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var blobStorageSettings = new BlobStorageSettings();
configuration.GetSection("BlobStorage").Bind(blobStorageSettings);

var blobServiceClient = new BlobServiceClient(blobStorageSettings.ConnectionString);

string containerName = "quickstartblobs" + Guid.NewGuid().ToString();

BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

string localPath = "data";
Directory.CreateDirectory(localPath);
string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
string localFilePath = Path.Combine(localPath, fileName);

await File.WriteAllTextAsync(localFilePath, "Hello, World!");

BlobClient blobClient = containerClient.GetBlobClient(fileName);

Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

await blobClient.UploadAsync(localFilePath, true);

Console.WriteLine("Listing blobs...");

await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
{
    Console.WriteLine("\t" + blobItem.Name);
}

string downloadFilePath = localFilePath.Replace(".txt", "DOWNLOADED.txt");

Console.WriteLine("\nDownloading blob to\n\t{0}\n", downloadFilePath);

await blobClient.DownloadToAsync(downloadFilePath);

Console.Write("Press any key to begin clean up");
Console.ReadLine();

Console.WriteLine("Deleting blob container...");
await containerClient.DeleteAsync();
