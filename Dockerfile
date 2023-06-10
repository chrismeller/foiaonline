FROM mcr.microsoft.com/playwright/dotnet:latest AS base

RUN useradd dotnet


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# install the entity framework tools
RUN dotnet tool install --global dotnet-ef

WORKDIR /source

# copy only the solution and project files to reduce the chances the layer will be invalidated
COPY ./*.sln ./
COPY ./src/FoiaOnline.App/*.csproj ./src/FoiaOnline.App/
COPY ./src/FoiaOnline.Client/*.csproj ./src/FoiaOnline.Client/
COPY ./src/FoiaOnline.Data/*.csproj ./src/FoiaOnline.Data/
COPY ./src/FoiaOnline.Domain/*.csproj ./src/FoiaOnline.Domain/

# restore all nuget packages
RUN dotnet restore

# copy everything else
COPY ./* ./
COPY ./src/FoiaOnline.App/* ./src/FoiaOnline.App/
COPY ./src/FoiaOnline.Client/* ./src/FoiaOnline.Client/
COPY ./src/FoiaOnline.Data/* ./src/FoiaOnline.Data/
COPY ./src/FoiaOnline.Domain/* ./src/FoiaOnline.Domain/

# publish
RUN dotnet publish -c Release -o /app --no-restore ./src/FoiaOnline.App

# generate a migrations bundle
RUN dotnet tool restore
RUN dotnet ef migrations bundle --project ./src/FoiaOnline.App --no-build --verbose --configuration Release -o /app/efbundle.exe

# copy all our build artifacts over
FROM base AS release
WORKDIR /app
COPY --from=build --chown=dotnet:dotnet /app ./

# and run our app as that user
USER dotnet

CMD ["dotnet", "FoiaOnline.App.dll"]