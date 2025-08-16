# MvcCV - Personal CV Management System

A comprehensive ASP.NET MVC web application for creating and managing a personal CV/resume website with an integrated admin panel for content management.

## 🚀 Features

### Public CV Website
- **Responsive Design**: Modern, mobile-friendly CV layout using Bootstrap
- **Personal Information**: Display name, contact details, and professional description
- **Experience Section**: Showcase work experience with detailed descriptions
- **Education**: Academic background with institutions and achievements
- **Skills**: Technical and professional skills showcase
- **Hobbies & Interests**: Personal interests and activities
- **Certificates**: Professional certifications and achievements
- **Contact Form**: Visitors can send messages directly through the website
- **Social Media Integration**: Links to social media profiles

### Admin Panel
- **Secure Authentication**: Password-protected admin access with session management
- **Content Management**: Full CRUD operations for all CV sections
- **User Management**: Admin user creation and management
- **Security Features**: Password hashing, session timeout, and security headers
- **Modern Interface**: AdminLTE-based admin panel with intuitive navigation

## 🛠️ Technology Stack

- **Framework**: ASP.NET MVC 5
- **Database**: SQL Server with Entity Framework 6
- **Frontend**: Bootstrap 5, jQuery 3, Font Awesome
- **Admin UI**: AdminLTE 3
- **Architecture**: Repository Pattern with Generic Repository
- **Security**: Custom authentication attributes, password hashing (PBKDF2)

## 🖼️ Project Images
<img width="2559" height="1276" alt="Default View" src="https://github.com/user-attachments/assets/c0801c83-d66f-4590-b054-375a98600b2e" />
<img width="2556" height="1278" alt="Login View" src="https://github.com/user-attachments/assets/2053d591-69a7-4154-94bc-08ddc0528dfc" />
<img width="2558" height="1279" alt="AdminView" src="https://github.com/user-attachments/assets/f53f54b5-b8ce-46ca-ad40-fa3cf6f48e76" />

## 📁 Project Structure

```
MvcCV/
├── Controllers/           # MVC Controllers
│   ├── DefaultController.cs     # Main CV display
│   ├── AdminController.cs       # Admin user management
│   ├── AboutMeController.cs     # About section management
│   ├── ExperienceController.cs  # Experience management
│   ├── EducationController.cs   # Education management
│   ├── SkillController.cs       # Skills management
│   ├── HobbyController.cs       # Hobbies management
│   ├── CertificateController.cs # Certificates management
│   ├── ContactController.cs     # Contact form handling
│   ├── SocialMediaController.cs # Social media management
│   └── LoginController.cs       # Authentication
├── Models/Entity/         # Entity Framework Models
│   ├── TBLAbout.cs             # Personal information
│   ├── TBLExperience.cs        # Work experience
│   ├── TBLEducation.cs         # Education records
│   ├── TBLSkill.cs             # Skills
│   ├── TBLHobby.cs             # Hobbies
│   ├── TBLCertificates.cs      # Certificates
│   ├── TBLContact.cs           # Contact messages
│   ├── TBLSocialMedia.cs       # Social media links
│   └── TBLAdmin.cs             # Admin users
├── Repositories/          # Data Access Layer
│   ├── GenericRepository.cs    # Base repository
│   └── [Specific]Repository.cs # Entity-specific repositories
├── Views/                 # Razor Views
│   ├── Default/               # Public CV views
│   ├── Admin/                 # Admin panel views
│   └── [Section]/             # Section-specific views
├── Helpers/               # Utility Classes
│   └── PasswordHelper.cs      # Password hashing utilities
├── Attributes/            # Custom Attributes
│   └── SecurityAttributes.cs  # Authentication & security
└── AdminLTE-3.0.4/       # Admin template assets
```

## 🗄️ Database Schema

The application uses the following main entities:

- **TBLAbout**: Personal information (name, contact, description, photo)
- **TBLExperience**: Work experience records
- **TBLEducation**: Educational background
- **TBLSkill**: Technical and professional skills
- **TBLHobby**: Personal interests and hobbies
- **TBLCertificates**: Professional certifications
- **TBLContact**: Contact form submissions
- **TBLSocialMedia**: Social media profile links
- **TBLAdmin**: Administrative users

## 🔧 Setup Instructions

### Prerequisites
- Visual Studio 2017 or later
- .NET Framework 4.6.2 or later
- SQL Server (LocalDB or full installation)
- IIS Express (included with Visual Studio)

### Installation Steps

1. **Clone the Repository**
   ```bash
   git clone [repository-url]
   cd MvcCV
   ```

2. **Database Setup**
   - Update the connection string in `Web.config` to match your SQL Server instance
   - The connection string is located in the `<connectionStrings>` section

3. **Restore NuGet Packages**
   ```bash
   nuget restore
   ```

4. **Build the Solution**
   - Open `MvcCV.sln` in Visual Studio
   - Build the solution (Ctrl+Shift+B)

5. **Database Migration**
   - The application uses Entity Framework Code First
   - Database will be created automatically on first run
   - Ensure your SQL Server instance is running

6. **Run the Application**
   - Press F5 or click "Start" in Visual Studio
   - The application will launch in your default browser

## 🔐 Security Features

- **Password Hashing**: Uses PBKDF2 with salt for secure password storage
- **Session Management**: 30-minute session timeout with activity tracking
- **Security Headers**: Implements various security headers (X-Frame-Options, X-XSS-Protection, etc.)
- **Authentication Attributes**: Custom attributes for protecting admin routes
- **Input Validation**: Anti-forgery tokens and model validation


## 👤 Author

**Bartuğ Kaan Çelebi**
- Software Developer

## 🙏 Acknowledgments

- [AdminLTE](https://adminlte.io/) for the admin panel template
- [Start Bootstrap Resume](https://startbootstrap.com/theme/resume) for the CV template
- [Bootstrap](https://getbootstrap.com/) for the responsive framework
- [Font Awesome](https://fontawesome.com/) for icons

---
