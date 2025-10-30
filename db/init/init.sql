-- Create table with SKU as primary key
CREATE TABLE IF NOT EXISTS inventory_item (
    Sku VARCHAR(50) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Quantity INTEGER NOT NULL
);

-- Insert initial data
INSERT INTO inventory_item (Sku, Name, Quantity) VALUES
('SKU001', 'Item A', 5),
('SKU002', 'Item B', 10),
('SKU003', 'Item C', 0);