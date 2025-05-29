FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Tạo user không phải root
RUN useradd -m appuser
USER appuser

WORKDIR /app
COPY ./publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Project_BanSach.dll"]
