# Doctorly.EventManager.Api

Doctorly.EventManager.Api is a backend service designed to manage events, attendees, and related workflows. It exposes a RESTful API with Swagger documentation for easy exploration and integration. The system is built with a layered architecture to support scalability, maintainability, and future enhancements such as CQRS and typed client generation.

Key features include:
- **Fuzzy search** for flexible event and attendee lookups.
- **Caching** to improve performance and reduce repeated queries.
- **Middleware for API call pattern validation**, ensuring consistent request structures and enforcing best practices across endpoints.
- **Email notifications** (planned) to streamline communication with attendees.
- **Swagger/OpenAPI documentation** for discoverability and client generation.

This project was developed using .NET 10 due to time constraints and environment availability.

## Getting Started
- Ensure the required development certificate is installed by running: dotnet dev-certs https --trust
This step is required for local HTTPS support.
- Open and run the solution in Visual Studio.
- API documentation is available via Swagger at: https://localhost:7044/index.html
- Work is in progress to generate a typed client using NSwag from the Swagger JSON, enabling frontend developers to integrate more easily.

## Roadmap
- Integrate NSwag client generation for frontend consumption.
- Implement a message queue for email delivery.
- Refactor towards a CQRS architecture.

## Testing
- Open **Test Explorer** in Visual Studio.
- Build the solution to discover available tests.
- Execute tests directly from the Test Explorer.
