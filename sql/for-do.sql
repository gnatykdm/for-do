-- Creating ENUM types for task color and status
CREATE TYPE task_color AS ENUM('red', 'green', 'yellow', 'purple', 'blue');
CREATE TYPE task_status AS ENUM('pending', 'in_progress', 'completed');

-- Creating Users table to store user information
CREATE TABLE Users (
    user_id SERIAL PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    email VARCHAR(100) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Creating Categories table to organize tasks
CREATE TABLE Categories (
    category_id SERIAL PRIMARY KEY,
    user_id INT NOT NULL,
    category_name VARCHAR(50) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE
);

-- Creating Priorities table to define task urgency
CREATE TABLE Priorities (
    priority_id SERIAL PRIMARY KEY,
    priority_name VARCHAR(20) NOT NULL,
    priority_level INT NOT NULL UNIQUE
);

-- Creating Tasks table to store task details
CREATE TABLE Tasks (
    task_id SERIAL PRIMARY KEY,
    user_id INT NOT NULL,
    category_id INT,
    priority_id INT,
    task_title VARCHAR(100) NOT NULL,
    task_description TEXT,
    color task_color DEFAULT 'purple',
    due_date DATE,
    status task_status DEFAULT 'pending',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE,
    FOREIGN KEY (category_id) REFERENCES Categories(category_id) ON DELETE SET NULL,
    FOREIGN KEY (priority_id) REFERENCES Priorities(priority_id) ON DELETE SET NULL
);

-- Creating a function to update the updated_at column
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Creating a trigger to call the function before updates on Tasks
CREATE TRIGGER update_tasks_updated_at
BEFORE UPDATE ON Tasks
FOR EACH ROW
EXECUTE FUNCTION update_updated_at_column();

-- Creating TaskHistory table to track task changes
CREATE TABLE TaskHistory (
    history_id SERIAL PRIMARY KEY,
    task_id INT NOT NULL,
    user_id INT NOT NULL,
    change_description VARCHAR(255) NOT NULL,
    changed_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (task_id) REFERENCES Tasks(task_id) ON DELETE CASCADE,
    FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE
);