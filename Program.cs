/*********************************************************************
** Program Filename: BlackJack
** Author: Matt Hans
** Date: 5/16/2019
** Description: This programs simulates the casino game blackjack, users can make wagers and compete against the house to earn winnings
** Input: wagers and starting amount of capital, also 'y' or 'n' indicated to hit from the dealer or not and to continue to play or not
** Output: cards that are being drawn with each hand as well as player score and total bankroll
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            int startingCash;
            int wager;
            bool validInput;
            bool keepPlaying = true;
            char userInput;

            //introduction to the game, specifies the game type and also the table minimum and maximum
            Console.WriteLine("Welcome to the blackjack table!");
            Console.WriteLine("There is a $100,000 limit for starting, and the table minimum is $50");
            Console.WriteLine();

            do
            {
                //allows user to specifiy the amount of money the start with provided it is within table rules and a valid integer
                Console.WriteLine("How much money would you like to play with?");

                validInput = Int32.TryParse(Console.ReadLine(), out startingCash);
                if (!validInput || startingCash < 50 || startingCash > 100000)
                {
                    Console.WriteLine("Please enter a value between 50 and 100000");
                    Console.WriteLine();
                }
            } while (!validInput || startingCash < 50 || startingCash > 100000);

            //initializes game object and passes the user specified starting bankroll
            Game game = new Game(startingCash);
            //initialies the deck and shuffles it for gameplay
            Deck deck = new Deck();
            deck.shuffleDeck();
           
            while (startingCash >= 50 && keepPlaying == true)
            {
                //gets user input in how much they would like to wager
               do
                {
                    Console.WriteLine("How much would you like to wager?");
                    validInput = Int32.TryParse(Console.ReadLine(), out wager);
                    Console.WriteLine();

                    if (!validInput || wager > game.getBankroll() || wager < 50)
                    {
                        Console.WriteLine("Please enter a value between 50 and " + game.getBankroll());
                        Console.WriteLine();
                    }
                } while (!validInput || wager < 50 || wager > game.getBankroll());
                
                //plays the hand using deck object and with user specified wager
                game.playHand(deck, wager);

                //informs player that they can no longer play once the drop below the table minimum in bankroll
                if (game.getBankroll() < 50)
                {
                    keepPlaying = false;
                    Console.WriteLine("You no longer have the minimum requirement of money to play this table");
                    Console.WriteLine("Final Bankroll: $" + game.getBankroll());
                    Console.WriteLine("Enter any key exit the program");
                    Console.ReadKey();
                    break;
                }

                do
                {
                    //asks player if they would like to continue to play, if so this loop repeats
                    Console.WriteLine("Would you like to keep playing? [y/n]");
                    userInput = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    Console.WriteLine();

                    if (Char.ToLower(userInput) != 'y' && Char.ToLower(userInput) != 'n')
                    {
                        Console.WriteLine("Invalid entry please key in 'y' to continue or 'n' to quit");
                    }
                } while (Char.ToLower(userInput) != 'y' && Char.ToLower(userInput) != 'n');

                if (userInput == 'n')
                {
                    keepPlaying = false;

                    Console.WriteLine("Final Bankroll: $" + game.getBankroll());
                    Console.WriteLine("Enter any key exit the program");
                    Console.ReadKey();
                }
            }
        }
    }
}
