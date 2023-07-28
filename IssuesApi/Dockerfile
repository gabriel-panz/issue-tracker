FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app
COPY ./bin/Debug/net7.0/linux-x64/publish .

ENTRYPOINT [ "dotnet", "IssuesApi.dll" ]

EXPOSE 80
