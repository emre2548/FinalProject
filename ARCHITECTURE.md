# Proje Mimari Detayları

## 🏛️ Katmanlı Mimari (N-Tier Architecture)

Bu proje, **Clean Architecture** ve **Domain Driven Design** prensiplerini takip eden N-Tier architecture pattern'ı kullanmaktadır.

## 📁 Katman Yapısı Detayları

### 1. WebAPI Katmanı (Presentation Layer)
**Amaç:** API endpoint'leri ve HTTP isteklerini yönetme

**İçerik:**
- **Controllers:** REST API endpoint'leri
  - `ProductsController.cs` - Ürün CRUD işlemleri
  - `CategoriesController.cs` - Kategori listeleme
  - `AuthController.cs` - Kimlik doğrulama
- **Startup.cs:** Dependency injection, middleware konfigürasyonu
- **Program.cs:** Uygulama entry point'i

**Önemli Konfigürasyonlar:**
```csharp
// JWT Authentication
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options => { ... });

// CORS Policy
app.UseCors(builder => builder.WithOrigins("http://localhost:4200"));
```

### 2. Business Katmanı (Business Logic Layer)
**Amaç:** İş mantığı, business rules ve validation

**Önemli Sınıflar:**
- **Abstract:** Interface tanımları (`IProductService`, `ICategoryService`)
- **Concrete:** Business logic implementasyonları (`ProductManager`)
- **ValidationRules:** FluentValidation kuralları (`ProductValidator`)
- **Constants:** Sabit mesajlar ve değerler
- **BusinessAspect:** Güvenlik aspect'leri (`SecuredOperation`)

**Business Rules Örnekleri:**
```csharp
// Kategori başına maksimum 15 ürün
private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
{
    var result = _productDal.GetAll(p => p.CategoryId == categoryId);
    if (result.Count >= 15)
        return new ErrorResult(Messages.ProductCountOfCategoryError);
    return new SuccessResult();
}
```

### 3. DataAccess Katmanı (Data Access Layer)
**Amaç:** Veri tabanı işlemleri ve Entity Framework context

**Yapı:**
- **Abstract:** Repository interface'leri (`IProductDal`, `ICategoryDal`)
- **Concrete/EntityFramework:** EF Core implementasyonları (`EfProductDal`)
- **Context:** Database context (`NorthwindContext`)

**Generic Repository Pattern:**
```csharp
public interface IEntityRepository<T> where T : class, IEntity, new()
{
    List<T> GetAll(Expression<Func<T, bool>> filter = null);
    T Get(Expression<Func<T, bool>> filter);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
```

### 4. Entities Katmanı (Data Transfer Objects)
**Amaç:** Veri transfer nesneleri ve entity sınıfları

**İçerik:**
- **Concrete:** Entity sınıfları (`Product`, `Category`, `User`)
- **DTOs:** Data Transfer Objects (`ProductDetailDto`)

**Entity Örneği:**
```csharp
public class Product : IEntity
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public string ProductName { get; set; }
    public short UnitsInStock { get; set; }
    public decimal UnitPrice { get; set; }
}
```

### 5. Core Katmanı (Infrastructure Layer)
**Amaç:** Cross-cutting concerns, utilities, genel infrastructure

**Alt Modüller:**

#### **Aspects (AOP Implementation)**
- **ValidationAspect:** Otomatik validation
- **CacheAspect:** Caching mekanizması  
- **PerformanceAspect:** Performans ölçümü
- **TransactionScopeAspect:** Transaction yönetimi
- **SecuredOperation:** Yetkilendirme kontrolü

#### **Utilities**
- **Results:** Sonuç yönetimi (`IResult`, `IDataResult`)
- **Security/JWT:** JWT token yönetimi
- **IoC:** Dependency injection helper'ları
- **Business:** Business rules runner
- **Interceptors:** AOP interceptor base sınıfları

#### **CrossCuttingConcerns**
- **Validation:** Validation araçları
- **Caching:** Cache yöneticileri
- **Logging:** Loglama araçları (interface'ler)

## 🔄 Aspect-Oriented Programming (AOP) Detayları

### Interceptor Mekanizması
Proje **Castle DynamicProxy** kullanarak AOP implementasyonu yapmaktadır.

```csharp
[ValidationAspect(typeof(ProductValidator))]
[CacheRemoveAspect("IProductService.Get")]
[SecuredOperation("product.add,admin")]
public IResult Add(Product product)
{
    // Business logic burada
}
```

### Aspect Execution Order
1. **SecuredOperation** - Yetki kontrolü
2. **ValidationAspect** - Input validation
3. **CacheRemoveAspect** - Cache temizleme
4. **TransactionScopeAspect** - Transaction başlatma
5. **PerformanceAspect** - Performans ölçümü
6. **Method Execution** - Asıl method çalışması

## 🔐 Güvenlik Mimarisi

### JWT Token Structure
```json
{
  "sub": "userId",
  "email": "user@example.com",
  "roles": ["Admin", "User"],
  "exp": 1643723400,
  "iss": "myapp",
  "aud": "myapp-users"
}
```

### Authorization Flow
1. User login → JWT token oluştur
2. Token'ı client'a gönder
3. Her API çağrısında token validate et
4. Claim'leri kontrol et
5. Method'a erişim izni ver/reddet

## 📊 Veri Akış Mimarisi

### Request Flow
```
Client → Controller → Business → DataAccess → Database
                  ↓
              Aspects (AOP)
              ↓
          Validation, Caching, Security, Logging
```

### Response Flow
```
Database → DataAccess → Business → Controller → Client
                                ↓
                            Result Pattern
                            (Success/Error)
```

## 🎯 Design Patterns Kullanımı

### 1. Repository Pattern
Veri erişim soyutlaması için kullanılır.
```csharp
public interface IProductDal : IEntityRepository<Product>
{
    List<ProductDetailDto> GetProductDetails();
}
```

### 2. Dependency Injection Pattern
Bağımlılıkları runtime'da çözümler.
```csharp
public ProductManager(IProductDal productDal, ICategoryService categoryService)
{
    _productDal = productDal;
    _categoryService = categoryService;
}
```

### 3. Result Pattern
İşlem sonuçlarını tutarlı şekilde döndürür.
```csharp
public IDataResult<List<Product>> GetAll()
{
    return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
}
```

### 4. Factory Pattern
ServiceTool ile IoC container'ı yönetir.
```csharp
public static class ServiceTool
{
    public static IServiceProvider ServiceProvider { get; private set; }
    
    public static IServiceCollection Create(IServiceCollection services)
    {
        ServiceProvider = services.BuildServiceProvider();
        return services;
    }
}
```

## 🔧 Dependency Injection Konfigürasyonu

### Autofac Module Configuration
```csharp
protected override void Load(ContainerBuilder builder)
{
    // Business layer registrations
    builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
    
    // Data access layer registrations
    builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
    
    // Authentication & Authorization
    builder.RegisterType<AuthManager>().As<IAuthService>();
    builder.RegisterType<JwtHelper>().As<ITokenHelper>();
    
    // AOP Interceptor registration
    var assembly = System.Reflection.Assembly.GetExecutingAssembly();
    builder.RegisterAssemblyTypes(assembly)
        .AsImplementedInterfaces()
        .EnableInterfaceInterceptors(new ProxyGenerationOptions()
        {
            Selector = new AspectInterceptorSelector()
        }).SingleInstance();
}
```

## 📈 Performans ve Optimizasyon

### Caching Strategy
- **L1 Cache:** Memory cache
- **Cache Keys:** Method signature + parameters
- **Cache Duration:** Configurable (default 60 minutes)
- **Cache Invalidation:** Specific patterns

### Database Optimizations
- **Eager Loading:** İlgili verileri tek sorguda çekme
- **Projection:** Sadece gerekli alanları seçme
- **Indexing:** Database index stratejileri

### Code Optimizations
- **Lazy Loading:** Gerektiğinde yükleme
- **Singleton Pattern:** Tek instance kullanımı
- **Expression Trees:** LINQ optimizasyonları

## 🧪 Test Mimarisi

### Unit Testing Approach
- Business logic unit testleri
- Mock dependencies ile izolasyon
- AAA pattern (Arrange, Act, Assert)

### Integration Testing
- API endpoint testleri
- Database integration testleri
- Authentication flow testleri

## 🔄 Error Handling Strategy

### Global Exception Middleware
```csharp
app.ConfigureCustomExceptionMiddleware();
```

### Business Rule Violations
```csharp
var result = BusinessRules.Run(
    CheckIfProductCountOfCategoryCorrect(product.CategoryId),
    CheckIfProductNameExists(product.ProductName),
    CheckIfCategoryLimitExceded()
);

if (result != null)
    return result;
```

## 📋 Configuration Management

### Environment-based Configuration
- **Development:** Local database, verbose logging
- **Production:** Remote database, minimal logging

### Configuration Sections
```json
{
  "TokenOptions": {
    "Audience": "myapp-users",
    "Issuer": "myapp",
    "AccessTokenExpiration": 10,
    "SecurityKey": "mysecretkeymysecretkey"
  }
}
```

Bu mimari, **SOLID prensipleri**ni takip ederek **maintainable**, **testable** ve **scalable** bir yazılım sistemi oluşturmaktadır.