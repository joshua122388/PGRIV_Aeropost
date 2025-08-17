using System.Data.Entity;
namespace Aeropost.Models
{
    /// <summary>
    /// Clase de servicio que actúa como contexto de base de datos y capa de acceso a datos
    /// Hereda de DbContext para proporcionar funcionalidad ORM (Object-Relational Mapping)
    /// </summary>
    public class Service : DbContext
    {
        /// <summary>
        /// Representa la tabla "Usuario" en la base de datos
        /// DbSet permite realizar operaciones CRUD (Create, Read, Update, Delete) sobre los usuarios
        /// </summary>
        public DbSet<Usuario> usuarios { get; set; }

        /// <summary>
        /// Representa la tabla "Clientes" en la base de datos
        /// DbSet permite realizar operaciones CRUD sobre los clientes
        /// </summary>
        public DbSet<Cliente> clientes { get; set; }

        /// <summary>
        /// Representa la tabla "Bitacora" en la base de datos
        /// DbSet permite realizar operaciones CRUD sobre los registros de bitácora
        /// </summary>
        public DbSet<Bitacora> bitacora { get; set; }

        /// <summary>
        /// Representa la tabla "Facturas" en la base de datos
        /// DbSet permite realizar operaciones CRUD sobre los registros de bitácora
        /// </summary>
        public DbSet<Factura> Facturas { get; set; }

        /// <summary>
        /// Representa la tabla "Paquetes" en la base de datos
        /// DbSet permite realizar operaciones CRUD sobre los registros de bitácora
        /// </summary>
        public DbSet<Paquete> Paquetes { get; set; }

        /// <summary>
        /// Constructor que inicializa el contexto de base de datos
        /// Llama al constructor base pasando "Aeropost" como nombre de la cadena de conexión
        /// Esta cadena esta definida en appsettings.json
        /// </summary>
        public Service() : base("Aeropost") { }

        // Región que agrupa todos los métodos relacionados con operaciones de Usuario
        #region Metodos Usuario

        /// <summary>
        /// Método para agregar un nuevo usuario a la base de datos
        /// Realiza la inserción y confirma los cambios inmediatamente
        /// </summary>
        /// <param name="usuario">Objeto Usuario con todos los datos a insertar</param>
        public void agregarUsuario(Usuario usuario)
        {
            // Agrega el usuario al contexto (estado: Added)
            usuarios.Add(usuario);

            // Ejecuta la instrucción SQL INSERT y confirma los cambios en la BD
            SaveChanges();
        }

        /// <summary>
        /// Método para obtener todos los usuarios de la base de datos
        /// Convierte el DbSet en un Array para facilitar su manipulación
        /// </summary>
        /// <returns>Array con todos los usuarios registrados</returns>
        public Array mostrarUsuario()
        {
            // Convierte el DbSet<Usuario> en un Array
            // Esto ejecuta una consulta SELECT * FROM Usuario
            return usuarios.ToArray();
        }

        /// <summary>
        /// Método para buscar un usuario específico por su ID
        /// Lanza excepción si el usuario no existe
        /// </summary>
        /// <param name="id">ID único del usuario a buscar</param>
        /// <returns>Usuario encontrado</returns>
        /// <exception cref="Exception">Se lanza cuando el usuario no existe</exception>
        public Usuario buscarUsuario(int id)
        {
            // Busca el primer usuario que coincida con el ID proporcionado
            // FirstOrDefault retorna null si no encuentra ninguna coincidencia
            var usuarioBuscado = this.usuarios.FirstOrDefault(x => x.Id == id);

            // Verifica si se encontró el usuario
            if (usuarioBuscado != null)
                return usuarioBuscado;  // Retorna el usuario encontrado
            else
                // Lanza excepción personalizada si no se encuentra el usuario
                // IMPORTANTE: En Razor Pages, esta excepción debe ser capturada
                // en el code-behind para mostrar un mensaje amigable al usuario
                throw new Exception("Usuario no encontrado");
        }

        /// <summary>
        /// Método para eliminar un usuario de la base de datos
        /// NOTA: El nombre del método no coincide con la funcionalidad (dice "Estudiante" pero elimina "Usuario")
        /// </summary>
        /// <param name="usuario">Objeto Usuario a eliminar (debe existir en el contexto)</param>
        public void eliminarUsuario(Usuario usuario)
        {
            // Marca el usuario para eliminación en el contexto
            this.usuarios.Remove(usuario);

            // Ejecuta la instrucción SQL DELETE y confirma los cambios
            SaveChanges();
        }

        /// <summary>
        /// Método para actualizar un usuario existente en la base de datos
        /// Busca el usuario por ID y actualiza todas sus propiedades editables
        /// </summary>
        /// <param name="usuario">Usuario con los nuevos datos a actualizar</param>
        /// <exception cref="Exception">Se lanza cuando el usuario a actualizar no existe</exception>
        public void actualizarUsuario(Usuario usuario)
        {
            // Busca el usuario existente en la base de datos por ID
            // Este usuario está "tracked" por Entity Framework
            var usuarioAntiguo = usuarios.FirstOrDefault(x => x.Id == usuario.Id);

            // Verifica si el usuario existe antes de intentar actualizarlo
            if (usuarioAntiguo != null)
            {
                // Actualiza cada propiedad del usuario existente con los nuevos valores
                // Entity Framework detecta automáticamente estos cambios (Change Tracking)

                usuarioAntiguo.Nombre = usuario.Nombre;  // Actualiza nombre completo
                usuarioAntiguo.Cedula = usuario.Cedula;                  // Permite corrección de cédula
                usuarioAntiguo.Genero = usuario.Genero;                  // Actualiza género
                usuarioAntiguo.Estado = usuario.Estado;                  // Permite activar/desactivar usuario
                usuarioAntiguo.User = usuario.User;                      // Actualiza nombre de usuario
                usuarioAntiguo.Pass = usuario.Pass;                      // Actualiza contraseña (ya viene preservada del controlador)

                // IMPORTANTE: FechaRegistro NO se actualiza intencionalmente
                // Debe conservarse la fecha original de cuando se creó el usuario
                // Esto mantiene la integridad histórica de los datos

                // Ejecuta la instrucción SQL UPDATE con solo los campos modificados
                SaveChanges();
            }
            else
            {
                // Lanza excepción si el usuario a actualizar no existe
                // En Razor Pages, esto debe manejarse en el code-behind para:
                // 1. Mostrar mensaje de error al usuario
                // 2. Redirigir a página de listado
                // 3. Registrar el error en logs si es necesario
                throw new Exception("Usuario no encontrado");
            }

        }

        /// <summary>
        /// Función para validar el login de un usuario
        /// Busca un usuario que coincida con las credenciales proporcionadas y que esté activo
        /// </summary>
        /// <param name="user">Nombre de usuario</param>
        /// <param name="password">Contraseña del usuario</param>
        /// <returns>Usuario autenticado si las credenciales son válidas</returns>
        /// <exception cref="Exception">Se lanza cuando las credenciales son incorrectas o el usuario está inactivo</exception>
        public Usuario login(string user, string password)
        {
            // Ejecuta una consulta LINQ sobre el DbSet de usuarios para buscar una coincidencia exacta
            // FirstOrDefault() retorna el primer usuario que cumpla TODAS las condiciones o null si no encuentra ninguno
            // Condición 1: x.User == user - El nombre de usuario debe coincidir exactamente (case-sensitive)
            // Condición 2: x.Pass == password - La contraseña debe coincidir exactamente (texto plano)
            // Condición 3: x.Estado == "activo" - Solo permite login a usuarios con estado activo
            // NOTA: Esto previene que usuarios suspendidos/inactivos puedan acceder al sistema
            var usuarioLogueado = usuarios.FirstOrDefault(x => x.User == user && x.Pass == password && x.Estado == "activo");

            // Verifica si se encontró un usuario que cumpla con todos los criterios de autenticación
            if (usuarioLogueado != null)
            {
                // Si las credenciales son válidas y el usuario está activo, retorna el objeto Usuario completo
                // Este objeto contiene toda la información del usuario autenticado (ID, Nombre, etc.)
                // que puede ser utilizada en la sesión para personalizar la experiencia del usuario
                return usuarioLogueado;
            }
            else
                // Si no se encontró ningún usuario que coincida con los criterios, lanza una excepción
                // Esto puede ocurrir por: usuario incorrecto, contraseña incorrecta, o usuario inactivo
                // En Razor Pages, esta excepción debe ser capturada en el code-behind para:
                // 1. Mostrar un mensaje de error amigable al usuario
                // 2. Limpiar los campos del formulario de login
                // 3. Registrar el intento fallido en la bitácora de seguridad si es necesario
                throw new Exception("Usuario o contraseña incorrectos");
        }
        #endregion

        // Región que agrupa todos los métodos relacionados con operaciones de Cliente
        #region Metodos Cliente

        /// <summary>
        /// Agrega un nuevo cliente a la base de datos
        /// </summary>
        /// <param name="cliente">Objeto Cliente con todos los datos a insertar</param>
        public void agregarCliente(Cliente cliente)
        {
            clientes.Add(cliente);
            SaveChanges();
        }

        /// <summary>
        /// Devuelve todos los clientes guardados en la base de datos
        /// </summary>
        /// <returns>Array con todos los clientes registrados</returns>
        public Array mostrarCliente()
        {
            return clientes.ToArray();
        }

        /// <summary>
        /// Busca un cliente por cédula
        /// </summary>
        /// <param name="cedula">Cédula del cliente a buscar</param>
        /// <returns>Cliente encontrado</returns>
        /// <exception cref="Exception">Se lanza cuando el cliente no está registrado</exception>
        public Cliente buscarCliente(string cedula)
        {
            var clienteBuscado = clientes.FirstOrDefault(x => x.Cedula == cedula);
            if (clienteBuscado != null)
                return clienteBuscado;
            else
                throw new Exception("Ese cliente no está registrado");
        }

        /// <summary>
        /// Elimina un cliente de la base de datos
        /// </summary>
        /// <param name="cliente">Objeto Cliente a eliminar</param>
        public void eliminarCliente(Cliente cliente)
        {
            clientes.Remove(cliente);
            SaveChanges();
        }

        /// <summary>
        /// Actualiza los datos de un cliente existente
        /// </summary>
        /// <param name="cliente">Cliente con los nuevos datos a actualizar</param>
        /// <exception cref="Exception">Se lanza cuando no se puede actualizar el cliente</exception>
        public void actualizarCliente(Cliente cliente)
        {
            var clienteAntiguo = clientes.FirstOrDefault(x => x.Cedula == cliente.Cedula);
            if (clienteAntiguo != null)
            {
                clienteAntiguo.Nombre = cliente.Nombre;
                clienteAntiguo.Tipo = cliente.Tipo;
                clienteAntiguo.Correo = cliente.Correo;
                clienteAntiguo.DireccionEntrega = cliente.DireccionEntrega;
                clienteAntiguo.Telefono = cliente.Telefono;
                SaveChanges();
            }
            else
                throw new Exception("No se pudo actualizar el cliente");
        }

        /// <summary>
        /// Devuelve un arreglo de clientes filtrados por el tipo especificado.
        /// </summary>
        /// <param name="tipo">Cadena que representa el tipo de cliente (por ejemplo: "Regular", "Frecuente", "Suspendido")</param>
        /// <returns>Un arreglo de objetos Cliente cuyo campo Tipo coincide con el valor proporcionado</returns>
        public Array mostrarClientesPorTipo(string tipo)
        {
            // Esta línea busca y devuelve todos los clientes que tienen el tipo indicado por el parámetro 'tipo'.
            // Por ejemplo, si se llama a ese método con "Regular", solo te regresará los clientes cuyo tipo sea "Regular".
            // Primero, 'clientes.Where(x => x.Tipo == tipo)' recorre toda la lista de clientes y selecciona solo los que cumplen con esa condición.
            // Después, 'ToArray()' convierte ese grupo filtrado en un arreglo para que puedas trabajar fácilmente con él.
            // Es útil cuando necesitas mostrar o procesar únicamente los clientes de un tipo específico, como para generar reportes o estadísticas.
            return clientes.Where(x => x.Tipo == tipo).ToArray();
        }

        #endregion

        #region Metodos Factura
        // Agregar factura
        public void AgregarFactura(Factura factura)
        {
            Facturas.Add(factura);
            SaveChanges();
        }

        // Mostrar todas las facturas
        public List<Factura> ObtenerFacturas()
        {
            return Facturas.ToList();
        }

        // Eliminar factura
        public void EliminarFactura(int id)
        {
            var factura = Facturas.Find(id);
            if (factura != null)
            {
                Facturas.Remove(factura);
                SaveChanges();
            }
        }

        // Actualizar factura
        public void ActualizarFactura(Factura factura)
        {
            var existente = Facturas.Find(factura.Id);
            if (existente != null)
            {
                existente.NumeroFactura = factura.NumeroFactura;
                existente.CedulaCliente = factura.CedulaCliente;
                existente.MontoTotal = factura.MontoTotal;
                existente.FechaEntrega = factura.FechaEntrega;
                existente.NumeroTracking = factura.NumeroTracking;
                SaveChanges();
            }
        }

        #endregion



        #region Metodos Paquete

        /// <summary>
        /// Genera el tracking: 2 letras de tienda + MM + yy + "MIA" + 5 dígitos
        /// </summary>
        private string GenerarTracking(string tienda)
        {
            string letras = string.IsNullOrWhiteSpace(tienda)
                ? "XX"
                : new string(tienda.Where(char.IsLetter).Take(2).ToArray())
                    .ToUpper()
                    .PadRight(2, 'X');

            var now = DateTime.Now;
            string mes = now.Month.ToString("D2");
            string yy = (now.Year % 100).ToString("D2");
            string rnd = new Random().Next(0, 100000).ToString("D5");

            return $"{letras}{mes}{yy}MIA{rnd}";
        }

        /// <summary>Inserta un paquete. Valida que exista la cédula del cliente y genera tracking/fecha.</summary>
        public void agregarPaquete(Paquete paquete)
        {
            if (!clientes.Any(c => c.Cedula == paquete.CedulaCliente))
                throw new Exception("La cédula del cliente no existe.");

            paquete.NumeroTracking = GenerarTracking(paquete.TiendaOrigen);
            paquete.FechaRegistro = DateTime.Now;

            Paquetes.Add(paquete);
            SaveChanges();
        }

        /// <summary>Devuelve todos los paquetes (ordenados por fecha de registro desc).</summary>
        public Paquete[] mostrarPaquete()
        {
            return Paquetes
                // .Include("Cliente") // Descomenta si tu modelo tiene navegación Cliente
                .OrderByDescending(p => p.FechaRegistro)
                .ToArray();
        }

        /// <summary>Busca un paquete por Id.</summary>
        public Paquete buscarPaquete(int id)
        {
            var p = Paquetes.FirstOrDefault(x => x.Id == id);
            if (p == null) throw new Exception("Paquete no encontrado");
            return p;
        }

        /// <summary>Busca un paquete por número de tracking.</summary>
        public Paquete buscarPaquetePorTracking(string tracking)
        {
            var p = Paquetes.FirstOrDefault(x => x.NumeroTracking == tracking);
            if (p == null) throw new Exception("No existe paquete con ese tracking");
            return p;
        }

        /// <summary>Elimina un paquete existente.</summary>
        public void eliminarPaquete(Paquete paquete)
        {
            Paquetes.Remove(paquete);
            SaveChanges();
        }

        /// <summary>
        /// Actualiza un paquete. NO se modifica el número de tracking ni la fecha de registro.
        /// </summary>
        public void actualizarPaquete(Paquete paquete)
        {
            var db = Paquetes.FirstOrDefault(x => x.Id == paquete.Id);
            if (db == null) throw new Exception("Paquete no encontrado");

            // Tracking y fecha NO se tocan
            db.CedulaCliente = paquete.CedulaCliente;
            db.TiendaOrigen = paquete.TiendaOrigen;
            db.Peso = paquete.Peso;
            db.ValorTotal = paquete.ValorTotal;
            db.ProductosEspeciales = paquete.ProductosEspeciales;

            SaveChanges();
        }

        /// <summary>Lista paquetes de un cliente por cédula.</summary>
        public Paquete[] mostrarPaquetesPorCliente(string cedula)
        {
            return Paquetes
                // .Include("Cliente") // Descomenta si tienes navegación
                .Where(x => x.CedulaCliente == cedula)
                .OrderByDescending(x => x.FechaRegistro)
                .ToArray();
        }

        #endregion

        // Region para Bitácora
        #region Metodos Bitacora
        public Array mostrarBitacora()
        {
            return bitacora.ToArray();
        }


        public void registrarLogin(Usuario usu)
        {
            bitacora.Add(new Bitacora
            {
                User = usu.User,
                NombreCompleto = usu.Nombre,
                FechaHora = DateTime.Now // Fecha y hora del evento de inicio de sesión
            });
            SaveChanges(); // Guarda los cambios en la base de datos
        }
        #endregion
    }
}

