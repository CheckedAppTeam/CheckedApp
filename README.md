##CHECKEDAPP: Your Ultimate Travel Packing Companion

**Welcome to CheckedApp, the innovative app designed to revolutionize the way you pack for your travels. With CheckedApp, preparing for your next adventure is effortless, organized, and interactive. Say goodbye to the stress of forgetting essentials or overpacking. Our app is here to ensure you bring exactly what you need, tailored to where you're going.
Key Features**

    Personalized Packing Lists: Create customized packing lists for each of your trips. Whether you're heading to the beach, embarking on a mountain hike, or attending a business conference, CheckedApp helps you compile a perfect list.

    Interactive Item Status: Easily categorize items into 'Packed', 'To Buy'. This dynamic feature allows you to track your packing progress, manage purchases, and ensure you're fully prepared before departure.

    Collaborative Packing Insights: Discover packing lists shared by others on an interactive map. Select your destination and explore what fellow travelers packed for the same location. Gain inspiration, avoid common mistakes, and maybe even find hidden gems to add to your list.

    Editable and Shareable Lists: Your packing list is not set in stone. Edit items on the go, add last-minute essentials, or remove unnecessary things. Share your lists with travel companions to coordinate packing and ensure nothing is left behind.

    Seamless Synchronization: Access your lists anytime, anywhere. CheckedApp syncs across all your devices, offering a seamless experience whether you're planning on your tablet at home or checking items on your phone at the airport.

**Getting Started**

    Create Your Trip: Input your destination and trip dates to start crafting your personalized packing list.
    Explore and Customize: Use our suggestions based on your destination or explore what others have packed. Customize your list to fit your needs perfectly.
    Track Your Progress: Update your list status as you pack, buy, or decide against items. Keep everything organized and under control.
    Share and Collaborate: Invite friends or family members to view or edit your list. Make packing a part of your travel experience.

**Why Choose CheckedApp?**

    Travel Smart: With insights from a global community, CheckedApp ensures you’re well-prepared for any destination.
    Stay Organized: Our intuitive interface makes managing your packing list a breeze.
    Peace of Mind: Never again worry about forgetting essentials or packing unnecessary items.

**CheckedApp is more than just an web app; it's your personal packing assistant. It's designed to make your travel preparation as enjoyable as the journey itself.**

Technologies and Tools

This project is built using a variety of technologies and tools to create a robust and scalable web application. Below is a detailed list of the technologies and packages used throughout the development of both the backend and frontend parts of the application.
Backend Technologies

    Programming Language: C#
    Web Frameworks and Extensions:
        ASP.NET Core (12.0.1) with AutoMapper extensions for streamlined object mapping.
        Microsoft.AspNet.WebApi.Core (5.3) for building HTTP services.
    Authentication and Identity:
        Microsoft.AspNetCore.Authentication.JwtBearer (7.0.15) for JWT-based authentication.
        Microsoft.AspNetCore.Identity (2.2.0) and Microsoft.AspNetCore.Identity.EntityFrameworkCore (7.0.14) for identity and user management.
    Database and ORM:
        Microsoft.EntityFrameworkCore (7.0.14) as the Object-Relational Mapping (ORM) framework.
        Support for SQL Server and PostgreSQL through Microsoft.EntityFrameworkCore.SqlServer and Npgsql.EntityFrameworkCore.PostgreSQL (7.0.11) respectively.
        Microsoft.EntityFrameworkCore.InMemory for in-memory database testing.
        Microsoft.EntityFrameworkCore.Tools for EF Core tooling.
    Logging and Testing:
        Serilog (3.1.1) for logging.
        NUnit (4.0.1) for unit testing alongside Microsoft.NET.Test.Sdk and Moq for mocking.
        Microsoft.AspNetCore.Mvc.Testing for integration testing.
    API Documentation and Testing:
        Swashbuckle.AspNetCore (6.5.0) for API documentation using Swagger.

Frontend Technologies

    JavaScript Framework: React.js for building user interfaces.
    HTTP Client: Axios for making HTTP requests from the frontend to the backend services.

Additional Tools and Libraries

    Microsoft.AspNetCore.OpenApi for OpenAPI support.
    Microsoft.VisualStudio.Web.CodeGeneration.Design for code generation in ASP.NET Core.
    Microsoft.Extensions.Logging for logging in ASP.NET Core applications.

This combination of technologies provides a comprehensive ecosystem for developing, testing, and deploying high-quality web applications.

CheckedApp soon will be deployed to AZURE!