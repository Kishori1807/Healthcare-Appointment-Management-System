# 🚀 HAMS - Ready to Execute!

## ✅ Gateway Configuration Fixed

The ApiGateway configuration has been corrected and **successfully started** on port 5000.

---

## 🎯 Execute the Full System Now

### **Best Method: Use 4 Separate PowerShell Terminals**

Each terminal will run one service independently. Copy-paste these commands:

**Terminal 1 - PatientService (Port 5001)**
```powershell
cd C:\Users\z004rfnd\Documents\BITs-MTech\3rd sem\SS\Project\HAMS
dotnet run --project .\PatientService\PatientService.csproj
```

**Terminal 2 - DoctorService (Port 5002)**
```powershell
cd C:\Users\z004rfnd\Documents\BITs-MTech\3rd sem\SS\Project\HAMS
dotnet run --project .\DoctorService\DoctorService.csproj
```

**Terminal 3 - AppointmentService (Port 5003)**
```powershell
cd C:\Users\z004rfnd\Documents\BITs-MTech\3rd sem\SS\Project\HAMS
dotnet run --project .\AppointmentService\AppointmentService.csproj
```

**Terminal 4 - ApiGateway (Port 5000)**
```powershell
cd C:\Users\z004rfnd\Documents\BITs-MTech\3rd sem\SS\Project\HAMS
dotnet run --project .\ApiGateway\ApiGateway.csproj
```

All 4 services will be **running in parallel**.

---

## 📍 Access Points

Once all 4 services are running:

| Service | Swagger URL | Direct URL |
|---------|-----------|-----------|
| **Patient** | http://localhost:5001/swagger | http://localhost:5001 |
| **Doctor** | http://localhost:5002/swagger | http://localhost:5002 |
| **Appointment** | http://localhost:5003/swagger | http://localhost:5003 |
| **Gateway** | N/A (routes only) | http://localhost:5000 |

---

## ✅ Verify Everything Works

### Test 1: Check Gateway Health
```powershell
curl http://localhost:5000/health
```
Should return: `Healthy`

### Test 2: Get All Patients (Direct)
```powershell
curl http://localhost:5001/api/v1/patients
```

### Test 3: Get All Patients (Via Gateway)
```powershell
curl http://localhost:5000/api/v1/patients
```

Should return the same data from both endpoints!

---

## 🧪 Quick API Workflow (Copy-Paste)

### 1. Create a Patient
```powershell
$body = @{
	fullName = "Test Patient"
	age = 30
	gender = "Male"
	phone = "+1-555-1111"
	email = "test@example.com"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5001/api/v1/patients" `
  -Method Post `
  -ContentType "application/json" `
  -Body $body
```

### 2. Create a Doctor
```powershell
$body = @{
	fullName = "Dr. Test"
	specialization = "General Medicine"
	experience = 5
	consultationFee = 500
	available = $true
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5002/api/v1/doctors" `
  -Method Post `
  -ContentType "application/json" `
  -Body $body
```

### 3. Book an Appointment
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

### 4. View Appointment History
```powershell
Invoke-RestMethod -Uri "http://localhost:5003/api/v1/appointments/history/1" -Method Get
```

---

## 🎯 Using Swagger (Easiest for Testing)

1. Open **http://localhost:5001/swagger** in browser
2. Click any endpoint
3. Click **Try it out**
4. Fill in the request body
5. Click **Execute**

Example: Create a patient, then check all endpoints on the other services.

---

## 📊 System Architecture

```
					Internet
						↓
		 http://localhost:5000 (API Gateway)
						↓
		 ┌──────────────┼──────────────┐
		 ↓              ↓              ↓
	Patient Service  Doctor Service  Appointment Service
	(Port 5001)     (Port 5002)      (Port 5003)
		 ↓              ↓              ↓
	PatientDB.db   DoctorDB.db   AppointmentDB.db
```

---

## 🔌 Port Summary

| Port | Service | Purpose |
|------|---------|---------|
| 5000 | **ApiGateway** | Main entry point for all APIs |
| 5001 | **PatientService** | Patient management |
| 5002 | **DoctorService** | Doctor management |
| 5003 | **AppointmentService** | Appointment management + inter-service calls |

---

## 🧪 Sample Data (Pre-seeded)

**Patients (automatically created):**
- ID 1: Aarav Sharma
- ID 2: Priya Nair

**Doctors (automatically created):**
- ID 1: Dr. Meera Kapoor (Cardiology)
- ID 2: Dr. Rahul Menon (Dermatology)

**Appointments (automatically created):**
- ID 1: Aarav → Dr. Meera
- ID 2: Priya → Dr. Rahul

---

## 📱 Mobile/External Access

To access from another machine:

Replace `localhost` with your computer's IP address:
```powershell
# Get your IP
ipconfig /all

# Then use
http://YOUR_IP:5001/swagger
http://YOUR_IP:5000  (via gateway)
```

---

## 🛠️ Troubleshooting

**Port 5000, 5001, 5002, or 5003 already in use?**
```powershell
# Find what's using the port
netstat -ano | findstr :5001

# Kill the process
taskkill /PID <PID> /F

# Or change port in launchSettings.json
```

**Services can't communicate?**
- Ensure ALL 4 services are running
- Check URLs in appsettings.json match port numbers
- Check firewall isn't blocking connections

**Database issues?**
```powershell
# Delete database files to reset
Remove-Item PatientService/*.db
Remove-Item DoctorService/*.db
Remove-Item AppointmentService/*.db

# Restart services - they'll recreate DBs with seed data
```

---

## 📚 Documentation Files

- **README.md** - Complete architecture and design
- **QUICKSTART.md** - Detailed setup instructions
- **IMPLEMENTATION_SUMMARY.md** - Project completion status
- **docs/HAMS.postman_collection.json** - Pre-built API requests for Postman

---

## ✨ Next Steps

1. ✅ **Open 4 PowerShell terminals**
2. ✅ **Run the 4 commands** (one per terminal)
3. ✅ **Wait 10-15 seconds** for all services to start
4. ✅ **Open http://localhost:5001/swagger** in browser
5. ✅ **Test creating a patient, doctor, and booking appointment**
6. ✅ **Try accessing via gateway at http://localhost:5000**

---

## 🎯 Success Indicators

When everything is running correctly:

✅ All 4 terminals show: **"Application started"**  
✅ No error messages in terminal output  
✅ Swagger UI loads at http://localhost:5001/swagger  
✅ API calls return data (not 500 errors)  
✅ AppointmentService validates patient/doctor (returns 404 if missing)  
✅ Gateway routes requests to correct services  

---

## 🚀 You're Ready!

All services are configured, built, and ready to run.

**Copy one of the 4 commands above and paste into PowerShell to start!**

Questions? Refer to README.md or QUICKSTART.md.

**Happy coding! 🎉**
