using System;
using System.Collections.Generic;

namespace EmpresaAut
{
    // ================= CLASE BASE =================
    // Todas las entidades tendrán un Id, de aquí heredan todas las demás clases
    class EntidadBase
    {
        public int Id { get; set; }

        // Constructor para inicializar el Id
        public EntidadBase(int id) => Id = id;

        // Método virtual que se puede sobreescribir en las clases hijas
        public virtual string Mostrar() => $"ID: {Id}";
    }

    // ================= CLASE PERSONA =================
    // Agrupa propiedades comunes de personas: nombre y cédula
    class Persona : EntidadBase
    {
        public string Nombre { get; set; }
        public string Cedula { get; set; }

        // Constructor que llama al constructor de EntidadBase
        public Persona(int id, string nombre, string cedula) : base(id)
        {
            Nombre = nombre;
            Cedula = cedula;
        }

        // Sobrescribimos Mostrar para incluir nombre y cédula
        public override string Mostrar() => $"{base.Mostrar()}, Nombre: {Nombre}, Cédula: {Cedula}";
    }

    // ================= CLASE EMPLEADO =================
    // Hereda de Persona, añade Puesto y Salario
    class Empleado : Persona
    {
        public string Puesto { get; set; }
        public double Salario { get; set; }

        public Empleado(int id, string nombre, string cedula, string puesto, double salario)
            : base(id, nombre, cedula)
        {
            Puesto = puesto;
            Salario = salario;
        }

        // Sobrescribimos Mostrar para incluir puesto y salario
        public override string Mostrar() => $"{base.Mostrar()}, Puesto: {Puesto}, Salario: {Salario:C}";
    }

    // ================= CLASE CLIENTE =================
    // Hereda de Persona, añade Teléfono y TipoCliente
    class Cliente : Persona
    {
        public string Telefono { get; set; }
        public string TipoCliente { get; set; }

        public Cliente(int id, string nombre, string cedula, string telefono, string tipoCliente)
            : base(id, nombre, cedula)
        {
            Telefono = telefono;
            TipoCliente = tipoCliente;
        }

        // Sobrescribimos Mostrar para incluir teléfono y tipo de cliente
        public override string Mostrar() => $"{base.Mostrar()}, Teléfono: {Telefono}, Tipo: {TipoCliente}";
    }

    // ================= CLASE VEHÍCULO =================
    // Hereda directamente de EntidadBase porque no es persona
    class Vehiculo : EntidadBase
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public double Precio { get; set; }

        public Vehiculo(int id, string marca, string modelo, double precio)
            : base(id)
        {
            Marca = marca;
            Modelo = modelo;
            Precio = precio;
        }

        // Sobrescribimos Mostrar para incluir marca, modelo y precio
        public override string Mostrar() => $"{base.Mostrar()}, Marca: {Marca}, Modelo: {Modelo}, Precio: {Precio:C}";
    }

    // ================= PROGRAMA PRINCIPAL =================
    class Program
    {
        // Listas para almacenar los objetos creados
        static List<Empleado> empleados = new();
        static List<Cliente> clientes = new();
        static List<Vehiculo> vehiculos = new();

        // Contadores para asignar Id automáticamente
        static int contadorEmpleado = 1, contadorCliente = 1, contadorVehiculo = 1;

        static void Main()
        {
            while (true)
            {
                // Menú principal
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

        // ================= MENÚ EMPLEADO =================
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

        // ================= MENÚ CLIENTE =================
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

        // ================= MÉTODOS =================
        // Agregar un nuevo empleado
        static void AgregarEmpleado()
        {
            Console.Write("Nombre: "); string nombre = Console.ReadLine();
            Console.Write("Cédula: "); string cedula = Console.ReadLine();
            Console.Write("Puesto: "); string puesto = Console.ReadLine();
            double salario;
            while (true) 
            { 
                Console.Write("Salario: "); 
                if (double.TryParse(Console.ReadLine(), out salario)) break; 
                Console.WriteLine("❌ Salario inválido."); 
            }
            empleados.Add(new Empleado(contadorEmpleado++, nombre, cedula, puesto, salario));
            Console.WriteLine("✅ Empleado agregado.");
        }

        // Listar todos los empleados
        static void ListarEmpleados() { empleados.ForEach(e => Console.WriteLine(e.Mostrar())); }

        // Buscar empleado por Id
        static void BuscarEmpleado()
        {
            Console.Write("ID empleado: "); 
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("❌ ID inválido."); return; }
            var emp = empleados.Find(e => e.Id == id);
            Console.WriteLine(emp != null ? emp.Mostrar() : "⚠️ No encontrado.");
        }

        // Agregar un nuevo cliente
        static void AgregarCliente()
        {
            Console.Write("Nombre: "); string nombre = Console.ReadLine();
            Console.Write("Cédula: "); string cedula = Console.ReadLine();
            Console.Write("Teléfono: "); string telefono = Console.ReadLine();
            Console.Write("Tipo (Regular/VIP): "); string tipo = Console.ReadLine();
            clientes.Add(new Cliente(contadorCliente++, nombre, cedula, telefono, tipo));
            Console.WriteLine("✅ Cliente agregado.");
        }

        // Listar todos los clientes
        static void ListarClientes() { clientes.ForEach(c => Console.WriteLine(c.Mostrar())); }

        // Buscar cliente por Id
        static void BuscarCliente()
        {
            Console.Write("ID cliente: "); 
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("❌ ID inválido."); return; }
            var cli = clientes.Find(c => c.Id == id);
            Console.WriteLine(cli != null ? cli.Mostrar() : "⚠️ No encontrado.");
        }

        // Agregar un nuevo vehículo
        static void AgregarVehiculo()
        {
            Console.Write("Marca: "); string marca = Console.ReadLine();
            Console.Write("Modelo: "); string modelo = Console.ReadLine();
            double precio;
            while (true) 
            { 
                Console.Write("Precio: "); 
                if (double.TryParse(Console.ReadLine(), out precio)) break; 
                Console.WriteLine("❌ Precio inválido."); 
            }
            vehiculos.Add(new Vehiculo(contadorVehiculo++, marca, modelo, precio));
            Console.WriteLine("✅ Vehículo agregado.");
        }

        // Listar todos los vehículos
        static void ListarVehiculos() { vehiculos.ForEach(v => Console.WriteLine(v.Mostrar())); }

        // Comprar un vehículo
        static void ComprarVehiculo()
        {
            if (clientes.Count == 0) { Console.WriteLine("⚠️ Regístrese primero."); return; }
            if (vehiculos.Count == 0) { Console.WriteLine("⚠️ No hay vehículos."); return; }

            ListarVehiculos();
            Console.Write("ID vehículo: "); 
            if (!int.TryParse(Console.ReadLine(), out int idVeh)) { Console.WriteLine("❌ ID inválido."); return; }
            var veh = vehiculos.Find(v => v.Id == idVeh); 
            if (veh == null) { Console.WriteLine("⚠️ No encontrado."); return; }

            Console.Write("ID cliente: "); 
            if (!int.TryParse(Console.ReadLine(), out int idCli)) { Console.WriteLine("❌ ID inválido."); return; }
            var cli = clientes.Find(c => c.Id == idCli); 
            if (cli == null) { Console.WriteLine("⚠️ No encontrado."); return; }

            // Descuento según tipo de cliente
            double descuento = cli.TipoCliente.ToLower() == "vip" ? 0.2 : 0.1;
            double precioFinal = veh.Precio * (1 - descuento);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ Compra exitosa para {cli.Nombre}!\nPrecio original: {veh.Precio:C}\nDescuento: {descuento * 100}%\nPrecio final: {precioFinal:C}");
            Console.ResetColor();

            vehiculos.Remove(veh); // Eliminar vehículo vendido
        }
    }
}