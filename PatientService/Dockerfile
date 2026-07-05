FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["PatientService/PatientService.csproj", "PatientService/"]
RUN dotnet restore "PatientService/PatientService.csproj"
COPY . .
WORKDIR "/src/PatientService"
RUN dotnet publish "PatientService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5001
ENTRYPOINT ["dotnet", "PatientService.dll"]
