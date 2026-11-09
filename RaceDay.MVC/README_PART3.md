# RaceDay Part 3 - Clean Rebuild

This folder contains the Part 2 API, Part 2 tests, and the Part 3 MVC project in one Visual Studio solution.

## Open in Visual Studio 2022
1. Extract the complete ZIP to a new folder (do not extract over an older copy).
2. Open `Part2/RaceDay.sln`.
3. Confirm all three projects load: `RaceDay.API`, `RaceDay.MVC`, and `RaceDay.Tests`.
4. If Visual Studio asks to restore NuGet packages, choose Restore.
5. Set multiple startup projects: API = Start, MVC = Start, Tests = None.
6. Check `RaceDay.MVC/appsettings.json` and set `ApiBaseUrl` to the HTTP URL shown by the API launch profile (normally `http://localhost:63904/`).
7. Set the SQL Server connection string in `RaceDay.API/appsettings.json` to the database instance on your computer.

## Azure Blob Storage
Set `AzureBlobStorage:ConnectionString` in API configuration or the environment variable `AzureBlobStorage__ConnectionString`. Do not commit a real connection string to GitHub. The API upload endpoints accept image uploads for event banners and profile pictures. The returned URL must be saved against the relevant record by the corresponding API feature before it can appear automatically in the UI.

## Docker (MVC)
From the `Part2/RaceDay.MVC` folder:
`docker build -t racedday-mvc .`
`docker run --rm -p 8080:8080 -e ApiBaseUrl=http://host.docker.internal:63904/ racedday-mvc`
The API and SQL Server must be running separately and reachable from the MVC container.

## Important implementation note
This is a clean project-structure rebuild to address the previous project-loading errors. The MVC screens and API client are an initial Part 3 implementation, not a verified complete submission. Features such as full organiser category/result administration and complete profile-picture persistence still require implementation and end-to-end testing against your local SQL Server and Azure account. This environment does not have the .NET SDK, so a Visual Studio build could not be run here.
