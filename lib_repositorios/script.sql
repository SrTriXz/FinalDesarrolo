CREATE DATABASE Bolos
GO
USE Bolos
GO

-- Tabla de Jugadores
CREATE TABLE Jugadores (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100) NOT NULL,
    apellido NVARCHAR(100) NOT NULL,
    edad INT NOT NULL
);

-- Tabla de Bolas
CREATE TABLE Bolas (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100) NOT NULL,
    diametro FLOAT NOT NULL,
    Color NVARCHAR(50) NOT NULL,
    peso FLOAT NOT NULL
);

-- Tabla de Lanzamientos
CREATE TABLE Lanzamientos (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100) NOT NULL
);

-- Tabla de Pistas
CREATE TABLE Pistas (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100) NOT NULL,
    estado NVARCHAR(50) NOT NULL
);

-- Tabla de Partidas
CREATE TABLE Partidas (
    id INT PRIMARY KEY IDENTITY(1,1),
    codigo NVARCHAR(50) NOT NULL UNIQUE,
    fecha DATE NOT NULL,
    hora_inicio TIME NOT NULL,
    hora_final TIME NOT NULL,
    estado NVARCHAR(50) NOT NULL,
    puntaje_final INT NOT NULL,
    pistaId INT NOT NULL,
    FOREIGN KEY (pistaId) REFERENCES Pistas(id)
);

-- Tabla de relaci�n Jugadores-Bolas
CREATE TABLE JugadoresBolas (
    id INT PRIMARY KEY IDENTITY(1,1),
    jugadorId INT NOT NULL,
    bolaId INT NOT NULL,
    efectividad NVARCHAR(50) NOT NULL,
    FOREIGN KEY (jugadorId) REFERENCES Jugadores(id),
    FOREIGN KEY (bolaId) REFERENCES Bolas(id),
    CONSTRAINT UQ_JugadorBola UNIQUE (jugadorId, bolaId)
);

-- Tabla de relaci�n Jugadores-Lanzamientos
CREATE TABLE JugadoresLanzamientos (
    id INT PRIMARY KEY IDENTITY(1,1),
    jugadorId INT NOT NULL,
    lanzamientoId INT NOT NULL,
    pino_derribado INT NOT NULL,
    puntaje_obtenido INT NOT NULL,
    FOREIGN KEY (jugadorId) REFERENCES Jugadores(id),
    FOREIGN KEY (lanzamientoId) REFERENCES Lanzamientos(id),
    CONSTRAINT UQ_JugadorLanzamiento UNIQUE (jugadorId, lanzamientoId)
);

-- Tabla de relaci�n Jugadores-Partidas
CREATE TABLE JugadoresPartidas (
    id INT PRIMARY KEY IDENTITY(1,1),
    jugadorId INT NOT NULL,
    partidaId INT NOT NULL,
    codigo NVARCHAR(50) NOT NULL,
    turno INT NOT NULL,
    puntaje INT NOT NULL,
    es_ganador BIT NOT NULL DEFAULT 0, -- Indica si este jugador gan� la partida
    FOREIGN KEY (jugadorId) REFERENCES Jugadores(id),
    FOREIGN KEY (partidaId) REFERENCES Partidas(id),
    CONSTRAINT UQ_JugadorPartida UNIQUE (jugadorId, partidaId));

-- No incluir la columna id (se generar� autom�ticamente)
INSERT INTO Jugadores (nombre, apellido, edad) VALUES
('Emmanuel', 'Torres', 25),
('Jeronimo', 'P�rez', 22),
('Carla', 'G�mez', 28),
('Sof�a', 'L�pez', 24),
('Miguel', 'Rodr�guez', 27);


-- Soluci�n 2 (sin IDs expl�citos)
INSERT INTO Bolas (nombre, diametro, Color, peso) VALUES
('GOLTY', 8.5, 'Azul', 6),
('Roto Grip', 8.0, 'Rojo', 5),
('Storm', 8.2, 'Verde', 7),
('Ebonite', 7.9, 'Negro', 6.5),
('Hammer', 8.3, 'Blanco', 7.2);

-- Insertar Pistas
INSERT INTO Pistas (nombre, estado) VALUES
('Savatica', 'Disponible'),
('Tormenta', 'Disponible'),
('R�pida', 'Ocupada'),
('Extrema', 'Disponible'),
('KingPin', 'Ocupada');


-- Insertar Lanzamientos
INSERT INTO Lanzamientos (nombre) VALUES
('Recto'),
('Cranker'),
('Tweener'),
('Hook'),
('Backup');


-- Insertar Partidas (versi�n corregida sin ganadorId)
INSERT INTO Partidas (codigo, fecha, hora_inicio, hora_final, estado, puntaje_final, pistaId) VALUES
('AA1', CAST(GETDATE() AS DATE), '14:00:00', '16:00:00', 'Finalizada', 300, 1),
('BB2', CAST(GETDATE() AS DATE), '15:00:00', '17:00:00', 'En curso', 0, 2),
('CC3', CAST(GETDATE() AS DATE), '16:00:00', '18:00:00', 'Pendiente', 0, 3),
('DD4', CAST(GETDATE() AS DATE), '18:00:00', '20:00:00', 'Finalizada', 280, 4),
('EE5', CAST(GETDATE() AS DATE), '19:00:00', '21:00:00', 'Finalizada', 290, 5);


INSERT INTO JugadoresBolas (jugadorId, bolaId, efectividad) VALUES
(1, 1, 'Alta'),
(2, 2, 'Media'),
(3, 3, 'Alta'),
(4, 4, 'Baja'),
(5, 5, 'Alta');


-- Insertar JugadoresLanzamientos
INSERT INTO JugadoresLanzamientos (jugadorId, lanzamientoId, pino_derribado, puntaje_obtenido) VALUES
(1, 1, 10, 300),
(2, 2, 8, 250),
(3, 3, 9, 270),
(4, 4, 7, 220),
(5, 5, 10, 290);


INSERT INTO JugadoresPartidas (jugadorId, partidaId, codigo, turno, puntaje, es_ganador) VALUES
(1, 1, 'AA1', 1, 300, 1),
(2, 2, 'BB2', 2, 250, 0),
(3, 3, 'CC3', 3, 270, 0),
(4, 4, 'DD4', 4, 220, 0),
(3, 4, 'DD4', 1, 280, 1),
(5, 5, 'EE5', 5, 290, 1);


SELECT * FROM Jugadores WHERE nombre = 'Emmanuel';
