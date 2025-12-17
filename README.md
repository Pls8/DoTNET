![image alt](https://github.com/Pls8/DoTNET/blob/master/repoCoverDOtNet_00000.jpg?raw=true)

## ASP.NET MVC Application

### **Features**

* **ASP.NET Core MVC** – Implements the Model-View-Controller architecture for clean separation of concerns.
* **Entity Framework Core** – Supports both Database-First and Code-First approaches for data access.
* **SQL Server** – Provides robust and scalable relational data storage.
* **Dependency Injection** – Uses the built-in IoC container for cleaner, testable architecture.
* **Razor Pages / Views** – Enables dynamic server-side rendering using Razor syntax.
* **Authentication & Authorization** – Integrated Identity framework for secure login and role management.
* **RESTful API Support** – Exposes API endpoints for client or third-party applications.
* **Responsive Design** – Built with Bootstrap or a custom frontend for mobile-friendly UI.

---


###  Technology Stack

- **Backend**: ASP.NET Core MVC 8.0
- **Database**: SQL Server with Entity Framework Core
- **Frontend**: Razor Views, Bootstrap 5
- **Architecture**: Repository Pattern with Dependency Injection
- **Tools**: Visual Studio 2022, SQL Server Management Studio


---
![image alt](https://github.com/Pls8/DoTNET/blob/master/Model-isValid_00000.jpg?raw=true)
## <p align="right">LOG 2025-12-10</p>


- Fixed issue with EF ModelState validation (nullable navigation property).
- Added explanation for ViewBag, ViewData, and ViewModel.
- Updated lazy/eager loading examples.
- About Identity Framework authentication setup.


### 1. Saving to the database (Entity Framework) — issue with `ModelState.IsValid`

I previously had an issue where the form did not save to the database and instead redirected back to the same *Create* view.
At first I thought the problem was caused by the `if (ModelState.IsValid)` check in the controller.

The real reason was that my navigation property was **not nullable**, while the foreign key **was required**.
Example:

```csharp
[ForeignKey(nameof(TaskCategory))]
public int TaskCategoryId { get; set; }

public TaskCategoryClass? TaskCategory { get; set; }
```

By adding the **question mark (`?`)** to the navigation property, Entity Framework understood the relationship correctly and the model passed validation. That fixed the issue.

*(Explanation: A non-nullable navigation property implies a required relationship. If EF cannot populate it during model binding, ModelState becomes invalid.)*



### 2. ViewBag, ViewData, ViewModel

In ASP.NET MVC you have:

1. **ViewBag** – dynamic type, easy to use
2. **ViewData** – dictionary (`ViewData["Key"]`)
3. **ViewModel** – strongly typed class passed to the view



### 3. Lazy Loading vs. Eager Loading (Entity Framework)

**Lazy loading:**

* Navigation properties are loaded **only when accessed**.
* Requires virtual properties and proxies (or separate configuration).
* Can cause many SQL queries.

**Eager loading:**

* Data is loaded **up front**, typically using `.Include()`.
  Example:

```csharp
var tasks = _context.Tasks.Include(t => t.TaskCategory).ToList();
```




### 4. ASP.NET Identity Framework

Topics you may include in your README:

* How Identity handles **authentication** (login, registration)
* How it stores users, roles, and claims in the database
* How to use `UserManager`, `SignInManager`, and `RoleManager`
* How to protect actions using `[Authorize]`
* Customizing the Identity user model

Example:

```csharp
[Authorize(Roles = "Admin")]
public IActionResult AdminPanel()
{
    return View();
}
```

---

# ViewBag, ViewData, ViewModel — What They Are, When to Use, and Why

## **1. ViewBag**

### **What it is**

* A *dynamic* object used to pass small amounts of data from the controller to the view.
* Uses dynamic properties: `ViewBag.Title`, `ViewBag.Categories`, etc.

### **When to use it**

* When passing **simple, temporary**, non-critical data to the view.
* Good for quick values like page titles, success messages, or dropdown lists (although ViewModel is still better).

### **Why (pros & cons)**

**Pros:**

* Very easy and fast to use.
* Does not require creating classes.

**Cons:**

* No compile-time checking → errors show only at runtime.
* Easy to misspell property names.
* Hard to maintain in large projects.


## **2. ViewData**

### **What it is**

* A dictionary (`Dictionary<string, object?>`) for passing data from controller → view.
* Accessed by key: `ViewData["Message"]`.

### **When to use it**

* Rarely; mostly when working with code that predates ViewBag.
* When binding data dynamically but your team prefers dictionary-based access.

### **Why (pros & cons)**

**Pros:**

* Works like ViewBag but dictionary-based.
* Useful for localization dictionaries or dynamic looping.

**Cons:**

* Requires casting (`var msg = (string)ViewData["Message"];`)
* Very easy to break if keys are mis-typed.
* Like ViewBag, no strong typing → not good for big projects.


## **3. ViewModel**

### **What it is**

* A **strongly-typed class** specifically created to send structured data to a view.
  Example:

```csharp
public class TaskCreateViewModel
{
    public Task Task { get; set; }
    public List<TaskCategory> Categories { get; set; }
}
```

### **When to use it**

* **Always** when your view needs more than one type of data.
* Best choice for:

  * Forms (create/edit pages)
  * Pages with multiple lists
  * Any structured data
  * Any important page in the application

### **Why (pros & cons)**

**Pros:**

* Strong typing → compile-time error checking.
* Clean, readable, maintainable.
* Perfect for validation with data annotations.
* Reduces errors dramatically.

**Cons:**

* Requires creating a class (some extra work).
* Not necessary for very simple pages.


#  Lazy Loading vs. Eager Loading (Entity Framework)

## **Lazy Loading**

### **What it is**

* Related entities are loaded **only when accessed**.
* EF creates a proxy object that fetches the data when needed.

### **Example**

```csharp
var task = _context.Tasks.First();
var category = task.TaskCategory; // SQL query happens here (lazy)
```

### **When to use it**

* When accessing related data rarely or conditionally.
* When performance is not critical.
* When you want minimal initial queries.

### **Why (pros & cons)**

**Pros:**

* Saves SQL queries if related data is never accessed.
* Keeps initial load fast.

**Cons:**

* Can cause the “N+1 query problem” (very slow).
* Harder to optimize.
* Requires virtual properties.


## **Eager Loading**

### **What it is**

* Loads related data **immediately**, using `.Include()`.

### **Example**

```csharp
var tasks = _context.Tasks
    .Include(t => t.TaskCategory)
    .ToList();
```

### **When to use it**

* When you know the related data *will* be needed (lists, views).
* When you want to reduce database round-trips.
* When building APIs that return full objects.

### **Why (pros & cons)**

**Pros:**

* Only 1 SQL query → much faster in many scenarios.
* More predictable performance.
* Best choice for complex views.

**Cons:**

* Loads extra data even if you don’t use it.
* Large `.Include()` chains can get heavy.


## Summary Table

| Feature           | What it is                            | When to Use                   | Why                                           |
| ----------------- | ------------------------------------- | ----------------------------- | --------------------------------------------- |
| **ViewBag**       | Dynamic object                        | Simple temporary data         | Easy, quick but not safe                      |
| **ViewData**      | Dictionary                            | Legacy/simple dynamic data    | Works like ViewBag but dictionary-based       |
| **ViewModel**     | Strong-typed class                    | All real pages & forms        | Safe, clean, best practice                    |
| **Lazy Loading**  | Loads related data only when accessed | Rare/conditional related data | Fewer queries at start, but can be slow later |
| **Eager Loading** | Loads related data immediately        | Lists, APIs, dashboards       | Faster overall, predictable performance       |

---


![image alt](https://github.com/Pls8/DoTNET/blob/master/auth_00000.jpg?raw=true)
## <p align="right">LOG 2025-12-14</p>
# Task Creation Fix - UserId Required Error

## Problem
When creating a task while logged in, the form would reload without creating the task. The console showed: **"The UserId field is required."**
## Root Cause
The `TaskClass` model had a `[Required]` attribute on the `UserId` property, but this field wasn't included in the form submission. ModelState validation failed before the controller could set the UserId.
## Solution Applied
### 1. Made UserId Nullable
```csharp
// Before (causing validation error):
public string UserId { get; set; }

// After (fix):
public string? UserId { get; set; }
```
### 2. Set UserId Before Validation
In `TaskController.Create()` action:
```csharp
[HttpPost]
public async Task<IActionResult> Create(TaskClass task)
{
    // Get current user FIRST
    var user = await _userManager.GetUserAsync(User);
    if (user == null) return RedirectToAction("Login", "Account");
    
    // Set UserId BEFORE ModelState validation
    task.UserId = user.Id;
    
    // Now check ModelState
    if (ModelState.IsValid)
    {
        // ... save task
    }
}
```
## Why This Works
1. **Model binding happens first** - Form data binds to `TaskClass`
2. **Validation runs immediately** - `[Required]` attributes are checked
3. **UserId was required but empty** in form data → validation failed
4. **Solution**: Set UserId programmatically before validation runs
## Key Points
- UserId should be set by the system, not by user input
- Use nullable reference types (`string?`) for optional relationships


<br>
# ASP.NET Core Identity with Entity Framework Core - Simple Guide

## What is ASP.NET Core Identity?
A **built-in authentication system** for ASP.NET Core apps that handles:
- User registration & login
- Password management
- Role-based authorization
- Email confirmation
- Two-factor authentication

## Key Components

### 1. **IdentityUser** (Base User Class)
```csharp
public class AppUser : IdentityUser
{
    // Add custom properties here
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
```

### 2. **IdentityDbContext** (Database Context)
```csharp
public class AppDbContext : IdentityDbContext<AppUser>
{
    // Your DbSets go here
    public DbSet<TaskClass> Tasks { get; set; }
}
```
- **Important**: Must inherit from `IdentityDbContext<AppUser>`
- Automatically creates tables: `AspNetUsers`, `AspNetRoles`, etc.

### 3. **Program.cs Setup**
```csharp
builder.Services.AddIdentity<AppUser, IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();
```

### 4. **Middleware Order (CRITICAL!)**
```csharp
app.UseAuthentication();   // MUST come before Authorization
app.UseAuthorization();    // MUST come before MapControllers
```

## Key Services (Dependency Injection)

### **UserManager<TUser>**
- Manages user operations: Create, Update, Delete, Find users
```csharp
var userManager = new UserManager<AppUser>();
var user = await userManager.GetUserAsync(User);
```

### **SignInManager<TUser>**
- Handles sign-in/sign-out operations
```csharp
await signInManager.SignInAsync(user, isPersistent: false);
await signInManager.SignOutAsync();
```

## Important Database Tables

| Table Name | Purpose |
|------------|---------|
| `AspNetUsers` | User accounts |
| `AspNetRoles` | Roles (Admin, User, etc.) |
| `AspNetUserRoles` | Links users to roles |
| `AspNetUserClaims` | Custom user claims |
| `AspNetUserLogins` | External logins (Google, Facebook) |

## Quick Steps to Add Identity

### 1. **Create AppUser Model**
```csharp
public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
```

### 2. **Update DbContext**
```csharp
public class AppDbContext : IdentityDbContext<AppUser>
{
    // Your existing DbSet properties
}
```

### 3. **NO NEED TO Add Migration OR Update-database !**

### 4. **Configure Program.cs**
```csharp
// Add Identity service
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// Add authentication middleware
app.UseAuthentication();
app.UseAuthorization();
```

### 5. **Access Current User in Controllers**
```csharp
public class TaskController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    
    public TaskController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        // Use user.Id for filtering data
    }
}
```

## Common Gotchas

1. **Middleware Order Wrong**
   - X `UseAuthorization()` before `UseAuthentication()`
   - ~ `UseAuthentication()` → `UseAuthorization()` → `MapControllers()`

2. **Not Inheriting IdentityDbContext**
   - X `public class AppDbContext : DbContext`
   - ~ `public class AppDbContext : IdentityDbContext<AppUser>`

3. **Missing [Authorize] Attribute**
   ```csharp
   [Authorize]  // Add this to protect controller
   public class TaskController : Controller
   ```

4. **UserId Not Set Before Validation**
   - Set `UserId = user.Id` BEFORE `ModelState.IsValid` check

## Simple Login/Logout Example

### Login Action
```csharp
public async Task<IActionResult> Login(string email, string password)
{
    var user = await _userManager.FindByEmailAsync(email);
    await _signInManager.PasswordSignInAsync(user, password, false, false);
    return RedirectToAction("Index");
}
```

### Access Current User
```csharp
// In controller
var user = await _userManager.GetUserAsync(User);

// In view
@if (User.Identity.IsAuthenticated)
{
    <p>Welcome @User.Identity.Name!</p>
}
```

## Benefits of Using Identity
-  Built-in security (password hashing, token generation)
-  Ready-to-use UI (scaffold with `dotnet new` templates)
-  Extensible (add custom properties to AppUser)
-  Role-based authorization out of the box
-  Email confirmation, 2FA, password reset

## In Short
1. **Identity = Authentication + Authorization**
2. **AppUser** = Your custom user class
3. **IdentityDbContext** = Database context with Identity tables
4. **UserManager/SignInManager** = Services to manage users
5. **[Authorize]** = Protect your controllers/actions
6. **Guid** = uuid

---

![image alt](https://github.com/Pls8/DoTNET/blob/master/api_00000.jpg?raw=true)
## <p align="right">LOG 2025-12-17</p>

### **1. CRUD Operations in Controllers**
- **GET** - Read data (retrieve from database)
- **POST** - Create data (add new to database)
- **PUT** - Update data (replace entire object)
- **DELETE** - Remove data (delete from database)

### **2. Async Programming**
- **Async/Await** pattern for better performance
- **Task<T>** = A "promise" to return data later
- **Why use async?** Server can handle other requests while waiting for database
- **Example:** `.ToListAsync()` instead of `.ToList()`

### **3. DTOs (Data Transfer Objects)**
- **What are DTOs?** Simple classes that carry only necessary data
- **Why use DTOs?** 
  - Send only what frontend needs
  - Hide sensitive database fields
  - Faster API responses (less data)
- **Example:** 
  ```csharp
  public class TaskDTO
  {
      public int Id { get; set; }
      public string Name { get; set; }
      public DateTime DeadLine { get; set; }
  }
  ```

### **4. BaseController Pattern**
- **What is it?** Parent class with common functionality
- **Benefits:**
  - Don't repeat code (DRY principle)
  - Consistent API responses
  - Easy to update all controllers
- **Contains:** Common database context, helper methods, validation

### **5. PATCH vs PUT**
- **PUT** = Replace entire object (send all fields)
- **PATCH** = Update only specific fields (send only what changes)
- **PATCH is better for:**
  - Mobile apps (save bandwidth)
  - Quick updates (like marking task complete)
  - When you only need to change 1-2 fields

## ** Key Code Examples:**

### **Async GET with DTO:**
```csharp
public async Task<ActionResult<List<TaskDTO>>> GetTasks()
{
    var tasks = await _db.Tasks.ToListAsync();
    var taskDTOs = tasks.Select(t => new TaskDTO 
    { 
        Id = t.Id, 
        Name = t.Name 
    }).ToList();
    return Ok(taskDTOs);
}
```

### **Simple PATCH:**
```csharp
[HttpPatch("{id}/complete")]
public async Task<IActionResult> MarkComplete(int id)
{
    var task = await _db.Tasks.FindAsync(id);
    task.IsCompleted = true;  // Update only this field
    await _db.SaveChangesAsync();
    return Ok("Task completed");
}
```

### **BaseController:**
```csharp
public abstract class BaseController : ControllerBase
{
    protected readonly AppDbContext _db;
    
    protected BaseController(AppDbContext db)
    {
        _db = db;
    }
}
```

## ** Simple Rules to Remember:**

1. **For database calls** → Use `Async` methods
2. **For API responses** → Use `DTOs` to send only needed data
3. **For common code** → Put in `BaseController`
4. **For partial updates** → Use `PATCH` instead of `PUT`
5. **Always validate** → Check if data exists before updating/deleting

## ** Quick Comparison:**

| Operation | HTTP Method | Use When... |
|-----------|-------------|-------------|
| **Create** | POST | Adding new item |
| **Read** | GET | Getting data |
| **Update All** | PUT | Editing form (all fields) |
| **Update Part** | PATCH | Quick changes (1-2 fields) |
| **Delete** | DELETE | Removing item |

## ** Best Practices Learned:**

1. **Keep controllers thin** - Business logic in services
2. **Use async for I/O operations** - Database, files, API calls
3. **Protect sensitive data** - DTOs hide database details
4. **Consistent responses** - Same format for all APIs
5. **Proper HTTP methods** - GET for read, POST for create, etc.

---
