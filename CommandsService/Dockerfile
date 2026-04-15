#Base Image for build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env  

#Set working directory
WORKDIR /app 

#Copy csproj file to the working directory
COPY *.csproj ./ 

#Restore Dependencies
RUN dotnet restore 

#Copy remaining files and folders`
COPY . ./ 

# Publish the application using the Release configuration and out as the output directory
RUN dotnet publish -c Release -o out  
#Build stage complete

#Base Image for runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
#Set working directory for runtime stage
WORKDIR /app

#Copy published output from build stage to /app directory in runtime stage
COPY --from=build-env /app/out .

#start .NET application
ENTRYPOINT ["dotnet", "CommandsService.dll"]