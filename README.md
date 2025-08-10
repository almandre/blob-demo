# Azure Blob Storage Demo (.NET)

This project demonstrates how to use Azure Blob Storage with .NET, including configuration via `appsettings.json` and modern C# features like object deconstruction.

## Features

- Reads the connection string from `appsettings.json`
- Creates a unique blob container
- Uploads a text file to the container
- Lists blobs in the container
- Downloads the blob to a local file
- Cleans up by deleting the container

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- An Azure Storage Account (not Cosmos DB)
- The connection string for your Storage Account

## How to Run

1. Clone this repository.
2. Set your Azure Storage connection string in `appsettings.json`:

    ```json
    {
      "BlobStorage": {
        "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=YOUR_ACCOUNT;AccountKey=YOUR_KEY;EndpointSuffix=core.windows.net"
      }
    }
    ```

3. Restore dependencies:

    ```powershell
    dotnet restore
    ```

4. Build and run the demo:

    ```powershell
    dotnet run
    ```

## Project Structure

- `Program.cs` — Main demo logic
- `Configuration/BlobStorageSettings.cs` — Configuration class with deconstruction support
- `appsettings.json` — Configuration file for secrets

## Notes

- The demo uses deconstruction to extract the connection string from the settings object, similar to JavaScript destructuring.
- The container and blob names are generated using GUIDs to avoid conflicts.
- The demo deletes the container at the end for cleanup.

## License

This project is for educational purposes.
