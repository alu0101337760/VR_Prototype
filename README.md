# Prototipo de una aplicación de Realidad Virtual

### Interfaces Inteligentes - Universidad de La Laguna

### Autores

* Dario Cerviño Luridiana (alu0101315058@ull.edu.es)
* Enrique Viña Alonso (alu0101337760@ull.edu.es)
* Juan Salvador Magariños Alba (alu0101352145@ull.edu.es)

## Introducción

El objetivo de este README es resaltar aspectos del desarrollo del prototipo que consideramos importantes para comprender el alcance del trabajo realizado, así como explicar el apartado técnico del proyecto. También actuará como un registro del trabajo realizado por cada miembro del grupo, y servirá para comprobar si los objetivos de la actividad han sido alcanzados.

## Explicación del juego

El bucle principal del juego se estructura en oleadas de enemigos, siendo el jugador decide cuándo comienzan pulsando un botón. Los enemigos aparecen en puntos especiales del mapa llamados "spawners", y su objetivo es alcanzar y destruir ciertos objetos cercanos a la torre en la que está el jugador. 

Para evitar esto, el usuario puede utilizar una pistola para dispararles; pero también se le da la posibilidad de recoger objetos dejados por los monstruos derrotados para fabricar pociones en un caldero que tiene en su zona de trabajo. Concretamente, todas las pociones requieren de 4 objetos para ser preparadas. Una vez se ha hecho una poción, esta aparece en la mesa de trabajo, y el jugador puede cargarla y dispararla con un cañón acoplable al brazo.

El área principal de trabajo del jugador es una torre desde la que se puede ver todo el mapa. La torre cuenta con una mesa de pociones con acceso al inventario de objetos; una mesa principal, donde aparecen la pistola y el cañón; Un panel de control de juego, para empezar una oleada o reiniciar el juego; y un panel de control de movimiento, para cambiar entre los dos tipos de movimiento disponibles. También hay un botón para salir del juego.

Al matar enemigos, estos pueden otorgar materiales para pociones. Cada poción cuesta 4 ingredientes, que se pierden al entrar en el caldero. Después de cada ronda, conviene gastar estos materiales en hacer pociones para la siguiente.

La mesa principal cuenta con dos libros que invocan armas de pólvora. El de la derecha, invoca hasta dos pistolas, mientras que el de la izquierda invoca un cañón para lanzar pociones. El cañón se carga poniendo la poción en la apertura.

Frente a la torre, se encuentra el terreno que recorren los enemigos. Estos aparecen de un cilindro de color negro e intentan atacar a los cilindros de color blanco. Si consiguen destruir uno de estos, este cilindro cambia de color y se convierte en otra posible entrada de los enemigos. Si consiguen destruir todos los cilindros blancos, el jugador pierde.

![WeaponExample](./gifs/WeaponExample.gif)
![ButtonExample](./gifs/ButtonExample.gif)

## Cuestiones importantes para el uso

Para adaptarse al jugador, el juego permite escoger entre dos tipos de movimiento:

  * Movimiento basado en las acciones del jugador: en este modo, el jugador se mueve utilizando el mando. Cabe destacar que, con el objetivo de evitar que este se maree, el movimiento se realiza a velocidad constante
  * Movimiento basado en teletransporte: como su nombre indica, al usar este modo, el jugador puede teletransportarse entre diferentes posiciones. Esto también puede ayudar a evitar que el jugador se maree, dado que evita que tenga que realizar cambios progresivos del movimiento estando quieto en la realidad

Sin embargo, independientemente del modo escogido, el jugador se puede mover en el mundo virtual si se mueve en el real. Además, también se le da la opción de rotar sobre sí mismo mediante giros "snap-based", que son rotaciones que suman siempre 45º o -45º, con el objetivo de evitar que se deba realizar un giro continuo, aunque sea a velocidad constante, evitando que el jugador se maree.

Este juego hace uso solo del botón de agarre y del gatillo. Con el agarre, el jugador puede coger objetos y lanzarlos. Con el gatillo, el jugador puede disparar.

Este prototipo se desarrolló con la versión 2021.3.6f1 de Unity.

## Hitos de programación logrados

- Se ha hecho uso efectivo de eventos y estructuras singleton para la comunicación entre objetos.
- Se ha hecho uso de scriptable objects para la gestión de datos.
- Se ha hecho uso de pooling para la gestión de objetos muy repetidos (enemigos, materiales, etc).
- Se ha hecho uso de simulaciones físicas integradas con el sistema de agarre.
- Se han preparado Shaders y efectos de partículas satisfactorios.
- Se han implementado mecanismos de interacción de VR (teletransporte, botones, objetos).

## Aspectos destacables de la aplicación

Hay muchos sistemas que consideraríamos destacables, pero es especialemente relevante destacar las decisiones de diseño y sistemas que se han desarrollado para acomodar una experiencia en realidad virtual:

- El escenario está dispuesto de tal manera que no es necesario girarse con urgencia y se puede ver todo el terreno de juego.
- A pesar de esto, el juego dispone de una reducida zona para el jugador, navegable mediante diferentes sistemas de movimiento cómodos en VR.
- Acciones como disparar tienen reacciones hápticas que ayudan a la inmersión.

## Distribución del trabajo

### Reparto de tareas básico

Inicialmente, se realizó la siguiente división del trabajo:

* Dario Cerviño Luridiana: sistema controlador de los enemigos (algoritmo A*)

* Enrique Viña Alonso: controladores

* Juan Salvador Magariños Alba: sistemas de fabricación de pociones e inventario

### Tareas desarrolladas individualmente

Adicionalmente, cada integrante llevó a cabo las siguientes tareas:

* Dario Cerviño Luridiana: creación de efectos especiales, pooling de objetos, terreno enemigo

* Enrique Viña Alonso: sistemas de armas, coordinación y testing

* Juan Salvador Magariños Alba: modelado de los ítems, pociones y la torre, creación de shaders

### Tareas desarrolladas en grupo

Las siguientes tareas fueron hechas por dos o más personas:

* Creación de sistemas de instanciación y pooling de objetos

* Mucho debug


Intentamos seguir un workflow de gitflow, pero degeneró en algo más caótico.
