# Build image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
COPY . .

RUN dotnet restore "./DealershipAPI/api.csproj" --disable-parallel
RUN dotnet publish "./DealershipAPI/api.csproj" -c release -o /app --no-restore


# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

# Expose the port your app listens on
EXPOSE 5000

# Set the entry point for your app
ENTRYPOINT ["dotnet", "api.dll", "--environment=Development"]
