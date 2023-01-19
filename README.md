# Prototipo de una aplicación de Realidad Virtual

### Interfaces Inteligentes - Universidad de La Laguna

### Autores

* Dario Cerviño Luridiana (alu0101315058@ull.edu.es)
* Enrique Viña Alonso (alu0101337760@ull.edu.es)
* Juan Salvador Magariños Alba (alu0101352145@ull.edu.es)

## Introducción

El objetivo de este README es resaltar aspectos del desarrollo del prototipo que consideramos importantes para comprender el alcance del trabajo realizado, así como explicar el apartado técnico del proyecto. También actuará como un registro del trabajo realizado por cada miembro del grupo, y servirá para comprobar si los objetivos de la actividad han sido alcanzados.

## Explicación del juego (TODO)

El bucle principal del juego se estructura en oleadas de enemigos, siendo el jugador decide cuándo comienzan pulsando un botón. Los enemigos aparecen en puntos especiales del mapa llamados "spawners", y su objetivo es alcanzar y destruir ciertos objetos cercanos a la torre en la que está el jugador. 

Para evitar esto, el usuario puede utilizar una pistola para dispararles; pero también se le da la posibilidad de recoger objetos dejados por los monstruos derrotados para fabricar pociones en un caldero que tiene en su zona de trabajo. Concretamente, todas las pociones requieren de 4 objetos para ser preparadas. Una vez se ha hecho una poción, esta aparece en la mesa de trabajo, y el jugador puede cargarla y dispararla con un cañón acoplable al brazo.

El área principal de trabajo del jugador es una torre desde la que se puede ver todo el mapa.

- Torre
- Spawners de pistola, cañón, objetos y pociones
- Cualquier otra cosa que quede por explicar de cómo se juega

## Cuestiones importantes para el uso (TODO)

Para adaptarse al jugador, el juego permite escoger entre dos tipos de movimiento:

  * Movimiento basado en las acciones del jugador: en este modo, el jugador se mueve utilizando el mando. Cabe destacar que, con el objetivo de evitar que este se maree, el movimiento se realiza a velocidad constante
  * Movimiento basado en teletransporte: como su nombre indica, al usar este modo, el jugador puede teletransportarse entre diferentes posiciones. Esto también puede ayudar a evitar que el jugador se maree, dado que evita que tenga que realizar cambios progresivos del movimiento estando quieto en la realidad

Sin embargo, independientemente del modo escogido, el jugador se puede mover en el mundo virtual si se mueve en el real. Además, también se le da la opción de rotar sobre sí mismo mediante giros "snap-based", que son rotaciones que suman siempre 45º o -45º, con el objetivo de evitar que se deba realizar un giro continuo, aunque sea a velocidad constante, evitando que el jugador se maree.

- cómo se utiliza la VR para jugar (básicamente, qué es lo que puede hacer el jugador aparte de moverse)
- cosas para evitar que el jugador se maree (quitando lo del movimiento)

Este prototipo se desarrolló con la versión 2021.3.6f1 de Unity.

## Hitos de programación logrados (TODO)

- uso de eventos
- uso de físicas
- uso de VFX y shader graph (URP)
- uso de scriptable objects
- managers (singleton que puede utilizar el patrón observer, etc)
- ... (yo pondría todas las cosas que no hemos visto en la asignatura)

## Aspectos destacables de la aplicación (TODO)



- esto es un poco más subjetivo, pero creo que podríamos poner una idea de cómo
  seguir desarrollando la aplicación en un futuro hipotético

## Distribución del trabajo (TODO)

### Reparto de tareas básico

Inicialmente, se realizó la siguiente división del trabajo:

* Dario Cerviño Luridiana: sistema controlador de los enemigos (algoritmo A*)

* Enrique Viña Alonso: controladores

* Juan Salvador Magariños Alba: sistemas de fabricación de pociones e inventario

### Tareas desarrolladas individualmente

Adicionalmente, cada integrante llevó a cabo las siguientes tareas:

* Dario Cerviño Luridiana: creación de efectos especiales

* Enrique Viña Alonso: 

* Juan Salvador Magariños Alba: modelado de los ítems, pociones y la torre, creación de shaders

### Tareas desarrolladas en grupo

Las siguientes tareas fueron hechas por dos o más personas:

* Creación del terreno enemigo: 

* 

* 
