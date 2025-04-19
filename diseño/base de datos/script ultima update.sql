-- Tabla de Centros
CREATE TABLE Centros (
    id_centro INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    direccion VARCHAR(200) NOT NULL,
    horario VARCHAR(200) NOT NULL,
    telefono VARCHAR(20) NOT NULL,
    logo MEDIUMBLOB
);

-- Tabla para Roles
CREATE TABLE Roles (
    id_rol INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_centro INT NOT NULL,
    nombre VARCHAR(50) NOT NULL, -- Ejemplo: Docente, Supervisor
    CONSTRAINT fk_roles_centros FOREIGN KEY (id_centro) REFERENCES Centros(id_centro)
);

-- Tabla de Familias Profesionales
CREATE TABLE FamiliasProfesionales (
    id_familia INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_centro INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    CONSTRAINT fk_familias_centros FOREIGN KEY (id_centro) REFERENCES Centros(id_centro)
);

-- Tabla de Profesores (MODIFICADA)
CREATE TABLE Profesores (
    id_profesor INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_rol INT NOT NULL,
    id_centro INT NOT NULL,
    id_familia INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    apellidos VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL,
    cargo VARCHAR(100) NOT NULL,
    password VARCHAR(255) NOT NULL, -- Contraseña del profesor
    reset_password_token VARCHAR(255), -- Token para restablecer contraseña
    reset_password_token_expiry DATETIME, -- Fecha de expiración del token
    sexo VARCHAR(1) NOT NULL,
    first_char VARCHAR(1) NOT NULL,
    bgColor VARCHAR(8) NOT NULL,
    activo INTEGER(1) NOT NULL -- 1 usuario activo, 0 usuario no activo
    CONSTRAINT fk_profesores_roles FOREIGN KEY (id_rol) REFERENCES Roles(id_rol),
    CONSTRAINT fk_profesores_centros FOREIGN KEY (id_centro) REFERENCES Centros(id_centro),
    CONSTRAINT fk_profesores_familias FOREIGN KEY (id_familia) REFERENCES FamiliasProfesionales(id_familia)
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
    id_centro INT NOT NULL,
    nombre VARCHAR(50) NOT NULL, -- Básica, Media, Superior
    CONSTRAINT fk_grados_centros FOREIGN KEY (id_centro) REFERENCES Centros(id_centro)
);

-- Tabla de Cursos
CREATE TABLE Cursos (
    id_curso INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_grado INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT,
    fecha_inicio DATE NOT NULL,
    fecha_fin DATE NOT NULL,
    CONSTRAINT fk_cursos_grados FOREIGN KEY (id_grado) REFERENCES Grados(id_grado)
);

-- Tabla de Cargos
CREATE TABLE Cargos (
    id_cargo INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_centro INT NOT NULL, 
    id_profesor INT NOT NULL, -- Relación con la tabla Profesores
    nombre VARCHAR(100) NOT NULL, -- Ejemplo: Director, Coordinador, Docente
    descripcion TEXT, 
    fecha_inicio DATE NOT NULL, 
    fecha_fin DATE, 
    CONSTRAINT fk_cargos_centros FOREIGN KEY (id_centro) REFERENCES Centros(id_centro),
    CONSTRAINT fk_cargos_profesores FOREIGN KEY (id_profesor) REFERENCES Profesores(id_profesor)
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
    id_centro INT NOT NULL,
    nombre VARCHAR(50) NOT NULL, -- Ejemplo: Ordinaria, Extraordinaria
    CONSTRAINT fk_tiposfase_centros FOREIGN KEY (id_centro) REFERENCES Centros(id_centro)
);

-- Tabla para Estado de Empresas
CREATE TABLE EstadosEmpresa (
    id_estado INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_centro INT NOT NULL,
    nombre VARCHAR(50) NOT NULL, -- Ejemplo: Vigente, Caducada
    CONSTRAINT fk_estadosempresa_centros FOREIGN KEY (id_centro) REFERENCES Centros(id_centro)
);

-- NUEVA: Tabla para Estado de Eventos
CREATE TABLE EstadosEventos (
    id_estado INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_centro INT NOT NULL,
    nombre VARCHAR(50) NOT NULL, -- Ejemplo: Pendiente, Completado, Cancelado
    CONSTRAINT fk_estadoseventos_centros FOREIGN KEY (id_centro) REFERENCES Centros(id_centro)
);

-- Tabla para Fase de Asignación
CREATE TABLE FasesAsignacion (
    id_fase INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_centro INT NOT NULL,
    nombre VARCHAR(50) NOT NULL, -- Ejemplo: Provisional, Validada
    CONSTRAINT fk_fasesasignacion_centros FOREIGN KEY (id_centro) REFERENCES Centros(id_centro)
);

-- Tabla de Convocatorias
CREATE TABLE Convocatorias (
    id_convocatoria INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_tipo_fase INT NOT NULL,
    fecha_inicio DATE NOT NULL,
    fecha_fin DATE NOT NULL,
    CONSTRAINT fk_convocatorias_tiposfase FOREIGN KEY (id_tipo_fase) REFERENCES TiposFase(id_tipo_fase)
);

-- Tabla de Alumnos (MODIFICADA: añadido id_curso)
CREATE TABLE Alumnos (
    id_alumno INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    apellidos VARCHAR(100) NOT NULL,
    id_profesor INT NOT NULL,
    id_perfil INT NOT NULL,
    id_grado INT NOT NULL,
    id_curso INT NOT NULL, -- Nueva columna para curso específico
    id_convocatoria INT NOT NULL,
    CONSTRAINT fk_alumnos_profesores FOREIGN KEY (id_profesor) REFERENCES Profesores(id_profesor),
    CONSTRAINT fk_alumnos_perfiles FOREIGN KEY (id_perfil) REFERENCES Perfiles(id_perfil),
    CONSTRAINT fk_alumnos_grados FOREIGN KEY (id_grado) REFERENCES Grados(id_grado),
    CONSTRAINT fk_alumnos_cursos FOREIGN KEY (id_curso) REFERENCES Cursos(id_curso),
    CONSTRAINT fk_alumnos_convocatorias FOREIGN KEY (id_convocatoria) REFERENCES Convocatorias(id_convocatoria)
);

-- Tabla de Empresas
CREATE TABLE Empresas (
    id_empresa INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_centro INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    direccion VARCHAR(200) NOT NULL,
    fecha_inicio_acuerdo DATE NOT NULL,
    fecha_fin_acuerdo DATE NOT NULL,
    id_estado INT NOT NULL,
    CONSTRAINT fk_empresas_estados FOREIGN KEY (id_estado) REFERENCES EstadosEmpresa(id_estado),
    CONSTRAINT fk_empresas_centros FOREIGN KEY (id_centro) REFERENCES Centros(id_centro)
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

-- Tabla de Eventos Profesores (MODIFICADA: ahora referencia a EstadosEventos)
CREATE TABLE EventosProfesores (
    id_evento INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_profesor INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    fecha DATE NOT NULL,
    hora TIME NOT NULL,
    id_estado INT NOT NULL,
    descripcion TEXT NOT NULL,
    CONSTRAINT fk_eventosprofesores_profesores FOREIGN KEY (id_profesor) REFERENCES Profesores(id_profesor),
    CONSTRAINT fk_eventosprofesores_estados FOREIGN KEY (id_estado) REFERENCES EstadosEventos(id_estado)
);


-- INDICES

-- Tabla Centros
CREATE INDEX idx_centros_nombre ON Centros(nombre);

-- Tabla FamiliasProfesionales
CREATE INDEX idx_familiasprofesionales_id_centro ON FamiliasProfesionales(id_centro);
CREATE INDEX idx_familiasprofesionales_nombre ON FamiliasProfesionales(nombre);

-- Tabla Perfiles
CREATE INDEX idx_perfiles_id_familia ON Perfiles(id_familia);

-- Tabla Grados
CREATE INDEX idx_grados_id_centro ON Grados(id_centro);

-- Tabla Roles
CREATE INDEX idx_roles_id_centro ON Roles(id_centro);

-- Tabla Profesores
CREATE INDEX idx_profesores_id_rol ON Profesores(id_rol);
CREATE INDEX idx_profesores_id_centro ON Profesores(id_centro);
CREATE INDEX idx_profesores_id_familia ON Profesores(id_familia);
CREATE INDEX idx_profesores_email ON Profesores(email);

-- Tabla ProfesoresGrados
CREATE INDEX idx_profesoresgrados_id_grado ON ProfesoresGrados(id_grado);

-- Tabla TiposFase
CREATE INDEX idx_tiposfase_id_centro ON TiposFase(id_centro);

-- Tabla EstadosEmpresa
CREATE INDEX idx_estadosempresa_id_centro ON EstadosEmpresa(id_centro);

-- NUEVO: Índices para EstadosEventos
CREATE INDEX idx_estadoseventos_id_centro ON EstadosEventos(id_centro);
CREATE INDEX idx_estadoseventos_nombre ON EstadosEventos(nombre);

-- Tabla FasesAsignacion
CREATE INDEX idx_fasesasignacion_id_centro ON FasesAsignacion(id_centro);

-- Tabla Convocatorias
CREATE INDEX idx_convocatorias_id_tipo_fase ON Convocatorias(id_tipo_fase);

-- Tabla Alumnos
CREATE INDEX idx_alumnos_id_profesor ON Alumnos(id_profesor);
CREATE INDEX idx_alumnos_id_perfil ON Alumnos(id_perfil);
CREATE INDEX idx_alumnos_id_grado ON Alumnos(id_grado);
CREATE INDEX idx_alumnos_id_curso ON Alumnos(id_curso); -- Nuevo índice para curso
CREATE INDEX idx_alumnos_id_convocatoria ON Alumnos(id_convocatoria);

-- Tabla Empresas
CREATE INDEX idx_empresas_id_centro ON Empresas(id_centro);
CREATE INDEX idx_empresas_id_estado ON Empresas(id_estado);

-- Tabla EmpresasProfesores
CREATE INDEX idx_empresasprofesores_id_profesor ON EmpresasProfesores(id_profesor);

-- Tabla AsignacionEmpresas
CREATE INDEX idx_asignacionempresas_id_alumno ON AsignacionEmpresas(id_alumno);
CREATE INDEX idx_asignacionempresas_id_empresa ON AsignacionEmpresas(id_empresa);
CREATE INDEX idx_asignacionempresas_id_fase ON AsignacionEmpresas(id_fase);

-- Tabla TareasCoordinacion
CREATE INDEX idx_tareascoordinacion_id_familia ON TareasCoordinacion(id_familia);

-- Tabla EventosProfesores
CREATE INDEX idx_eventosprofesores_id_profesor ON EventosProfesores(id_profesor);
CREATE INDEX idx_eventosprofesores_id_estado ON EventosProfesores(id_estado);

-- Índices para la tabla Cursos
CREATE INDEX idx_cursos_id_grado ON Cursos(id_grado);
CREATE INDEX idx_cursos_nombre ON Cursos(nombre);

-- Índices para la tabla Cargos
CREATE INDEX idx_cargos_id_centro ON Cargos(id_centro);
CREATE INDEX idx_cargos_id_profesor ON Cargos(id_profesor);
CREATE INDEX idx_cargos_nombre ON Cargos(nombre);