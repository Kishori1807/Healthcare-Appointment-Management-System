# HAMS Microservices - Quick Start Guide

## Prerequisites

- **.NET 10 SDK** or higher ([download](https://dotnet.microsoft.com/download))
- **Visual Studio 2022+** / **VS Code** / **Rider** (optional)
- **Docker Desktop** (optional, for container deployment)
- **Postman** (optional, for API testing)

---

## 🚀 Getting Started

### 1. Clone/Extract the Repository
```bash
cd C:\Users\z004rfnd\Documents\BITs-MTech\3rd sem\SS\Project\HAMS
```

### 2. Restore Dependencies
```powershell
dotnet restore HAMS.sln
```

### 3. Build Solution
```powershell
dotnet build HAMS.sln
```

---

## ▶️ Running the Application

### **Option A: Independent Services (Recommended for Development)**

Open **4 PowerShell terminals** and run each service separately:

**Terminal 1 - Patient Service (Port 5001)**
```powershell
dotnet run --project .\PatientService\PatientService.csproj
```

**Terminal 2 - Doctor Service (Port 5002)**
```powershell
dotnet run --project .\DoctorService\DoctorService.csproj
```

**Terminal 3 - Appointment Service (Port 5003)**
```powershell
dotnet run --project .\AppointmentService\AppointmentService.csproj
```

**Terminal 4 - API Gateway (Port 5000)**
```powershell
dotnet run --project .\ApiGateway\ApiGateway.csproj
```

**Access Swagger Documentation:**
- Patient Service: http://localhost:5001/swagger
- Doctor Service: http://localhost:5002/swagger
- Appointment Service: http://localhost:5003/swagger

---

### **Option B: Visual Studio Multi-Startup Projects**

1. Open `HAMS.sln` in Visual Studio
2. Right-click Solution → **Set Startup Projects...**
3. Select **Multiple startup projects**
4. For each project (PatientService, DoctorService, AppointmentService, ApiGateway):
   - Set **Action** to **Start**
5. Click **OK** and press **F5**

---

### **Option C: Docker Compose (Production-like)**

**Prerequisites:** Docker Desktop must be running

```powershell
# Build and start all services
docker compose up --build

# Run in background
docker compose up -d --build

# View logs
docker compose logs -f

# Stop all services
docker compose down
```

**Service URLs in Docker:**
- Patient Service: http://localhost:5001
- Doctor Service: http://localhost:5002
- Appointment Service: http://localhost:5003
- API Gateway: http://localhost:5000

---

## ✅ Verify Services are Running

### Health Check Endpoints
```powershell
# Patient Service
curl http://localhost:5001/health

# Doctor Service
curl http://localhost:5002/health

# Appointment Service
curl http://localhost:5003/health

# API Gateway
curl http://localhost:5000/health
```

All should return: `Healthy`

---

## 🧪 Testing the APIs

### Using Swagger UI (Recommended)
1. Go to http://localhost:5001/swagger
2. Expand endpoints and click **Try it out**
3. Fill in request body and click **Execute**

### Using Postman
1. Import `docs/HAMS.postman_collection.json`
2. Select from available requests
3. Modify parameters and send

### Using PowerShell

**Get all patients:**
```powershell
Invoke-RestMethod -Uri "http://localhost:5001/api/v1/patients" -Method Get
```

**Create a patient:**
```powershell
$body = @{
	fullName = "John Doe"
	age = 35
	gender = "Male"
	phone = "+1-555-0100"
	email = "john@example.com"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5001/api/v1/patients" `
  -Method Post `
  -ContentType "application/json" `
  -Body $body
```

**Book an appointment:**
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

## 🗂️ Database Information

Each service has its own SQLite database automatically created on startup:

- **PatientService:** `PatientService/PatientDB.db`
- **DoctorService:** `DoctorService/DoctorDB.db`
- **AppointmentService:** `AppointmentService/AppointmentDB.db`

### Reset Databases
Delete the `.db` files and restart services. They'll be recreated with seed data.

```powershell
Remove-Item PatientService/*.db
Remove-Item DoctorService/*.db
Remove-Item AppointmentService/*.db
```

---

## 🔧 Configuration

### Service URLs (Local Development)

**appsettings.json** (each service):
```json
{
  "ConnectionStrings": {
	"DefaultConnection": "Data Source=PatientDB.db"
  }
}
```

### External Service URLs (AppointmentService)

**AppointmentService/appsettings.json:**
```json
{
  "ExternalServices": {
	"PatientServiceBaseUrl": "http://localhost:5001",
	"DoctorServiceBaseUrl": "http://localhost:5002"
  }
}
```

### Gateway Routes (ApiGateway)

**ApiGateway/appsettings.json:**
```json
{
  "ReverseProxy": {
	"Routes": {
	  "patients-route": { "ClusterId": "patients-cluster", "Match": { "Path": "/api/v{version}/patients{**catch-all}" } },
	  "doctors-route": { "ClusterId": "doctors-cluster", "Match": { "Path": "/api/v{version}/doctors{**catch-all}" } },
	  "appointments-route": { "ClusterId": "appointments-cluster", "Match": { "Path": "/api/v{version}/appointments{**catch-all}" } }
	},
	"Clusters": {
	  "patients-cluster": { "Destinations": { "patients-service": { "Address": "http://localhost:5001" } } },
	  "doctors-cluster": { "Destinations": { "doctors-service": { "Address": "http://localhost:5002" } } },
	  "appointments-cluster": { "Destinations": { "appointments-service": { "Address": "http://localhost:5003" } } }
	}
  }
}
```

---

## 📊 Sample Data

Services auto-seed sample data on startup:

**Patients:**
- Aarav Sharma (32, Male)
- Priya Nair (28, Female)

**Doctors:**
- Dr. Meera Kapoor (Cardiology, 10 years)
- Dr. Rahul Menon (Dermatology, 8 years)

**Appointments:**
- Aarav + Dr. Meera (Initial consultation)
- Priya + Dr. Rahul (Follow-up)

---

## 🛑 Troubleshooting

### Port Already in Use
**Error:** Address already in use

**Solution:** Change port in `launchSettings.json`:
```json
"applicationUrl": "http://localhost:5004"  // Change 5001 to available port
```

### Services Can't Communicate
**Error:** Cannot connect to appointment service from gateway

**Solution:** Ensure all services are running and check URLs in `appsettings.json`

### Database Locked
**Error:** Database is locked

**Solution:** 
```powershell
# Close the service
# Delete the .db file
Remove-Item PatientService/PatientDB.db
# Restart service
```

### Docker Build Fails
**Error:** Docker build fails

**Solution:**
```powershell
# Clean Docker resources
docker system prune -a

# Rebuild
docker compose up --build
```

---

## 🎯 Complete API Workflow

1. **Create Patient**
   ```
   POST http://localhost:5001/api/v1/patients
   ```

2. **Create Doctor**
   ```
   POST http://localhost:5002/api/v1/doctors
   ```

3. **Book Appointment** (validates patient & doctor)
   ```
   POST http://localhost:5003/api/v1/appointments
   ```

4. **View Appointment History**
   ```
   GET http://localhost:5003/api/v1/appointments/history/1
   ```

5. **Cancel Appointment**
   ```
   DELETE http://localhost:5003/api/v1/appointments/1
   ```

---

## 📚 Documentation

- **Full README:** `README.md`
- **Postman Collection:** `docs/HAMS.postman_collection.json`
- **.NET 10 Docs:** https://learn.microsoft.com/dotnet
- **ASP.NET Core:** https://learn.microsoft.com/aspnet/core
- **Entity Framework Core:** https://learn.microsoft.com/ef/core
- **YARP Proxy:** https://microsoft.github.io/reverse-proxy

---

## 💡 Tips

- Use Swagger for interactive API testing
- Import Postman collection for automated requests
- Check logs in terminal for debugging
- Set breakpoints in Visual Studio for step debugging
- Use VS Code REST Client extension for inline testing

---

## 📋 Assignment Checklist

✅ Three microservices (Patient, Doctor, Appointment)  
✅ Separate SQLite databases  
✅ REST APIs with CRUD operations  
✅ API Gateway with YARP  
✅ Inter-service communication  
✅ API Versioning (/api/v1/)  
✅ Clean Architecture layers  
✅ Dependency Injection  
✅ Global exception handling  
✅ Request logging  
✅ Swagger documentation  
✅ Health checks  
✅ Docker & Docker Compose  
✅ Production-ready code

---

## 🆘 Support

For issues or questions, refer to the main `README.md` or check service logs.

---

**Happy coding! 🚀**
