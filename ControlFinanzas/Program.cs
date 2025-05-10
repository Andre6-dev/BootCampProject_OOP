using ControlFinanzas.Modelos;
using ControlFinanzas.UI;

namespace ControlFinanzas;

class Program
{
    class GestorFinanzas
    {
        private List<Transaccion> transacciones;
        private List<Cuenta> cuentas;
        private List<Categoria> categorias;
        private List<Presupuesto> presupuestos;

        public GestorFinanzas()
        {
            // Cargar datos desde archivos si existen
            if (File.Exists("transacciones.json"))
            {
                string jsonTransacciones = File.ReadAllText("transacciones.json");
                transacciones = JsonConvert.DeserializeObject<List<Transaccion>>(jsonTransacciones);
            }

            if (File.Exists("cuentas.json"))
            {
                string jsonCuentas = File.ReadAllText("cuentas.json");
                cuentas = JsonConvert.DeserializeObject<List<Cuenta>>(jsonCuentas);
            }

            if (File.Exists("categorias.json"))
            {
                string jsonCategorias = File.ReadAllText("categorias.json");
                categorias = JsonConvert.DeserializeObject<List<Categoria>>(jsonCategorias);
            }

            if (File.Exists("presupuestos.json"))
            {
                string jsonPresupuestos = File.ReadAllText("presupuestos.json");
                presupuestos = JsonConvert.DeserializeObject<List<Presupuesto>>(jsonPresupuestos);
            }

            // Si no hay categorías, crear algunas por defecto
            if (categorias.Count == 0)
            {
                categorias.Add(new Categoria { Nombre = "Salario", TipoAsociado = TipoTransaccion.Ingreso });
                categorias.Add(new Categoria { Nombre = "Alquiler", TipoAsociado = TipoTransaccion.Gasto });
                categorias.Add(new Categoria { Nombre = "Comida", TipoAsociado = TipoTransaccion.Gasto });
                categorias.Add(new Categoria { Nombre = "Transporte", TipoAsociado = TipoTransaccion.Gasto });
                categorias.Add(new Categoria { Nombre = "Entretenimiento", TipoAsociado = TipoTransaccion.Gasto });
                categorias.Add(new Categoria { Nombre = "Servicios", TipoAsociado = TipoTransaccion.Gasto });
                GuardarCategorias();
            }
        }

        public void GuardarTransacciones()
        {
            string json = JsonConvert.SerializeObject(transacciones);
            File.WriteAllText("transacciones.json", json);
        }

        public void GuardarCuentas()
        {
            string json = JsonConvert.SerializeObject(cuentas);
            File.WriteAllText("cuentas.json", json);
        }

        public void GuardarCategorias()
        {
            string json = JsonConvert.SerializeObject(categorias);
            File.WriteAllText("categorias.json", json);
        }

        public void GuardarPresupuestos()
        {
            string json = JsonConvert.SerializeObject(presupuestos);
            File.WriteAllText("presupuestos.json", json);
        }

        // PUNTO DE REFACTORIZACIÓN 5: Este método es muy largo y hace demasiadas cosas
        public void AgregarTransaccion()
        {
            Console.Clear();
            Console.WriteLine("=== AGREGAR NUEVA TRANSACCIÓN ===\n");

            if (cuentas.Count == 0)
            {
                Console.WriteLine("No hay cuentas registradas. Primero debe crear una cuenta.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            // Solicitar tipo de transacción
            Console.WriteLine("Tipo de transacción:");
            Console.WriteLine("1. Ingreso");
            Console.WriteLine("2. Gasto");
            Console.Write("Seleccione una opción: ");
            int opcionTipo;
            while (!int.TryParse(Console.ReadLine(), out opcionTipo) || opcionTipo < 1 || opcionTipo > 2)
            {
                Console.Write("Opción inválida. Intente nuevamente: ");
            }

            TipoTransaccion tipo = (opcionTipo == 1) ? TipoTransaccion.Ingreso : TipoTransaccion.Gasto;

            // Mostrar categorías disponibles según el tipo
            var categoriasDisponibles = categorias.Where(c => c.TipoAsociado == tipo).ToList();
            if (categoriasDisponibles.Count == 0)
            {
                Console.WriteLine($"No hay categorías para {tipo}. Primero debe crear una categoría.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nCategorías disponibles:");
            for (int i = 0; i < categoriasDisponibles.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {categoriasDisponibles[i].Nombre}");
            }

            Console.Write("Seleccione una categoría: ");
            int opcionCategoria;
            while (!int.TryParse(Console.ReadLine(), out opcionCategoria) || opcionCategoria < 1 ||
                   opcionCategoria > categoriasDisponibles.Count)
            {
                Console.Write("Opción inválida. Intente nuevamente: ");
            }

            string categoria = categoriasDisponibles[opcionCategoria - 1].Nombre;

            // Mostrar cuentas disponibles
            Console.WriteLine("\nCuentas disponibles:");
            for (int i = 0; i < cuentas.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {cuentas[i].Nombre} (Saldo: {cuentas[i].Saldo:C})");
            }

            Console.Write("Seleccione una cuenta: ");
            int opcionCuenta;
            while (!int.TryParse(Console.ReadLine(), out opcionCuenta) || opcionCuenta < 1 ||
                   opcionCuenta > cuentas.Count)
            {
                Console.Write("Opción inválida. Intente nuevamente: ");
            }

            string cuenta = cuentas[opcionCuenta - 1].Nombre;

            // Solicitar monto
            Console.Write("\nIngrese el monto: ");
            decimal monto;
            while (!decimal.TryParse(Console.ReadLine(), out monto) || monto <= 0)
            {
                Console.Write("Monto inválido. Debe ser un número positivo: ");
            }

            // Solicitar descripción
            Console.Write("Ingrese una descripción: ");
            string descripcion = Console.ReadLine();

            // Solicitar fecha
            Console.Write("Ingrese la fecha (DD/MM/AAAA) o deje en blanco para usar la fecha actual: ");
            string fechaStr = Console.ReadLine();
            DateTime fecha;
            if (string.IsNullOrWhiteSpace(fechaStr))
            {
                fecha = DateTime.Now;
            }
            else
            {
                while (!DateTime.TryParseExact(fechaStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None,
                           out fecha))
                {
                    Console.Write("Formato de fecha inválido. Intente nuevamente (DD/MM/AAAA): ");
                    fechaStr = Console.ReadLine();
                }
            }

            // Crear y guardar la transacción
            int nuevoId = transacciones.Count > 0 ? transacciones.Max(t => t.Id) + 1 : 1;
            Transaccion nuevaTransaccion = new Transaccion
            {
                Id = nuevoId,
                Descripcion = descripcion,
                Monto = monto,
                Fecha = fecha,
                Tipo = tipo,
                Categoria = categoria,
                Cuenta = cuenta
            };

            transacciones.Add(nuevaTransaccion);
            GuardarTransacciones();

            // Actualizar saldo de la cuenta
            Cuenta cuentaSeleccionada = cuentas.Find(c => c.Nombre == cuenta);
            if (tipo == TipoTransaccion.Ingreso)
            {
                cuentaSeleccionada.Saldo += monto;
            }
            else
            {
                cuentaSeleccionada.Saldo -= monto;
            }

            GuardarCuentas();

            // Actualizar presupuesto si es un gasto
            if (tipo == TipoTransaccion.Gasto)
            {
                ActualizarPresupuesto(categoria, monto, fecha);
            }

            Console.WriteLine("\nTransacción registrada exitosamente.");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ActualizarPresupuesto(string categoria, decimal monto, DateTime fecha)
        {
            // Buscar el presupuesto para esta categoría en el mes actual
            DateTime inicioMes = new DateTime(fecha.Year, fecha.Month, 1);
            Presupuesto presupuestoExistente = presupuestos.FirstOrDefault(
                p => p.Categoria == categoria && p.MesAño.Year == inicioMes.Year && p.MesAño.Month == inicioMes.Month);

            if (presupuestoExistente != null)
            {
                presupuestoExistente.MontoGastado += monto;
                GuardarPresupuestos();

                // Verificar si se excedió el presupuesto
                if (presupuestoExistente.MontoGastado > presupuestoExistente.MontoAsignado)
                {
                    Console.WriteLine($"\n¡ALERTA! Has excedido el presupuesto para {categoria}");
                    Console.WriteLine($"Presupuesto: {presupuestoExistente.MontoAsignado:C}");
                    Console.WriteLine($"Gastado: {presupuestoExistente.MontoGastado:C}");
                }
            }
        }

        public void AgregarCuenta()
        {
            Console.Clear();
            Console.WriteLine("=== AGREGAR NUEVA CUENTA ===\n");

            Console.Write("Ingrese el nombre de la cuenta: ");
            string nombre = Console.ReadLine();

            // Verificar si ya existe una cuenta con ese nombre
            if (cuentas.Any(c => c.Nombre == nombre))
            {
                Console.WriteLine("\nYa existe una cuenta con ese nombre. Intente con otro nombre.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nTipo de cuenta:");
            Console.WriteLine("1. Efectivo");
            Console.WriteLine("2. Cuenta Bancaria");
            Console.WriteLine("3. Tarjeta de Crédito");
            Console.WriteLine("4. Inversión");
            Console.Write("Seleccione una opción: ");
            int opcionTipo;
            while (!int.TryParse(Console.ReadLine(), out opcionTipo) || opcionTipo < 1 || opcionTipo > 4)
            {
                Console.Write("Opción inválida. Intente nuevamente: ");
            }

            string tipo;
            switch (opcionTipo)
            {
                case 1: tipo = "Efectivo"; break;
                case 2: tipo = "Cuenta Bancaria"; break;
                case 3: tipo = "Tarjeta de Crédito"; break;
                case 4: tipo = "Inversión"; break;
                default: tipo = "Otro"; break;
            }

            Console.Write("\nIngrese el saldo inicial: ");
            decimal saldo;
            while (!decimal.TryParse(Console.ReadLine(), out saldo))
            {
                Console.Write("Valor inválido. Ingrese un número decimal: ");
            }

            Cuenta nuevaCuenta = new Cuenta
            {
                Nombre = nombre,
                Tipo = tipo,
                Saldo = saldo
            };

            cuentas.Add(nuevaCuenta);
            GuardarCuentas();

            Console.WriteLine("\nCuenta agregada exitosamente.");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        public void AgregarCategoria()
        {
            Console.Clear();
            Console.WriteLine("=== AGREGAR NUEVA CATEGORÍA ===\n");

            Console.Write("Ingrese el nombre de la categoría: ");
            string nombre = Console.ReadLine();

            // Verificar si ya existe una categoría con ese nombre
            if (categorias.Any(c => c.Nombre == nombre))
            {
                Console.WriteLine("\nYa existe una categoría con ese nombre. Intente con otro nombre.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nTipo de categoría:");
            Console.WriteLine("1. Ingreso");
            Console.WriteLine("2. Gasto");
            Console.Write("Seleccione una opción: ");
            int opcionTipo;
            while (!int.TryParse(Console.ReadLine(), out opcionTipo) || opcionTipo < 1 || opcionTipo > 2)
            {
                Console.Write("Opción inválida. Intente nuevamente: ");
            }

            TipoTransaccion tipo = (opcionTipo == 1) ? TipoTransaccion.Ingreso : TipoTransaccion.Gasto;

            Categoria nuevaCategoria = new Categoria
            {
                Nombre = nombre,
                TipoAsociado = tipo
            };

            categorias.Add(nuevaCategoria);
            GuardarCategorias();

            Console.WriteLine("\nCategoría agregada exitosamente.");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        public void CrearPresupuesto()
        {
            Console.Clear();
            Console.WriteLine("=== CREAR NUEVO PRESUPUESTO ===\n");

            // Obtener categorías de gastos
            var categoriasGasto = categorias.Where(c => c.TipoAsociado == TipoTransaccion.Gasto).ToList();
            if (categoriasGasto.Count == 0)
            {
                Console.WriteLine("No hay categorías de gasto disponibles. Primero debe crear categorías.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            // Mostrar categorías
            Console.WriteLine("Categorías de gasto disponibles:");
            for (int i = 0; i < categoriasGasto.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {categoriasGasto[i].Nombre}");
            }

            Console.Write("Seleccione una categoría: ");
            int opcionCategoria;
            while (!int.TryParse(Console.ReadLine(), out opcionCategoria) || opcionCategoria < 1 ||
                   opcionCategoria > categoriasGasto.Count)
            {
                Console.Write("Opción inválida. Intente nuevamente: ");
            }

            string categoria = categoriasGasto[opcionCategoria - 1].Nombre;

            // Solicitar mes y año
            Console.Write("\nIngrese el mes (1-12): ");
            int mes;
            while (!int.TryParse(Console.ReadLine(), out mes) || mes < 1 || mes > 12)
            {
                Console.Write("Mes inválido. Intente nuevamente (1-12): ");
            }

            Console.Write("Ingrese el año: ");
            int año;
            while (!int.TryParse(Console.ReadLine(), out año) || año < 2000 || año > 2100)
            {
                Console.Write("Año inválido. Intente nuevamente: ");
            }

            DateTime mesAño = new DateTime(año, mes, 1);

            // Verificar si ya existe un presupuesto para esta categoría y mes
            bool presupuestoExistente = presupuestos.Any(
                p => p.Categoria == categoria && p.MesAño.Year == año && p.MesAño.Month == mes);

            if (presupuestoExistente)
            {
                Console.WriteLine("\nYa existe un presupuesto para esta categoría en el mes seleccionado.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            // Solicitar monto asignado
            Console.Write("\nIngrese el monto asignado al presupuesto: ");
            decimal montoAsignado;
            while (!decimal.TryParse(Console.ReadLine(), out montoAsignado) || montoAsignado <= 0)
            {
                Console.Write("Monto inválido. Debe ser un número positivo: ");
            }

            // Crear presupuesto
            Presupuesto nuevoPresupuesto = new Presupuesto
            {
                Categoria = categoria,
                MesAño = mesAño,
                MontoAsignado = montoAsignado,
                MontoGastado = 0 // Inicialmente no se ha gastado nada
            };

            presupuestos.Add(nuevoPresupuesto);
            GuardarPresupuestos();

            Console.WriteLine("\nPresupuesto creado exitosamente.");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
         public void MostrarTransacciones()
        {
            Console.Clear();
            Console.WriteLine("=== LISTADO DE TRANSACCIONES ===\n");

            if (transacciones.Count == 0)
            {
                Console.WriteLine("No hay transacciones registradas.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            // Ordenar transacciones por fecha (más recientes primero)
            var transaccionesOrdenadas = transacciones.OrderByDescending(t => t.Fecha).ToList();

            // Mostrar encabezados
            Console.WriteLine($"{"ID",-5} {"FECHA",-12} {"TIPO",-10} {"CATEGORÍA",-15} {"CUENTA",-15} {"MONTO",-15} {"DESCRIPCIÓN",-30}");
            Console.WriteLine(new string('-', 100));

            // Mostrar transacciones
            foreach (var t in transaccionesOrdenadas)
            {
                string tipo = t.Tipo == TipoTransaccion.Ingreso ? "Ingreso" : "Gasto";
                string monto = t.Tipo == TipoTransaccion.Ingreso ? $"+{t.Monto:C}" : $"-{t.Monto:C}";

                Console.WriteLine($"{t.Id,-5} {t.Fecha.ToString("dd/MM/yyyy"),-12} {tipo,-10} {t.Categoria,-15} {t.Cuenta,-15} {monto,-15} {t.Descripcion,-30}");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        public void MostrarCuentas()
        {
            Console.Clear();
            Console.WriteLine("=== LISTADO DE CUENTAS ===\n");

            if (cuentas.Count == 0)
            {
                Console.WriteLine("No hay cuentas registradas.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            // Mostrar encabezados
            Console.WriteLine($"{"NOMBRE",-20} {"TIPO",-20} {"SALDO",-15}");
            Console.WriteLine(new string('-', 60));

            // Mostrar cuentas
            foreach (var c in cuentas)
            {
                Console.WriteLine($"{c.Nombre,-20} {c.Tipo,-20} {c.Saldo:C,-15}");
            }

            // Mostrar saldo total
            decimal saldoTotal = cuentas.Sum(c => c.Saldo);
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"SALDO TOTAL:",-41} {saldoTotal:C,-15}");

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        public void MostrarPresupuestos()
        {
            Console.Clear();
            Console.WriteLine("=== LISTADO DE PRESUPUESTOS ===\n");

            if (presupuestos.Count == 0)
            {
                Console.WriteLine("No hay presupuestos registrados.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            // Mostrar encabezados
            Console.WriteLine($"{"CATEGORÍA",-20} {"MES/AÑO",-10} {"ASIGNADO",-15} {"GASTADO",-15} {"RESTANTE",-15} {"% USADO",-10}");
            Console.WriteLine(new string('-', 90));

            // Mostrar presupuestos ordenados por fecha (más recientes primero)
            var presupuestosOrdenados = presupuestos.OrderByDescending(p => p.MesAño).ToList();
            foreach (var p in presupuestosOrdenados)
            {
                decimal restante = p.MontoAsignado - p.MontoGastado;
                decimal porcentajeUsado = (p.MontoGastado / p.MontoAsignado) * 100;

                string mesAño = p.MesAño.ToString("MM/yyyy");
                Console.WriteLine($"{p.Categoria,-20} {mesAño,-10} {p.MontoAsignado:C,-15} {p.MontoGastado:C,-15} {restante:C,-15} {porcentajeUsado:F1}%");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        public void MostrarResumenFinanciero()
        {
            Console.Clear();
            Console.WriteLine("=== RESUMEN FINANCIERO ===\n");

            if (transacciones.Count == 0)
            {
                Console.WriteLine("No hay transacciones registradas para generar un resumen.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            // Obtener el mes actual
            DateTime hoy = DateTime.Now;
            DateTime inicioMes = new DateTime(hoy.Year, hoy.Month, 1);
            DateTime finMes = inicioMes.AddMonths(1).AddDays(-1);

            // Obtener transacciones del mes actual
            var transaccionesMes = transacciones.Where(
                t => t.Fecha >= inicioMes && t.Fecha <= finMes).ToList();

            // Calcular ingresos y gastos del mes
            decimal ingresosMes = transaccionesMes
                .Where(t => t.Tipo == TipoTransaccion.Ingreso)
                .Sum(t => t.Monto);

            decimal gastosMes = transaccionesMes
                .Where(t => t.Tipo == TipoTransaccion.Gasto)
                .Sum(t => t.Monto);

            decimal balanceMes = ingresosMes - gastosMes;

            // Mostrar resumen del mes
            Console.WriteLine($"RESUMEN DEL MES: {inicioMes.ToString("MMMM yyyy").ToUpper()}");
            Console.WriteLine($"Ingresos totales: {ingresosMes:C}");
            Console.WriteLine($"Gastos totales: {gastosMes:C}");
            Console.WriteLine($"Balance del mes: {balanceMes:C}");

            // Mostrar saldo total actual
            decimal saldoTotal = cuentas.Sum(c => c.Saldo);
            Console.WriteLine($"\nSaldo total actual: {saldoTotal:C}");

            // Mostrar gastos por categoría
            Console.WriteLine("\nGASTOS POR CATEGORÍA:");
            var gastosPorCategoria = transaccionesMes
                .Where(t => t.Tipo == TipoTransaccion.Gasto)
                .GroupBy(t => t.Categoria)
                .Select(g => new { Categoria = g.Key, Total = g.Sum(t => t.Monto) })
                .OrderByDescending(g => g.Total);

            foreach (var gasto in gastosPorCategoria)
            {
                decimal porcentaje = (gasto.Total / gastosMes) * 100;
                Console.WriteLine($"{gasto.Categoria,-20} {gasto.Total:C,-15} ({porcentaje:F1}%)");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }

    static void Main(string[] args)
    {
        Console.Title = "Sistema de Finanzas Personales";
        var gestorDatos = new GestorDatos();
        var interfazUsuario = new InterfazUsuario(gestorDatos);

        interfazUsuario.IniciarMenu();

    }
}