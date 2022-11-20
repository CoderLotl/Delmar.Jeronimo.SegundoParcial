# Delmar.Jeronimo.SegundoParcial

---

### Title: *"Simple Truco"*

---

* Resume:

The program consists in a multi-thread app which can create multiple, virtually unlimited, rooms where 2 "bots" plays Truco against each other using a very simplistic version of the card game *Truco*.
When the game starts it tries to connect to the Data Base and load all the players there to the game, and then automatically creates 1 room where 2 bots are chosen to play against each other, in a way to show what the program is about.
When one presses double-click on the room, a new form opens and allows the user to see what's going on inside of the room. - The form can be moved around the screen without affecting the game and can be even closed without this stopping the game inside of the room.

Games are set to 4 rounds, each of which consists of 3 hands or less, depending on if someone wins the round before reaching the 3rd hand or not.

All games can be closed either from "outside", at the Lobby, or from "inside" at the **view form**.
Once a game finishes, the visual representation of the room at the Lobby changes to reflect this, same as the name of the room and the title of the view form. - A timer of 120 seconds long starts then, allowing the user to explore the events of the game in the text box if they want, then without mattering if the view form is opened or closed, the same is disposed and the game removed from the list of active games, putting it apart for the GC to take care of it.

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

It's used to read data from the Data Base and write data on the Data Base, specifically related to Players.
At the beginning of the application the same creates an instance of the Data Access class to read all the players from the Data Base, and then it's used again every time any operation of writing or reading data to or from the Data Base is performed.

### EXCEPTIONS

It's lightly used in the program, given the fact that most methods are fail-proof in the tightly controlled and unflexible data environment.
This becomes handy, anyway, when some aspects are impossible to be accurately forseen and controlled, like when then program needs to contact the Data Base, or when it needs to make use of some file which its existence is not granted.
If by any chance these methods happen to fail, they will throw an exception which will cause a message box to promp and warn the user about the situation.
This subject is therefore used in 2 classes: Data Access (used for SQL querys) and Data Serialization (for serializing and deserializing data to and from files).

### GENERICS

This subject is actively used and actually a core aspect of the serialization and deserialization of data.
Although the game makes no use of it for anything other than cards, for when it has to make a new deck for a new game, the whole class is prepared for interacting with any kind of data, which it can serialize down to a file or deserialize back from a file to data.

NOTE: ... and although I didn't use this any further in this TP, in the moments of boredom when I switched to personal projects I made a wide use of this.
This is just a color note here; I wanted to thank you for this.

### SERIALIZATION AND FILE WRITING

Although these are 2 different subjects and processes, in my case they are used together in a same method. - When I need to write data down, the data is serialized to a formatted string of text, and then it's written down in the same method to the file I need to write. Then, in a different method for when I need to a load a file, I first read the file and then I deserialize it back to data.
This works together with **Generics**, as I said before. Even if I don't make a real use of Generics, the potential for writing any kind of data if I truly wanted (Players, single cards, or text from the logs) is there.

### INTERFACES

This is a rather controvertial subject in my program. - Based on the previous experiences and reviews, where I was said that my program lacked of flexibility and possibilities for an expansion in a future, I wrote this program thinking from the start in the possibility of a future expansion.




