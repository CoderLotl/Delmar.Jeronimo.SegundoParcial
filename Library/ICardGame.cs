using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public interface ICardGame
    {
        List<Card> GenerateDeck();
        List<Card> ShuffleDeck(List<Card> deck);
        void GiveCards(List<Card> deck, Player player);
        void DrawCard(Player player, List<Card> deck);
        void PlayCard(Player player, List<Card> stack);
        //public void EndGame(List<Card> stack, List<Card> deck);

    }
}
