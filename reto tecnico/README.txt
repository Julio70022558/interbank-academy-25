INTRODUCCIÓN

El reto consiste en generar una aplicación CLI de transacciones bancarias y genere un reporte resumido de:
-Balance final
-Transacción de mayor monto
-Conteo de transacciones
Importando un archivo CSV, para dicha aplicación se uso el IDE C#.

INSTRUCCIONES DE EJECUCIÓN

De preferencia para visualizar el código de la aplicación se debe de tener instalado el IDE Visual Studio 2022.

En caso contrario un sistema operativo compatible y un editor de texto IDE como: 
Visual Studio para Windows o Visual Studio Code para multiple plataforma o incluso editores simples como el Notepad++.	

Para poder ejecutarlo sin tener dependencias instaladas puede ir a la dirección del ejecutable en la carpeta del proyecto: "...\Procesamiento_de_transacciones_bancarias\Procesamiento_de_transacciones_bancarias\bin\Debug\net8.0\Procesamiento_de_transacciones_bancarias.exe" y ejecutar el programa para lo cual le pedirá la ruta del archivo CSV que desea importar.

Debe colocar la ruta exacta por ejemplo "C:\Users\USUARIO\Downloads\data.csv", esta ruta puede ser obtenida fácilmente dando clic derecho al archivo y seleccionar la opción "Copiar como ruta de acceso".

ENFOQUE Y SOLUCIÓN

-Entrada del usuario
El programa solicita la ruta de un archivo CSV mediante Console.ReadLine().
Se valida si el archivo existe con File.Exists(ruta).

-Lectura y validación del archivo
Se leen todas las líneas con File.ReadAllLines.
Se verifica si hay al menos una línea de datos (se asume que la primera es el encabezado).

-Procesamiento de datos
Se define una clase Transaccion para representar cada fila con 3 campos: Id, Tipo, Monto.
Se recorren las líneas desde la segunda fila en adelante.
Se filtran líneas mal formateadas (menos de 3 columnas o errores de conversión de datos).
Se agregan las transacciones válidas a una lista.

-Análisis
Se usa LINQ para:
Obtener todas las transacciones de tipo "Crédito" y "Débito".
Calcular sumas totales por tipo.
Encontrar la transacción con mayor monto.

-Salida por consola
Se imprime un reporte con:
Balance final (Créditos – Débitos).
Transacción de mayor monto.
Conteo de créditos y débitos.

-Generación de reporte
Se crea un arreglo con los datos del resumen y se guarda en un archivo llamado reporte.csv.

-Uso de la clase Transaccion
Se usó una clase para encapsular la lógica de cada transacción, mejorando la legibilidad y mantenimiento del código. De esta forma se evita trabajar directamente con arreglos de strings sin estructura.

-Uso de LINQ
LINQ (Where, OrderByDescending, Sum, etc.) simplifica la lógica de filtrado, ordenamiento y agregación de datos.
Hace el código más fácil de entender que usar bucles manuales.

-Manejo de errores 
Se omiten líneas con formato inválido (con continue) para evitar que una línea mal escrita detenga toda la ejecución.
Se ignoran errores silenciosamente, lo cual es útil para robustez pero podría mejorarse con logs si fuera un sistema más complejo.

-Separación de responsabilidades (a nivel básico)
Aunque todo el código está en Main, las operaciones están organizadas claramente: entrada, validación, procesamiento, salida y generación de reporte.


ESTRUCTURA DEL PROYECTO

El proyecto se encuentra en un archivo llamado "Procesamiento_de_transacciones_bancarias", dentro se encuentra un archivo tipo SLN que sirve para abrir el proyecto en el Visual Studio y la carpeta que almacena el Program.CS, el launcher del proyecto para Visual Studio y las carpetas bin y obj. Bin almacena los archivos compilados, copias de bibliotecas necesarias y otros recursos requeridos durante el tiempo de ejecución, su propósito es la salida final del proyecto, dentro de esta carpeta también contiene el ejecutable final del proyecto. La carpeta obj contiene archivos intermedios que Visual Studio utiliza para la compilación, no contiene el ejecutable final de proyecto.

  
