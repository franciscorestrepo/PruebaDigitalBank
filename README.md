# PruebaDigitalBank
# 📘 Sistema de Gestión de Usuarios

---

## 📌 Descripción

Este proyecto implementa un sistema de gestión de usuarios utilizando **arquitectura en capas**, distribuido en tres proyectos:

- `Usuario.Web`: Aplicación web (capa de presentación).
- `Usuario.API`: API REST (capa de negocio).
- `Usuario.Data`: Acceso a base de datos (Npgsql, PostgreSQL)

Permite **crear, consultar, editar y eliminar usuarios** con los siguientes campos:

- `Nombre` (texto, máx 100)
- `Fecha de Nacimiento` (tipo fecha)
- `Sexo` (carácter `M` o `F`)

---

## 🧱 Estructura del Proyecto
UsuarioSistema/
├── Usuario.Web # Frontend MVC (ASP.NET Core)
├── Usuario.API # API REST (.NET Core Web API)
├── Usuario.Data # Acceso a base de datos (Npgsql, PostgreSQL)
└── README.md # Documentación del proyecto

## 🔧 Requisitos previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL (v15 o superior)
- DBeaver (opcional)
- Visual Studio 2022+

---

## 🗃️ Configuración de la base de datos

### 1. Crear la base de datos

```sql
CREATE DATABASE usuariosdb;

2. Crear la tabla y el procedimiento almacenado
CREATE TABLE usuarios (
    id SERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    fecha_nacimiento DATE NOT NULL,
    sexo CHAR(1) NOT NULL
);

CREATE OR REPLACE FUNCTION sp_crud_usuarios(
    operacion TEXT,
    uid INT DEFAULT NULL,
    unombre VARCHAR(100) DEFAULT NULL,
    ufecha TIMESTAMP WITH TIME ZONE DEFAULT NULL,
    usexo CHAR(1) DEFAULT NULL
)
RETURNS SETOF usuarios AS $$
BEGIN
    IF operacion = 'INSERT' THEN
        INSERT INTO usuarios (nombre, fecha_nacimiento, sexo)
        VALUES (unombre, ufecha::DATE, usexo);
        RETURN QUERY SELECT * FROM usuarios;

    ELSIF operacion = 'UPDATE' THEN
        UPDATE usuarios
        SET nombre = unombre,
            fecha_nacimiento = ufecha::DATE,
            sexo = usexo
        WHERE id = uid;
        RETURN QUERY SELECT * FROM usuarios;

    ELSIF operacion = 'DELETE' THEN
        DELETE FROM usuarios WHERE id = uid;
        RETURN QUERY SELECT * FROM usuarios;

    ELSIF operacion = 'SELECT_BY_ID' THEN
        RETURN QUERY SELECT * FROM usuarios WHERE id = uid;

    ELSIF operacion = 'SELECT' THEN
        RETURN QUERY SELECT * FROM usuarios;

    ELSE
        RAISE EXCEPTION 'Operación inválida: %', operacion;
    END IF;
END;
$$ LANGUAGE plpgsql;



⚙️ Configuración de la solución

1. Clona el proyecto
git clone https://github.com/franciscorestrepo/PruebaDigitalBank.git

2. Verifica la conexión en Usuario.API/appsettings.json:
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=usuariosdb;Username=postgres;Password=tu_contraseña"
}

3. Configura los proyectos de inicio
En Visual Studio:

Clic derecho sobre la solución → "Configurar proyectos de inicio..."

Selecciona:

Usuario.Web: ✅

Usuario.API: ✅

Usuario.Data: ❌


🚀 Ejecutar la aplicación
Con Visual Studio
Presiona F5 o clic en el botón verde ▶️ y se abrirán dos ventanas del navegador:

🌐 Interfaces de usuario
Crear usuario
👉 http://localhost:5272/Usuario/Create

Gestión de usuarios
👉 http://localhost:5272/Usuario/Gestion

🧪 API REST - Swagger
Documentación API
👉 http://localhost:5133/swagger/index.html

Endpoints disponibles
Método	Ruta	Descripción
GET	/api/usuarios	Listar usuarios
GET	/api/usuarios/{id}	Obtener usuario por ID
POST	/api/usuarios	Crear nuevo usuario
PUT	/api/usuarios/{id}	Editar usuario
DELETE	/api/usuarios/{id}	Eliminar usuario

🧼 Notas
Usuario.Web usa HttpClient para comunicarse con Usuario.API.

El acceso a la base de datos se realiza mediante Npgsql y procedimiento almacenado.
