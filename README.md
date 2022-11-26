# Delmar.Jeronimo.SegundoParcial

---

[English](#english) :gb:

[Español](#español) :es:

---
---
# English

### Title: *"Simple Truco"*

---

* Resume:

The program consists in a multi-thread app which can create multiple, virtually unlimited, rooms where 2 "bots" plays Truco against each other using a very simplistic version of the card game *Truco*.
When the game starts it tries to connect to the Data Base and load all the players there to the game, and then automatically creates 1 room where 2 bots are chosen to play against each other, in a way to show what the program is about.
When one presses double-click on the room, a new form opens and allows the user to see what's going on inside of the room. - The form can be moved around the screen without affecting the game and can be even closed without this stopping the game inside of the room.

Games are set to 4 rounds, each of which consists of 3 hands or less, depending on if someone wins the round before reaching the 3rd hand or not.

All games can be closed either from "outside", at the Lobby, or from "inside" at the **view form**.
Once a game finishes, the visual representation of the room at the Lobby changes to reflect this, same as the name of the room and the title of the view form. - A timer of 10 seconds long starts then, allowing the user to explore the events of the game in the text box if they want, then without mattering if the view form is opened or closed, the same is disposed and the game removed from the list of active games, putting it apart for the GC to take care of it.

The very moment the game finishes, it updates the statistics of the players and then tries to update them at the Data Base.

The user can opt for creating any number of new players, which are immediately uploaded to the Data Base the very moment they are created, and in the same way they can delete any player.

* RULES:

The rules used in this version of Truco are utterly basic.
The players can call both Envido and Truco at any point, but they can't call Truco if Envido hasn't been called previously.
They may call things and play cards unintelligently, simply based on chances, so there are no smart moves at all. - Answers to the calls are also random.
At the very bottom of all this, there's no even any kind of individuality for the "players", since they are anything but names. Everything is just a set of methods and variables.

If Envido is called and not wanted, the caller earns 1 point. If Truco is called and not wanted, the caller earns 1 point and the round ends with the caller standing as the winner of that round.
If Envido is wanted, then the points of the hands of the players is compared and the winner gets the point. - If the round ends and Truco has been called and wanted at some point, the winner of the round earns 2 points.
If nothing is called at any stance of the game, the player that wins 2 hands out of 3 earns the point of the round.

---

## **CLASS DIAGRAM**
![alt text](ClassDiagram1.png)

---

* Subjects and their use:

### SQL

It's used to read data from the Data Base and write data on the Data Base, specifically related to Players and the Matches.
At the beginning of the application the same creates an instance of the Data Access class to read all the players from the Data Base, and then it's used again every time any operation of writing or reading data to or from the Data Base is performed.

### EXCEPTIONS

Exceptions are used every time the execution of some method or code has a chance of throwing an exception in their normal running due to an unexpected issued, which is contemplated and properly caught.
In most cases, this ends informing the user about the issue when the exception may alter the normal running and flow of the application. Some exceptions may prevent some features of being executed. - At some other cases, the exceptions are dealt with by returning some value to other areas of the program.

There's an specific case in which a custom exception had to be created and implemented, due to the fact that the specific case was not contemplated by the language. This is the case of empty decks.

### GENERICS

This subject is actively used and actually a core aspect of the serialization and deserialization of data.
Although the game makes no use of it for anything other than cards and logs of matches, for when it has to make a new deck for a new game or to save a class containing the data of the finished match, the whole class is prepared for interacting with any kind of data, which it can serialize down to a file or deserialize back from a file to data.

### SERIALIZATION AND FILE WRITING

Although these are 2 different subjects and processes, in my case they are used together in a same method. - When I need to write data down, the data is serialized to a formatted string of text, and then it's written down in the same method to the file I need to write. Then, in a different method for when I need to a load a file, I first read the file and then I deserialize it back to data.
This works together with **Generics**, as I said before. Althoug I make a limited use of Generics, the potential for writing any kind of data if I truly wanted (Players, single cards, or text from the logs) is there.

Currently it's used for the decks, and for making a copy of the matches's logs if there's no connection with the server.

### INTERFACES

Based on the previous experiences and reviews, where I was said that my program lacked of flexibility and possibilities for an expansion in a future, I wrote this program thinking from the start on the possibility of a future expansion.
For this I made "Game" as an abstract class. Every room contains a game, but not every game is the same. But every game shares some basic, raw concepts with every other possible game.
Since C# doesn't allow multi-inheritance, I created then interfaces for Cards and Dices games; may the need ever arise for expanding the program in those directions, the implementation of such kind of games would be smooth, and wouldn't require any rewriting of the core aspects of the program. - For Cards, again, I created a single kind of class, which is Truco.

So my class GameTruco inherits from Game and ICard. It has the basic aspects of a game (a start and an end) and the basic aspects of a card game (shuffle a deck, get cards, play cards, return cards to the deck, etc.).

If I ever happen to make other games, I could either group them by their interfaces, or modify them by modifying something in their interfaces, and this wouldn't affect all games but just those that would inherit from those particular interfaces.

### DELEGATES

They are used for storing methods of the forms and then using them in the Class Library at some specifict points, generally related to the prompting of message boxes or performing changes on controls nested in the forms.
Another use of delegates is for when a control is affected from a thread different to which the control has been created on; there I need to execute an invokation inside of which I need to declare a delegate with the piece of code I need to execute to then run it in the thread the control belongs to.

### TASKS

The very 1st half of the core of the program.
Every room in this program is an object, which contains a game. I made that clear before. - Inside of the room, the very moment the room is instanced, the constructor defines a task which is launched right away and contains the method 'Play' of whatever game the room has instanced inside. So the moment a room is created, the game inside is started to be played. A thread for every room.

Given the fact that this is one of the first aspects of the program I tackled, and given I did it by my own, the implementation of this subject differs from the cathedra. I don't start the task with a Canellation Token passed by parameter, but the Cancellation Token is **inside** the room as an attribute and a property, and I can access it from outside through a method.
So if I ever need to stop the game, I just run the method of the particular room, and the game stops.

### EVENTS

Another early-tackled subject, this is the 2nd half of the core of the program.
The approach has 2 examples here. The first, quite simple, is a notification of changes whenever something is added to the game's log. This is heard by a method of the view form designated to watch that specific game, which in turn refreshes the text box that acts as a monitor for the game.
The second approach is more intrincate, and happens when the game finishes, wich fires an event that tells the view form to start the countdown. This is heard at the Lobby too, at which the visual representation of the room in the tree view changes to show the state of the game.
When the countdown finishes, **the form unsubscribes itself from the game**, and then proceeds to close itself.

### UNIT TESTING

I made tests for the SQL connection, the data serialization and deserialization, and some of the methods of the game.

---

---

# Español

### Título: *"Simple Truco"*

---

* Resumen:

El programa consiste en una aplicación multi-hilo la cual puede crear múltiples, virtualmente ilimitados, cuartos en donde 2 "bots" juegan Truco el uno contra el otro usando una versión muy simplista del juego de cartas *Truco*.
Cuando el juego comienza intenta conectarse con la Base de Datos y cargar todos los jugadores allí al juego, y luego automáticamente crea 1 cuarto en donde 2 bots son elegidos para jugar el uno contra el otro, de forma que se pueda ver de qué trata el programa.
Cuando uno presiona doble-click en el cuarto, un nuevo formulario se abre y permite al usuario ver qué está ocurriendo dentro del cuarto. - El formulario puede ser movido por la pantalla sin afectar el juego y puede incluso ser cerrado sin que esto detenga el juego dentro del cuarto.

Los juegos están seteados a 4 rondas, cada una consiste de 3 manos o menos, dependiendo de si alguien gana la ronda antes de alcanzar la 3er mano o no.

Todos los juegos pueden ser cerrados desde "afuera", en el Lobby, o desde "adentro" en el **formulario de vista**.
Ona vez que un juego finaliza, la representación visual del cuarto en el Lobby cambia para reflejar esto, lo mismo que el nombre del cuarto y el título del formulario de vista. - Un timer de 10 segundos comienza entonces, permitiendo al usuario explorar los eventos del juego en el cuadro de texto si él/ella/elle quiere, luego sin importar si el formulario de vista está abierto o cerrado, el mismo es descatado y el juego removido de la lista de juegos activos, colocándolo aparte para que el Garbage Collector se encargue de él.

En el mismo momento que el juego finaliza, éste actualiza las estadísticas de los jugadores y luego intenta actualizarlas en la Base de Datos.

El usuario puede optar por crear cualquier número de jugadores nuevos, los cuales son inmediatamente cargaods a la Base de Datos en el momento en que son creados, en la misma que también se puede borrar cualquier jugador.

* REGLAS:

Las reglas usadas en esta versión de Truco son sumamente básicas. Los jugadores pueden cantar tanto Envido como Truco en cualquier punto, pero no pueden cantar Truco si Envido no ha sido cantado previamente. Cantarán y jugarán cartas de forma no inteligente, simplemente basándose en chances, así que no hay para nada jugadas inteligentes. - Las respuestas a los cantos también son aleatorias. En el fondo de todo esto, no hay ningún tipo de individualidad para los "jugadores", ya que ellos no son otra cosa que nombres. Todo son simplemente un juego de métodos y variables.

Si Envido fue cantado y no querido, el que canta gana 1 punto. Si Truco fue cantado y no querido, el que canta gana 1 punto y la ronda termina con el que cantó saliendo como ganador de la ronda. Si Envido es querido, entonces los puntos de las manos de los jugadores son comparados y el ganador gana el punto. - Si la ronda termina y Truco ha sido cantado y querido en algún punto, el ganador gana 2 puntos. Si nada fue cantado en ninguna instancia del juego, el jugador que gana 2 manos de 3 gana el punto de la ronda.

---

## **DIAGRAMA DE CLASES**
![alt text](ClassDiagram1.png)

---

* Temas y su uso:

### SQL

Es utilizado para leer datos de la Base de Datos y escribir datos en la Base de Datos, específicamente relacionado a los Jugadores y las Partidas.
Al comienzo mismo de la aplicación la misma crea una instancia de la clase Data Access para leer todos los jugadores de la Base de Datos, y luego es usado de nuevo en cada operación de escritura o lectura de datos desde o en la Base de Datos.

### EXCEPCIONES

Las excepciones son usadas cada vez que la ejecución de algún método o código tiene algúna  chance de tirar una excepción en su normal funcionamiento debido a problema inesperado, lo cual es contemplado y capturado apropiadamente.
En la mayoría de los casos, esto termina informando al usuario acerca del inconveniente cuando la excepción altere el normal desempeño y flujo de la aplicación. Algunas excepciones pueden prevenir ciertas características de ser ejecutadas. - En algunos casos, las excepciones son tratadas devolviendo algún valor a otras áreas del programa.

Hay un caso particular en el cual una excepción personalizada debió ser creada e implementada, debido a que el caso específico no estaba contemplado por el lenguaje. Este es el cazo de los mazos vacíos.

### GENÉRICOS

Este tema es activamente usado y actualmente un aspecto principal de la serialización y deserialización de datos.
Aunque el juego no hace uso de ello para otra cosa distinta a las cartas y los logs de las partidas, para cuando tiene que hacer un nuevo mazo para un nuevo juego o cuando tiene que guardar una glase conteniendo los datos de la partida finalizada, la clase entera (la clase serializadora) está preparada para interactuar con cualquier tipo de datos, los cuales puede serializar en un archivo o deserializar de regreso de un archivo a datos.

### SERIALIZACIÓN Y ESCRITURA DE ARCHIVOS

Aunque estos son 2 diferentes temas y procesos, en mi caso son usados de forma conjunta en un mismo método. - Cuando necesito escribir datos, los datos son serializados a una cadena de texto formateada, y luego es escrita en el mismo método al archivo que necesito escribir. Luego, en un método diferente para cuando necesito cargar un archivo, primero leo el archivo y luego lo deserealizo de nuevo a datos.
Esto funciona conjuntamente con **Genéricos**, como dije antes. Aunque hago un uso limitado de Genéricos, el potencial para escribir cualquier tipo de data si realmente lo quisiera (jugadores, cartas individuales, o texto de los logs) está ahí.

Actualmente se utiliza para los mazos, y para realizar una copia de los logs en caso de no haber conexión con el servidor.

### INTERFACES

Basándome en las experiencias previas y revisiones, en donde se me ha dicho que mi programa carecía de flexibilidad y posibilidades de expansión en el futuro, escribí este programa pensando desde el inicio en la posibilidad de una futura expansión.
Para esto hice "Juego" como una clase abstracta. Cada habitación/cuarto contiene un juego, pero no todos los juegos son lo mismo. Pero todos los juegos compartes algunos principios básicos, conceptos rústicos con cada otro juego posible.
Debido a que C# no permite multi-herencia, creé entonces interfaces para Cartas y Dados; acaso la necesidad para expandir el programa en esas direcciones alguna vez llegue, la implementación de tales tipos de juegos sería suave, y no requeriría ninguna reescritura de los aspectos fundamentales del programa. - Para Cartas, de nuevo, creé un único tipo de clase, la cual es Truco.

Entonces mi clase GameTruco hereda de Game y de ICard. Tiene los aspectos básicos de un juego (un comienzo y un final) y los aspectos básicos de un juego de cartas (mezclar un mazo, obtener cartas, jugar cartas, devolver cartas al mazo, etc.).

Si alguna vez necesito hacer otros juegos, podría agruparlos por interfaces, o modificarlos al modificar algo en sus interfaces, y esto no afectaría a todos los juegos, tan sólo a aquellos que hereden de la interface particular que fue modificada.

### DELEGADOS

Son utilizados para almacenar métodos de los formularios y luego utilizarlos en la Biblioteca de Clases en puntos específicos, generalmente relacionados con lanzar ventanas con mensajes o realizar cambios en los controles anidados en los formularios.
Otro uso de delegados es para cuando un control es afectado desde un hilo diferente al cual el control fue creado; allí necesito ejecutar una invocación dentro de la cual debo declarar un delegado con la pieza de código que necesito ejecutar para luego correrlo en el hilo al cual el control pertenece.

### TAREAS

La mismísima 1er mitad del núcleo del programa.
Cada cuarto en este programa es un objeto, el cual contiene un juego. Eso ha sido aclarado antes. - Dentro de cada cuarto, en el mismo momento en que el cuarto es instanciado, el constructor define una tarea la cual es lanzada inmediatamente y contiene el método 'Play' de cualquier juego que el cuarto haya instanciado dentro. Entonces en el momento en que el cuarto es creado, el juego dentro se comienza a jugar. Un hilo por cada cuarto.

Dado el hecho de que esto es uno de los aspectos del programa que tackleé primero, y debido a que lo hice por mi propia cuenta, la implementación de este tema difiere de la cátedra. No comienzo la tarea con un Cancellation Token pasado por parámetro, en cambio el Cancellation Token está **dentro** del cuarto como un atributo y una propiedad, y puedo accederlo desde afuera a través de un método.
Entonces si en algún momento necesito detener el juego, tan sólo corro el método del cuarto en particular, y el juego se detiene.

### EVENTOS

Otro tema tackleado de forma temprana, ésto es la 2da mitad del núcleo del programa.
El enfoque tiene 2 ejemplos aquí. El primero, bastante simple, es una notificación de cambios cada vez que algo es agregado al log del juego. Es oído por el método del formulario de vista designado a observar el juego específico, el cual refresca el cuadro de texto que actúa como monitor para el juego.
El segundo enfoque es más intricando, y ocurre cuando el juego finaliza, lo cual dispara un evento que le dice al formulario de visualización que comience una cuenta regresiva. Esto es oído en el Lobby también, en el cual la representación visual del cuarto en la vista de árbol cambia para mostrar el estado del juego.
Cuando la cuenta regresiva finaliza, *el formulario de desubscribe a sí mismo del juego**, y luego procede a cerrarse a sí mismo.

### UNIT TESTING

Hice pruebas para la conexión SQL, la serialización y deserialización de datos, y algunos de los métodos del juego.
