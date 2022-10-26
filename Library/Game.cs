using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public enum GameType
    {
        Cards,
        Dices
    }

    public enum GameSubType
    {
        Chinchon,
        Truco,
        Escoba,
        Uno,
        Generala
    }

    public abstract class Game
    {
        public virtual void Play(Room room, string text) { }

        public abstract void EndGame();
        
    }
}
