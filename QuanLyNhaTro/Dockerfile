# ==== Bước 1: Build ====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy file dự án trước để tận dụng cache khi restore
COPY QuanLyNhaTro.csproj ./
RUN dotnet restore

# Copy phần còn lại của mã nguồn và build
COPY . .
RUN dotnet publish -c Release -o /app/publish --no-restore

# ==== Bước 2: Chạy ====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 10000

# Render gán biến PORT khi khởi động container (shell-form CMD nên biến được mở rộng lúc chạy,
# không phải lúc build). Mặc định 8080 khi chạy Docker cục bộ.
CMD ["sh", "-c", "exec dotnet QuanLyNhaTro.dll --urls http://0.0.0.0:${PORT:-8080}"]
