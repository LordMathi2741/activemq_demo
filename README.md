# AMQ (Proyecto)

Repositorio pequeño con tres proyectos .NET y un contenedor con un broker AMQP (ActiveMQ).

Estructura principal:
- Core/: Entidades y lógica compartida
- Sender/: Aplicación que envía mensajes
- Receiver/: Aplicación que recibe mensajes
- compose.yaml: Definición del contenedor del broker (ActiveMQ)

Cómo levantar el broker (Docker):

1. Asegúrate de tener Docker Desktop instalado y funcionando.
2. Desde la raíz del proyecto:

```bash
docker compose up -d
```

3. Verifica que el contenedor esté corriendo:

```bash
docker ps
```

4. Revisa logs si algo falla:

```bash
docker compose logs -f AMQ
# o
docker logs -f <container_id>
```

Acceso a la consola web del broker:
- Web Console: http://localhost:8161 (puerto mapeado en `compose.yaml`)
- Credenciales por defecto (ActiveMQ clásico): `admin` / `admin` (si aplica)

Probar conectividad AMQP desde la máquina anfitriona:

- Usando nc (netcat):

```bash
nc -vz localhost 5672
```

- Usando telnet (simple chequeo de puerto):

```bash
telnet localhost 5672
```

Si ves mensajes como "Listening for connections at: amqp://2a38ff7662b9:5672" en los logs del contenedor, eso indica que el broker está escuchando en la interfaz interna del contenedor (el nombre "2a38ff7662b9" es el hostname del contenedor). Con el mapeo de puertos (`5672:5672`) podrás conectarte desde el host usando `localhost:5672`.

Conexión desde otros contenedores en la misma red de Docker Compose:
- Cuando corras otra tarea como servicio en el mismo `docker compose` (o dentro del mismo proyecto), el hostname a usar será el nombre del servicio (`AMQ`). Por ejemplo, la URL desde otro contenedor sería `amqp://AMQ:5672`.

Ejecutar proyectos .NET (desde la raíz o dentro de cada carpeta Sender/Receiver):

```bash
cd Sender && dotnet run
cd Receiver && dotnet run
```

Puntos comunes de fallo y cómo revisarlos:
- El cliente intenta conectarse a `2a38ff7662b9` (hostname interno): desde el host debes usar `localhost` si el puerto está mapeado.
- Firewall / reglas de seguridad bloquean el puerto 5672.
- El contenedor no expuso correctamente el puerto (revisa `docker ps` y `docker compose ps`).
- El broker requiere credenciales o un protocolo diferente; revisa la documentación de la imagen `apache/activemq` que usas.
- Si usas una versión antigua de Docker (Docker Toolbox), el host no será `localhost` sino la IP de la VM `docker-machine ip default`.

Si necesitas, puedo revisar los strings de conexión en el código `Sender`/`Receiver` y proponerte la forma correcta (por ejemplo `amqp://guest:guest@localhost:5672` o `amqp://localhost:5672`).

Más ayuda:
- Para depuración, adjunta los logs del contenedor (`docker compose logs AMQ`) y el string de conexión que usan `Sender` y `Receiver`.

