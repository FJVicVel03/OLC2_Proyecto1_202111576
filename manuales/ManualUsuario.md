# Manual de Usuario - Intérprete GoLight

### Fernando José Vicente Velásquez, 202111576

## Tabla de Contenidos
1. [Introducción](#1-introducción)
2. [Interfaz de Usuario](#2-interfaz-de-usuario)
3. [Funcionalidades Básicas](#3-funcionalidades-básicas)
4. [Guía de Programación](#4-guía-de-programación)
5. [Solución de Problemas](#5-solución-de-problemas)

## 1. Introducción
GoLight es un intérprete basado en Go que permite escribir y ejecutar programas de manera sencilla. Este manual le ayudará a familiarizarse con la herramienta y sus funcionalidades.

## 2. Interfaz de Usuario

![Interfaz Principal](/manuales/Front.png)

### 2.1 Componentes Principales
- **Editor de Código**: Área principal para escribir código
- **Consola**: Muestra resultados y errores
- **Barra de Herramientas**: Opciones y controles

### 2.2 Barra de Herramientas
- **Nuevo Archivo** (🗋): Crea un archivo en blanco
- **Abrir** (📂): Abre archivos `.glt` existentes
- **Guardar** (💾): Guarda el archivo actual
- **Ejecutar** (▶): Compila y ejecuta el código

## 3. Funcionalidades Básicas

### 3.1 Gestión de Archivos
1. **Crear Nuevo Archivo**
   - Click en "Nuevo Archivo"
   - El editor se limpiará para nuevo código

2. **Abrir Archivo**
   - Click en "Abrir Archivo"
   - Seleccionar archivo `.glt`

3. **Guardar Archivo**
   - Click en "Guardar"
   - Elegir ubicación y nombre
   - Se guardará con extensión `.glt`

### 3.2 Escribir y Ejecutar Código
1. Escribir código en el editor
2. Click en "Ejecutar"
3. Ver resultados en la consola

## 4. Guía de Programación

### 4.1 Estructura Básica
```go
func main() {
    fmt.Println("Hola Mundo")
}
```

### 4.2 Tipos de Datos
- **Primitivos**
  ```go
  var entero int = 42
  var decimal float = 3.14
  var texto string = "Hola"
  var booleano bool = true
  ```

- **Estructuras**
  ```go
  type Persona struct {
      nombre string
      edad int
  }
  ```

### 4.3 Estructuras de Control
- **Condicionales**
  ```go
  if x > 0 {
      fmt.Println("Positivo")
  } else {
      fmt.Println("No positivo")
  }
  ```

- **Bucles**
  ```go
  for i := 0; i < 5; i++ {
      fmt.Println(i)
  }
  ```

### 4.4 Funciones
```go
func suma(a int, b int) {
    return a + b
}
```

## 5. Solución de Problemas

### 5.1 Errores Comunes

| Error | Solución |
|-------|----------|
| Error de sintaxis | Verificar llaves, paréntesis y punto y coma |
| Variable no declarada | Asegurar que la variable está definida |
| Tipo incorrecto | Verificar compatibilidad de tipos |

### 5.2 Mensajes de Error
Los errores se muestran en la consola con el formato:
```
Error en línea X: Descripción del error
```

### 5.3 Tips
1. Guardar frecuentemente el trabajo
2. Usar indentación consistente
3. Verificar errores en la consola
4. Probar el código por partes

## 6. Ejemplos Prácticos

### 6.1 Calculadora Simple
```go
func main() {
    a := 10
    b := 5
    
    fmt.Println("Suma:", a + b)
    fmt.Println("Resta:", a - b)
    fmt.Println("Multiplicación:", a * b)
    fmt.Println("División:", a / b)
}
```

### 6.2 Uso de Slices
```go
func main() {
    numeros := []int{1, 2, 3, 4, 5}
    fmt.Println("Original:", numeros)
    
    numeros = append(numeros, 6)
    fmt.Println("Después de append:", numeros)
}
```

### 6.3 Struct con Métodos
```go
type Rectangulo struct {
    base float
    altura float
}

func main() {
    r := Rectangulo{base: 5, altura: 3}
    fmt.Println("Área:", r.base * r.altura)
}
```

---
