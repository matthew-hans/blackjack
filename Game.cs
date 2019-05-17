using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Game
    {
        private int dealerScore;
        private int playerScore;
        private int deckIdx;
        private int playerEarnings;

/********************************************************************************************************************************************************
 ******** Function: Game default construtor
 ******** Description: initializes the game variables and assigns the amount of starting capital
 ******** Parameters: integer representing the amount of starting capital
 ******** Pre-Conditions: none
 ******** Post-Conditions: game variables are now initialized and ready to be used
 ********************************************************************************************************************************************************/
        public Game(int startingCash)
        {
            this.dealerScore = 0;
            this.playerScore = 0;
            this.deckIdx = 0;
            this.playerEarnings = startingCash;
        }
/********************************************************************************************************************************************************
 ******** Function: playHand
 ******** Description: plays a hand of blackjack, allowing user to choose when to hit, dealer hits until they reach 17
 ******** Parameters: deck object and an integer for the amount the user is betting
 ******** Pre-Conditions: user has starting capital and other game variables initialized, deck object has been created
 ******** Post-Conditions: flow of gameplay is printed to screen
 ********************************************************************************************************************************************************/

        public void playHand(Deck deck, int bet)
        {
            //if we get to the halfway point of the deck then it is shuffled and the index is reset to avoid indexing out of range
            if (deckIdx > 25)
            {
                deck.shuffleDeck();
                deckIdx = 0;
            }

            bool playerBust = false;
            bool addCard = true;
            char input;

            //the 4 cards the begin a blackjack hand are drawn and assigned
            Card dealerDraw = deck.drawCard(deckIdx++);
            Card playerDraw = deck.drawCard(deckIdx++);
            Card dealerDrawTwo = deck.drawCard(deckIdx++);
            Card playerDrawTwo = deck.drawCard(deckIdx++);

            //next draw variable to be used for continued draws from the deck
            Card nextDraw;

            //sets the scores of the dealer and the player
            playerScore = playerDraw.getValue() + playerDrawTwo.getValue();
            dealerScore = dealerDraw.getValue() + dealerDrawTwo.getValue();

            //displays one of the dealer cards to console
            Console.WriteLine("The dealer has drawn a " + dealerDrawTwo.getName());
            Console.WriteLine("Their first card is hidden");
            Console.WriteLine();

            //displays the two cards player has received on their draws as well as their current score
            Console.WriteLine("Your hand: ");
            Console.WriteLine(playerDraw.getName());
            Console.WriteLine(playerDrawTwo.getName());
            Console.WriteLine();

            Console.WriteLine("Your current score is " + playerScore);
            do
            {
                //asks the player if the would like to add another card to try and increase their score
                Console.WriteLine("Would you like to add another card? [y/n]");
                input = Console.ReadKey().KeyChar;
                Console.WriteLine();
                Console.WriteLine();

                //if player wants another card it is drawn from the deck and both scores are now displayed
                if (Char.ToLower(input) == 'y')
                {
                    nextDraw = deck.drawCard(deckIdx++);
                    playerScore += nextDraw.getValue();
                    Console.WriteLine("You have drawn a " + nextDraw.getName());
                    Console.WriteLine("Player Score: " + playerScore);
                    Console.WriteLine("Dealer Score: " + dealerScore);
                    Console.WriteLine();

                    //if the players hand now exceeds 21 they have lost this hand to the dealer
                    if (playerScore > 21)
                    {
                        Console.WriteLine("You have busted, the dealer wins");
                        playerEarnings -= bet;
                        Console.WriteLine("Total bankroll: $" + playerEarnings);
                        Console.WriteLine();
                        playerBust = true;
                        addCard = false;
                    }
                }

                //breaks out of loop if player does not want any more cards
                else if (Char.ToLower(input) == 'n')
                {
                    addCard = false;
                }
                else
                {
                    Console.WriteLine("Invalid response, please enter 'y' to add another card or 'n' to stay");
                }

            } while (input != Char.ToLower('y') && input != Char.ToLower('n') || addCard == true);

            //only continues to dealer if the player has not busted, meaning exceeded 21
            if (playerBust == false)
            {
                //the dealer will not aquire more cards if they have at least a 17
                if (dealerScore >= 17)
                {
                    Console.WriteLine();
                    evalWin(playerScore, dealerScore, bet); //evaluates both scores
                }
                else
                {
                    //continues to draw cards and display current scores while the dealer has a score below 17
                    while (dealerScore < 17)
                    {
                        nextDraw = deck.drawCard(deckIdx++);
                        dealerScore += nextDraw.getValue();
                        Console.WriteLine("The dealer has drawn a " + nextDraw.getName());
                        Console.WriteLine("Player Score: " + playerScore);
                        Console.WriteLine("Dealer Score: " + dealerScore);
                        Console.WriteLine();

                        //if dealer accidently exceeds 21 then the player wins the hand
                        if (dealerScore > 21)
                        {
                            Console.WriteLine("The dealer has busted, you have won this hand!");
                            playerEarnings += bet;
                            Console.WriteLine("Total bankroll: $" + playerEarnings);
                            Console.WriteLine();
                        }
                    }
                    //evaluates both scores after dealer has aquired more cards but only if they have not exceeded 21
                    if (dealerScore <= 21)
                    {
                        evalWin(playerScore, dealerScore, bet);
                    }
                }
            }
        }

/********************************************************************************************************************************************************
 ******** Function: evalWin helper function
 ******** Description: function determines who the winner is and adjusts bankroll accordinly
 ******** Parameters: 3 integers, 1 for the player score, 1 for the dealer score and 1 for the wager made
 ******** Pre-Conditions: none
 ******** Post-Conditions: bankroll adjusted accordingly based on outcome
 ********************************************************************************************************************************************************/

        public void evalWin(int pScore, int dScore, int wager)
        {
            //diplays scores and credits the player with their winnnings if they have the higher score
            if (pScore > dScore)
            {
                Console.WriteLine("Player Score: " + playerScore);
                Console.WriteLine("Dealer Score: " + dealerScore);
                Console.WriteLine("You have won this hand!");
                playerEarnings += wager;
                Console.WriteLine("Total bankroll: $" + playerEarnings);
                Console.WriteLine();
            }
            //displays scores and removes the wager from the players bankroll if they have the lower score
            else if (pScore < dScore)
            {
                Console.WriteLine("Player Score: " + playerScore);
                Console.WriteLine("Dealer Score: " + dealerScore);
                Console.WriteLine("The dealer has won this hand");
                playerEarnings -= wager;
                Console.WriteLine("Total bankroll: $" + playerEarnings);
                Console.WriteLine();
            }
            //nobody loses anything in a tie
            else
            {
                Console.WriteLine("This hand is a tie");
                Console.WriteLine("Total bankroll: $" + playerEarnings);
            }
        }

/********************************************************************************************************************************************************
 ******** Function: getBankroll
 ******** Description: returns the players current bankroll
 ******** Parameters: none
 ******** Pre-Conditions: none
 ******** Post-Conditions: none
 ********************************************************************************************************************************************************/

        public int getBankroll()
        {
            return playerEarnings;
        }
    }
}
