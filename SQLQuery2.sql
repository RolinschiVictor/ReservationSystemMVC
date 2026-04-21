-- Întâi setează o valoare pentru rândurile existente
UPDATE Users SET FullName = '' WHERE FullName IS NULL;

-- Apoi fă coloana NOT NULL
ALTER TABLE Users
ALTER COLUMN FullName NVARCHAR(256) NOT NULL;