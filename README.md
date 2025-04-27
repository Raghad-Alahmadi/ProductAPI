# ProductAPI

## Overview
ProductAPI is a simple ASP.NET Core Web API project built with .NET 8. It provides endpoints to manage a product catalog, including listing all products, retrieving a product by ID, and adding new products. The project uses an in-memory product list and demonstrates key concepts such as dependency injection, configuration management, and containerization.

The project also includes a frontend application (frontend/index.html) that consumes the API and displays the product catalog in a user-friendly interface.

## Features
- **API Endpoints**:
  - GET `/api/products`: List all products.
  - GET `/api/products/{id}`: Retrieve a product by ID.
  - POST `/api/products`: Add a new product.
  - GET `/api/products/config`: Retrieve application configuration (AppName and DefaultCurrency).
- **In-Memory Product List**: Stores product data in memory for simplicity.
- **Dependency Injection**: Uses a service layer (ProductService) to manage business logic.
- **Configuration Management**: Reads AppName and DefaultCurrency from appsettings.json.
- **Frontend Integration**: A simple HTML/JavaScript frontend to interact with the API.
- **Containerization**: Dockerfile included for containerizing the API.
- **Kubernetes Deployment**: Kubernetes manifests (deployment.yaml and service.yaml) for deploying the API.

## Getting Started

### Prerequisites
- .NET 8 SDK
- Docker
- Kubernetes (Minikube)
- A browser or API testing tool like Postman

### Running the API Locally
1. Clone the repository:
   ```
   git clone https://github.com/Raghad-Alahmadi/ProductAPI
   cd ProductAPI
   ```

2. Restore dependencies and run the API:
   ```
   dotnet restore
   dotnet run
   ```

3. The API will be available at https://localhost:5275.

4. Test the API endpoints using Postman or a browser:
   - GET https://localhost:5275/api/products
   - POST https://localhost:5275/api/products (with a JSON body)

### Running the Frontend
1. Navigate to the frontend directory:
   ```
   cd frontend
   ```

2. Serve the index.html file using a simple HTTP server

3. Ensure the apiUrl in frontend/index.html points to the correct API URL.

### Containerizing the API
1. Build the Docker image:
   ```
   docker build -t product-api:latest -f ProductAPI/Dockerfile .
   ```

2. Run the container:
   ```
   docker run -p 5275:80 product-api:latest
   ```

### Deploying to Kubernetes
1. Start Minikube:
   ```
   minikube start
   ```

2. Use Minikube's Docker daemon:
   ```
   eval $(minikube docker-env)
   ```

3. Build the Docker image for Minikube:
   ```
   docker build -t product-api:latest -f ProductAPI/Dockerfile .
   ```

4. Apply the Kubernetes manifests:
   ```
   kubectl apply -f deployment.yaml
   kubectl apply -f service.yaml
   ```

5. Access the service:
   ```
   minikube service product-api
   ```
