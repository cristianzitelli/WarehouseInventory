-- Create table with SKU as primary key
CREATE TABLE IF NOT EXISTS inventory_item (
    sku VARCHAR(50) PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    quantity INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS "outbox_message" (
  "id" uuid PRIMARY KEY,
  "datetime" timestamp with time zone NOT NULL DEFAULT now(),
  "type" varchar(200) NOT NULL,
  "content" varchar(200) NOT NULL,
  "processed_at" timestamp with time zone NULL
);

CREATE TABLE IF NOT EXISTS inventory_item_read (
    sku VARCHAR(50) PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    quantity INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS "stock_movement_read" (
  "id" uuid PRIMARY KEY,
  "sku" VARCHAR(50) NOT NULL,
  "occurred_at" timestamp with time zone NOT NULL DEFAULT now(),
  "quantity" integer NOT NULL,
  "new_quantity" integer NOT NULL,
  "type" varchar(100) NOT NULL
);
