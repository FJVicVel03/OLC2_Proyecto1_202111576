# GoLight - Intérprete de Lenguaje de Programación
## Proyecto de Compiladores 2 - 202111576

## 📝 Descripción
GoLight es un intérprete de lenguaje de programación inspirado en Go, implementado en C# con ANTLR4 para análisis léxico y sintáctico. Incluye una interfaz web moderna desarrollada en Next.js.

##  Características

### 🔧 Funcionalidades del Lenguaje
- **Tipos de Datos**: 
  - Primitivos (`int`, `float`, `bool`, `string`, `rune`)
  - Estructurados (structs, slices)
- **Control de Flujo**: 
  - Condicionales (`if`, `else`, `switch`)
  - Bucles (`for`, `while`, `do-while`)
- **Funciones y Estructuras**:
  - Declaración y llamada de funciones
  - Structs y métodos
  - Manejo de ámbitos

### 🛠️ Funciones Nativas
- `fmt.Println()`: Salida en consola
- `len()`: Longitud de slices
- `append()`: Añadir elementos
- Operaciones con strings y slices

## 📁 Estructura del Proyecto
```
proyecto/
├── api/                # Backend (C#)
│   ├── compiler/      # Núcleo del intérprete
│   ├── grammars/     # Gramática ANTLR4
│   └── Controllers/  # API endpoints
├── frontend/          # Frontend (Next.js)
│   ├── components/   # Componentes React
│   └── pages/       # Páginas
└── manuales/         # Documentación
```

## Instalación

### Requisitos Previos
- .NET 9.0+
- Node.js 18.x+
- ANTLR4 Runtime

### Backend
```bash
cd api
dotnet restore
dotnet build
dotnet run
```

### Frontend
```bash
cd frontend
npm install
npm run dev
```

## 💻 Uso
1. Navegar a `http://localhost:3000`
2. Crear o abrir archivo `.glt`
3. Escribir/editar código
4. Ejecutar con el botón "Run"

## 📋 Ejemplos

### Ejemplo Básico
```go
func main() {
    fmt.Println("¡Hola, GoLight!")
}
```

### Structs y Métodos
```go
type Persona struct {
    nombre string
    edad int
}

func main() {
    p := Persona{
        nombre: "Juan",
        edad: 25
    }
    fmt.Println(p)
}
```

## 🛠️ Tecnologías
- C# (.NET 9.0)
- ANTLR4
- Next.js/React
- TypeScript

## 📖 Documentación
- [Manual de Usuario](./manuales/ManualUsuario.md)
- [Manual Técnico](./manuales/ManualTecnico.md)

## 👤 Autor
Fernando José Vicente Velásquez  
Carné: 202111576


