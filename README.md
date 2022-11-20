# Delmar.Jeronimo.SegundoParcial

### Title: *"Simple Truco"*

### Resume:

The program consists in a multi-thread app which can create multiple, virtually unlimited, rooms where 2 "bots" plays Truco against each other using a very simplistic version of the card game *Truco*.
When the game starts it tries to connect to the Data Base and load all the players there to the game, and then automatically creates 1 room where 2 bots are chosen to play against each other, in a way to show what the program is about.
When one presses double-click on the room, a new form opens and allows the user to see what's going on inside of the room. - The form can be moved around the screen without affecting the game and can be even closed without this stopping the game inside of the room.

Games are set to 4 rounds, each of which consists of 3 hands or less, depending on if someone wins the round before reaching the 3rd hand or not.

All games can be closed either from "outside", at the Lobby, or from "inside" at the **view form**.
Once a game finishes, the visual representation of the room at the Lobby changes to reflect this, same as the name of the room and the title of the view form. - A timer of 120 seconds long starts then, allowing the user to explore the events of the game in the text box if they want, then without mattering if the view form is opened or closed, the same is disposed and the game removed from the list of active games, putting it apart for the GC to take care of it.

The very moment the game finishes, it updates the statistics of the players and then tries to update them at the Data Base.

The user can opt for creating any number of new players, which are immediately uploaded to the Data Base the very moment they are created, and in the same way they can delete any player.

**RULES:**

The rules used in this version of Truco are utterly basic.
The players can call both Envido and Truco at any point, but they can't call Truco if Envido hasn't been called previously.
They may call things and play cards unintelligently, simply based on chances, so there are no smart moves at all. - Answers to the calls are also random.
At the very bottom of all this, there's no even any kind of individuality for the "players", since they are anything but names. Everything is just a set of methods and variables.

If Envido is called and not wanted, the caller earns 1 point. If Truco is called and not wanted, the caller earns 1 point and the round ends with the caller standing as the winner of that round.
If Envido is wanted, then the points of the hands of the players is compared and the winner gets the point. - If the round ends and Truco has been called and wanted at some point, the winner of the round earns 2 points.
If nothing is called at any stance of the game, the player that wins 2 hands out of 3 earns the point of the round.
