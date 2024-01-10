#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ToDoListProject/ToDoListProject.csproj", "ToDoListProject/"]
RUN dotnet restore "ToDoListProject/ToDoListProject.csproj"
COPY ["ToDoListProject", "ToDoListProject"]
WORKDIR "/src/ToDoListProject"
RUN dotnet build "ToDoListProject.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 在这里执行数据库迁移
FROM build AS migrate
WORKDIR /app
COPY --from=build /app/build .
RUN dotnet ef database update

FROM build AS publish
RUN dotnet publish "ToDoListProject.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoListProject.dll"]
