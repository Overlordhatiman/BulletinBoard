Use BulletinBoard

CREATE PROCEDURE spAd_GetAll
AS
BEGIN
    SELECT 
        a.Id, a.Title, a.Description, a.CreatedDate, a.Status, a.UserId,
        a.CategoryId, c.CategoryName,
        a.SubcategoryId, s.SubcategoryName
    FROM Ads a
    INNER JOIN Categories c ON a.CategoryId = c.CategoryId
    INNER JOIN Subcategories s ON a.SubcategoryId = s.SubcategoryId
    WHERE a.Status = 1 -- Active ads only
    ORDER BY a.CreatedDate DESC;
END

CREATE PROCEDURE spAd_GetById
    @Id INT
AS
BEGIN
    SELECT 
        a.Id, a.Title, a.Description, a.CreatedDate, a.Status, a.UserId,
        a.CategoryId, c.CategoryName,
        a.SubcategoryId, s.SubcategoryName
    FROM Ads a
    INNER JOIN Categories c ON a.CategoryId = c.CategoryId
    INNER JOIN Subcategories s ON a.SubcategoryId = s.SubcategoryId
    WHERE a.Id = @Id;
END

CREATE PROCEDURE spAd_GetByUser
    @UserId NVARCHAR(450)
AS
BEGIN
    SELECT 
        a.Id, a.Title, a.Description, a.CreatedDate, a.Status, a.UserId,
        a.CategoryId, c.CategoryName,
        a.SubcategoryId, s.SubcategoryName
    FROM Ads a
    INNER JOIN Categories c ON a.CategoryId = c.CategoryId
    INNER JOIN Subcategories s ON a.SubcategoryId = s.SubcategoryId
    WHERE a.UserId = @UserId
    ORDER BY a.CreatedDate DESC;
END

CREATE PROCEDURE spAd_Create
    @Title NVARCHAR(100),
    @Description NVARCHAR(MAX),
    @UserId NVARCHAR(450),
    @CategoryId INT,
    @SubcategoryId INT
AS
BEGIN
    -- Validate category/subcategory relationship
    IF NOT EXISTS (
        SELECT 1 FROM Subcategories 
        WHERE SubcategoryId = @SubcategoryId AND CategoryId = @CategoryId
    )
    BEGIN
        RAISERROR('Invalid category/subcategory combination', 16, 1);
        RETURN;
    END

    INSERT INTO Ads (Title, Description, UserId, CategoryId, SubcategoryId)
    VALUES (@Title, @Description, @UserId, @CategoryId, @SubcategoryId);
    
    SELECT SCOPE_IDENTITY() AS Id; -- Return new ad ID
END

CREATE PROCEDURE spAd_Update
    @Id INT,
    @Title NVARCHAR(100),
    @Description NVARCHAR(MAX),
    @CategoryId INT,
    @SubcategoryId INT,
    @UserId NVARCHAR(450) -- For ownership validation
AS
BEGIN
    -- Validate ownership
    IF NOT EXISTS (SELECT 1 FROM Ads WHERE Id = @Id AND UserId = @UserId)
    BEGIN
        RAISERROR('You can only update your own ads', 16, 1);
        RETURN;
    END

    -- Validate category/subcategory
    IF NOT EXISTS (
        SELECT 1 FROM Subcategories 
        WHERE SubcategoryId = @SubcategoryId AND CategoryId = @CategoryId
    )
    BEGIN
        RAISERROR('Invalid category/subcategory combination', 16, 1);
        RETURN;
    END

    UPDATE Ads SET
        Title = @Title,
        Description = @Description,
        CategoryId = @CategoryId,
        SubcategoryId = @SubcategoryId
    WHERE Id = @Id;
END

CREATE PROCEDURE spAd_Delete
    @Id INT,
    @UserId NVARCHAR(450) -- For ownership validation
AS
BEGIN
    -- Soft delete (set inactive) with ownership check
    UPDATE Ads SET
        Status = 0 -- Inactive
    WHERE Id = @Id AND UserId = @UserId;
    
    IF @@ROWCOUNT = 0
        RAISERROR('Ad not found or you don''t have permission', 16, 1);
END