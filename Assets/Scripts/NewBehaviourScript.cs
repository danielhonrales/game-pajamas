using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomNumberGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Random gen = new Random();
            int number;
            bool over = false;
            string userChoice;
            int guess = 0;
            int counter = 0;

            while (!over)
            {
                over = true;
                Console.WriteLine("1 - Easy: 1 - 10");
                Console.WriteLine("2 - Medium: 1 - 50");
                Console.WriteLine("3 - Hard: 1 - 100");
                Console.Write("Please input the number of the difficulty you wish to  play: ");
                string str = Console.ReadLine();

                switch (str)
                {
                    case "1":
                        Console.WriteLine("You chose easy difficulty.");
                        number = gen.Next(1, 11);

                        Console.WriteLine("I'm thinking of a number between 1 - 10");
                        for (guess = Convert.ToInt32(Console.ReadLine()); guess != number; guess = Convert.ToInt32(Console.ReadLine()))
                        {
                            if (guess < 1 || guess > 10)
                            {
                                Console.WriteLine("Please enter a number between 1 - 10");
                            }
                            else if (guess < number)
                            {
                                Console.WriteLine("Higher");
                                counter++;
                                Console.WriteLine("You have " + (5 - counter) + " attempts left");
                            }
                            else if (guess > number)
                            {
                                Console.WriteLine("Lower");
                                counter++;
                                Console.WriteLine("You have " + (5 - counter) + " attempts left");
                            }
                            else
                            {
                                Console.WriteLine("You have guessed correctly! It only took you: " + number + " tries.");
                                Console.WriteLine("Would you like to play again? (y/n)");
                                userChoice = Console.ReadLine();
                                if (userChoice == "n")
                                {
                                    Console.WriteLine("Thanks for playing!");
                                    Environment.Exit(0);
                                }
                                else if (userChoice == "y")
                                {
                                    over = false;
                                }
                            }
                            if (counter == 5)
                            {
                                Console.WriteLine("Sorry, you didn't guess it under 5 attempts.");
                                Console.WriteLine("Would you like to play again? (y/n)");
                                userChoice = Console.ReadLine();
                                if (userChoice == "n")
                                {
                                    Console.WriteLine("Thanks for playing!");
                                    Environment.Exit(0);
                                }
                                else if (userChoice == "y")
                                {
                                    over = false;
                                }
                            }
                        }
                        break;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
