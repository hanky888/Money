CREATE DATABASE Money;  

USE Money;  

CREATE TABLE Currency (  
    id INT IDENTITY(1,1) PRIMARY KEY,  
    code VARCHAR(10) NOT NULL,  
    name NVARCHAR(50) NOT NULL 	
);

INSERT INTO Currency (code, name) VALUES
('TWD', N'新台幣'),
('USD', N'美元'),  
('EUR', N'歐元'),  
('JPY', N'日元'),  
('GBP', N'英鎊'),  
('AUD', N'澳元'); 