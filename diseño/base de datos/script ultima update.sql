-- Tabla de Centros
CREATE TABLE Centros (
    id_centro INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    direccion VARCHAR(200) NOT NULL,
    horario VARCHAR(200) NOT NULL
);

-- Tabla de Familias Profesionales
CREATE TABLE FamiliasProfesionales (
    id_familia INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_centro INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    CONSTRAINT fk_familias_centros FOREIGN KEY (id_centro) REFERENCES Centros(id_centro)
);

-- Tabla de Perfiles
CREATE TABLE Perfiles (
    id_perfil INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_familia INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    CONSTRAINT fk_perfiles_familias FOREIGN KEY (id_familia) REFERENCES FamiliasProfesionales(id_familia)
);

-- Tabla de Grados
CREATE TABLE Grados (
    id_grado INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    nombre VARCHAR(50) NOT NULL -- Básica, Media, Superior
);

-- Tabla para Roles
CREATE TABLE Roles (
    id_rol INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    nombre VARCHAR(50) NOT NULL -- Ejemplo: Docente, Supervisor
);

-- Modificar la tabla Profesores
CREATE TABLE Profesores (
    id_profesor INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    apellidos VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL,
    password VARCHAR(255) NOT NULL, -- Contraseña del profesor
    reset_password_token VARCHAR(255), -- Token para restablecer contraseña
    reset_password_token_expiry DATETIME, -- Fecha de expiración del token
    id_rol INT NOT NULL,
    CONSTRAINT fk_profesores_roles FOREIGN KEY (id_rol) REFERENCES Roles(id_rol)
);

-- Tabla de Relación Profesores-Familias Profesionales
CREATE TABLE ProfesoresFamilias (
    id_profesor INT NOT NULL,
    id_familia INT NOT NULL,
    PRIMARY KEY (id_profesor, id_familia),
    CONSTRAINT fk_profesoresfamilias_profesores FOREIGN KEY (id_profesor) REFERENCES Profesores(id_profesor),
    CONSTRAINT fk_profesoresfamilias_familias FOREIGN KEY (id_familia) REFERENCES FamiliasProfesionales(id_familia)
);

-- Tabla de Relación Profesores-Grados
CREATE TABLE ProfesoresGrados (
    id_profesor INT NOT NULL,
    id_grado INT NOT NULL,
    PRIMARY KEY (id_profesor, id_grado),
    CONSTRAINT fk_profesoresgrados_profesores FOREIGN KEY (id_profesor) REFERENCES Profesores(id_profesor),
    CONSTRAINT fk_profesoresgrados_grados FOREIGN KEY (id_grado) REFERENCES Grados(id_grado)
);

-- Tabla para Tipo de Fase
CREATE TABLE TiposFase (
    id_tipo_fase INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    nombre VARCHAR(50) NOT NULL -- Ejemplo: Ordinaria, Extraordinaria
);

-- Tabla para Estado de Empresas
CREATE TABLE EstadosEmpresa (
    id_estado INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    nombre VARCHAR(50) NOT NULL -- Ejemplo: Vigente, Caducada
);

-- Tabla para Fase de Asignación
CREATE TABLE FasesAsignacion (
    id_fase INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    nombre VARCHAR(50) NOT NULL -- Ejemplo: Provisional, Validada
);

-- Tabla de Convocatorias
CREATE TABLE Convocatorias (
    id_convocatoria INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    fecha_inicio DATE NOT NULL,
    fecha_fin DATE NOT NULL,
    id_tipo_fase INT NOT NULL,
    CONSTRAINT fk_convocatorias_tiposfase FOREIGN KEY (id_tipo_fase) REFERENCES TiposFase(id_tipo_fase)
);

-- Tabla de Alumnos
CREATE TABLE Alumnos (
    id_alumno INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    apellidos VARCHAR(100) NOT NULL,
    id_profesor INT NOT NULL,
    id_perfil INT NOT NULL,
    id_grado INT NOT NULL,
    id_convocatoria INT NOT NULL,
    CONSTRAINT fk_alumnos_profesores FOREIGN KEY (id_profesor) REFERENCES Profesores(id_profesor),
    CONSTRAINT fk_alumnos_perfiles FOREIGN KEY (id_perfil) REFERENCES Perfiles(id_perfil),
    CONSTRAINT fk_alumnos_grados FOREIGN KEY (id_grado) REFERENCES Grados(id_grado),
    CONSTRAINT fk_alumnos_convocatorias FOREIGN KEY (id_convocatoria) REFERENCES Convocatorias(id_convocatoria)
);

-- Tabla de Empresas
CREATE TABLE Empresas (
    id_empresa INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    direccion VARCHAR(200) NOT NULL,
    fecha_inicio_acuerdo DATE NOT NULL,
    fecha_fin_acuerdo DATE NOT NULL,
    id_estado INT NOT NULL,
    CONSTRAINT fk_empresas_estados FOREIGN KEY (id_estado) REFERENCES EstadosEmpresa(id_estado)
);

-- Tabla de Gestión de Empresas por Profesores
CREATE TABLE EmpresasProfesores (
    id_empresa INT NOT NULL,
    id_profesor INT NOT NULL,
    PRIMARY KEY (id_empresa, id_profesor),
    CONSTRAINT fk_empresasprofesores_empresas FOREIGN KEY (id_empresa) REFERENCES Empresas(id_empresa),
    CONSTRAINT fk_empresasprofesores_profesores FOREIGN KEY (id_profesor) REFERENCES Profesores(id_profesor)
);

-- Tabla de Asignación de Empresas a Alumnos
CREATE TABLE AsignacionEmpresas (
    id_asignacion INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_alumno INT NOT NULL,
    id_empresa INT NOT NULL,
    id_fase INT NOT NULL,
    fecha_asignacion DATE NOT NULL,
    CONSTRAINT fk_asignacionempresas_alumnos FOREIGN KEY (id_alumno) REFERENCES Alumnos(id_alumno),
    CONSTRAINT fk_asignacionempresas_empresas FOREIGN KEY (id_empresa) REFERENCES Empresas(id_empresa),
    CONSTRAINT fk_asignacionempresas_fases FOREIGN KEY (id_fase) REFERENCES FasesAsignacion(id_fase)
);

-- Tabla de Tareas de Coordinación
CREATE TABLE TareasCoordinacion (
    id_tarea INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_familia INT NOT NULL,
    titulo VARCHAR(100) NOT NULL,
    descripcion TEXT NOT NULL,
    fecha_creacion DATE NOT NULL,
    CONSTRAINT fk_tareascoordinacion_familias FOREIGN KEY (id_familia) REFERENCES FamiliasProfesionales(id_familia)
);

-- Tabla de Eventos Profesores
CREATE TABLE EventosProfesores (
    id_evento INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_profesor INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    fecha DATE NOT NULL,
    hora TIME NOT NULL,
    id_estado INT NOT NULL,
    descripcion TEXT NOT NULL,
    CONSTRAINT fk_eventosprofesores_profesores FOREIGN KEY (id_profesor) REFERENCES Profesores(id_profesor),
    CONSTRAINT fk_eventosprofesores_estados FOREIGN KEY (id_estado) REFERENCES EstadosEmpresa(id_estado)
);