## Overview
⚠️ This project were implemented with a STRAIGHTFORWARD approach.

The task description is quite vague and lacks sufficient detail, leaving a lot open to interpretation. The code itself has scattered comments that only address a few aspects. I'd be happy to dive deeper, clarify these uncertainties, and explore all potential implementation options together.

## Goals
* This project is designed to Using any public weather API receive data (country, city, temperature) from 2 cities in 2 countries - with periodical update 1/min.
* Provide API for FE to get the data from DB (country, city, min temperature, max temperature, last update time).

### Setup Instructions

#### MS SQL Server.(Used as the database) coz we don't have restrictions on the database type and requirements about db).
   - Ensure you have a running instance of MS SQL Server.
   - Update the connection string in `appsettings.json` to point to your database.
   - Run the migrations to create the necessary tables.
#### Solution
1. **Clone the repository:**
   
2. **Configure the API Key:**
   - Open the `appsettings.json` file.
   - Add your OpenWeather API key.
     
3. **Run the service**

## Solution Structure
### Weather Data Fetching Service

This service fetches weather data from a public API for specified cities and stores it in a database.
The data is updated every minute and includes the country, city, minimum temperature, maximum temperature, and the last update time.
   
### Database Schema

Relational database schema is used to store the weather data.

The database schema includes the following fields:
- Id
- Country
- City
- MinTemperature
- MaxTemperature
- Modified

Sample:
![image](https://github.com/user-attachments/assets/8b302d04-c5b2-475d-ade2-83de2c157b86)

### Endpoints

- `GET /api/weather`: Retrieves the weather data for all cities in the database.
- ![image](https://github.com/user-attachments/assets/61232b67-eb46-4a69-a010-5aa12d8181ca)

## Unit Tests
Added some.

![image](https://github.com/user-attachments/assets/24a3817d-5089-4d8f-af52-c94d52a54f54)
