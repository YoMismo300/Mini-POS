using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Crear una lista de productos, donde cada producto contiene el nombre, precio y cantidad
            List<(string nombre, double precio, int cantidad)> productos = new List<(string, double, int)>();

            // Crear una lista de ventas, donde cada venta contiene el nombre del producto, precio y cantidad vendida
            List<(string nombre, double precio, int cantidad)> ventas = new List<(string, double, int)>();

            // Variables para el menú y la gestión de productos y ventas
            int opcion = 0;
            string nProducto = "";
            double pProducto = 0;
            int cProducto = 0;
            int idProdVenta = 0;
            int cantProdVenta = 0;
            bool continuar;
            double subTotVenta;
            double tVenta = 0;            
            double mIva;
            double promVenta = 0;
            string nombreProdMayorVenta;
            // Variable para el porcentaje de IVA
            double Iva = 0.19;

            // Variables auxiliares
            bool aux;
            int aux2;
            string aux3;

            // Ciclo principal del programa
            while (opcion != 5)
            {
                // Mostrar el menú principal
                ImprimirEncabezado();

                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    // Validar que la opción ingresada esté dentro del rango permitido
                    switch (opcion)
                    {
                        case 1:
                            continuar = true;
                            Console.Clear();
                            Console.WriteLine("REGISTRAR NUEVO PRODUCTO EN INVENTARIO");

                            // Ciclo para registrar el nombre del producto
                            while (continuar)
                            {
                                Console.Clear();
                                Console.Write("Ingrese el nombre del producto: ");
                                nProducto = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(nProducto))
                                {
                                    Console.WriteLine("Error al registrar el nombre del producto.");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    aux = false;

                                    for (int i = 0; i < productos.Count; i++)
                                    {
                                        if (string.Equals(productos[i].nombre, nProducto, StringComparison.OrdinalIgnoreCase))
                                        {
                                            aux = true;
                                            i = productos.Count;
                                        }
                                    }

                                    if (aux)
                                    {
                                        Console.WriteLine("El producto ya existe en el inventario.");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        continuar = false;
                                    }
                                }
                            }

                            // Ciclo para registrar el precio del producto
                            continuar = true;
                            while (continuar)
                            {
                                pProducto = LeerDecimal("Ingrese el precio del producto: $", 0);
                                continuar = false;
                            }

                            // Ciclo para registrar la cantidad del producto
                            continuar = true;
                            while (continuar)
                            {
                                cProducto = LeerEntero("Ingrese la cantidad del producto: ", 1);
                                continuar = false;
                            }

                            // Agregar el producto a la lista de productos
                            productos.Add((nProducto, pProducto, cProducto));

                            Console.Clear();
                            Console.WriteLine("Producto '" + nProducto + "' registrado exitosamente.");
                            Console.ReadKey();
                            break;

                        case 2:
                            Console.Clear();
                            Console.WriteLine("CONSULTAR INVENTARIO COMPLETO");
                            Console.WriteLine();

                            if (productos.Count == 0)
                            {
                                // No hay productos registrados en el inventario
                                Console.WriteLine("No hay productos registrados en el inventario.");
                            }
                            else
                            {
                                // Mostrar la lista de productos registrados
                                Console.WriteLine("Productos en inventario:");
                                Console.WriteLine();
                                Console.WriteLine("ID   Nombre          Precio          Cantidad");
                                for (int i = 0; i < productos.Count; i++)
                                {
                                    Console.WriteLine((i + 1) + ".      (" + productos[i].nombre + " - " + productos[i].precio + "$ - " + productos[i].cantidad + " unidades)");
                                    Console.WriteLine();
                                }

                                // Revisar si hay productos con cantidad menor a 5
                                aux2 = 0;
                                for (int i = 0; i < productos.Count; i++)
                                {
                                    if (productos[i].cantidad < 5)
                                    {
                                        i = productos.Count;
                                        aux2 = 1;
                                    }
                                }

                                if (aux2 == 1)
                                {
                                    // Mostrar los productos con bajo stock
                                    Console.WriteLine("Productos con bajo stock (menos de 5 unidades):");
                                    Console.WriteLine();
                                    Console.WriteLine("[ALERTA: BAJO STOCK]");
                                    Console.WriteLine();
                                    Console.WriteLine();
                                    Console.WriteLine("ID   Nombre          Precio          Cantidad");
                                    Console.WriteLine();
                                    for (int i = 0; i < productos.Count; i++)
                                    {
                                        if (productos[i].cantidad < 5)
                                        {
                                            Console.WriteLine(aux2 + ".      (" + productos[i].nombre + " - " + productos[i].precio + "$ - " + productos[i].cantidad + " unidades)");
                                            Console.WriteLine();
                                            aux2++;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No hay productos con bajo stock.");
                                    Console.ReadKey();
                                }
                            }
                            Console.ReadKey();
                            break;

                        case 3:
                            Console.Clear();
                            Console.WriteLine("REGISTRAR UNA VENTA");
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("Productos en inventario:");
                            Console.WriteLine();

                            // Validar si hay productos registrados en el inventario
                            if (productos.Count == 0)
                            {
                                Console.WriteLine("No hay productos registrados en el inventario.");
                                Console.ReadKey();
                            }
                            else
                            {
                                // Mostrar la lista de productos registrados
                                Console.WriteLine("ID   Nombre          Precio          Cantidad");
                                Console.WriteLine();

                                for (int i = 0; i < productos.Count; i++)
                                {
                                    Console.WriteLine((i + 1) + ".      (" + productos[i].nombre + " - " + productos[i].precio + "$ - " + productos[i].cantidad + " unidades)");
                                    Console.WriteLine();
                                }

                                // Solicitar el ID del producto a vender
                                Console.WriteLine();
                                aux = true;

                                // Ciclo para validar el ID del producto a vender
                                while (aux)
                                {
                                    Console.Clear();
                                    idProdVenta = LeerEntero("Ingrese el ID del producto a vender: ", 1, productos.Count);
                                    aux = false;

                                    // Validar que la cantidad a vender no sea mayor a la cantidad disponible en el inventario
                                    while (!aux)
                                    {
                                        Console.WriteLine();
                                        cantProdVenta = LeerEntero("Ingrese la cantidad de productos de '" + productos[idProdVenta - 1].nombre + "' a vender: ", 1, productos[idProdVenta - 1].cantidad);

                                        // Actualizar la cantidad del producto en el inventario
                                        productos[idProdVenta - 1] = (productos[idProdVenta - 1].nombre, productos[idProdVenta - 1].precio, productos[idProdVenta - 1].cantidad - cantProdVenta);
                                        Console.WriteLine("Venta registrada exitosamente.");
                                        Console.ReadKey();
                                        aux = true;
                                    }
                                    aux = false;
                                }

                                Console.Clear();

                                // Preguntar si el cliente aplica para descuento
                                aux = true;
                                while (aux)
                                {
                                    Console.WriteLine();
                                    Console.Write("¿El cliente aplica para 'Descuento de Cliente Frecuente' (S/N)? ");
                                    aux3 = Console.ReadLine().ToUpper();
                                    if (aux3 == "S")
                                    {
                                        (subTotVenta, mIva, tVenta) = CalcularFactura(productos[idProdVenta - 1].precio, cantProdVenta, Iva, true);
                                        aux = false;
                                    }
                                    else if (aux3 == "N")
                                    {
                                        (subTotVenta, mIva, tVenta) = CalcularFactura(productos[idProdVenta - 1].precio, cantProdVenta, Iva, false);
                                        aux = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Ingrese una opción válida (S/N).");
                                        Console.ReadKey();
                                    }
                                }

                                // Agregar la venta a la lista de ventas
                                ventas.Add((productos[idProdVenta - 1].nombre,tVenta,cantProdVenta));

                                // Imprimir el recibo de la venta
                                Console.Clear();
                                Console.WriteLine("RECIBO DE LA VENTA");
                                Console.WriteLine();
                                Console.WriteLine("Producto: " +productos[idProdVenta - 1].nombre);
                                Console.WriteLine("Cantidad: " + cantProdVenta);
                                Console.WriteLine("Total: " + tVenta);
                                Console.ReadKey();
                            }
                            break;

                        case 4:
                            Console.Clear();
                            Console.WriteLine("REPORTE DE CAJA Y ESTADÍSTICAS DIARIAS");
                            Console.WriteLine();

                            if (ventas.Count == 0)
                            {
                                Console.WriteLine("No se han registrado ventas.");
                                Console.ReadKey();
                            }
                            else
                            {
                                // Mostrar el reporte de caja y estadísticas
                                Console.WriteLine("Ventas registradas:");
                                Console.WriteLine();
                                Console.WriteLine("ID   Nombre          Precio          Cantidad");
                                for (int i = 0; i < ventas.Count; i++)
                                {
                                    Console.WriteLine((i + 1) + ".      (" +ventas[i].nombre + " - " +ventas[i].precio + "$ - " +ventas[i].cantidad +" unidades)");
                                    Console.WriteLine();
                                }

                                // Calcular el total de ventas de la sesión
                                tVenta = 0;
                                for (int i = 0; i < ventas.Count; i++)
                                {
                                    tVenta += ventas[i].precio;
                                }

                                // Calcular el promedio de dinero por venta
                                promVenta = 0;
                                promVenta = tVenta / ventas.Count;

                                // Calcular el producto con mayor cantidad de unidades vendidas
                                nombreProdMayorVenta = "";
                                aux2 = 0;

                                for (int i = 0; i < ventas.Count; i++)
                                {
                                    if (ventas[i].cantidad > aux2)
                                    {
                                        aux2 = ventas[i].cantidad;
                                        nombreProdMayorVenta = ventas[i].nombre;
                                    }
                                }

                                // Mostrar el total de ventas de la sesión
                                Console.WriteLine("Total de ventas de la sesión: " + tVenta + "$");
                                Console.WriteLine("Promedio de dinero por venta: " + promVenta + "$");
                                Console.WriteLine("Producto con mayor cantidad de unidades vendidas: " + nombreProdMayorVenta);
                                Console.ReadKey();
                            }
                            break;


                        case 5:
                            Console.Clear();
                            Console.WriteLine("Saliendo del sistema...");
                            Console.ReadKey();
                            break;

                        default:
                            Console.WriteLine("Ingrese una opción válida (1-5).");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ingrese una opción válida (1-5).");
                    Console.ReadKey();
                }
            }
        }


        // MÉTODO 1: LEER ENTERO
        static int LeerEntero(string msj, int min, int max = int.MaxValue)
        {
            int valor;

            while (true)
            {
                Console.Write(msj);
                if (int.TryParse(Console.ReadLine(), out valor) && valor >= min && valor <= max)
                {
                    return valor;
                }
                Console.WriteLine("Ingrese un valor entero válido.");
                Console.ReadKey();
            }
        }

        // MÉTODO 2: LEER DECIMAL
        static double LeerDecimal(string msj, double min)
        {
            double valor;
            while (true)
            {
                Console.Write(msj);
                if (double.TryParse(Console.ReadLine(), out valor) && valor >= min)
                {
                    return valor;
                }
                Console.WriteLine("Ingrese un precio válido.");
                Console.ReadKey();
            }
        }


        // MÉTODO 3: CALCULAR FACTURA
        static (double subtotal, double iva, double total) CalcularFactura(double precio,int cantidad,double porcentajeIva,bool descuento)
        {
            double subtotal = precio * cantidad;
            if (descuento)
            {
                subtotal = subtotal * 0.9;
            }
            double iva = subtotal * porcentajeIva;
            double total = subtotal + iva;

            return (subtotal, iva, total);
        }

        // MÉTODO 4: IMPRIMIR ENCABEZADO
        static void ImprimirEncabezado()
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("  SISTEMA GESTOR DE VENTAS E INVENTARIO (MINI-POS)");
            Console.WriteLine("====================================================");
            Console.WriteLine();
            Console.WriteLine("1. Registrar nuevo producto en inventario.");
            Console.WriteLine("2. Consultar inventario completo.");
            Console.WriteLine("3. Registrar una venta.");
            Console.WriteLine("4. Ver reporte de caja y estadisticas diarias.");
            Console.WriteLine("5. Salir.");
            Console.WriteLine();
            Console.WriteLine("====================================================");
            Console.Write("Seleccione una opción (1-5): ");
        }
    }
}