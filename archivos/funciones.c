// Función sin parámetros que no retorna nada
func saludar() {
	fmt.Println("¡Hola, mundo!")
}

// Función sin parámetros que retorna un valor
func obtenerNumero() {
	return 42
}

// Función con parámetros que no retorna nada
func saludarPersona(var nombre: string) {
	fmt.Println("¡Hola,", nombre+"!")
}

// Función con parámetros que retorna un valor
func sumar(var a: int, var b: int) {
	return a + b
}

// Función recursiva: factorial
func factorial(var n: int) {
	if n <= 1 {
		return 1
	}
	return n * factorial(n-1)
}

// Función recursiva: fibonacci
func fibonacci(var n: int) {
	if n <= 1 {
		return n
	}
	return fibonacci(n-1) + fibonacci(n-2)
}

// Función recursiva: torres de Hanoi
func hanoi(var n: int, var origen: string, var auxiliar: string,var destino: string) {
	if n == 1 {
		fmt.Println("Mover disco 1 de", origen, "a", destino)
		return
	}

	hanoi(n-1, origen, destino, auxiliar)
	fmt.Println("Mover disco", n, "de", origen, "a", destino)
	hanoi(n-1, auxiliar, origen, destino)
}


func main(){
    puntos := 0

	fmt.Println("=== Archivo de prueba de funciones ===")

	// 1. Funciones no recursivas sin parámetros (1 punto)
	fmt.Println("==== Funciones no recursivas sin parámetros ====")
	puntosFuncionesSimples := 0

	fmt.Println("Función sin retorno:")
	fmt.Println("###Validacion Manual")
	saludar()

    fmt.Println("\nFunción con retorno:")
	numero := obtenerNumero()
	fmt.Println("El número obtenido es:", numero)

    if numero == 42 {
		puntosFuncionesSimples = puntosFuncionesSimples + 1
		fmt.Println("OK Funciones no recursivas sin parámetros: correcto")
	} else {
		fmt.Println("X Funciones no recursivas sin parámetros: incorrecto")
	}

    // 2. Funciones no recursivas con parámetros (2 puntos)
	fmt.Println("\n==== Funciones no recursivas con parámetros ====")
	puntosFuncionesParametros := 0

	fmt.Println("Función con parámetros sin retorno:")
	fmt.Println("###Validacion Manual")
	saludarPersona("Juan")

    fmt.Println("\nFunción con parámetros y retorno:")
	resultadoSuma := sumar(10, 20)
	fmt.Println("La suma de 10 y 20 es:", resultadoSuma)

    if resultadoSuma == 30 {
		puntosFuncionesParametros = puntosFuncionesParametros + 2
		fmt.Println("OK Función con parámetros y retorno: correcto")
	} else {
		fmt.Println("X Función con parámetros y retorno: incorrecto")
	}

    // 3. Funciones recursivas (6 puntos)
	fmt.Println("\n==== Funciones recursivas ====")
	puntosFuncionesRecursivas := 0

	fmt.Println("Factorial:")
	resultadoFactorial := factorial(5)
	fmt.Println("Factorial de 5:", resultadoFactorial)

	if resultadoFactorial == 120 {
		puntosFuncionesRecursivas = puntosFuncionesRecursivas + 2
		fmt.Println("OK Función factorial: correcto")
	} else {
		fmt.Println("X Función factorial: incorrecto")
	}

    fmt.Println("\nFibonacci:")
	resultadoFibonacci := fibonacci(10)
	fmt.Println("Fibonacci de 10:", resultadoFibonacci)

	if resultadoFibonacci == 55 {
		puntosFuncionesRecursivas = puntosFuncionesRecursivas + 2
		fmt.Println("OK Función fibonacci: correcto")
	} else {
		fmt.Println("X Función fibonacci: incorrecto")
	}

    fmt.Println("\nTorres de Hanoi:")
	fmt.Println("###Validacion Manual")
	hanoi(3, "A", "B", "C")
	puntosFuncionesRecursivas = puntosFuncionesRecursivas + 2


}

