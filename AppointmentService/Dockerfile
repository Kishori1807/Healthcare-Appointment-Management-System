FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["AppointmentService/AppointmentService.csproj", "AppointmentService/"]
RUN dotnet restore "AppointmentService/AppointmentService.csproj"
COPY . .
WORKDIR "/src/AppointmentService"
RUN dotnet publish "AppointmentService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5003
ENTRYPOINT ["dotnet", "AppointmentService.dll"]
