FROM node AS node-build-env
WORKDIR /app

COPY . ./
WORKDIR /app/ProcessWorker.Web/process-worker
RUN npm install
RUN npm run deploy

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy everything else and build
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out
COPY --from=node-build-env /app/ProcessWorker.Web/process-worker/dist/process-worker out/process-worker/dist


# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ProcessWorker.Web.dll"]