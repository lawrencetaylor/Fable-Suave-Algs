FROM microsoft/dotnet:1.1.0-sdk-msbuild-rc4
COPY /deploy /app
WORKDIR /app
EXPOSE 8085
ENTRYPOINT ["dotnet", "Server.dll"]