# Teknik Özellikler ve Kullanım Kılavuzu

## 🔍 Proje Analizi ve Teknoloji Stack'i

### Framework ve Runtime Detayları

| Komponent | Teknoloji | Versiyon | Target Framework |
|-----------|-----------|----------|------------------|
| Web API | ASP.NET Core | 3.1 | netcoreapp3.1 |
| Business Layer | .NET Standard | 2.0 | netstandard2.0 |
| Data Access | .NET Standard | 2.0 | netstandard2.0 |
| Core Library | .NET Standard | 2.0 | netstandard2.0 |
| Entities | .NET Standard | 2.0 | netstandard2.0 |
| Console App | .NET Core | 3.1 | netcoreapp3.1 |

## 📦 NuGet Paketleri Detay Analizi

### 🔧 Dependency Injection & IoC
```xml
<!-- Autofac - Advanced IoC Container -->
<PackageReference Include="Autofac" Version="6.1.0" />
<PackageReference Include="Autofac.Extensions.DependencyInjection" Version="7.1.0" />
<PackageReference Include="Autofac.Extras.DynamicProxy" Version="6.0.0" />
```
**Kullanım Amacı:** 
- Advanced dependency injection
- AOP (Aspect-Oriented Programming) desteği
- Dynamic proxy generation
- Module-based service registration

**Kod Örneği:**
```csharp
builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
```

### 🛡️ Validation
```xml
<!-- FluentValidation - Rule-based validation -->
<PackageReference Include="FluentValidation" Version="9.5.1" />
```
**Kullanım Amacı:**
- Business rule validasyonları
- Fluent API ile validation rules
- Complex validation scenarios

**Kod Örneği:**
```csharp
public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.ProductName).NotEmpty().MinimumLength(2);
        RuleFor(p => p.UnitPrice).GreaterThan(0);
    }
}
```

### 🔐 Authentication & Security
```xml
<!-- JWT Authentication -->
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="3.1.12" />
<PackageReference Include="Microsoft.IdentityModel.Tokens" Version="6.8.0" />
<PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="6.8.0" />
```
**Kullanım Amacı:**
- JWT token tabanlı authentication
- API endpoint security
- Claims-based authorization

**Token Oluşturma:**
```csharp
public string CreateToken(User user, List<OperationClaim> operationClaims)
{
    _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
    var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
    var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
    var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
    var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    var token = jwtSecurityTokenHandler.WriteToken(jwt);
    return token;
}
```

### 💾 Data Access
```xml
<!-- Entity Framework Core -->
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="3.1.11" />
```
**Kullanım Amacı:**
- ORM (Object-Relational Mapping)
- Code First approach
- Database migrations
- LINQ query support

**Context Konfigürasyonu:**
```csharp
public class NorthwindContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true");
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}
```

### 📡 HTTP & Web Services
```xml
<!-- HTTP Context & Abstractions -->
<PackageReference Include="Microsoft.AspNetCore.Http" Version="2.2.2" />
<PackageReference Include="Microsoft.AspNetCore.Http.Abstractions" Version="2.2.0" />
```
**Kullanım Amacı:**
- HTTP context access
- Request/Response handling
- Middleware development

### 🔄 JSON Processing
```xml
<!-- JSON Serialization -->
<PackageReference Include="Newtonsoft.Json" Version="12.0.3" />
```
**Kullanım Amacı:**
- JSON serialization/deserialization
- API response formatting
- Complex object handling

## 🏗️ Architectural Patterns Implementation

### 1. Generic Repository Pattern
```csharp
public interface IEntityRepository<T> where T : class, IEntity, new()
{
    List<T> GetAll(Expression<Func<T, bool>> filter = null);
    T Get(Expression<Func<T, bool>> filter);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}

public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
{
    public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
    {
        using (TContext context = new TContext())
        {
            return filter == null
                ? context.Set<TEntity>().ToList()
                : context.Set<TEntity>().Where(filter).ToList();
        }
    }
}
```

### 2. Result Pattern Implementation
```csharp
public interface IResult
{
    bool Success { get; }
    string Message { get; }
}

public interface IDataResult<T> : IResult
{
    T Data { get; }
}

public class SuccessDataResult<T> : DataResult<T>
{
    public SuccessDataResult(T data, string message) : base(data, true, message) { }
    public SuccessDataResult(T data) : base(data, true) { }
    public SuccessDataResult(string message) : base(default(T), true, message) { }
    public SuccessDataResult() : base(default(T), true) { }
}
```

### 3. AOP Aspect Implementation
```csharp
public class ValidationAspect : MethodInterception
{
    private Type _validatorType;
    
    public ValidationAspect(Type validatorType)
    {
        if (!typeof(IValidator).IsAssignableFrom(validatorType))
            throw new System.Exception("Bu bir doğrulama sınıfı değil");
        _validatorType = validatorType;
    }
    
    protected override void OnBefore(IInvocation invocation)
    {
        var validator = (IValidator)Activator.CreateInstance(_validatorType);
        var entityType = _validatorType.BaseType.GetGenericArguments()[0];
        var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
        
        foreach (var entity in entities)
        {
            ValidationTool.Validate(validator, entity);
        }
    }
}
```

## 🔧 Konfigürasyon Detayları

### Startup.cs Middleware Pipeline
```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();
    
    // Custom exception middleware
    app.ConfigureCustomExceptionMiddleware();
    
    // CORS policy
    app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());
    
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication(); // JWT authentication
    app.UseAuthorization();  // Claims-based authorization
    
    app.UseEndpoints(endpoints => endpoints.MapControllers());
}
```

### JWT Configuration
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

### Service Registration
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddCors();
    
    var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
            };
        });
    
    services.AddDependencyResolvers(new ICoreModule[] { new CoreModule() });
}
```

## 📊 Database Schema

### Product Entity
```csharp
public class Product : IEntity
{
    public int ProductId { get; set; }         // Primary Key
    public int CategoryId { get; set; }        // Foreign Key
    public string ProductName { get; set; }    // Product name
    public short UnitsInStock { get; set; }    // Stock quantity
    public decimal UnitPrice { get; set; }     // Price
}
```

### Database Context Mapping
```csharp
public DbSet<Product> Products { get; set; }
public DbSet<Category> Categories { get; set; }
public DbSet<Customer> Customers { get; set; }
public DbSet<Order> Orders { get; set; }
public DbSet<User> Users { get; set; }
public DbSet<OperationClaim> OperationClaims { get; set; }
public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
```

## 🔍 Business Rules Implementation

### Product Business Rules
```csharp
public class ProductManager : IProductService
{
    [ValidationAspect(typeof(ProductValidator))]
    [CacheRemoveAspect("IProductService.Get")]
    [SecuredOperation("product.add,admin")]
    public IResult Add(Product product)
    {
        IResult result = BusinessRules.Run(
            CheckIfProductCountOfCategoryCorrect(product.CategoryId),
            CheckIfProductNameExists(product.ProductName),
            CheckIfCategoryLimitExceded()
        );

        if (result != null)
            return result;

        _productDal.Add(product);
        return new SuccessResult(Messages.ProductAdded);
    }
}
```

### Validation Rules
```csharp
private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
{
    var result = _productDal.GetAll(p => p.CategoryId == categoryId);
    if (result.Count >= 15)
        return new ErrorResult(Messages.ProductCountOfCategoryError);
    return new SuccessResult();
}

private IResult CheckIfProductNameExists(string productName)
{
    var result = _productDal.Get(p => p.ProductName == productName);
    if (result != null)
        return new ErrorResult(Messages.ProductNameAlreadyExists);
    return new SuccessResult();
}
```

## 🚀 Performance Features

### Caching Implementation
```csharp
[CacheAspect] // Default 60 minutes
public IDataResult<List<Product>> GetAll()
{
    return new SuccessDataResult<List<Product>>(
        _productDal.GetAll(), 
        Messages.ProductsListed
    );
}

[CacheRemoveAspect("IProductService.Get")] // Remove cache entries containing "IProductService.Get"
public IResult Add(Product product)
{
    // Add logic
}
```

### Performance Monitoring
```csharp
[PerformanceAspect(5)] // Log if execution takes more than 5 seconds
public IResult SlowOperation()
{
    // Method implementation
}
```

## 🔐 Security Implementation Details

### Role-based Authorization
```csharp
[SecuredOperation("product.add,admin")]
public IResult Add(Product product)
{
    // Only users with "product.add" or "admin" claims can access
}
```

### JWT Claims Structure
```csharp
private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
    SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
{
    var jwt = new JwtSecurityToken(
        issuer: tokenOptions.Issuer,
        audience: tokenOptions.Audience,
        expires: _accessTokenExpiration,
        notBefore: DateTime.Now,
        claims: SetClaims(user, operationClaims),
        signingCredentials: signingCredentials
    );
    return jwt;
}
```

## 🧪 Testing Strategy

### Unit Testing Setup (Recommendation)
```csharp
[Test]
public void Add_WhenProductIsValid_ShouldReturnSuccess()
{
    // Arrange
    var mockProductDal = new Mock<IProductDal>();
    var mockCategoryService = new Mock<ICategoryService>();
    var productManager = new ProductManager(mockProductDal.Object, mockCategoryService.Object);
    var product = new Product { ProductName = "Test Product", UnitPrice = 10 };

    // Act
    var result = productManager.Add(product);

    // Assert
    Assert.IsTrue(result.Success);
}
```

## 🔧 Development Tools & IDE Support

### Visual Studio Extensions (Recommendations)
- **Entity Framework Core Tools:** Migrations & scaffolding
- **JWT Debugger:** Token inspection
- **Postman:** API testing
- **SQL Server Object Explorer:** Database management

### CLI Commands
```bash
# Entity Framework migrations
dotnet ef migrations add InitialCreate
dotnet ef database update

# Package management
dotnet add package PackageName
dotnet restore
dotnet build
dotnet run

# Project creation
dotnet new webapi -n WebAPI
dotnet new classlib -n Business
```

## 📈 Monitoring & Logging

### Custom Exception Middleware
```csharp
public async Task InvokeAsync(HttpContext httpContext)
{
    try
    {
        await _next(httpContext);
    }
    catch (Exception e)
    {
        await HandleExceptionAsync(httpContext, e);
    }
}
```

### Performance Logging
```csharp
protected override void OnAfter(IInvocation invocation)
{
    if (_stopwatch.Elapsed.TotalSeconds > _interval)
    {
        Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
    }
    _stopwatch.Reset();
}
```

Bu teknik dokümantasyon, projedeki her teknolojinin nasıl ve neden kullanıldığını detaylı olarak açıklamaktadır. Geliştiricilerin projeyi anlamasını ve üzerinde çalışmasını kolaylaştırmak için oluşturulmuştur.