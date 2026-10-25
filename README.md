# Aire Logic Assignment

A lightweight C# automation framework utilising the Playwright library and NUnit Test Explorer for validating Cookie Clicker through both **UI** and **API** test coverage (a total of 8 tests). The framework is designed for readability, maintainability, and scalable test growth.

## Framework Overview

This project follows the **Page Object Model (POM)** design pattern for UI automation and a structured service-flow approach for API validation.

### UI Automation (POM)
With POM, page-specific locators and interactions are encapsulated in Page classes, while tests focus on behaviour and assertions.  
This helps:
- reduce duplication,
- improve readability,
- isolate UI changes to a single layer.

### API Automation
API tests are designed around reusable request/response flows so endpoint behaviour can be validated independently and in combination with UI scenarios.  
This helps:
- verify backend rules directly,
- test data setup/cleanup flows faster,
- support end-to-end checks where UI and API interact.

## Design Principles

- **Page Objects** – test intent, interaction logic, and assertions are kept distinct
- **Reusability** – shared page actions and API clients are reused across scenarios
- **Maintainability** – updates are made in one place when UI/API contracts change
- **Scalability** – new UI journeys and API flows can be added without major refactoring

## Typical Test Flows

- **UI flow**: Test → Page Object actions → Assertions on visible behaviour/state
- **API flow**: Test → API client/service call → Response/body/status assertions
- **Integrated flow**: API setup → UI validation (or UI action → API verification)

## Scope

This framework provides a clean base for automating core Cookie Clicker behaviours across presentation and service layers, with room to expand coverage as features evolve.

## Installation and Setup

- ### 1) Clone the repository

```bash
git clone https://github.com/SC-QA/AireLogicAssignment.git
cd AireLogicAssignment
```
### 2) Install prerequisites

Ensure these are installed on your machine:

- **.NET SDK** (version used by this project)
- **IDE/editor** (Visual Studio / Rider / VS Code)
- **Git**
- Any required browser/runtime dependencies for UI automation
- Any required tooling/packages for API testing

### 3) Restore project dependencies

```bash
dotnet restore
```

### 4) Build the solution/project

```bash
dotnet build
```

### 5) Run tests

At this point you should be able to run UI, API or both sets of tests. 
