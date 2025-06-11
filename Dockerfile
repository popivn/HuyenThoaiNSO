FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM base AS build
WORKDIR /src
COPY ["HuyenThoaiNSO.csproj", "./"]
RUN dotnet restore "HuyenThoaiNSO.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "HuyenThoaiNSO.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HuyenThoaiNSO.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "watch", "run", "--no-launch-profile"] 