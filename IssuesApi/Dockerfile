# first image
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /source
COPY . .

RUN dotnet restore
RUN dotnet publish -c release -o /app --no-restore

# final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT [ "dotnet", "IssuesApi.dll" ]

EXPOSE 80
