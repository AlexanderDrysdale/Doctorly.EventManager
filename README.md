# Doctorly.EventManager.Api

Doctorly.EventManager.Api is a backend service designed to manage events, attendees, and related workflows. It provides a RESTful API with Swagger documentation for easy exploration and integration. The system is built with a layered architecture to support scalability, maintainability, and future enhancements such as CQRS and client generation.

This project was developed using .NET 10 due to time constraints and environment availability.

## Getting Started
- Ensure the required development certificate is installed.
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
