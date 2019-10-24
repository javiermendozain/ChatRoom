# Estructura del proyecto
*Este proyecto se encuentra separado en dos microservicios para su futura escalabilidad.*

*En la carpeta `screenshot` se encuentran los captures de funcionamiento, también los pobrás ver al final de esta página*



## Backend
*Ubicado en BackChat, este microservicio corre por el puesto `5050` por el cual, se exponen 14 APIs (Explicaré más adelante), está configurado para soportar solo conexión HTTP y no HTTPS*

### Distribución de directorios 
  - Controllers
  >Contiene los controladores encargados de recibir las peticiones por los diferentes metodos.
  - DataContext
  > Contiene un archivo context, encargado de crear el modelo en la base de datos,
en este también se encuentran  las variables de conexion a la base de datos.
  - Interfaces
  >  Con el fin de aplicar las buenas prácticas en el desarrollo, se crearon interfaces 
  - Models
  > En esta carpeta se encuentran los diferentes modelos creados para modelar la data.
  - Providers
  > Los providers fueron creados como funciones auxiliares que se inyectan y proveen la data, cuando son invocadas las interfaces.

### APIs
###### Endpoints para ChatTracebility
> Estos endpoints están enfocados a guardar y obtener los mensajes de las salas de chats

- [POST] api/ChatTracebility/
> Para guardar mensajes del chat en las salas.
- [GET] api/ChatTracebility/sala/`:idsala`  
> Para obtener mensajes por el identificador de una sala.
- [GET] api/ChatTracebility/`:idusuarios`/`:idsala`
> Para obtener los mensajes en una sala por  ID del usuario y ID de la sala.

###### Endpoints para Enrolados
> Estos endpoints se consumen para unirse a una sala o salir de ella.

- [POST] api/Enrolados/
> Para unirse a una sala, recibe en el body `SalaId`,  `UsuarioId`, `Status`; esté ultimo parámetro es usado para cambiar el esatdo del usuario en la sala (salirse) de las salas.
- [PUT] api/Enrolados/`:id` 
> Para establecer la sala como eliminada. Recibe en el body `SalaId`,  `UsuarioId`, `Status`

###### Endpoints para Salas
> Estos endpoints están enfocados a guardar y obtener los mensajes de las salas de chats

- [POST] api/Salas/
> Para crear una nueva sala recibe en el body `Id` `Name` `Status`
- [GET] api/Salas/  
> Para obtener todas las salas. 
- [GET] api/Salas/usuario/`:id`  
> Para obtener las salas donde ha interactuado el usuario, filtra por  `UID`
- [GET] api/Salas/state/`:status`
> Para obtener las salas según su estado: `0:Inactiva`, `1:Activa`
- [PUT] api/Salas/state/`:id`
> Para cambiar el estado de las salas, estados: `0:Inactiva`, `1:Activa`

###### Endpoints para Usuarios
> Estos endpoints están enfocados a crear, actualizar y obtener los usuarios.

- [POST] api/Usuarios/
> Para crear un nuevo usuario recibe en el body `UID` `Name` `Status`
- [GET] api/ Usuarios/`:status`  
> Para obtener todos los usuarios según su estado. estados: `0:Inactivo`, `1:Activo`
- [PUT] api/Usuarios/`:id`
> Para cambiar el estado de los usuarios a expulsados,  recibe `UsuarioId` como `id` y en el body `UID` `Name` `Status`,  estados: `0:Inactivo`, `1:Activo`


## Frontend
*Ubicado en FrontChat, este microservicio corre por los puesto `5000` or `5001`, está configurado para soportar solo conexión HTTP y no HTTPS*

### Distribución de directorios 
  - Controllers
  >Contiene los controladores encargados de recibir y responder a las acciones de las diferentes vistas creadas.
  - Interfaces
  >  Con el fin de aplicar las buenas prácticas en el desarrollo, se crearon interfaces 
  - Models
  > En esta carpeta se encuentran los diferentes modelos creados para modelar la data.
  - Providers
  > Los providers fueron creados como funciones auxiliares que se inyectan y proveen la data, cuando son invocadas las interfaces.

- Views
  > En las Views encontrará los componentes que le permiten al usuario interactuar con la plataforma.

**NOTA:** *En el método `ConfigureServices`  de los archivo Startup se hacen las inyeciones de los Providers de data, esto para Back (Provee la data de la base de datos) y Front (Provee la data del Back), entregándola a los respectivos controladores*


### Screenshot
>Usuario logiado y lista de salas

![1](https://github.com/javiermendozain/testJayaChat/blob/master/screenshot/1.jpeg)

>Chat por el usuario Javier Mendoza en la sala Administracón y recursos humanos

![2](https://github.com/javiermendozain/testJayaChat/blob/master/screenshot/2.jpeg)

>Chat por el mismo usuario Javier Mendoza en la sala Publicidad y mercadotecnia

![3](https://github.com/javiermendozain/testJayaChat/blob/master/screenshot/3.jpeg)

>El usuario acaba de crear una nueva sala, y les comunica a los integrantes de la sala de Publicidad y mercadotecnia

![4](https://github.com/javiermendozain/testJayaChat/blob/master/screenshot/4.jpeg)

>Visualización en el panel de administración los usuarios activos.

![5](https://github.com/javiermendozain/testJayaChat/blob/master/screenshot/5.jpeg)

>El usuario fue expulsado por el administrador y ahora puede verse en la prestaña de Historial de usuarios, donde al dar click en `Ver salas`, mostrará las salas donde el usuario interactuo.

![6](https://github.com/javiermendozain/testJayaChat/blob/master/screenshot/6.jpeg)

>Aquí se aprecia las salas donde el usuario interactuo.

![7](https://github.com/javiermendozain/testJayaChat/blob/master/screenshot/7.jpeg)

>Una vez clickeado en `Ver mensajes de la sala` de Publicidad y mercadotecnia, se aprecían los mensajes en esta sala por el usuario Javier Mendoza.

![8](https://github.com/javiermendozain/testJayaChat/blob/master/screenshot/8.jpeg)


>Aquí se aprecían las salas que han existido, notese que la `Nueva sala`, creada en la cuarta imagen, no se aprecia en esta lista, eso se debe ha que se encuentra activa aún. 

![10](https://github.com/javiermendozain/testJayaChat/blob/master/screenshot/10.jpeg)

>Estos son los mensajes filtados por sala (Publicidad y Mercadotecnia), indicando el usuario que envío los mensajes, según los requerimientos.

![11](https://github.com/javiermendozain/testJayaChat/blob/master/screenshot/11.jpeg)
