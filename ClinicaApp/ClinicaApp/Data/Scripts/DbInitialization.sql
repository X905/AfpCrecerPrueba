USE Db_ClinicaAFP


CREATE TABLE Paciente (
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Nombres VARCHAR(100) NOT NULL,
	Apellidos VARCHAR(100) NOT NULL,
	FechaNacimiento DATE NOT NULL DEFAULT('1900-01-01'),
	Estado BIT NOT NULL DEFAULT(1),
	FechaCreacion DATETIME2 NOT NULL DEFAULT(GETDATE()),
	FechaActualizacion DATETIME2 NOT NULL DEFAULT(GETDATE()),
);

CREATE TABLE Doctor (
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Nombres VARCHAR(100) NOT NULL,
	Apellidos VARCHAR(100) NOT NULL,
	FechaCreacion DATETIME2 NOT NULL DEFAULT(GETDATE()),
	FechaActualizacion DATETIME2 NOT NULL DEFAULT(GETDATE()),
	Estado BIT NOT NULL DEFAULT(1)
);

CREATE TABLE Citas (
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	FechaCita DATETIME2 NOT NULL DEFAULT(GETDATE()),
	IdPaciente INT NOT NULL FOREIGN KEY REFERENCES Paciente(Id),
	IdDoctor INT NOT NULL FOREIGN KEY REFERENCES Doctor(Id),
	FechaCreacion DATETIME2 NOT NULL DEFAULT(GETDATE()),
	FechaActualizacion DATETIME2 NOT NULL DEFAULT(GETDATE()),
	Estado BIT NOT NULL DEFAULT(1)
);

CREATE TABLE Diagnostico(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Titulo VARCHAR(50) NOT NULl,
	Comentarios VARCHAR(150) NOT NULL,
	IdCita INT NOT NULL FOREIGN KEY REFERENCES Citas(Id),
	FechaCreacion DATETIME2 NOT NULL DEFAULT(GETDATE()),
	FechaActualizacion DATETIME2 NOT NULL DEFAULT(GETDATE()),
	Estado BIT NOT NULL DEFAULT(1)
)

CREATE TABLE DiagnosticoDetalle(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	PruebaNombre VARCHAR(30) NOT NULL,
	Observacion VARCHAR (150) NOT NULL,
	IdDiagnostico INT NOT NULL FOREIGN KEY REFERENCES Diagnostico(Id),
	FechaCreacion DATETIME2 NOT NULL DEFAULT(GETDATE()),
	FechaActualizacion DATETIME2 NOT NULL DEFAULT(GETDATE()),
	Estado BIT NOT NULL DEFAULT(1)
)

--SEEDER

--left Estado,FechaCreacion,FechaActualizacion
INSERT INTO Paciente (Nombres,Apellidos,FechaNacimiento) 
VALUES ('Angel Daniel', 'Castillo Bonilla', '2000-05-09')
--left Estado,FechaCreacion,FechaActualizacion
INSERT INTO Doctor (Nombres,Apellidos)
VALUES('Jose','Cruz')
--left Estado,FechaCreacion,FechaActualizacion
INSERT INTO Citas (FechaCita,IdPaciente,IdDoctor)
VALUES('2022-04-21 09:58:00', 1,1)
--left Estado,FechaCreacion,FechaActualizacion
INSERT INTO Diagnostico (Titulo,Comentarios,IdCita)
VALUES('Diagnostico','El Paciente no presenta problemas',1)
--left Estado,FechaCreacion,FechaActualizacion
INSERT INTO DiagnosticoDetalle (PruebaNombre,Observacion,IdDiagnostico)
VALUES('Test Sanguineo', 'No presenta alteraciones', 1)
INSERT INTO DiagnosticoDetalle (PruebaNombre,Observacion,IdDiagnostico)
VALUES('Presion Arterial', 'No presenta alteraciones', 1)
INSERT INTO DiagnosticoDetalle (PruebaNombre,Observacion,IdDiagnostico)   
VALUES('Colesterol', 'Normal', 1)





SELECT *FROM Paciente
SELECT *FROM Doctor
SELECT *FROM Citas
SELECT *FROM Diagnostico
SELECT *FROM DiagnosticoDetalle