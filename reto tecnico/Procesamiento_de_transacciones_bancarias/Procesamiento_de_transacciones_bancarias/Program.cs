//--Iniciamos el aplicativo preguntando por la ruta del archivo
using System.Globalization;

//-- creamos la clase transaccion para manejar mejor los datos de cada transaccion

class Transaccion
{
    public int Id {  get; set; }
    public string Tipo { get; set; }
    public double Monto { get; set; }
}

class Programa
{
    static void Main()
    {
        Console.WriteLine("Introduce la ruta del archivo CSV:");
        String ruta = Console.ReadLine(); //-- solicitamos la dirección del archivo CSV

        if(string.IsNullOrWhiteSpace(ruta)|| !File.Exists(ruta)) //-- se comprueba que el archivo exista y la ruta no este en blanco
        {
            Console.WriteLine("Ruta invalida o el archivo no existe.");
            return;
        }

        var lineas = File.ReadAllLines(ruta); //-- lee todas las lineas del archivo y las almacena

        if(lineas.Length<=1) 
        {
            Console.WriteLine("El archivo esta vacío o solo contiene encabezados");
        }


        List<Transaccion> transacciones = new(); //-- creamos una lista para almacenar cada transacción

        for (int i = 1; i < lineas.Length; i++)
        {
            var campos = lineas[i].Split(','); //-- separamos cada campo separado por una coma ','

            if(campos.Length < 3)
                continue; //-- saltamos lineas que no esten formateadas correctamente

            if (!int.TryParse(campos[0].Trim(), out int id)) continue; //-- si el formato del ID no es un numero entero lo saltamos

            string tipo = campos[1].Trim();

            if (!double.TryParse(campos[2].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double monto)) continue; //- si los numeros estan mal escritos saltamos linea

            transacciones.Add(new Transaccion { Id = id, Tipo = tipo, Monto = monto }); //-- etiquetamos y guardamos los datos en la lista
                        
        }

        var creditos = transacciones.Where(t => t.Tipo.Equals("Crédito", StringComparison.OrdinalIgnoreCase)); //-- filtramos y almacenamos por tipo las transacciones y las almacenamos en una variable
        var debitos = transacciones.Where(t => t.Tipo.Equals("Débito", StringComparison.OrdinalIgnoreCase));
        var mayor = transacciones.OrderByDescending(t => t.Monto).FirstOrDefault(); //- ordenamos las transsaciones de mayor a menor para obtener la transaación más alta

        //-- dentro de las variables antes creadas vamos a sumar todos los montos almacenados para guardarlos en variables para optimizar su manejo
        double montoCredito = creditos.Sum(t => t.Monto);
        double montoDebito = debitos.Sum(t => t.Monto);
        double balanceFinal = montoCredito - montoDebito;


        //-- por último imprimimos el reporte final
        Console.WriteLine("\nReporte de transacción");
        Console.WriteLine("-------------------");
        Console.WriteLine($"Balance final: {balanceFinal:F2}");
        if (mayor != null)
            Console.WriteLine($"Transacción de mayor monto: ID {mayor.Id} - {mayor.Monto:F2}");

        Console.WriteLine($"Conteo de transacciones: Crédito: {creditos.Count()}, Débito: {debitos.Count()}");

        


        Console.WriteLine("Presione la tecla (y) si desea generar un archivo CSV con su reporte");
        if (Console.ReadLine()=="y") 
        {
            //-- crearemos un reporte en archivo CSV 
            string[] reporte = new[]
            {
            "Resumen de transacciones:",
            $"Balance final,{balanceFinal:F2}",
            $"Mayor transacción,ID {mayor.Id},{mayor.Monto:F2}",
            $"Total Créditos,{creditos.Count()},{montoCredito:F2}",
            $"Total Débitos,{debitos.Count()},{montoDebito:F2}"
            };

            File.WriteAllLines("reporte.csv", reporte);
            //-- genera el archivo CSV y copia todas las lineas del arreglo reporte
            //-- el reporte sera guardado el la carpeta del ejecutable del proyecto
            Console.WriteLine("Reporte generado: reporte.csv");
        }
    }
}

