# first image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY ./IssuesApi/IssuesApi.csproj .
RUN dotnet restore IssuesApi.csproj

# copy everything else and build app
COPY . .
RUN dotnet publish ./IssuesApi/IssuesApi.csproj -c release -o /publish

# final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /publish ./

ENTRYPOINT [ "dotnet", "IssuesApi.dll" ]

EXPOSE 80
