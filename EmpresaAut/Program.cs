using System;
using System.Collections.Generic;

namespace EmpresaAut
{
    // ================= CLASE ABSTRACTA (ABSTRACCIÓN) =================
    // La abstracción nos permite definir comportamientos generales
    // que obligan a las clases hijas a implementarlos.
    // "EntidadBase" será la clase padre de todas las entidades del sistema.
    abstract class EntidadBase
    {
        // Propiedad común a todas las entidades: un identificador único
        public int Id { get; set; }

        // Constructor que inicializa el ID de la entidad
        public EntidadBase(int id) => Id = id;

        // MÉTODO ABSTRACTO:
        // Define una "firma" que las clases hijas deben implementar obligatoriamente.
        public abstract string Mostrar();

        // MÉTODO VIRTUAL:
        // Permite que las clases hijas lo reescriban (Polimorfismo).
        public virtual string Saludar() => "Hola, soy una entidad de la empresa.";
    }

    // ================= CLASE PERSONA =================
    // Ejemplo de herencia: Persona hereda de EntidadBase.
    class Persona : EntidadBase
    {
        public string Nombre { get; set; }
        public string Cedula { get; set; }

        // Constructor que recibe parámetros y usa el constructor de la clase padre (base).
        public Persona(int id, string nombre, string cedula) : base(id)
        {
            Nombre = nombre;
            Cedula = cedula;
        }

        // Implementación obligatoria del método abstracto Mostrar().
        public override string Mostrar() => $"ID: {Id}, Nombre: {Nombre}, Cédula: {Cedula}";

        // Sobrescritura del método virtual Saludar().
        public override string Saludar() => $"👤 Hola, soy {Nombre}, una persona en la empresa.";
    }

    // ================= CLASE EMPLEADO =================
    // Herencia en 3 niveles: EntidadBase -> Persona -> Empleado
    class Empleado : Persona
    {
        // Ejemplo de ENCAPSULAMIENTO:
        // Usamos un campo privado para controlar el acceso al salario.
        private double salario;

        public string Puesto { get; set; }
        
        // Propiedad con validación:
        // Controla que el salario no sea negativo.
        public double Salario
        {
            get => salario;
            set
            {
                if (value < 0) throw new ArgumentException("El salario no puede ser negativo.");
                salario = value;
            }
        }

        // Constructor que inicializa un empleado
        public Empleado(int id, string nombre, string cedula, string puesto, double salario)
            : base(id, nombre, cedula)
        {
            Puesto = puesto;
            Salario = salario;
        }

        // Mostrar información detallada del empleado
        public override string Mostrar() =>
            $"{base.Mostrar()}, Puesto: {Puesto}, Salario: {Salario:C}";

        // Polimorfismo: redefinimos el saludo específico para empleados
        public override string Saludar() => $"💼 Hola, soy {Nombre} y trabajo como {Puesto}.";
    }

    // ================= CLASE CLIENTE =================
    class Cliente : Persona
    {
        public string Telefono { get; set; }
        public string TipoCliente { get; set; } // Ej: "VIP" o "Regular"

        public Cliente(int id, string nombre, string cedula, string telefono, string tipoCliente)
            : base(id, nombre, cedula)
        {
            Telefono = telefono;
            TipoCliente = tipoCliente;
        }

        public override string Mostrar() =>
            $"{base.Mostrar()}, Teléfono: {Telefono}, Tipo: {TipoCliente}";

        public override string Saludar() => $"🙋 Hola, soy {Nombre}, cliente {TipoCliente}.";
    }

    // ================= CLASE VEHÍCULO =================
    class Vehiculo : EntidadBase
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public double Precio { get; set; }

        public Vehiculo(int id, string marca, string modelo, double precio) : base(id)
        {
            Marca = marca;
            Modelo = modelo;
            Precio = precio;
        }

        public override string Mostrar() =>
            $"ID: {Id}, Marca: {Marca}, Modelo: {Modelo}, Precio: {Precio:C}";

        public override string Saludar() =>
            $"🚗 Soy un {Marca} {Modelo} y estoy en venta.";
    }

    // ================= CLASE VENTA =================
    // Ejemplo de composición: una venta está compuesta por un cliente y un vehículo.
    class Venta : EntidadBase
    {
        public Cliente Comprador { get; set; }
        public Vehiculo VehiculoVendido { get; set; }
        public double PrecioFinal { get; set; }

        public Venta(int id, Cliente comprador, Vehiculo vehiculo, double precioFinal)
            : base(id)
        {
            Comprador = comprador;
            VehiculoVendido = vehiculo;
            PrecioFinal = precioFinal;
        }

        public override string Mostrar() =>
            $"Venta ID: {Id}, Cliente: {Comprador.Nombre}, Vehículo: {VehiculoVendido.Marca} {VehiculoVendido.Modelo}, Precio Final: {PrecioFinal:C}";
    }

    // ================= PROGRAMA PRINCIPAL =================
    // Aquí se ejecuta la lógica del programa, con menús interactivos.
    class Program
    {
        // Listas para almacenar entidades en memoria (simulación de base de datos).
        static List<Empleado> empleados = new();
        static List<Cliente> clientes = new();
        static List<Vehiculo> vehiculos = new();
        static List<Venta> ventas = new();

        // Contadores para asignar IDs únicos
        static int contadorEmpleado = 1, contadorCliente = 1, contadorVehiculo = 1, contadorVenta = 1;

        static void Main()
        {
            // Bucle principal del programa
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n=== Bienvenido a EmpresaAut ===");
                Console.ResetColor();

                Console.WriteLine("Seleccione su rol: (1) Empleado  (2) Cliente  (0) Salir");
                string rol = Console.ReadLine();

                if (rol == "1") MenuEmpleado();
                else if (rol == "2") MenuCliente();
                else if (rol == "0")
                {
                    Console.WriteLine("👋 Gracias por usar EmpresaAut. ¡Hasta luego!");
                    break;
                }
                else Console.WriteLine("❌ Opción inválida.");
            }
        }

        // ================= MENÚ EMPLEADO =================
        // Aquí se manejan las operaciones que puede hacer un empleado
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
                Console.WriteLine("7. Agregar vehículo  8. Listar vehículos  9. Ver ventas  0. Volver");
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
                    case 9: ListarVentas(); break;
                    case 0: return;
                    default: Console.WriteLine("❌ Opción no válida."); break;
                }
            } while (opcion != 0);
        }

        // ================= MENÚ CLIENTE =================
        // Opciones que puede realizar un cliente en el sistema
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

        // ================= MÉTODOS EMPLEADO =================
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

        static void ListarEmpleados() { empleados.ForEach(e => Console.WriteLine(e.Mostrar())); }

        static void BuscarEmpleado()
        {
            Console.Write("ID empleado: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("❌ ID inválido."); return; }
            var emp = empleados.Find(e => e.Id == id);
            Console.WriteLine(emp != null ? emp.Mostrar() : "⚠️ No encontrado.");
        }

        // ================= MÉTODOS CLIENTE =================
        static void AgregarCliente()
        {
            Console.Write("Nombre: "); string nombre = Console.ReadLine();
            Console.Write("Cédula: "); string cedula = Console.ReadLine();
            Console.Write("Teléfono: "); string telefono = Console.ReadLine();
            Console.Write("Tipo (Regular/VIP): "); string tipo = Console.ReadLine();
            clientes.Add(new Cliente(contadorCliente++, nombre, cedula, telefono, tipo));
            Console.WriteLine("✅ Cliente agregado.");
        }

        static void ListarClientes() { clientes.ForEach(c => Console.WriteLine(c.Mostrar())); }

        static void BuscarCliente()
        {
            Console.Write("ID cliente: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("❌ ID inválido."); return; }
            var cli = clientes.Find(c => c.Id == id);
            Console.WriteLine(cli != null ? cli.Mostrar() : "⚠️ No encontrado.");
        }

        // ================= MÉTODOS VEHÍCULO =================
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

        static void ListarVehiculos() { vehiculos.ForEach(v => Console.WriteLine(v.Mostrar())); }

        // ================= MÉTODOS VENTA =================
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

            // Aplicación de descuento según tipo de cliente
            double descuento = cli.TipoCliente.ToLower() == "vip" ? 0.2 : 0.1;
            double precioFinal = veh.Precio * (1 - descuento);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ Compra exitosa para {cli.Nombre}!\nPrecio original: {veh.Precio:C}\nDescuento: {descuento * 100}%\nPrecio final: {precioFinal:C}");
            Console.ResetColor();

            // Registrar venta y eliminar el vehículo vendido
            ventas.Add(new Venta(contadorVenta++, cli, veh, precioFinal));
            vehiculos.Remove(veh);
        }

        static void ListarVentas() { ventas.ForEach(v => Console.WriteLine(v.Mostrar())); }
    }
}