# RaceDay Part 3 – MVC integration

## Projects
- `RaceDay.API`: existing Part 2 API and SQL Server/EF Core backend.
- `RaceDay.MVC`: MVC frontend. It communicates with the API over HTTP; it does not access SQL Server directly.
- `RaceDay.Tests`: existing test project.

## Open in Visual Studio
1. Open `RaceDay.sln`.
2. Restore NuGet packages.
3. Configure `RaceDay.API` and `RaceDay.MVC` as startup projects (both Start); set `RaceDay.Tests` to None.
4. Confirm the API launch profile listens on `http://localhost:63904`.
5. Configure the SQL Server `DefaultConnection` in `RaceDay.API/appsettings.json`.
6. Run both projects.

## Important integration notes
The MVC client forwards the API cookie so the API remains responsible for authentication and authorization. The API login response uses `Id` and `Role`; MVC reads JSON property names case-insensitively.

The supplied Part 2 API exposes:
- `POST api/auth/register`, `POST api/auth/login`, `POST api/auth/logout`
- `GET api/events`, `GET api/events/{id}`, `POST api/events`, `PUT api/events/{id}`, `DELETE api/events/{id}`
- `GET/POST api/events/{eventId}/categories`, `DELETE api/events/{eventId}/categories/{categoryId}`
- `POST api/enrolments/events/{eventId}`, `GET api/enrolments/mine`
- `GET api/events/{id}/enrolments`
- `POST api/results/enrolments/{enrolmentId}`, `GET api/results/mine`
- `PUT api/profile`

## Azure Blob Storage
Set `AzureBlobStorage:ConnectionString` in API configuration before using the upload endpoints. Do not commit a real connection string to source control. Upload endpoint responses and URLs should be checked against your Azure Storage account/container configuration.

## Docker
Build from the `Part2` directory:
`docker build -f RaceDay.MVC/Dockerfile -t raceday-mvc .`
Run:
`docker run --rm -p 8080:8080 -e ApiBaseUrl=http://host.docker.internal:63904/ raceday-mvc`

The API must be running and reachable from the container. For Linux Docker hosts, `host.docker.internal` may need explicit host-gateway configuration.

## Before submission
Build and test in Visual Studio. The MVC project is an integration implementation, not a guarantee that every rubric feature is complete: verify every view/API response shape against your running API. In particular, verify event banner persistence, participant profile image persistence, and result display. Profile picture upload and saving image URLs require corresponding API/model/database support and must not be claimed complete until implemented and tested.
