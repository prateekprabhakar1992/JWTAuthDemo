# JWTAuthDemo
API Authentication using JWT - C# .NET 9

# JWT Authentication Demo

This project demonstrates a simple implementation of JWT (JSON Web Token) authentication in an ASP.NET Core Web API. It covers the following:

- User authentication using JWT.
- Access control with role-based authentication.
- Refresh token mechanism.
- Securing API endpoints with JWT authorization.

## Features

1. **User Authentication**: 
   - Users can authenticate by providing a valid username and password.
   - Upon successful login(hadcoded for demo purpose), a JWT token is generated and returned to the client.
   
2. **Role-Based Access Control (RBAC)**:
   - JWT tokens include claims such as roles (`Admin`, `User`).
   - Based on these claims, access to certain API endpoints can be restricted.
   - The `[Authorize]` attribute ensures that only authenticated and authorized users can access specific resources.

3. **JWT Expiration & Refresh Tokens**:
   - The JWT has an expiration time (e.g., 15 minutes).
   - A refresh token is provided to the client to get a new JWT without requiring the user to log in again.

4. **Secure API Endpoints**:
   - Endpoints such as `GET /test/GetUser` and `GET /test/GetAdmin` are secured using JWT authentication.
   - Users need to pass the JWT in the Authorization header of their HTTP request to access protected resources.

## Setup

### Prerequisites

Make sure you have the following tools installed:

- .NET 6.0 or higher
- Visual Studio Code or your preferred IDE
- Git (for version control)
- Postman (or any API testing tool)

### Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/prateekprabhakar1992/JWTAuthDemo.git
    ```

2. Navigate to the project directory:

    ```bash
    cd JWTAuthDemo
    ```

3. Restore the dependencies:

    ```bash
    dotnet restore
    ```

4. Run the application:

    ```bash
    dotnet run
    ```

The application will start running on `http://localhost:5116` (or `https://localhost:7277` if HTTPS is configured).

## Usage

### 1. Authentication

To authenticate and get a JWT, send a POST request to the `/api/auth/login` endpoint with the following JSON body:

```json 
For User role
{
  "username": "test",
  "password": "password"
}

For Admin role
{
  "username": "admin",
  "password": "secured"
}

