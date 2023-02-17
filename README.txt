dotnet add package Dapper --version 2.0.123
dotnet add package System.Data.SqlClient --version 4.8.5


CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Description VARCHAR(255),
    Price DECIMAL(10,2) NOT NULL,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
);