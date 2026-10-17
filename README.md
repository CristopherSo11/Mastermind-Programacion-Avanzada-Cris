
# Mastermind - Programación Avanzada

Proyecto desarrollado en C# con .NET 10 para el curso de Programación Avanzada.

## Descripción

Mastermind es un juego de lógica en el que el jugador debe descubrir una combinación secreta de colores mediante intentos.

Después de cada intento, el programa indica:

- Cuántos colores están en la posición correcta.
- Cuántos colores son correctos, pero están en otra posición.

El juego termina cuando el jugador descubre completamente la combinación secreta.

## Funcionalidades

- Juego interactivo mediante consola.
- Partidas de 4 o 6 posiciones.
- Generación aleatoria del patrón secreto.
- Configuración de colores mediante JSON.
- Validación de los intentos del jugador.
- Soporte para colores repetidos.
- Registro de evaluaciones en un archivo de texto.
- Sistema de cheat codes con usos limitados.
- Una pista como máximo por partida.
- Persistencia de usos de cheat codes.
- Opción para iniciar una nueva partida.
- Pruebas unitarias automatizadas con xUnit.

## Tecnologías utilizadas

- C#
- .NET 10
- Visual Studio Code
- xUnit
- JSON
- Git y GitHub

## Estructura del proyecto

### Mastermind.BusinessLogic

Contiene la lógica del juego, el algoritmo de evaluación, las validaciones, el sistema de registro y los cheat codes.

### Mastermind.AppConsole

Aplicación de consola que permite al usuario jugar Mastermind.

### Mastermind.Tests

Proyecto de pruebas automatizadas para comprobar el funcionamiento de las clases y sus integraciones.

## Colores disponibles

| Código | Color |
|--------|-------|
| 1 | Yellow |
| C | Cyan |
| B | Black |
| P | Purple |
| W | White |
| K | Pink |
| R | Red |
| M | Brown |
| N | Green |

Los colores se configuran mediante el archivo `colores.json`.

## Cómo ejecutar el proyecto

Se requiere tener instalado el SDK de .NET 10.

Desde la carpeta principal del repositorio, ejecutar:

```powershell
dotnet run --project Mastermind.AppConsole
```

El programa solicitará seleccionar una partida de 4 o 6 posiciones.

Después, el jugador podrá introducir combinaciones utilizando los códigos de colores disponibles.

## Cheat codes

Durante una partida, el jugador puede escribir `CHEAT` después de realizar al menos un intento válido.

El programa solicitará un código y, si es válido y tiene usos disponibles, intentará revelar una posición y su color.

Cada código tiene una cantidad limitada de usos, almacenada en un archivo JSON persistente.

Solamente se permite utilizar un cheat code por partida.

Si la posición seleccionada ya estaba correctamente adivinada, la pista puede mostrar el estado `Sin suerte`.

## Registro de evaluaciones

Las evaluaciones válidas se registran en un archivo de texto dentro de la carpeta `Logs` de la aplicación.

El registro incluye información del intento, el patrón secreto, la fecha y los resultados de la evaluación.

## Pruebas unitarias

El proyecto utiliza xUnit.

Para ejecutar las pruebas:

```powershell
dotnet test Mastermind.sln
```

Se han implementado 16 pruebas automatizadas que cubren:

- Algoritmo básico de Mastermind.
- Validación de combinaciones.
- Registro de evaluaciones.
- Funcionamiento de cheat codes.
- Restricción de una pista por partida.
- Integración de los servicios del juego.

## Control de versiones

El desarrollo se organizó mediante commits correspondientes a las distintas etapas del proyecto.

## Autor

Cristopher Solano Orozco
