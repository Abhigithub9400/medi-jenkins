# Base Linux image
FROM ubuntu:22.04 AS base

# Install dependencies
RUN apt-get update && apt-get install -y \
    wget \
    apt-transport-https \
    software-properties-common \
    curl

# Install .NET SDK and runtime
RUN wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y dotnet-sdk-8.0 aspnetcore-runtime-8.0

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_20.x | bash - \
    && apt-get install -y nodejs

WORKDIR /app
EXPOSE 5038

# Set the ASPNETCORE_URLS environment variable
ENV ASPNETCORE_URLS=http://+:5038

# Node.js build stage for the ClientApp
FROM base AS node-build
WORKDIR /src/MediAssist.UI/ClientApp
COPY MediAssist.UI/ClientApp/package*.json ./
RUN npm ci
COPY MediAssist.UI/ClientApp/. ./
RUN npm run build

# .NET build stage
FROM base AS build
WORKDIR /src
COPY ["MediAssist.UI/MediAssist.UI.csproj", "MediAssist.UI/"]
COPY ["MediAssist.Application/MediAssist.Application.csproj", "MediAssist.Application/"]
COPY ["MediAssist.Application.Abstract/MediAssist.Application.Abstract.csproj", "MediAssist.Application.Abstract/"]
COPY ["MediAssist.Infrastructure.Abstract/MediAssist.Infrastructure.Abstract.csproj", "MediAssist.Infrastructure.Abstract/"]
COPY ["MediAssist.Dependency/MediAssist.Dependency.csproj", "MediAssist.Dependency/"]
COPY ["MediAssist.Configurations/MediAssist.Configurations.csproj", "MediAssist.Configurations/"]
RUN dotnet restore "MediAssist.UI/MediAssist.UI.csproj"
COPY . .
WORKDIR "/src/MediAssist.UI"
RUN dotnet build "MediAssist.UI.csproj" -c Release -o /app/build

# Publish the .NET application
FROM build AS publish
RUN dotnet publish "MediAssist.UI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=node-build /src/MediAssist.UI/ClientApp/dist ./ClientApp/dist
ENTRYPOINT ["dotnet", "MediAssist.UI.dll", "--urls", "http://+:5038"]
