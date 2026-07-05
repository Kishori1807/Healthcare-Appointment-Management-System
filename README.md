# Healthcare Appointment Management System (HAMS)

A production-quality microservices architecture built with **ASP.NET Core (.NET 10)**, **Entity Framework Core**, **SQLite**, and **YARP Reverse Proxy**, following **Clean Architecture** and **SOLID** principles.

## 📋 Architecture Overview

```
┌─────────────────────────────────────────────────────────────────┐
│                     API Gateway (YARP)                          │
│                      Port: 5000                                  │
│                                                                 │
│  /api/v1/patients/*  → PatientService                           │
│  /api/v1/doctors/*   → DoctorService                            │
│  /api/v1/appointments/* → AppointmentService                    │
└─────────────────────────────────────────────────────────────────┘
		 ↓                    ↓                      ↓
┌─────────────────┐  ┌──────────────────┐  ┌──────────────────────┐
│ Patient Service │  │  Doctor Service  │  │ Appointment Service  │
│   Port: 5001    │  │   Port: 5002     │  │     Port: 5003       │
│                 │  │                  │  │                      │
│ ✓ SQLite DB     │  │ ✓ SQLite DB      │  │ ✓ SQLite DB          │
│ ✓ Layered Arch  │  │ ✓ Layered Arch   │  │ ✓ Layered Arch       │
│ ✓ Swagger API   │  │ ✓ Swagger API    │  │ ✓ REST API           │
│ ✓ Health Check  │  │ ✓ Health Check   │  │ ✓ HttpClient Calls   │
└─────────────────┘  └──────────────────┘  └──────────────────────┘
```

## 🏗️ Microservices

### 1. Patient Service (5001)
**Responsibilities:**
- Register, update, delete, and view patients
- List all patients with metadata (age, gender, contact)

**Database:** `PatientDB.db`

**Key Endpoints:**
- `GET /api/v1/patients` - Get all patients
- `GET /api/v1/patients/{id}` - Get patient by ID
- `POST /api/v1/patients` - Create patient
- `PUT /api/v1/patients/{id}` - Update patient
- `DELETE /api/v1/patients/{id}` - Delete patient
- `GET /health` - Service health

---

### 2. Doctor Service (5002)
**Responsibilities:**
- Add, update, delete, and search doctors
- List doctors by specialization and availability

**Database:** `DoctorDB.db`

**Key Endpoints:**
- `GET /api/v1/doctors` - Get all doctors
- `GET /api/v1/doctors/{id}` - Get doctor by ID
- `POST /api/v1/doctors` - Create doctor
- `PUT /api/v1/doctors/{id}` - Update doctor
- `DELETE /api/v1/doctors/{id}` - Delete doctor
- `GET /health` - Service health

---

### 3. Appointment Service (5003)
**Responsibilities:**
- Book appointments (validates patient and doctor existence)
- Cancel appointments
- Retrieve appointment history by patient
- Cross-service communication via HttpClient

**Database:** `AppointmentDB.db`

**Key Endpoints:**
- `GET /api/v1/appointments` - Get all appointments
- `GET /api/v1/appointments/{id}` - Get appointment by ID
- `GET /api/v1/appointments/history/{patientId}` - Get patient history
- `POST /api/v1/appointments` - Book appointment (validates patient + doctor)
- `DELETE /api/v1/appointments/{id}` - Cancel appointment
- `GET /health` - Service health

---

### 4. API Gateway (5000)
**Responsibilities:**
- Route external requests to internal services
- Handle cross-cutting concerns
- Health aggregation

**Technology:** YARP Reverse Proxy

---

## 🛠️ Tech Stack

| Component | Version |
|-----------|---------|
| .NET SDK | 10.0 |
| ASP.NET Core | 10.0 |
| Entity Framework Core | 10.0 |
| SQLite | Latest |
| YARP (Reverse Proxy) | 2.3.0 |
| AutoMapper | 12.0.1 |
| API Versioning | 10.0.0 |
| Swagger/Swashbuckle | 10.2.3 |
| Docker | Latest |

---

## 📁 Project Structure

```
HAMS/
├── HAMS.sln                      # Solution file
├── docker-compose.yml            # Docker Compose orchestration
├── README.md                      # This file
│
├── ApiGateway/
│   ├── Program.cs                # YARP configuration + DI
│   ├── appsettings.json          # Gateway routes & clusters
│   ├── Dockerfile                # Multi-stage Docker build
│   └── ApiGateway.csproj
│
├── PatientService/
│   ├── Program.cs                # Bootstrap + middleware
│   ├── appsettings.json          # DB connection string
│   ├── Dockerfile
│   ├── Models/
│   │   └── Patient.cs
│   ├── Data/
│   │   ├── PatientDbContext.cs
│   │   └── PatientDbSeeder.cs
│   ├── DTOs/
│   │   ├── CreatePatientRequestDto.cs
│   │   ├── UpdatePatientRequestDto.cs
│   │   └── PatientResponseDto.cs
│   ├── Repositories/
│   │   ├── IPatientRepository.cs
│   │   └── PatientRepository.cs
│   ├── Services/
│   │   ├── IPatientService.cs
│   │   └── PatientService.cs
│   ├── Controllers/
│   │   └── V1/
│   │       └── PatientsController.cs
│   ├── Mappings/
│   │   └── PatientProfile.cs
│   └── Middleware/
│       ├── RequestLoggingMiddleware.cs
│       └── GlobalExceptionMiddleware.cs
│
├── DoctorService/
│   ├── Program.cs
│   ├── appsettings.json
│   ├── Dockerfile
│   ├── Models/
│   │   └── Doctor.cs
│   ├── Data/
│   │   ├── DoctorDbContext.cs
│   │   └── DoctorDbSeeder.cs
│   ├── DTOs/
│   │   ├── CreateDoctorRequestDto.cs
│   │   ├── UpdateDoctorRequestDto.cs
│   │   └── DoctorResponseDto.cs
│   ├── Repositories/
│   │   ├── IDoctorRepository.cs
│   │   └── DoctorRepository.cs
│   ├── Services/
│   │   ├── IDoctorService.cs
│   │   └── DoctorService.cs
│   ├── Controllers/
│   │   └── V1/
│   │       └── DoctorsController.cs
│   ├── Mappings/
│   │   └── DoctorProfile.cs
│   └── Middleware/
│       ├── RequestLoggingMiddleware.cs
│       └── GlobalExceptionMiddleware.cs
│
├── AppointmentService/
│   ├── Program.cs
│   ├── appsettings.json          # ExternalServices config
│   ├── Dockerfile
│   ├── Models/
│   │   └── Appointment.cs
│   ├── Data/
│   │   ├── AppointmentDbContext.cs
│   │   └── AppointmentDbSeeder.cs
│   ├── DTOs/
│   │   ├── CreateAppointmentRequestDto.cs
│   │   └── AppointmentResponseDto.cs
│   ├── Clients/
│   │   ├── IExternalValidationClient.cs  # HttpClient interface
│   │   └── ExternalValidationClient.cs   # Cross-service calls
│   ├── Repositories/
│   │   ├── IAppointmentRepository.cs
│   │   └── AppointmentRepository.cs
│   ├── Services/
│   │   ├── IAppointmentService.cs
│   │   └── AppointmentService.cs
│   ├── Controllers/
│   │   └── V1/
│   │       └── AppointmentsController.cs
│   ├── Mappings/
│   │   └── AppointmentProfile.cs
│   ├── Middleware/
│   │   ├── RequestLoggingMiddleware.cs
│   │   └── GlobalExceptionMiddleware.cs
│   └── Common/
│       └── ResourceNotFoundException.cs
│
└── docs/
	└── HAMS.postman_collection.json  # Postman requests
```

---

## 🚀 Running the Application

### Prerequisites
- .NET 10 SDK installed
- Docker Desktop (for container setup) - **Optional**
- Visual Studio 2022+ / Visual Studio Code

### Option 1: Run Individual Services (Development)

#### Terminal 1: Patient Service
```powershell
dotnet run --project .\PatientService\PatientService.csproj
```
Access: `http://localhost:5001/swagger`

#### Terminal 2: Doctor Service
```powershell
dotnet run --project .\DoctorService\DoctorService.csproj
```
Access: `http://localhost:5002/swagger`

#### Terminal 3: Appointment Service
```powershell
dotnet run --project .\AppointmentService\AppointmentService.csproj
```
Access: `http://localhost:5003/swagger`

#### Terminal 4: API Gateway
```powershell
dotnet run --project .\ApiGateway\ApiGateway.csproj
```
Access: `http://localhost:5000/health`

---

### Option 2: Run via Visual Studio (Multi-Startup)

1. Open `HAMS.sln` in Visual Studio
2. Right-click solution → **Set Startup Projects**
3. Select **Multiple startup projects**
4. Set action to **Start** for:
   - `PatientService`
   - `DoctorService`
   - `AppointmentService`
   - `ApiGateway`
5. Press **F5** or **Debug → Start Debugging**

---

### Option 3: Docker Compose (Production-like)

```powershell
# Build and start all containers
docker compose up --build

# Detached mode (run in background)
docker compose up -d --build

# Stop containers
docker compose down

# View logs
docker compose logs -f
```

**Service URLs (via Docker Compose):**
- Patient Service: `http://localhost:5001`
- Doctor Service: `http://localhost:5002`
- Appointment Service: `http://localhost:5003`
- API Gateway: `http://localhost:5000`

---

## 🧪 Testing the APIs

### Using Swagger UI

1. **Patient Service:** http://localhost:5001/swagger
2. **Doctor Service:** http://localhost:5002/swagger
3. **Appointment Service:** http://localhost:5003/swagger

### Using Postman

Import the collection: `docs/HAMS.postman_collection.json`

### Using PowerShell / cURL

#### Create a Patient
```powershell
$body = @{
	fullName = "John Doe"
	age = 35
	gender = "Male"
	phone = "+1-555-0100"
	email = "john.doe@example.com"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5001/api/v1/patients" `
  -Method Post `
  -ContentType "application/json" `
  -Body $body
```

#### Create a Doctor
```powershell
$body = @{
	fullName = "Dr. Smith"
	specialization = "Cardiology"
	experience = 15
	consultationFee = 1500
	available = $true
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5002/api/v1/doctors" `
  -Method Post `
  -ContentType "application/json" `
  -Body $body
```

#### Book an Appointment
```powershell
$body = @{
	patientId = 1
	doctorId = 1
	appointmentDate = "2025-12-25T10:00:00Z"
	notes = "Routine checkup"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5003/api/v1/appointments" `
  -Method Post `
  -ContentType "application/json" `
  -Body $body
```

---

## 🏗️ Architecture Patterns

### 1. **Layered Architecture**
Each service follows:
```
Controllers → Services → Repositories → Data (DbContext)
```

### 2. **Dependency Injection**
- Configured in `Program.cs`
- Repository and Service interfaces injected into controllers
- HttpClient typed clients for cross-service calls

### 3. **DTO Pattern**
- Request DTOs for input validation
- Response DTOs for API contracts
- Entity models never exposed directly

### 4. **Repository Pattern**
- `IPatientRepository`, `IDoctorRepository`, `IAppointmentRepository`
- Abstraction for data access
- Async/await throughout

### 5. **Middleware**
- `RequestLoggingMiddleware`: Logs all incoming/outgoing requests
- `GlobalExceptionMiddleware`: Centralized error handling with consistent JSON responses

### 6. **AutoMapper**
- Profiles: `PatientProfile`, `DoctorProfile`, `AppointmentProfile`
- Automatic entity ↔ DTO mapping

### 7. **API Versioning**
- URL-based versioning: `/api/v1/...`
- Configured for future `/api/v2/...` support

### 8. **Health Checks**
- Endpoint: `/health` on each service
- Used by docker-compose for dependency resolution

---

## 🔄 Inter-Service Communication

**AppointmentService → Patient/Doctor Services**

When booking an appointment, AppointmentService:
1. Receives `CreateAppointmentRequestDto` with patientId and doctorId
2. Calls `IExternalValidationClient.PatientExistsAsync(patientId)`
3. Calls `IExternalValidationClient.DoctorExistsAsync(doctorId)`
4. If either returns false, throws `ResourceNotFoundException` (404)
5. Otherwise, creates appointment with status "Booked"

**Configuration** (appsettings.json):
```json
"ExternalServices": {
  "PatientServiceBaseUrl": "http://localhost:5001",
  "DoctorServiceBaseUrl": "http://localhost:5002"
}
```

In Docker Compose:
```json
"ExternalServices": {
  "PatientServiceBaseUrl": "http://patient-service:5001",
  "DoctorServiceBaseUrl": "http://doctor-service:5002"
}
```

---

## ✅ SOLID Principles Compliance

| Principle | Implementation |
|-----------|-----------------|
| **S**ingle Responsibility | Each service has one business capability; each class has one reason to change |
| **O**pen/Closed | Repository/Service interfaces allow extension without modification |
| **L**iskov Substitution | Repositories and services are interchangeable via interfaces |
| **I**nterface Segregation | Focused interfaces (IPatientRepository, IDoctorService) |
| **D**ependency Inversion | Depend on abstractions (interfaces), not concretions; DI container manages resolution |

---

## 📊 Database Schema

### Patient Table
| Column | Type | Constraints |
|--------|------|-------------|
| Id | int | Primary Key, Auto-increment |
| FullName | string(120) | Not Null |
| Age | int | 0-150 |
| Gender | string(20) | Not Null |
| Phone | string(20) | Not Null |
| Email | string(150) | Unique, Not Null |
| CreatedDate | DateTime | Not Null, Default: CURRENT_TIMESTAMP |

### Doctor Table
| Column | Type | Constraints |
|--------|------|-------------|
| Id | int | Primary Key, Auto-increment |
| FullName | string(120) | Not Null |
| Specialization | string(80) | Not Null |
| Experience | int | 0-70 |
| ConsultationFee | decimal | 0-50000 |
| Available | bool | Default: true |

### Appointment Table
| Column | Type | Constraints |
|--------|------|-------------|
| Id | int | Primary Key, Auto-increment |
| PatientId | int | Not Null, Foreign Key reference |
| DoctorId | int | Not Null, Foreign Key reference |
| AppointmentDate | DateTime | Not Null |
| Status | string(40) | Not Null, Default: "Booked" |
| Notes | string(300) | Nullable |
| CreatedDate | DateTime | Not Null, Default: CURRENT_TIMESTAMP |

---

## 📝 Sample Data (Auto-Seeded)

**Patients:**
- Aarav Sharma (32, Male, +1-555-1001)
- Priya Nair (28, Female, +1-555-1002)

**Doctors:**
- Dr. Meera Kapoor (Cardiology, 10 years, ₹1200/session)
- Dr. Rahul Menon (Dermatology, 8 years, ₹900/session)

**Appointments:**
- Aarav → Dr. Meera (Initial consultation)
- Priya → Dr. Rahul (Follow-up)

---

## 🛣️ API Gateway Routes

| External Route | Internal Service | Port |
|---|---|---|
| `/api/v1/patients/*` | PatientService | 5001 |
| `/api/v1/doctors/*` | DoctorService | 5002 |
| `/api/v1/appointments/*` | AppointmentService | 5003 |

---

## 🐛 Logging

All services log to console output by default. Log levels:
- `Information`: Request/response flow
- `Warning`: Validation failures, potential issues
- `Error`: Exceptions, unhandled errors

Enable more verbose logging by modifying `appsettings.json`:
```json
"Logging": {
  "LogLevel": {
	"Default": "Debug",
	"Microsoft.EntityFrameworkCore": "Information"
  }
}
```

---

## 🔐 Error Handling

**Consistent error response format:**
```json
{
  "success": false,
  "statusCode": 404,
  "message": "Patient not found."
}
```

**HTTP Status Codes:**
- `200 OK` - Success
- `201 Created` - Resource created
- `204 No Content` - Deletion successful
- `400 Bad Request` - Validation error
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server error

---

## ✨ Key Features

✅ **Microservices Architecture**  
✅ **Database per Service**  
✅ **Independent Deployment**  
✅ **API Versioning (v1 ready for v2)**  
✅ **REST APIs with Swagger documentation**  
✅ **Layered Architecture (Controller → Service → Repository → Data)**  
✅ **Async/Await throughout**  
✅ **Global Exception Handling**  
✅ **Request Logging**  
✅ **AutoMapper for DTOs**  
✅ **Data Validation with DataAnnotations**  
✅ **Health Checks**  
✅ **YARP Reverse Proxy Gateway**  
✅ **Docker & Docker Compose**  
✅ **SQLite Databases (auto-created + seeded)**

---

## 📦 Building for Production

### Build Release Image
```powershell
docker build -f PatientService/Dockerfile -t hams-patient:latest .
docker build -f DoctorService/Dockerfile -t hams-doctor:latest .
docker build -f AppointmentService/Dockerfile -t hams-appointment:latest .
docker build -f ApiGateway/Dockerfile -t hams-gateway:latest .
```

### Compose Production Stack
```powershell
docker compose -f docker-compose.yml up -d --build
```

---

## 📚 Additional Resources

- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [YARP Reverse Proxy](https://microsoft.github.io/reverse-proxy/)
- [AutoMapper](https://automapper.org/)
- [Docker Documentation](https://docs.docker.com/)

---

## 📄 License

This project is part of the **Systems Software (SS) Course** at **BITS Pilani**, Hyderabad.

---

## 👨‍💻 Author

Built as an assignment for demonstrating microservices architecture principles with .NET 10.

---

## 🎯 Assignment Requirements Met

✅ Three independent microservices (Patient, Doctor, Appointment)  
✅ Database per microservice (separate SQLite instances)  
✅ REST APIs with CRUD operations  
✅ API Gateway (YARP)  
✅ Inter-service communication (HttpClient)  
✅ API Versioning (/api/v1/...)  
✅ Clean Architecture patterns  
✅ Dependency Injection throughout  
✅ Global exception handling  
✅ Logging  
✅ Swagger documentation  
✅ Health checks  
✅ Docker & Docker Compose  
✅ Production-ready code quality

---

## 🤝 Contributing

For assignment improvements, submit suggestions or report issues.

---

## ❓ FAQ

### Q: Can I run only specific services?
**A:** Yes! Each service is fully independent. Run only the ones you need using `dotnet run --project`.

### Q: What if ports are already in use?
**A:** Modify `launchSettings.json` for local dev, or change `docker-compose.yml` port mappings for containers.

### Q: How do I reset the databases?
**A:** Delete `*.db` files in the service directories. They'll be recreated with seed data on next startup.

### Q: Can I modify the appointment booking workflow?
**A:** Absolutely! The validation logic is in `AppointmentService.cs` and can be extended with business rules (e.g., doctor availability checks).

### Q: How does the gateway know about service URLs?
**A:** Configuration comes from `appsettings.json` in ApiGateway. In Docker Compose, environment variables override the config for service-to-service DNS resolution.

---

**Happy coding! 🚀**
