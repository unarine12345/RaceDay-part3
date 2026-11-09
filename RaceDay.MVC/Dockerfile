FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY RaceDay.MVC/RaceDay.MVC.csproj RaceDay.MVC/
RUN dotnet restore RaceDay.MVC/RaceDay.MVC.csproj
COPY . .
RUN dotnet publish RaceDay.MVC/RaceDay.MVC.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "RaceDay.MVC.dll"]
