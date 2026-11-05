-- Lumo.Api development database setup
-- 1. Create an empty database named TrainingDb on (localdb)\mssqllocaldb
-- 2. Run this script against it

CREATE TABLE Customers (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    City NVARCHAR(50) NOT NULL,
    CreatedAt DATE NOT NULL
);

CREATE TABLE Orders (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CustomerId INT NOT NULL FOREIGN KEY REFERENCES Customers(Id),
    Amount DECIMAL(10,2) NOT NULL,
    OrderDate DATE NOT NULL,
    Status NVARCHAR(20) NOT NULL
);

INSERT INTO Customers (Name, City, CreatedAt) VALUES
('Ali',   'Antalya',  '2024-11-02'),
('Veli',  'Istanbul', '2024-12-15'),
('Ayse',  'Antalya',  '2025-01-10'),
('Fatma', 'Izmir',    '2025-02-20'),
('Can',   'Istanbul', '2025-03-01');

INSERT INTO Orders (CustomerId, Amount, OrderDate, Status) VALUES
(1, 100, '2025-01-15', 'Completed'),
(1, 200, '2025-02-10', 'Completed'),
(2,  50, '2025-01-20', 'Cancelled'),
(3, 300, '2025-03-05', 'Completed'),
(1, 150, '2025-03-20', 'Pending'),
(2, 500, '2025-02-25', 'Completed'),
(3,  80, '2025-03-28', 'Completed');
