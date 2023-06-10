FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base

RUN useradd dotnet


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# install the entity framework tools
RUN dotnet tool install --global dotnet-ef

WORKDIR /source

# copy only the solution and project files to reduce the chances the layer will be invalidated
COPY ./*.sln ./
COPY ./FoiaOnline.App/*.csproj ./FoiaOnline.App/
COPY ./FoiaOnline.Client/*.csproj ./FoiaOnline.Client/
COPY ./FoiaOnline.Data/*.csproj ./FoiaOnline.Data/
COPY ./FoiaOnline.Domain/*.csproj ./FoiaOnline.Domain/

# restore all nuget packages
RUN dotnet restore

# copy everything else
COPY ./* ./
COPY ./FoiaOnline.App/* ./FoiaOnline.App/
COPY ./FoiaOnline.Client/* ./FoiaOnline.Client/
COPY ./FoiaOnline.Data/* ./FoiaOnline.Data/
COPY ./FoiaOnline.Domain/* ./FoiaOnline.Domain/

# publish
RUN dotnet publish -c Release -o /app --no-restore ./FoiaOnline.App

# generate a migrations bundle
RUN dotnet tool restore
RUN dotnet ef migrations bundle --self-contained --project ./FoiaOnline.Data --startup-project ./FoiaOnline.App --no-build --verbose --configuration Release -o /app/efbundle.exe

# copy all our build artifacts over
FROM base AS release
WORKDIR /app
COPY --from=build --chown=dotnet:dotnet /app ./

# and run our app as that user
USER dotnet

CMD ["dotnet", "FoiaOnline.App.dll"]