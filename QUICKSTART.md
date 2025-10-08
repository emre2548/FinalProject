# Hızlı Başlangıç Kılavuzu

## 🚀 Projeyi Hızlıca Çalıştırma

### 1. Gereksinimler
- **.NET Core 3.1 SDK** - [İndirme Linki](https://dotnet.microsoft.com/download/dotnet/3.1)
- **SQL Server LocalDB** - Visual Studio ile birlikte gelir
- **Visual Studio 2019+** veya **VS Code**

### 2. Kurulum Adımları

```bash
# 1. Projeyi klonlayın
git clone https://github.com/emre2548/FinalProject.git
cd FinalProject

# 2. Paketleri yükleyin
dotnet restore

# 3. Projeyi build edin
dotnet build

# 4. Web API'yi başlatın
cd WebAPI
dotnet run
```

### 3. API Test Etme

**Base URL:** `https://localhost:5001`

#### Test Endpoint'leri:
```bash
# Tüm ürünleri listele
GET https://localhost:5001/api/products

# Kategori listesi
GET https://localhost:5001/api/categories

# Kullanıcı kaydı (örnek)
POST https://localhost:5001/api/auth/register
{
    "firstName": "John",
    "lastName": "Doe",
    "email": "john@example.com",
    "password": "123456"
}
```

## 📚 Dokümantasyon İndeksi

| Dosya | İçerik |
|-------|--------|
| `README.md` | Genel proje bilgisi ve teknoloji özeti |
| `ARCHITECTURE.md` | Detaylı mimari açıklaması |
| `TECHNICAL_SPECS.md` | Teknik detaylar ve kod örnekleri |
| `QUICKSTART.md` | Bu dosya - Hızlı başlangıç |

## 🎯 Önemli Notlar

### Veritabanı
- **SQL Server LocalDB** otomatik olarak kullanılır
- Bağlantı string'i: `Server=(localdb)\\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true`

### Authentication
- JWT token tabanlı kimlik doğrulama
- Token süresi: 10 dakika (konfigürasyondan değiştirilebilir)

### CORS
- Frontend için `http://localhost:4200` adresine izin verilir
- Angular uygulaması için optimize edilmiş

## 🔧 Geliştirme İpuçları

### IDE Önerilen Eklentiler
- **C# Extension** (VS Code için)
- **Entity Framework Tools**
- **REST Client** (API test için)

### Faydalı Komutlar
```bash
# Migration oluşturma
dotnet ef migrations add MigrationName

# Veritabanını güncelleme  
dotnet ef database update

# Projeyi watch mode'da çalıştırma
dotnet watch run
```

### Console App Çalıştırma
```bash
cd ConsoleUI
dotnet run
```

Bu dosya ile projeyi hızlıca anlamak ve çalıştırmak mümkündür!