-- Create Category and Subcategory tables
CREATE TABLE Categories (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(50) NOT NULL,
    CONSTRAINT UQ_Categories_Name UNIQUE (CategoryName)
);

CREATE TABLE Subcategories (
    SubcategoryId INT PRIMARY KEY IDENTITY(1,1),
    SubcategoryName NVARCHAR(50) NOT NULL,
    CategoryId INT NOT NULL,
    CONSTRAINT FK_Subcategories_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId),
    CONSTRAINT UQ_Subcategories_Name UNIQUE (SubcategoryName, CategoryId)
);

-- Create Ads table with simplified constraints
CREATE TABLE Ads (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    Status BIT DEFAULT 1, -- 1=Active, 0=Inactive
    UserId NVARCHAR(450) NOT NULL, -- For Google Auth
    CategoryId INT NOT NULL,
    SubcategoryId INT NOT NULL,
    CONSTRAINT FK_Ads_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId),
    CONSTRAINT FK_Ads_Subcategories FOREIGN KEY (SubcategoryId) REFERENCES Subcategories(SubcategoryId)
);

-- Insert categories
INSERT INTO Categories (CategoryName) VALUES 
('Household appliances'),
('Computer equipment'),
('Smartphones'),
('Other');

-- Insert subcategories
INSERT INTO Subcategories (SubcategoryName, CategoryId) VALUES
-- Household appliances
('Refrigerators', 1),
('Washing machines', 1),
('Boilers', 1),
('Ovens', 1),
('Cooker hoods', 1),
('Microwave ovens', 1),
-- Computer equipment
('PCs', 2),
('Laptops', 2),
('Monitors', 2),
('Printers', 2),
('Scanners', 2),
-- Smartphones
('Android smartphones', 3),
('iOS/Apple smartphones', 3),
-- Other
('Clothing', 4),
('Shoes', 4),
('Accessories', 4),
('Sports equipment', 4),
('Toys', 4);