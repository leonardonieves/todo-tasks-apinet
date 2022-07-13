# Introduction 
TODO TASK API  

# Requirements
Installed .Net Core version 5.0
SQL Server (optional) - Current project defaults to in-memory storage

# Getting Started
You need to execute the following steps for installation process
1.	Open a cmd terminal on folder APICore.API
2.	Run command "dotnet run"


# Set project for works with SQL Server database
1.	Navigate to folder APICore.API
2.	Open the ServicesExtensions.cs file with your preferred Edit Code
3.  Go to method ConfigureDbContext
4.  Remove comments from written code
5.  Go to appsettings.json and set your connection string
6.  Go to APICore.Data folder
7.  Open cmd in this folder
8.	Run command "dotnet ef database update" to execute migrations
9.  Open cmd on folder APICore.API
10.	Run command "dotnet run"

# Run in Docker
1. Go to project root
2. Open a cmd terminal
3. Run command "docker run -d -p 8000:8000 task-apinet"

# Developed by 
Leonardo Nieves

To access the demos click on the link:
- [Demo](https://todo-tasks-apinet.herokuapp.com/)
