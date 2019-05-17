using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlackJack
{
    class Deck
    {
        private Card[] deckArr = new Card[52];
        static Random _random = new Random();

/********************************************************************************************************************************************************
 ******** Function: Deck default constructor
 ******** Description: assigns properties to deck array consisting of 52 Card objects
 ******** Parameters: none
 ******** Pre-Conditions: Card array must be initialized
 ******** Post-Conditions: each Card object in the array now has its own unique properties
 ********************************************************************************************************************************************************/
        public Deck()
        {

            for (int i = 0; i < 52; i++)
            {
                deckArr[i] = new Card();
            }
            int index = 0;

            //double for loop, outer assigns suits and inner assigns the type of the card
            for (CardSuits suit = CardSuits.hearts; suit <= CardSuits.clubs; suit++)
            {
                for (CardTypes type = CardTypes.ace; type <= CardTypes.king; type++)
                {
                    deckArr[index].setName(type);
                    deckArr[index].setValue((int)type);
                    
                    //for the purpose of blackjack all face cards have a value of 10 
                    if((int) type > 10)
                    {
                        deckArr[index].setValue(10);
                    } 
                    index++;
                }
            }
        }

/********************************************************************************************************************************************************
 ******** Function: shuffleDeck
 ******** Description: randomely organizes the Card array via the fisher yates shuffle
 ******** Source: https://www.dotnetperls.com/fisher-yates-shuffle
 ******** Parameters: none
 ******** Pre-Conditions: Card array must be initialized
 ******** Post-Conditions: The deck is now shuffled and ready for gameplay
 ********************************************************************************************************************************************************/
        public void shuffleDeck()
        {
            int n = deckArr.Length;
            for (int i = 0; i < n; i++)
            {
                int r = i + _random.Next(n - i);
                Card c = deckArr[r];
                deckArr[r] = deckArr[i];
                deckArr[i] = c;
            }
        }
/********************************************************************************************************************************************************
 ******** Function: drawCard
 ******** Description: returns a Card object from within the deck based on an index
 ******** Parameters: index of the card within the deck
 ******** Pre-Conditions: Card array must be initialized
 ******** Post-Conditions: returns the Card object at specified index
 ********************************************************************************************************************************************************/
        public Card drawCard(int index)
        {
            return deckArr[index];
        }
    }
}
