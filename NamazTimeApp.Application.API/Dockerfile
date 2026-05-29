FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore NamazTimeApp.Application.API/NamazTimeApp.Application.API.csproj
RUN dotnet publish NamazTimeApp.Application.API/NamazTimeApp.Application.API.csproj \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5001
EXPOSE 5001

COPY --from=build /app/publish .

USER app
ENTRYPOINT ["dotnet", "NamazTimeApp.Application.API.dll"]
