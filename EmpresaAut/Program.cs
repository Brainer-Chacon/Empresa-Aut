using System;  
using System.Collections.Generic; 

namespace EmpresaAut
{
    // ----------- CLASES -----------
    class Empleado
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } 
        public string Puesto { get; set; } 
        public double Salario { get; set; } 

        public Empleado(int id, string nombre, string puesto, double salario)
        {
            Id = id; Nombre = nombre; Puesto = puesto; Salario = salario;
        }

        public override string ToString() => $"ID: {Id}, Nombre: {Nombre}, Puesto: {Puesto}, Salario: {Salario:C}";
    }

    class Cliente
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } 
        public string Cedula { get; set; } 
        public string Telefono { get; set; } 
        public string TipoCliente { get; set; } 

        public Cliente(int id, string nombre, string cedula, string telefono, string tipoCliente)
        {
            Id = id; Nombre = nombre; Cedula = cedula; Telefono = telefono; TipoCliente = tipoCliente;
        }

        public override string ToString() => $"ID: {Id}, Nombre: {Nombre}, Cédula: {Cedula}, Teléfono: {Telefono}, Tipo: {TipoCliente}";
    }

    class Vehiculo
    {
        public int Id { get; set; } 
        public string Marca { get; set; } 
        public string Modelo { get; set; } 
        public double Precio { get; set; } 

        public Vehiculo(int id, string marca, string modelo, double precio)
        {
            Id = id; Marca = marca; Modelo = modelo; Precio = precio;
        }

        public override string ToString() => $"ID: {Id}, Marca: {Marca}, Modelo: {Modelo}, Precio: {Precio:C}";
    }

    // ----------- PROGRAMA PRINCIPAL -----------
    class Program
    {
        static List<Empleado> empleados = new();
        static List<Cliente> clientes = new();
        static List<Vehiculo> vehiculos = new();

        static int contadorEmpleado = 1, contadorCliente = 1, contadorVehiculo = 1;

        static void Main()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n=== Bienvenido a EmpresaAut ===");
                Console.ResetColor();

                Console.WriteLine("Seleccione su rol: (1) Empleado  (2) Cliente  (0) Salir");
                string rol = Console.ReadLine();

                if (rol == "1") MenuEmpleado();
                else if (rol == "2") MenuCliente();
                else if (rol == "0") { Console.WriteLine("👋 Gracias por usar EmpresaAut. ¡Hasta luego!"); break; }
                else Console.WriteLine("❌ Opción inválida.");
            }
        }

        // ----------- MENÚ EMPLEADO -----------
        static void MenuEmpleado()
        {
            int opcion;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n=== MENÚ EMPLEADO ===");
                Console.ResetColor();

                Console.WriteLine("1. Agregar empleado  2. Listar empleados  3. Buscar empleado");
                Console.WriteLine("4. Agregar cliente   5. Listar clientes   6. Buscar cliente");
                Console.WriteLine("7. Agregar vehículo  8. Listar vehículos  0. Volver");
                Console.Write("Seleccione opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion)) { Console.WriteLine("❌ Opción inválida."); continue; }

                switch (opcion)
                {
                    case 1: AgregarEmpleado(); break;
                    case 2: ListarEmpleados(); break;
                    case 3: BuscarEmpleado(); break;
                    case 4: AgregarCliente(); break;
                    case 5: ListarClientes(); break;
                    case 6: BuscarCliente(); break;
                    case 7: AgregarVehiculo(); break;
                    case 8: ListarVehiculos(); break;
                    case 0: return;
                    default: Console.WriteLine("❌ Opción no válida."); break;
                }
            } while (opcion != 0);
        }

        // ----------- MENÚ CLIENTE -----------
        static void MenuCliente()
        {
            int opcion;
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n=== MENÚ CLIENTE ===");
                Console.ResetColor();

                Console.WriteLine("1. Registrarse  2. Listar vehículos  3. Comprar vehículo  0. Volver");
                Console.Write("Seleccione opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion)) { Console.WriteLine("❌ Opción inválida."); continue; }

                switch (opcion)
                {
                    case 1: AgregarCliente(); break;
                    case 2: ListarVehiculos(); break;
                    case 3: ComprarVehiculo(); break;
                    case 0: return;
                    default: Console.WriteLine("❌ Opción no válida."); break;
                }
            } while (opcion != 0);
        }

        // ----------- MÉTODOS -----------

        static void AgregarEmpleado()
        {
            Console.Write("Nombre: "); string nombre = Console.ReadLine();
            Console.Write("Puesto: "); string puesto = Console.ReadLine();
            double salario;
            while (true) { Console.Write("Salario: "); if (double.TryParse(Console.ReadLine(), out salario)) break; Console.WriteLine("❌ Salario inválido."); }
            empleados.Add(new Empleado(contadorEmpleado++, nombre, puesto, salario));
            Console.WriteLine("✅ Empleado agregado.");
        }

        static void ListarEmpleados()
        {
            Console.WriteLine("\n=== Empleados ===");
            if (empleados.Count == 0) { Console.WriteLine("No hay empleados."); return; }
            empleados.ForEach(e => Console.WriteLine(e));
        }

        static void BuscarEmpleado()
        {
            Console.Write("ID empleado: "); if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("❌ ID inválido."); return; }
            var emp = empleados.Find(e => e.Id == id);
            Console.WriteLine(emp != null ? emp.ToString() : "⚠️ No encontrado.");
        }

        static void AgregarCliente()
        {
            Console.Write("Nombre: "); string nombre = Console.ReadLine();
            Console.Write("Cédula: "); string cedula = Console.ReadLine();
            Console.Write("Teléfono: "); string telefono = Console.ReadLine();
            Console.Write("Tipo (Regular/VIP): "); string tipo = Console.ReadLine();
            clientes.Add(new Cliente(contadorCliente++, nombre, cedula, telefono, tipo));
            Console.WriteLine("✅ Cliente agregado.");
        }

        static void ListarClientes()
        {
            Console.WriteLine("\n=== Clientes ===");
            if (clientes.Count == 0) { Console.WriteLine("No hay clientes."); return; }
            clientes.ForEach(c => Console.WriteLine(c));
        }

        static void BuscarCliente()
        {
            Console.Write("ID cliente: "); if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("❌ ID inválido."); return; }
            var cli = clientes.Find(c => c.Id == id);
            Console.WriteLine(cli != null ? cli.ToString() : "⚠️ No encontrado.");
        }

        static void AgregarVehiculo()
        {
            Console.Write("Marca: "); string marca = Console.ReadLine();
            Console.Write("Modelo: "); string modelo = Console.ReadLine();
            double precio;
            while (true) { Console.Write("Precio: "); if (double.TryParse(Console.ReadLine(), out precio)) break; Console.WriteLine("❌ Precio inválido."); }
            vehiculos.Add(new Vehiculo(contadorVehiculo++, marca, modelo, precio));
            Console.WriteLine("✅ Vehículo agregado.");
        }

        static void ListarVehiculos()
        {
            Console.WriteLine("\n=== Vehículos ===");
            if (vehiculos.Count == 0) { Console.WriteLine("No hay vehículos."); return; }
            vehiculos.ForEach(v => Console.WriteLine(v));
        }

        static void ComprarVehiculo()
        {
            if (clientes.Count == 0) { Console.WriteLine("⚠️ Regístrese primero."); return; }
            if (vehiculos.Count == 0) { Console.WriteLine("⚠️ No hay vehículos."); return; }

            ListarVehiculos();
            Console.Write("ID vehículo: "); if (!int.TryParse(Console.ReadLine(), out int idVeh)) { Console.WriteLine("❌ ID inválido."); return; }
            var veh = vehiculos.Find(v => v.Id == idVeh); if (veh == null) { Console.WriteLine("⚠️ No encontrado."); return; }

            Console.Write("ID cliente: "); if (!int.TryParse(Console.ReadLine(), out int idCli)) { Console.WriteLine("❌ ID inválido."); return; }
            var cli = clientes.Find(c => c.Id == idCli); if (cli == null) { Console.WriteLine("⚠️ No encontrado."); return; }

            double descuento = cli.TipoCliente.ToLower() == "vip" ? 0.2 : 0.1;
            double precioFinal = veh.Precio * (1 - descuento);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ Compra exitosa para {cli.Nombre}!\nPrecio original: {veh.Precio:C}\nDescuento: {descuento * 100}%\nPrecio final: {precioFinal:C}");
            Console.ResetColor();

            vehiculos.Remove(veh);
        }
    }
}