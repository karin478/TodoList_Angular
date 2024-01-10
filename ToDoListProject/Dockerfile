#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# 复制 csproj 文件并执行 restore
COPY ["ToDoListProject/ToDoListProject.csproj", "ToDoListProject/"]
RUN dotnet restore "ToDoListProject/ToDoListProject.csproj"

# 复制整个 ToDoListProject 子目录
COPY ["ToDoListProject", "ToDoListProject"]
WORKDIR "/src/ToDoListProject"
RUN dotnet build "ToDoListProject.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "ToDoListProject.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoListProject.dll"]
