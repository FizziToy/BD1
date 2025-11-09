# BD
## 1. Загальна інформація
**Мета проєкту:** створити інформаційну систему для обліку об’єктів нерухомості, орендарів, договорів оренди, рахунків, платежів і технічного обслуговування.

**Технології:**
- **СУБД:** Microsoft SQL Server  
- **ORM:** Entity Framework Core (Database First)  
- **Архітектура:** ASP.NET Core MVC + Repository + Unit of Work  
- **Мова:** C# (.NET 8)

---

## 2. Структура бази даних
- **Кількість сутностей:** 20+  
- **Основні таблиці:**  
  `Property`, `Building`, `Unit`, `Tenant`, `Lease`, `Invoice`, `Payment`,  
  `ServiceRequest`, `WorkOrder`, `Meter`, `MeterReading`, `Owner`,  
  `Ownership`, `Vendor`, `User`, `Role`, `Employee`, `AuditLog`

**Зв’язки між сутностями:**
- `Property` 1–M `Building`
- `Building` 1–M `Unit`
- `Unit` 1–M `Lease`
- `Lease` M–1 `Tenant`
- `Lease` 1–M `Invoice`
- `Invoice` 1–M `Payment`
- `Unit` 1–M `ServiceRequest`
- `WorkOrder` M–1 `ServiceRequest`
- `Unit` M–M `Owner` через `Ownership`

**Ключі:**
- Первинні — `Id` (наприклад, `TenantId`, `LeaseId`, `PropertyId`)
- Зовнішні ключі — у всіх зв’язаних таблицях

---

## 3. Вимоги до даних
✅ Виконано:
- Понад **15 сутностей**
- **Soft delete:** `Tenant`, `Lease`, `Unit`, `Vendor`, `WorkOrder`
- **Аудит змін:** поля `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy`
- **Журнал змін:** таблиця `AuditLog`

---

## 4. Реалізація у SQL Server

### 🔹 Тригери
- `trg_Lease_Audit`  
- `trg_Lease_ValidateDates`  
- `trg_SetAudit_Tenant`  
- `trg_SetAudit_Unit`  
- `trg_SetAudit_Property`  
- `trg_Unit_SoftDelete_Lease`  
- `trg_Unit_UpdateAudit`  
- `trg_Payment_UpdateInvoice`

### 🔹 Збережені процедури (Stored Procedures)
- `sp_AddTenant`  
- `sp_GetActiveTenants`  
- `sp_DeleteTenantSoft`  
- `sp_GetActiveLeases`  
- (та інші для операцій з орендою, рахунками і платежами)

### 🔹 Представлення (Views)
- `vw_ActiveLeases`  
- `vwCurrentLeases`  
- `vwActiveUnits`  
- `vwOutstandingInvoices`  
- `vw_DeletedEntities`  
- `vwOpenServiceRequests`

### 🔹 Індекси
- **Унікальні:** `UQ_MR`, `UQ__Meter__...`, `UQ_Ownership`  
- **Фільтровані:** `IX_Lease_Unit_Tenant (IsDeleted=0)`  
- **Нефільтровані:** `IX_Tenant_Phone`, `IX_Invoice_Lease_DueDate`, `IX_Payment_PaidDate`

---

## 5. Робота з БД через C#
**Патерни:**  
- `Repository` — для кожної сутності (наприклад, `TenantRepository`, `LeaseRepository`)  
- `UnitOfWork` — для керування транзакціями та доступом до репозиторіїв  

**Приклад використання:**
```csharp
var tenants = uow.Tenants.GetActiveTenants();
var leases = uow.Leases.GetActiveLeases();
