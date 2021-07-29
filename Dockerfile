FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RoomResizer.csproj", "./"]
RUN dotnet restore "RoomResizer.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "RoomResizer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RoomResizer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RoomResizer.dll"]
