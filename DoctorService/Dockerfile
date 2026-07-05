FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["DoctorService/DoctorService.csproj", "DoctorService/"]
RUN dotnet restore "DoctorService/DoctorService.csproj"
COPY . .
WORKDIR "/src/DoctorService"
RUN dotnet publish "DoctorService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5002
ENTRYPOINT ["dotnet", "DoctorService.dll"]
