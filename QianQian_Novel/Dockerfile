#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["QianQian_Novel/QianQian_Novel.csproj", "QianQian_Novel/"]
RUN dotnet restore "QianQian_Novel/QianQian_Novel.csproj"
COPY . .
WORKDIR "/src/QianQian_Novel"
RUN dotnet build "QianQian_Novel.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QianQian_Novel.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QianQian_Novel.dll"]