-- Sentencias INSERT para agregar 3 usuarios de prueba en SQL Server

-- Usuario 1 - Ana María García (Activo)
INSERT INTO usuarios (Nombre, Cedula, Genero, FechaRegistro, Estado, [User], Pass)
VALUES ('Ana María García López', '697123458', 'femenino', GETDATE(), 'activo', 'agarcia', 'password123');

-- Usuario 2 - Carlos Eduardo Rodríguez (Activo)  
INSERT INTO usuarios (Nombre, Cedula, Genero, FechaRegistro, Estado, [User], Pass)
VALUES ('Carlos Eduardo Rodríguez Martínez', '179876542', 'masculino', GETDATE(), 'activo', 'crodriguez', 'mypass456');

-- Usuario 3 - María Fernanda Santos (Inactivo)
INSERT INTO usuarios (Nombre, Cedula, Genero, FechaRegistro, Estado, [User], Pass) 
VALUES ('María Fernanda Santos Pérez', '9645554667', 'femenino', GETDATE(), 'inactivo', 'msantos', 'test789');

select * from usuarios;