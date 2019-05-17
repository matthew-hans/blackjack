using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//enumerator to represent the different types of cards in a deck
enum CardTypes
{
    ace = 1,
    two,
    three,
    four,
    five,
    six,
    seven,
    eight,
    nine,
    ten,
    jack,
    queen,
    king
}
//enumerator used to represent the 4 suits of cards
enum CardSuits
{
    hearts,
    diamonds,
    spades,
    clubs
}

namespace BlackJack
{
    class Card
    {
        private CardTypes name;
        private CardSuits suit;
        private int value;

/********************************************************************************************************************************************************
 ******** Functions: default constructor and setter functions
 ******** Description: used  to allow card objects to receive appropriate characteristics
 ******** Parameters: cardtype(enum), string to represent suit, int to represent the value
 ******** Pre-Conditions: none
 ******** Post-Conditions: card has appropriate attributes
 ********************************************************************************************************************************************************/
        public Card()
        {

        }
        public void setName(CardTypes name)
        {
            this.name = name;
        }

        public void setSuit(CardSuits suit)
        {
            this.suit = suit;
        }

        public void setValue(int value)
        {
            this.value = value;
        }

/********************************************************************************************************************************************************
 ******** Functions: getter functions
 ******** Description: used to access the card types and values
 ******** Parameters: none
 ******** Pre-Conditions: card has attributes assigned to it
 ******** Post-Conditions: none
 ********************************************************************************************************************************************************/

        public CardTypes getName()
        {
            return name;
        }
        public int getValue()
        {
            return value;
        }
    }
}
