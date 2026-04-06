# Especificacion de funcionalidades iniciadas por usuarios

## 1. Actores del sistema
- Usuario autenticado
- Estudiante
- Profesor

## 2. Funcionalidades (casos de uso)

### 2.1 Usuario autenticado
- Como usuario autenticado, quiero actualizar mis datos personales para mantener mi perfil vigente.
  action: actualizar_datos_personales
  descripcion: Actualiza informacion personal basica del usuario.
  parametros:
    - usuarioId: number
    - nombre: string
    - apellido: string
    - fechaNacimiento: string

- Como usuario autenticado, quiero actualizar mi direccion para mantener mi informacion de ubicacion correcta.
  action: actualizar_direccion
  descripcion: Modifica los datos de direccion del usuario.
  parametros:
    - usuarioId: number
    - calle: string
    - numero: string
    - ciudad: string
    - provincia: string
    - codigoPostal: string

- Como usuario autenticado, quiero actualizar mis datos de contacto para recibir comunicaciones.
  action: actualizar_contacto
  descripcion: Modifica email y/o telefono del usuario.
  parametros:
    - usuarioId: number
    - email: string
    - telefono: string

- Como usuario autenticado, quiero consultar mi perfil para visualizar mi informacion actual.
  action: obtener_perfil_usuario
  descripcion: Obtiene la informacion de perfil del usuario autenticado.
  parametros:
    - usuarioId: number

### 2.2 Estudiante
- Como estudiante, quiero inscribirme en un curso para comenzar mi proceso de aprendizaje.
  action: inscribir_estudiante_a_curso
  descripcion: Registra al estudiante en un curso disponible.
  parametros:
    - cursoId: number
    - estudianteId: number

  - Como estudiante, quiero desinscribirme de un curso para dejar una cursada activa.
    action: desinscribir_estudiante
    descripcion: Elimina la inscripcion activa del estudiante en un curso.
    parametros:
      - cursoId: number
      - estudianteId: number

- Como estudiante, quiero consultar mis cursos inscritos actualmente para gestionar mi cursada.
  action: obtener_cursos_inscriptos_actualmente
  descripcion: Lista cursos con estado de cursada activa del estudiante.
  parametros:
    - estudianteId: number

- Como estudiante, quiero cancelar una inscripcion para dejar un curso cuando lo necesite.
  action: cancelar_inscripcion
  descripcion: Cancela una inscripcion activa del estudiante en un curso.
  parametros:
    - inscripcionId: number
    - estudianteId: number

- Como estudiante, quiero acceder a las clases de un curso para consumir el contenido.
  action: acceder_clases_curso
  descripcion: Recupera las clases disponibles de un curso para un estudiante inscrito.
  parametros:
    - cursoId: number
    - estudianteId: number

- Como estudiante, quiero entregar mi examen.
  action: entregar_examen
  descripcion: Registra las respuestas de un estudiante en un examen de un curso.
  parametros:
    - examenId: number
    - estudianteId: number

- Como estudiante, quiero consultar el detalle de mi entrega de examen para revisar mi presentacion.
  action: obtener_detalle_entrega_examen
  descripcion: Obtiene informacion de una entrega puntual de examen.
  parametros:
    - entregaExamenId: number
    - estudianteId: number

- Como estudiante, quiero consultar mi nota de entrega de examen para conocer mi resultado.
  action: obtener_nota_de_entrega_examen
  descripcion: Devuelve la calificacion de una entrega de examen.
  parametros:
    - entregaExamenId: number
    - estudianteId: number

- Como estudiante, quiero consultar las encuestas de un curso para responder retroalimentacion.
  action: obtener_encuestas_por_curso
  descripcion: Lista encuestas disponibles para el curso.
  parametros:
    - cursoId: number
    - estudianteId: number

- Como estudiante, quiero consultar una encuesta por ID para responderla.
  action: obtener_encuesta_por_id
  descripcion: Obtiene detalle de una encuesta especifica.
  parametros:
    - encuestaId: number
    - estudianteId: number

### 2.3 Profesor
- Como profesor, quiero crear cursos para ofrecer contenido academico.
  action: crear_curso
  descripcion: Crea un curso asociado a un profesor responsable.
  parametros:
    - profesorId: number
    - titulo: string
    - descripcion: string
    - fechaInicio: string
    - fechaFin: string
    - cupoMaximo: number

- Como profesor, quiero actualizar el temario del curso para mantener contenidos vigentes.
  action: actualizar_temario
  descripcion: Modifica el temario de un curso existente.
  parametros:
    - cursoId: number
    - profesorId: number
    - temario: string

- Como profesor, quiero cambiar el profesor responsable de un curso cuando sea necesario.
  action: cambiar_profesor
  descripcion: Reasigna la titularidad del curso a otro profesor.
  parametros:
    - cursoId: number
    - profesorActualId: number
    - nuevoProfesorId: number

- Como profesor, quiero crear una clase dentro de un curso para organizar la cursada.
  action: crear_clase
  descripcion: Crea una clase para un curso con su fecha y contenido base.
  parametros:
    - cursoId: number
    - profesorId: number
    - titulo: string
    - fechaProgramada: string
    - material: string

- Como profesor, quiero reprogramar la fecha de una clase por cambios de agenda.
  action: reprogramar_fecha_de_clase
  descripcion: Modifica la fecha planificada de una clase existente.
  parametros:
    - claseId: number
    - profesorId: number
    - nuevaFecha: string

- Como profesor, quiero actualizar el material de una clase para mejorar su contenido.
  action: actualizar_material
  descripcion: Reemplaza o ajusta el material asociado a una clase.
  parametros:
    - claseId: number
    - profesorId: number
    - material: string

- Como profesor, quiero marcar asistencia en una clase para registrar presentes.
  action: dar_presente
  descripcion: Registra asistencia de un estudiante en una clase.
  parametros:
    - claseId: number
    - estudianteId: number
    - presente: boolean

- Como profesor, quiero agregar una consulta en clase para registrar dudas relevantes.
  action: agregar_consulta
  descripcion: Crea una consulta asociada a la clase para seguimiento.
  parametros:
    - claseId: number
    - profesorId: number
    - consulta: string

- Como profesor, quiero finalizar una clase para cerrar su estado operativo.
  action: finalizar_clase
  descripcion: Marca la clase como finalizada.
  parametros:
    - claseId: number
    - profesorId: number

- Como profesor, quiero finalizar un curso para cerrar su ciclo academico.
  action: finalizar_curso
  descripcion: Marca el curso como finalizado y no editable para cursada activa.
  parametros:
    - cursoId: number
    - profesorId: number

- Como profesor, quiero dar de baja un estudiante de un curso cuando corresponda.
  action: dar_de_baja_estudiante
  descripcion: Remueve a un estudiante de la lista activa del curso por decision academica o administrativa.
  parametros:
    - cursoId: number
    - profesorId: number
    - estudianteId: number
    - motivo: string

- Como profesor, quiero consultar informacion de un curso por ID para revisar su estado.
  action: obtener_informacion_curso_por_id
  descripcion: Obtiene detalle general de un curso.
  parametros:
    - cursoId: number
    - profesorId: number

- Como profesor, quiero consultar estudiantes inscriptos en un curso para hacer seguimiento.
  action: obtener_estudiantes_inscriptos_curso
  descripcion: Lista estudiantes activos inscriptos en un curso.
  parametros:
    - cursoId: number
    - profesorId: number

- Como profesor, quiero consultar informacion de una clase para controlar su ejecucion.
  action: obtener_informacion_clase
  descripcion: Obtiene detalle de una clase especifica.
  parametros:
    - claseId: number
    - profesorId: number

- Como profesor, quiero consultar asistencias de una clase para seguimiento academico.
  action: obtener_asistencias_de_clase
  descripcion: Obtiene listado de asistencias registradas por clase.
  parametros:
    - claseId: number
    - profesorId: number

- Como profesor, quiero consultar consultas de una clase para gestionar respuestas.
  action: obtener_consultas_de_clase
  descripcion: Lista consultas registradas en una clase.
  parametros:
    - claseId: number
    - profesorId: number

- Como profesor, quiero crear encuestas de curso para recolectar retroalimentacion.
  action: crear_encuesta
  descripcion: Crea una encuesta asociada a un curso.
  parametros:
    - cursoId: number
    - profesorId: number
    - titulo: string
    - preguntas: string[]

- Como profesor, quiero modificar encuestas de curso para ajustar preguntas.
  action: modificar_encuesta
  descripcion: Actualiza una encuesta existente del curso.
  parametros:
    - encuestaId: number
    - profesorId: number
    - titulo: string
    - preguntas: string[]

- Como profesor, quiero consultar mejores resenas por curso para detectar fortalezas.
  action: obtener_mejores_resenas_por_curso
  descripcion: Devuelve resenas con mejor valoracion del curso.
  parametros:
    - cursoId: number
    - profesorId: number

- Como profesor, quiero consultar peores resenas por curso para detectar oportunidades de mejora.
  action: obtener_peores_resenas_por_curso
  descripcion: Devuelve resenas con menor valoracion del curso.
  parametros:
    - cursoId: number
    - profesorId: number

- Como profesor, quiero consultar mi perfil por ID para validar mi informacion.
  action: obtener_perfil_profesor_por_id
  descripcion: Obtiene la informacion de perfil del profesor.
  parametros:
    - profesorId: number

- Como profesor, quiero consultar el perfil de un estudiante por ID para seguimiento academico.
  action: obtener_perfil_estudiante_por_id
  descripcion: Obtiene la informacion de perfil del estudiante.
  parametros:
    - estudianteId: number

## 3. Reglas funcionales generales
- El sistema debe validar permisos por rol antes de ejecutar cada accion.
- El sistema debe registrar auditoria de acciones criticas (inscripciones, bajas, finalizacion de curso, cambios de profesor).
- El sistema debe manejar errores de validacion y concurrencia con mensajes claros.
- El sistema debe permitir busqueda por ID en entidades principales (curso, clase, inscripcion, encuesta, examen, estudiante, profesor).
- El sistema debe separar operaciones de escritura (commands) y lectura (queries) para mantener coherencia con CQRS.

## 4. Ejemplos de funcionalidades especificas por ID
- Usuario consulta perfil por ID.
- Estudiante consulta inscripcion por ID.
- Profesor consulta curso por ID.
- Profesor consulta clase por ID.
- Estudiante consulta encuesta por ID.
- Estudiante consulta entrega de examen por ID.
