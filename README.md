# AMQ (Proyecto)

Este repositorio contiene un ejemplo simple de envío/recepción de mensajes hacia un broker AMQP (ActiveMQ/Artemis) usando proyectos .NET (Sender, Receiver y Core).

Estructura principal
- `Core/` - Entidades y lógica compartida.
- `Sender/` - Proyecto que envía mensajes al broker.
- `Receiver/` - Proyecto que recibe mensajes desde el broker.
- `compose.yaml` - Configuración opcional para levantar el broker en Docker.

Requisitos
- .NET SDK 8.x (dotnet)
- Docker (si arrancas el broker en contenedor)

Levantar el broker (contenedor)
1. Asegúrate de que `compose.yaml` exponga/publíque los puertos necesarios (ej. 5672 para AMQP, 8161 para consola web):

   ```bash
   docker compose up -d
   docker ps
   docker port <container-id>
   ```

2. Comprueba que desde tu host puedes alcanzar el puerto AMQP (ej. 5672):

   ```bash
   nc -vz localhost 5672
   lsof -nP -iTCP:5672 -sTCP:LISTEN
   docker logs <container-id> | tail -n 200
   ```

Ejecución de los proyectos
Desde la raíz del repo, puedes ejecutar cualquiera de los proyectos con `dotnet run --project`:

```bash
# Enviar un mensaje (Sender)
dotnet run --project Sender

# Ejecutar receptor (Receiver)
dotnet run --project Receiver
```

Configuración de conexión
Los proyectos usan por defecto `BROKER_HOST=localhost` y `BROKER_PORT=5672`. Si el broker corre en contenedor, asegúrate de mapear los puertos con `ports:` en `compose.yaml` (no sólo `expose:`). También puedes exportar variables antes de ejecutar:

```bash
export BROKER_HOST=localhost
export BROKER_PORT=5672
export BROKER_USER=admin
export BROKER_PASS=admin
```

Diagnóstico rápido
- Si ves en logs del broker que escucha en un nombre interno (ej. `2a38ff7662b9:5672`), debes publicar el puerto para acceder desde el host.
- Revisa `docker logs` para confirmar que AMQP está habilitado y sin errores.
- Asegúrate de usar el proveedor NMS correcto (`Apache.NMS.AMQP` para AMQP).
- Para obtener más detalle en excepciones, el código de ejemplo imprime `ex.ToString()` con la traza completa.

Licencia
Este proyecto está bajo la licencia MIT (archivo `LICENSE`).

