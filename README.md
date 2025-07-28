# Probability Calculator

A simple full-stack app for calculating probability operations. The backend is built with ASP.NET Core Web API, and the frontend uses React with TypeScript.

## 🛠️ Backend (ASP.NET Core Web API)

### What it does

It accepts two probability values and a calculation type, then returns the result.

### Main endpoint

- `POST /api/calculator` — expects a JSON body with two probabilities and the type of calculation.

### Request example

```json
{
  "a": 0.5,
  "b": 0.5,
  "type": "Either"
}
