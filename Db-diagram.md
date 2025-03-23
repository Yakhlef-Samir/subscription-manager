# Diagramme de Classes - Application de Gestion d'Abonnements (Backend C#)

Ce diagramme représente la structure des classes pour l'implémentation du backend en C# de l'application de gestion d'abonnements.

## Entités Principales

### User
```
+------------------+
| User             |
+------------------+
| + Id: Guid       |
| + Email: string  |
| + UserName: string|
| + DisplayName: string|
| + PasswordHash: string|
| + CreatedAt: DateTime|
| + UpdatedAt: DateTime|
| + IsActive: bool |
+------------------+
| + Subscriptions  |
+------------------+
```

### Subscription
```
+----------------------+
| Subscription         |
+----------------------+
| + Id: Guid           |
| + Name: string       |
| + Description: string|
| + Price: decimal     |
| + Currency: string   |
| + Status: SubscriptionStatus|
| + StartDate: DateTime|
| + NextBillingDate: DateTime|
| + BillingCycle: string|
| + Provider: string   |
| + UserId: Guid       |
| + CategoryId: Guid   |
| + CreatedAt: DateTime|
| + UpdatedAt: DateTime|
+----------------------+
| + User               |
| + Category           |
| + Payments           |
+----------------------+
```

### Category
```
+------------------+
| Category         |
+------------------+
| + Id: Guid       |
| + Name: string   |
| + Description: string|
| + IconName: string|
| + CreatedAt: DateTime|
| + UpdatedAt: DateTime|
+------------------+
| + Subscriptions  |
+------------------+
```

### Payment
```
+------------------+
| Payment          |
+------------------+
| + Id: Guid       |
| + Amount: decimal|
| + PaymentDate: DateTime|
| + Status: PaymentStatus|
| + SubscriptionId: Guid|
| + PaymentMethod: string|
| + TransactionId: string|
| + CreatedAt: DateTime|
+------------------+
| + Subscription   |
+------------------+
```

### Notification
```
+------------------+
| Notification     |
+------------------+
| + Id: Guid       |
| + UserId: Guid   |
| + Title: string  |
| + Message: string|
| + Type: NotificationType|
| + IsRead: bool   |
| + CreatedAt: DateTime|
| + RelatedEntityId: Guid?|
| + RelatedEntityType: string|
+------------------+
| + User           |
+------------------+
```

## Énumérations

### SubscriptionStatus
```
+------------------+
| SubscriptionStatus (enum) |
+------------------+
| Active           |
| Inactive         |
| PendingCancellation |
| Cancelled        |
| Expired          |
+------------------+
```

### PaymentStatus
```
+------------------+
| PaymentStatus (enum) |
+------------------+
| Pending          |
| Completed        |
| Failed           |
| Refunded         |
+------------------+
```

### NotificationType
```
+------------------+
| NotificationType (enum) |
+------------------+
| PaymentDue       |
| PaymentReceived  |
| SubscriptionExpiring |
| PriceChange      |
| SystemMessage    |
+------------------+
```

## Classes de Service

### AuthService
```
+------------------+
| AuthService      |
+------------------+
| + Register(RegisterDTO): Task<UserDTO>|
| + Login(LoginDTO): Task<AuthResultDTO>|
| + GenerateJwtToken(User): string|
| + RefreshToken(string): Task<AuthResultDTO>|
| + GetUserById(Guid): Task<UserDTO>|
| + UpdateUser(Guid, UpdateUserDTO): Task<UserDTO>|
| + ChangePassword(Guid, ChangePasswordDTO): Task<bool>|
+------------------+
```

### SubscriptionService
```
+------------------+
| SubscriptionService |
+------------------+
| + GetAllByUserId(Guid): Task<List<SubscriptionDTO>>|
| + GetById(Guid): Task<SubscriptionDTO>|
| + Create(CreateSubscriptionDTO): Task<SubscriptionDTO>|
| + Update(Guid, UpdateSubscriptionDTO): Task<SubscriptionDTO>|
| + Delete(Guid): Task<bool>|
| + CalculateNextBillingDate(DateTime, string): DateTime|
| + GetActiveSubscriptions(): Task<List<SubscriptionDTO>>|
| + GetExpiringSoon(int days): Task<List<SubscriptionDTO>>|
+------------------+
```

### CategoryService
```
+------------------+
| CategoryService  |
+------------------+
| + GetAll(): Task<List<CategoryDTO>>|
| + GetById(Guid): Task<CategoryDTO>|
| + Create(CreateCategoryDTO): Task<CategoryDTO>|
| + Update(Guid, UpdateCategoryDTO): Task<CategoryDTO>|
| + Delete(Guid): Task<bool>|
| + InitializeDefaultCategories(): Task<bool>|
+------------------+
```

### PaymentService
```
+------------------+
| PaymentService   |
+------------------+
| + GetPaymentsBySubscriptionId(Guid): Task<List<PaymentDTO>>|
| + GetPaymentById(Guid): Task<PaymentDTO>|
| + CreatePayment(CreatePaymentDTO): Task<PaymentDTO>|
| + UpdatePaymentStatus(Guid, PaymentStatus): Task<PaymentDTO>|
| + GeneratePaymentReport(DateTime, DateTime): Task<PaymentReportDTO>|
+------------------+
```

### NotificationService
```
+------------------+
| NotificationService |
+------------------+
| + GetNotificationsByUserId(Guid): Task<List<NotificationDTO>>|
| + GetUnreadNotifications(Guid): Task<List<NotificationDTO>>|
| + MarkAsRead(Guid): Task<bool>|
| + MarkAllAsRead(Guid): Task<bool>|
| + CreateNotification(CreateNotificationDTO): Task<NotificationDTO>|
| + DeleteNotification(Guid): Task<bool>|
+------------------+
```

### DashboardService
```
+------------------+
| DashboardService |
+------------------+
| + GetUserSummary(Guid): Task<UserSummaryDTO>|
| + GetMonthlyExpenses(Guid, int): Task<List<MonthlyExpenseDTO>>|
| + GetSubscriptionsByCategory(Guid): Task<List<CategorySummaryDTO>>|
| + GetUpcomingPayments(Guid, int): Task<List<UpcomingPaymentDTO>>|
+------------------+
```

## Relations entre classes

1. **User à Subscription**: Un utilisateur peut avoir plusieurs abonnements (Relation 1:N)
2. **Category à Subscription**: Une catégorie peut contenir plusieurs abonnements (Relation 1:N)
3. **Subscription à Payment**: Un abonnement peut avoir plusieurs paiements (Relation 1:N)
4. **User à Notification**: Un utilisateur peut avoir plusieurs notifications (Relation 1:N)

## Fonctionnalités à venir

1. **Gestion des catégories**:
   - Catégories prédéfinies (Streaming, Cloud, Logiciels, Sport)
   - Possibilité d'ajouter des catégories personnalisées

2. **Authentification sécurisée**:
   - JWT pour l'authentification
   - Rafraîchissement de tokens
   - Protection des routes via middleware d'authentification

3. **Gestion avancée des paiements**:
   - Suivi historique des paiements
   - Génération de rapports de paiement
   - Alertes pour paiements à venir

4. **Analyses et rapports**:
   - Dashboard avec dépenses mensuelles
   - Répartition des abonnements par catégorie
   - Prévisions de dépenses futures

5. **Notifications**:
   - Alertes pour paiements à venir
   - Notifications de changements de prix
   - Rappels d'expiration d'abonnements

## Architecture en Couches Oignon (Onion Architecture)

Cette section détaille l'organisation de la solution en utilisant l'architecture en couches oignon avec les couches Domain, Application et Infrastructure.

### Structure de la Solution

```
SubscriptionManager/
├── SubscriptionManager.Domain (Class Library)
├── SubscriptionManager.Application (Class Library)
├── SubscriptionManager.Infrastructure (Class Library)
├── SubscriptionManager.API (ASP.NET Core Web API)
└── SubscriptionManager.Tests (xUnit Test Project)
```

### Répartition des Éléments par Couche

#### 1. Couche Domain (Class Library)

Cette couche est le cœur de votre application, elle contient les entités du domaine, les énumérations et les interfaces de repository.

##### Dossiers et fichiers:
```
SubscriptionManager.Domain/
├── Entities/
│   ├── User.cs
│   ├── Subscription.cs
│   ├── Category.cs
│   ├── Payment.cs
│   └── Notification.cs
├── Enums/
│   ├── SubscriptionStatus.cs
│   ├── PaymentStatus.cs
│   └── NotificationType.cs
├── Common/
│   └── BaseEntity.cs
└── Repositories/
    ├── IUserRepository.cs
    ├── ISubscriptionRepository.cs
    ├── ICategoryRepository.cs
    ├── IPaymentRepository.cs
    └── INotificationRepository.cs
```

##### Éléments spécifiques:
- **Entités**: Toutes les entités principales (User, Subscription, Category, Payment, Notification)
- **Énumérations**: Toutes les énumérations (SubscriptionStatus, PaymentStatus, NotificationType)
- **Interfaces de repository**: Interfaces définissant les opérations de persistence

#### 2. Couche Application (Class Library)

Cette couche contient la logique métier et orchestre les opérations entre la couche présentation et le domaine.

##### Dossiers et fichiers:
```
SubscriptionManager.Application/
├── DTOs/
│   ├── Auth/
│   │   ├── LoginDTO.cs
│   │   ├── RegisterDTO.cs
│   │   ├── AuthResultDTO.cs
│   │   └── UserDTO.cs
│   ├── Subscriptions/
│   │   ├── SubscriptionDTO.cs
│   │   ├── CreateSubscriptionDTO.cs
│   │   └── UpdateSubscriptionDTO.cs
│   ├── Categories/
│   │   ├── CategoryDTO.cs
│   │   ├── CreateCategoryDTO.cs
│   │   └── UpdateCategoryDTO.cs
│   ├── Payments/
│   │   ├── PaymentDTO.cs
│   │   ├── CreatePaymentDTO.cs
│   │   └── PaymentReportDTO.cs
│   ├── Notifications/
│   │   ├── NotificationDTO.cs
│   │   └── CreateNotificationDTO.cs
│   └── Dashboard/
│       ├── UserSummaryDTO.cs
│       ├── MonthlyExpenseDTO.cs
│       ├── CategorySummaryDTO.cs
│       └── UpcomingPaymentDTO.cs
├── Interfaces/
│   ├── Services/
│   │   ├── IAuthService.cs
│   │   ├── ISubscriptionService.cs
│   │   ├── ICategoryService.cs
│   │   ├── IPaymentService.cs
│   │   ├── INotificationService.cs
│   │   └── IDashboardService.cs
│   └── Infrastructure/
│       ├── IUnitOfWork.cs
│       └── IEmailService.cs
├── Services/
│   ├── AuthService.cs
│   ├── SubscriptionService.cs
│   ├── CategoryService.cs
│   ├── PaymentService.cs
│   ├── NotificationService.cs
│   └── DashboardService.cs
└── Mappings/
    └── MappingProfile.cs
```

##### Éléments spécifiques:
- **DTOs**: Objets de transfert de données entre couches
- **Interfaces de service**: Interfaces définissant les contrats de service
- **Implémentations de service**: Classes implémentant la logique métier

#### 3. Couche Infrastructure (Class Library)

Cette couche gère l'accès aux données externes, l'implémentation des repositories et services externes.

##### Dossiers et fichiers:
```
SubscriptionManager.Infrastructure/
├── Data/
│   ├── AppDbContext.cs
│   ├── EntityConfigurations/
│   │   ├── UserConfiguration.cs
│   │   ├── SubscriptionConfiguration.cs
│   │   ├── CategoryConfiguration.cs
│   │   ├── PaymentConfiguration.cs
│   │   └── NotificationConfiguration.cs
│   └── Repositories/
│       ├── BaseRepository.cs
│       ├── UserRepository.cs
│       ├── SubscriptionRepository.cs
│       ├── CategoryRepository.cs
│       ├── PaymentRepository.cs
│       ├── NotificationRepository.cs
│       └── UnitOfWork.cs
├── Services/
│   ├── EmailService.cs
│   ├── TokenService.cs
│   └── DateTimeService.cs
├── Identity/
│   ├── IdentityService.cs
│   └── JwtGenerator.cs
└── DependencyInjection.cs
```

##### Éléments spécifiques:
- **DbContext et configurations**: Configuration de la persistance avec Entity Framework Core
- **Repositories**: Implémentations des repositories définis dans le Domain
- **Services externes**: Services d'infrastructure comme Email, JWT, etc.

#### 4. Couche API (ASP.NET Core Web API)

Cette couche expose l'API REST et gère les requêtes HTTP.

##### Dossiers et fichiers:
```
SubscriptionManager.API/
├── Controllers/
│   ├── AuthController.cs
│   ├── SubscriptionsController.cs
│   ├── CategoriesController.cs
│   ├── PaymentsController.cs
│   ├── NotificationsController.cs
│   └── DashboardController.cs
├── Middleware/
│   ├── ErrorHandlingMiddleware.cs
│   └── RequestLoggingMiddleware.cs
├── Program.cs
├── appsettings.json
└── appsettings.Development.json
```

### Types de Projets à Créer

1. **SubscriptionManager.Domain**: Projet de type "Class Library" (.NET Core)
   - Contient les entités, énumérations et interfaces de repository
   - Aucune dépendance externe sauf les packages .NET standard

2. **SubscriptionManager.Application**: Projet de type "Class Library" (.NET Core)
   - Dépend uniquement de la couche Domain
   - Packages recommandés: AutoMapper, FluentValidation, MediatR (pour CQRS)

3. **SubscriptionManager.Infrastructure**: Projet de type "Class Library" (.NET Core)
   - Dépend des couches Domain et Application
   - Packages recommandés: Entity Framework Core, Microsoft.Extensions.Configuration, MailKit

4. **SubscriptionManager.API**: Projet de type "ASP.NET Core Web API"
   - Dépend des couches Application et Infrastructure
   - Packages recommandés: Swashbuckle (Swagger), Microsoft.AspNetCore.Authentication.JwtBearer

5. **SubscriptionManager.Tests**: Projet de type "xUnit Test Project"
   - Contient les tests unitaires et d'intégration
   - Packages recommandés: xUnit, Moq, FluentAssertions

### Détails des Énumérations

Toutes les énumérations doivent être créées dans la couche **Domain**, dans le dossier `Enums`:

```csharp
// Domain/Enums/SubscriptionStatus.cs
namespace SubscriptionManager.Domain.Enums
{
    public enum SubscriptionStatus
    {
        Active,
        Inactive,
        PendingCancellation,
        Cancelled,
        Expired
    }
}

// Domain/Enums/PaymentStatus.cs
namespace SubscriptionManager.Domain.Enums
{
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }
}

// Domain/Enums/NotificationType.cs
namespace SubscriptionManager.Domain.Enums
{
    public enum NotificationType
    {
        PaymentDue,
        PaymentReceived,
        SubscriptionExpiring,
        PriceChange,
        SystemMessage
    }
}
```

### Interfaces Principales

#### Dans la couche Domain:

```csharp
// Domain/Repositories/ISubscriptionRepository.cs
namespace SubscriptionManager.Domain.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetAllByUserIdAsync(Guid userId);
        Task<Subscription> GetByIdAsync(Guid id);
        Task AddAsync(Subscription subscription);
        Task UpdateAsync(Subscription subscription);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Subscription>> GetActiveSubscriptionsAsync();
        Task<IEnumerable<Subscription>> GetExpiringSoonAsync(int days);
    }
}
```

#### Dans la couche Application:

```csharp
// Application/Interfaces/Services/ISubscriptionService.cs
namespace SubscriptionManager.Application.Interfaces.Services
{
    public interface ISubscriptionService
    {
        Task<List<SubscriptionDTO>> GetAllByUserIdAsync(Guid userId);
        Task<SubscriptionDTO> GetByIdAsync(Guid id);
        Task<SubscriptionDTO> CreateAsync(CreateSubscriptionDTO dto);
        Task<SubscriptionDTO> UpdateAsync(Guid id, UpdateSubscriptionDTO dto);
        Task<bool> DeleteAsync(Guid id);
        DateTime CalculateNextBillingDate(DateTime startDate, string billingCycle);
        Task<List<SubscriptionDTO>> GetActiveSubscriptionsAsync();
        Task<List<SubscriptionDTO>> GetExpiringSoonAsync(int days);
    }
}
