# Final Project - Proje Teknolojileri Dokümantasyonu

## 📋 Proje Genel Bakış

Bu proje, **N-Tier Architecture (Katmanlı Mimari)** yapısı kullanılarak geliştirilmiş bir **ASP.NET Core Web API** uygulamasıdır. Modern yazılım geliştirme prensiplerini ve design pattern'larını kullanarak enterprise-level bir e-ticaret sistemi geliştirilmiştir.

## 🛠️ Kullanılan Teknolojiler ve Frameworkler

### 🔧 Ana Teknolojiler

| Teknoloji | Versiyon | Açıklama |
|-----------|----------|----------|
| **.NET Core** | 3.1 | Ana framework (Web API için) |
| **.NET Standard** | 2.0 | Class library'ler için |
| **C#** | 8.0+ | Programlama dili |
| **Entity Framework Core** | 3.1.11 | ORM (Object-Relational Mapping) |
| **SQL Server** | LocalDB | Veritabanı sistemi |

### 📦 NuGet Paketleri ve Amaçları

#### Web API Katmanı
```xml
<PackageReference Include="Autofac.Extensions.DependencyInjection" Version="7.1.0" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="3.1.12" />
<PackageReference Include="Microsoft.AspNetCore.Http.Abstractions" Version="2.2.0" />
```

#### Business Katmanı
```xml
<PackageReference Include="Autofac" Version="6.1.0" />
<PackageReference Include="Autofac.Extensions.DependencyInjection" Version="7.1.0" />
<PackageReference Include="Autofac.Extras.DynamicProxy" Version="6.0.0" />
<PackageReference Include="FluentValidation" Version="9.5.1" />
<PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="5.0.1" />
```

#### Core Katmanı
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="3.1.11" />
<PackageReference Include="Microsoft.IdentityModel.Tokens" Version="6.8.0" />
<PackageReference Include="Newtonsoft.Json" Version="12.0.3" />
<PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="6.8.0" />
```

#### Data Access Katmanı
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="3.1.11" />
```

## 🏗️ Proje Mimarisi

### Katmanlı Mimari Yapısı

```
📁 FinalProject/
├── 📁 WebAPI/           # API Controller'ları ve Startup konfigürasyonları
├── 📁 Business/         # İş mantığı ve business rule'ları
├── 📁 DataAccess/       # Veri erişim katmanı (Repository Pattern)
├── 📁 Entities/         # Entity sınıfları ve DTO'lar
├── 📁 Core/            # Cross-cutting concerns ve utilities
└── 📁 ConsoleUI/       # Test amaçlı console uygulaması
```

### Kullanılan Design Patterns

1. **Repository Pattern** - Veri erişim soyutlaması
2. **Dependency Injection** - Bağımlılık yönetimi
3. **Aspect-Oriented Programming (AOP)** - Cross-cutting concerns
4. **Generic Repository Pattern** - Genel veri işlemleri
5. **Result Pattern** - İşlem sonuç yönetimi
6. **DTO Pattern** - Veri transfer nesneleri

## 🔐 Güvenlik Özellikleri

### JWT Authentication
- **JSON Web Token** tabanlı kimlik doğrulama
- Token tabanlı yetkilendirme sistemi
- Güvenli API endpoint'leri

### Authorization Aspects
- Role-based yetkilendirme
- Method-level güvenlik kontrolleri
- `[SecuredOperation]` attribute'ları ile endpoint güvenliği

## 🔄 Cross-Cutting Concerns (AOP)

### Validation Aspects
- **FluentValidation** ile otomatik doğrulama
- Business rule validasyonları
- `[ValidationAspect]` ile method seviyesinde doğrulama

### Caching Aspects
- Memory caching implementasyonu
- `[CacheAspect]` ile otomatik önbellekleme
- `[CacheRemoveAspect]` ile önbellek temizleme

### Performance Aspects
- Method execution süre ölçümü
- `[PerformanceAspect]` ile performans takibi
- Debug çıktıları ile performans analizi

### Transaction Aspects
- Otomatik transaction yönetimi
- `[TransactionScopeAspect]` ile işlem güvenliği

## 📊 Veri Yönetimi

### Entity Framework Core
- **Code First** yaklaşımı
- Migration support
- LINQ sorgu desteği
- SQL Server LocalDB bağlantısı

### Database Context
```csharp
// Connection String
Server=(localdb)\\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true
```

### Entity Sınıfları
- `Product` - Ürün bilgileri
- `Category` - Kategori bilgileri
- `Customer` - Müşteri bilgileri
- `User` - Kullanıcı bilgileri
- `OperationClaim` - Yetki tanımları

## 🔧 Dependency Injection & IoC

### Autofac Container
- Advanced dependency injection
- Module-based configuration
- Interceptor support for AOP
- Lifetime management

### Service Registrations
```csharp
builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
```

## 📡 API Endpoints

### Products Controller
- `GET /api/products` - Tüm ürünleri listele
- `GET /api/products/{id}` - Ürün detayı
- `POST /api/products` - Yeni ürün ekle
- `PUT /api/products` - Ürün güncelle

### Categories Controller
- `GET /api/categories` - Tüm kategorileri listele

### Auth Controller
- `POST /api/auth/login` - Kullanıcı girişi
- `POST /api/auth/register` - Kullanıcı kaydı

## 🚀 Kurulum ve Çalıştırma

### Gereksinimler
- .NET Core 3.1 SDK
- SQL Server LocalDB
- Visual Studio 2019+ veya VS Code

### Kurulum Adımları

1. **Repository'yi klonlayın:**
```bash
git clone https://github.com/emre2548/FinalProject.git
cd FinalProject
```

2. **Paketleri restore edin:**
```bash
dotnet restore
```

3. **Projeyi build edin:**
```bash
dotnet build
```

4. **Veritabanını oluşturun:**
```bash
dotnet ef database update
```

5. **API'yi çalıştırın:**
```bash
cd WebAPI
dotnet run
```

API şu adreste çalışacak: `https://localhost:5001`

## 🧪 Test Konfigürasyonu

### Console Application
- Test senaryoları için ConsoleUI projesi
- Business katmanı test işlemleri
- Dependency injection test'leri

## 📈 Performans Optimizasyonları

### Caching Strategy
- In-memory caching
- Method-level cache implementation
- Cache invalidation strategies

### Database Optimizations
- Entity Framework Core optimizations
- Eager loading strategies
- Query performance monitoring

## 🔧 Geliştirme Araçları

### IDE/Editor Support
- Visual Studio IntelliSense
- Code analysis ve refactoring
- Debugging support
- Package Manager UI

### Build & Deploy
- MSBuild build system
- NuGet package management
- Cross-platform compatibility

## 📋 Proje Yapılandırması

### Configuration Files
- `appsettings.json` - Ana konfigürasyon
- `appsettings.Development.json` - Development ayarları
- Token options ve connection string'ler

### CORS Configuration
- Frontend entegrasyonu için CORS desteği
- `http://localhost:4200` için Angular uygulaması desteği

## 🎯 Business Rules

### Product Management
- Ürün adı minimum 2 karakter
- Ürün fiyatı 0'dan büyük olmalı
- Kategori ID'si geçerli olmalı
- Kategori başına maksimum ürün sayısı kontrolü

### Validation Rules
- FluentValidation ile business rule validasyonları
- Custom validation rules
- Multilingual validation messages

## 🔄 Future Enhancements

- Redis cache implementation (klasör oluşturulmuş)
- Advanced logging mechanisms
- API versioning
- Swagger documentation
- Unit testing framework
- Integration testing

---

*Bu dokümantasyon proje içerisindeki tüm teknolojileri ve mimari kararları kapsamaktadır. Herhangi bir soru için repository issue'ları kullanabilirsiniz.*