FROM microsoft/dotnet:2.1-sdk AS builder
WORKDIR /source

COPY *.csproj .
RUN dotnet restore

COPY ./ ./

RUN dotnet publish "./Engineering Project.csproj" --output "./dist" --no-restore

FROM microsoft/dotnet:2.1-sdk
WORKDIR /app
COPY --from=builder /source/dist .
EXPOSE 80
ENTRYPOINT ["dotnet", "Engineering Project.dll"]