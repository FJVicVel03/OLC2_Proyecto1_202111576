# Manual Técnico - Intérprete GoLight

### Fernando José Vicente Velásquez, 202111576

## Tabla de Contenidos
- [1. Competencias](#1-competencias)
- [2. Descripción del Proyecto](#2-descripción-del-proyecto)
- [3. Arquitectura](#3-arquitectura)
- [4. Componentes Principales](#4-componentes-principales)
- [5. Flujo de Trabajo](#5-flujo-de-trabajo)
- [6. Herramientas y Tecnologías](#6-herramientas-y-tecnologías)
- [7. Especificaciones Técnicas](#7-especificaciones-técnicas)

## 1. Competencias

### 1.1 Competencia General
El proyecto se centra en el desarrollo de un intérprete funcional para el lenguaje de programación GoLight, abarcando los siguientes aspectos fundamentales:

- **Análisis Léxico**: Identificación y clasificación de tokens
- **Análisis Sintáctico**: Validación de la estructura gramatical
- **Análisis Semántico**: Aplicación de reglas de coherencia
- **Generación y Recorrido del AST**: Construcción y manipulación del Árbol de Sintaxis Abstracta

### 1.2 Competencias Específicas
- **Uso de ANTLR**: Implementación de analizadores léxicos y sintácticos en C#
- **Estructuras de Datos**: Selección e implementación de estructuras apropiadas
- **Manejo del AST**: Desarrollo de técnicas de recorrido y generación de tabla de símbolos

## 2. Descripción del Proyecto

### 2.1 Objetivo
Desarrollo de un intérprete para GoLight, un lenguaje inspirado en Go, incluyendo una interfaz gráfica para la creación, edición y ejecución de código.

### 2.2 Componentes de la Interfaz

#### 2.2.1 Editor
- Funcionalidades de edición de código
- Soporte para múltiples archivos
- Indicador de línea actual
- Extensión de archivo: `.glt`

#### 2.2.2 Funcionalidades Principales
- Crear archivos nuevos
- Abrir archivos existentes (.glt)
- Guardar archivos
- Ejecutar código

#### 2.2.3 Reportes
- **Reporte de Errores**: Errores léxicos, sintácticos y semánticos
- **Tabla de Símbolos**: Variables, métodos y funciones declarados
- **AST**: Visualización del Árbol de Sintaxis Abstracta

## 3. Arquitectura

### 3.1 Componentes del Sistema
```
├── Frontend (Next.js)
│   ├── Editor
│   ├── Consola
│   └── Visualizador de Reportes
│
├── Backend (C#)
│   ├── Analizador Léxico (ANTLR)
│   ├── Analizador Sintáctico (ANTLR)
│   ├── Analizador Semántico
│   └── Intérprete
```

## 4. Componentes Principales

### 4.1 Analizador Léxico
- Generado con ANTLR
- Identificación de tokens
- Manejo de errores léxicos

### 4.2 Analizador Sintáctico
- Generado con ANTLR
- Construcción del AST
- Validación de estructura gramatical

### 4.3 Analizador Semántico
- Validación de tipos
- Verificación de declaraciones
- Control de ámbito

## 5. Flujo de Trabajo

1. **Entrada de Código**
   - Recepción de archivo `.glt`
   - Validación inicial

2. **Proceso de Análisis**
   ```
   Código Fuente → Análisis Léxico → Análisis Sintáctico → AST → Análisis Semántico → Ejecución
   ```

3. **Generación de Salida**
   - Resultados de ejecución
   - Reportes generados
   - Mensajes de error (si existen)

## 6. Herramientas y Tecnologías

### 6.1 Desarrollo
- **Frontend**: Next.js
- **Backend**: C# (.NET Core)
- **Generación de Analizadores**: ANTLR4
- **Control de Versiones**: Git

### 6.2 Entorno de Desarrollo
- Visual Studio Code
- Visual Studio (C#)
- Node.js

## 7. Especificaciones Técnicas

### 7.1 Gramática ANTLR4
La gramática del lenguaje GoLight está definida usando ANTLR4:

```antlr
grammar Language;

program: dcl*;

dcl: varDcl | stmt | funcDcl | structDcl | structFuncDcl | expr;

varDcl: 'var' ID ':' type ('=' expr)?    
    | 'var' ID '=' expr
    | ID ':=' expr;

type: 'int' | 'float' | 'bool' | 'string' | 'rune' | ID;
```

### 7.2 Sistema de Tipos
El sistema implementa una jerarquía de tipos basada en `ValueWrapper`:

```csharp
public abstract record ValueWrapper;

public record IntValue(int Value) : ValueWrapper;
public record FloatValue(float Value) : ValueWrapper;
public record StringValue(string Value) : ValueWrapper;
public record BoolValue(bool Value) : ValueWrapper;
public record NullValue : ValueWrapper;
```

### 7.3 Patrón Visitor
El intérprete utiliza el patrón Visitor para recorrer y ejecutar el AST:

```csharp
public class CompilerVisitor : LanguageBaseVisitor<ValueWrapper>
{
    public Environment currentEnvironment;
    public string output = "";

    // Visita declaraciones de variables
    public override ValueWrapper VisitVarDcl(LanguageParser.VarDclContext context)
    {
        // Implementación de declaraciones
    }

    // Visita expresiones binarias
    public override ValueWrapper VisitBinary(LanguageParser.BinaryContext context)
    {
        // Implementación de operaciones binarias
    }
}
```

### 7.4 Manejo de Ámbitos
El sistema utiliza una estructura de ámbitos anidados:

```csharp
public class Environment
{
    private Dictionary<string, ValueWrapper> variables;
    private Environment parent;

    public void Declare(string name, ValueWrapper value)
    {
        // Declaración de variables
    }

    public ValueWrapper Get(string name)
    {
        // Búsqueda en ámbito actual y padres
    }
}
```

### 7.5 Características Implementadas

#### 7.5.1 Estructuras de Control
- Condicionales (if-else, switch)
- Ciclos (for, while, do-while)
- Control de flujo (break, continue, return)

#### 7.5.2 Tipos de Datos
- **Primitivos**: int, float, bool, string, rune
- **Compuestos**: structs, slices
- **Referencia**: nil

#### 7.5.3 Operaciones
- Aritméticas (+, -, *, /, %)
- Relacionales (==, !=, <, >, <=, >=)
- Lógicas (&&, ||, !)

### 7.6 API REST
La comunicación entre frontend y backend se realiza mediante endpoints REST:

```csharp
[ApiController]
[Route("api/[controller]")]
public class CompileController : ControllerBase
{
    [HttpPost]
    public ActionResult<CompileResponse> Compile([FromBody] CompileRequest request)
    {
        // Lógica de compilación
    }
}
```

### 7.7 Interfaz de Usuario

#### 7.7.1 Editor de Código
- Monaco Editor para edición de código
- Resaltado de sintaxis
- Manejo de archivos .glt

#### 7.7.2 Consola de Salida
- Visualización de resultados
- Mensajes de error
- Estado de ejecución

### 7.8 Manejo de Errores
Sistema de errores en tres niveles:

1. **Errores Léxicos**
   - Caracteres inválidos
   - Tokens no reconocidos

2. **Errores Sintácticos**
   - Estructura incorrecta
   - Tokens fuera de lugar

3. **Errores Semánticos**
   - Variables no declaradas
   - Tipos incompatibles
   - Operaciones inválidas

```csharp
public class SemanticError : Exception
{
    public IToken Token { get; }
    public string Message { get; }
    public int Line { get; }
    public int Column { get; }
}
```