using System;
using System.Threading;
using System.Speech.Synthesis;
using System.Collections.Generic;

namespace TheMagic
{
    class Program
    {
        /// <summary>
        /// this program is used for determining a random number in the brain of the user in a known range (1:50) ,
        /// by using six arrays which the program asks the user if his number is in those arrays or no.
        /// </summary>


        //list was made to randomize number which represent the rank of the array
        //and also it is used to check if the array was showed or no.
        static List<int> randomizer = new List<int>();

        //start  : variable used to prevent the user from go back when he is in the start of the game.
        //answer : variable used to restore the magic number  .
        //randomizerCount : variable used to determine the data about the back array by knowing its rank.
        static int start, answer, randomizerCount;

        //determine the action after the answer "back" .
        static int answer_back_;

        //list was made to store console colors for the intro and ending.
        static List<ConsoleColor> colors = new List<ConsoleColor>();

        //determine if the answer of the user is yes or no for each array.
        static string isYesOrNo1, isYesOrNo2, isYesOrNo3, isYesOrNo4, isYesOrNo5, isYesOrNo6;

        static string introduction;             //show the introduction of the game.
        static bool skipping = false;           //skip the intro.
        static int x = 166, y = 95, z = 32;     //x and y are ascii codes for the frame , z is the ascii of the space.
        static char ch1InOk;                           //determine if the first character after skipping equals 'o' or no.
        /// <summary>
        /// the program.
        /// </summary>
        static void Main()
        {
            Console.Title = "The Magic";

            //intialized by 1 to show the animation of the frame in the intro
            //for more details go to showTheMagic function.
            start = 1;                  
            
            intro();

            int[,] arr   = { { 1, 3, 5, 7 }, { 9, 11, 13, 15 }, { 17, 19, 21, 23 }, { 25, 27, 29, 33 }, { 35, 37, 39, 41 }, { 43, 45, 47, 49 } };
            int[,] arr2 = { { 2, 3, 6, 7 }, { 10, 11, 14, 15 }, { 18, 19, 22, 26 }, { 27, 30, 31, 34 }, { 35, 38, 39, 42 }, { 43, 46, 47, 50 }, { 23, 66, 58, 71 } };
            int[,] arr4  = { { 4, 5, 6, 7 }, { 12, 13, 14, 15 }, { 20, 21, 22, 23 }, { 28, 29, 30, 31 }, { 36, 37, 38, 39 }, { 44, 45, 46, 47 } };
            int[,] arr8  = { { 8, 9, 10, 11 }, { 12, 13, 14, 15 }, { 24, 25, 26, 27 }, { 28, 29, 30, 31 }, { 40, 41, 42, 43 }, { 44, 45, 46, 47 } };
            int[,] arr16 = { { 16, 17, 18, 19 }, { 20, 21, 22, 23 }, { 24, 25, 26, 27 }, { 28, 29, 30, 31 }, { 48, 49, 50, 85 } };
            int[,] arr32 = { { 32, 33, 34, 35 }, { 36, 37, 38, 39 }, { 40, 41, 42, 43 }, { 44, 45, 46, 47 }, { 48, 49, 50, 67 } };

            //the start of the game
        answer3: 
            //intialize the global variables
            randomizerCount = 1;
            start = 2;
            randomizer.Clear();
            answer = 0;
            answer_back_ = 1;
            isYesOrNo1 = isYesOrNo2 = isYesOrNo3 = isYesOrNo4 = isYesOrNo5 = isYesOrNo6 = "";
            //-------------------------------------------------------------------------------

            showTheMagic();

            Console.SetCursorPosition(60, 15);
            string request = "Guess number between 1 : 50\n";
            writing(request,50);
            Console.WriteLine();
            Thread.Sleep(1500);
            Console.Beep();
            Console.SetCursorPosition(55, 17);
            string order = "if you guessed the number , write \"Ok\"";
            writing(order,50);

            //check if the value of "ch1InOk" == 'o' or no, if true 
            //the program will write 'o' .
            string ok;
            int writeO = 0;
            if(ch1InOk == 'o')
            {
                Console.SetCursorPosition(72, 18);
                Console.Write("o");
                writeO++;
            }
            answer1:
            Console.SetCursorPosition(72 + writeO, 18);
            writeO = 0;
            ok = Console.ReadLine();
            if (ch1InOk == 'o' && ok == "k")
            {
                if (!skipping) goto go;
                ok = "ok";
                Console.SetCursorPosition(72, 18);
                Console.Write("ok");
                ch1InOk = ' ';
            }
            go:
            if (ok.ToLower() == "ok")
            {
                //program starts
                for (int i = 0; i < 6; i++)
                {
                    showTheMagic();
                    getArray(arr, arr2, arr4, arr8, arr16, arr32);
                    answer_back_ = 1;
                }
                showTheMagic();
                thinking();
                showTheMagic();
                //program ends
                if (answer > 50 || answer <= 0) wrongAnswer();
                else correctAnswer();
                
                //determine if the user wants to play again or no
                Console.SetCursorPosition(50, 16);
                string question = "wanna play again or exit ? write \"play\" or \"exit\"";
                writing(question, 50);

                answer9:
                Console.SetCursorPosition(71, 17);
                switch (Console.ReadLine().ToLower())
                {
                    case "play":
                        {
                            answer = 0;
                            goto answer3;
                        }
                    case "exit":
                        {
                            Console.Clear();
                            showTheMagic();
                            leftFrameAnimation(z, z);
                            theMagicFrameAnimation(z, z);
                            rightFrameAnimation(z, z);
                            Thread.Sleep(50);
                            Console.Clear();
                            ending();
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        {
                            Console.SetCursorPosition(67, 17);
                            Console.WriteLine("                                                                          ");
                            Console.SetCursorPosition(67, 17);
                            Console.Write("invalid answer");
                            Thread.Sleep(1000);
                            Console.SetCursorPosition(67, 17);
                            Console.WriteLine("              ");
                            goto answer9;
                        }
                }
            }
            else
            {
                Console.SetCursorPosition(67, 18);
                Console.WriteLine("                                                                          ");
                Console.SetCursorPosition(67, 18);
                Console.WriteLine("invalid answer");
                Thread.Sleep(1000);
                Console.SetCursorPosition(67, 18);
                Console.WriteLine("              ");
                goto answer1;
            }
        }

        /// <summary>
        /// function showing the array
        /// </summary>
        /// <param name="arr"> the array which will be shown </param>
        static void showArray (int[,]arr)
        {
            for (int i = 0,k = 0; i < arr.GetLength(0); i++,k+=2)
            {
                Console.SetCursorPosition(67,k+7 );
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] < 10) Console.Write(arr[i, j] + "   ");
                    else Console.Write(arr[i, j] + "  ");
                }
            }
        }

        /// <summary>
        /// the main function of the game.
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <param name="arr3"></param>
        /// <param name="arr4"></param>
        /// <param name="arr5"></param>
        /// <param name="arr6"></param>
        static void getArray(int [,] arr1 , int[,] arr2 , int[,] arr3 , int[,] arr4 , int[,] arr5 , int[,] arr6)
        {
            string _return;
            int[,] first  = arr1;
            int[,] second = arr2;
            int[,] third  = arr3;
            int[,] fourth = arr4;
            int[,] fifth  = arr5;
            int[,] sixth  = arr6;
            int checkRandomizer;  //store the return of the "randomization" function.
            
        //Randomiztaion array
        Randomiztaion:
            checkRandomizer = randomization();    // make randomizer to get unique number which is the rank of the array chosen

            //-------------------------------------------------->
            //in this section
            // if true in any condition of the inner " if " that means that the array was showed,
            // but if false , the array will be shown.

            //first array
            //this array has no back
            if (checkRandomizer == 1)
            {
                if (randomizer.Contains(1)) 
                {
                    goto Randomiztaion;
                }
                else
                {
                    //add the rank of the array in the randomizer list 
                    //to make sure that the array won't be shown again.
                    randomizer.Add(1);    
                    
                    first = shuffleArray(arr1);
                    showArray(first);
                    _return = answerAboutArray(first);
                    isYesOrNo1 = _return;
                    if (_return == "yes") answer += 1;
                }
            }
            else if (checkRandomizer == 2)
            {
                if (check(2))
                {
                    int[,] BackArr = back_Array(first, second, third, fourth, fifth,sixth);
                    string Back_IsYesOrNo = CheckIsYesOrNo(isYesOrNo1, isYesOrNo2, isYesOrNo3, isYesOrNo4, isYesOrNo5, isYesOrNo6);
                    int increamentBack = incrementOfBackArr();
                    arrRandomizer(2, arr2, ref Back_IsYesOrNo, ref isYesOrNo2, 2, increamentBack, BackArr);
                    randomizerCount = 2;
                }
                   
                else goto Randomiztaion;
            }

            else if (checkRandomizer == 3)
            {
                if (check(3))
                {
                    int[,] BackArr = back_Array(first, second, third, fourth, fifth,sixth);
                    string Back_IsYesOrNo = CheckIsYesOrNo(isYesOrNo1, isYesOrNo2, isYesOrNo3, isYesOrNo4, isYesOrNo5, isYesOrNo6);
                    int increamentBack = incrementOfBackArr();
                    arrRandomizer(3, arr3, ref Back_IsYesOrNo, ref isYesOrNo3, 4, increamentBack, BackArr);
                    randomizerCount = 3;
                }
                else goto Randomiztaion;
            }

            else if (checkRandomizer == 4)
            {
                if (check(4))
                {
                    int[,] BackArr = back_Array(first, second, third, fourth, fifth, sixth);
                    string Back_IsYesOrNo = CheckIsYesOrNo(isYesOrNo1, isYesOrNo2, isYesOrNo3, isYesOrNo4, isYesOrNo5, isYesOrNo6);
                    int increamentBack = incrementOfBackArr();
                    arrRandomizer(4, arr4, ref Back_IsYesOrNo, ref isYesOrNo4, 8, increamentBack, BackArr);
                    randomizerCount = 4;
                }
                else goto Randomiztaion;
            }

            else if (checkRandomizer == 5)
            {
                if (check(5))
                {
                    int[,] BackArr = back_Array(first, second, third, fourth, fifth, sixth);
                    string Back_IsYesOrNo = CheckIsYesOrNo(isYesOrNo1, isYesOrNo2, isYesOrNo3, isYesOrNo4, isYesOrNo5, isYesOrNo6);
                    int increamentBack = incrementOfBackArr();
                    arrRandomizer(5, arr5, ref Back_IsYesOrNo, ref isYesOrNo5, 16, increamentBack, BackArr);
                    randomizerCount = 5;
                }
                else goto Randomiztaion;
            }

            else
            {
                if(check(6))
                {
                    int[,] BackArr = back_Array(first, second, third, fourth, fifth, sixth);
                    string Back_IsYesOrNo = CheckIsYesOrNo(isYesOrNo1, isYesOrNo2, isYesOrNo3, isYesOrNo4, isYesOrNo5, isYesOrNo6);
                    int increamentBack = incrementOfBackArr();
                    arrRandomizer(6, arr6, ref Back_IsYesOrNo, ref isYesOrNo6, 32, increamentBack, BackArr);
                    randomizerCount = 6;
                }
                else goto Randomiztaion;
            }
        }

        /// <summary>
        /// asking the user if his number in the array or no.
        /// </summary>
        /// <param name="arr"> the array which the question about it </param>
        /// <returns></returns>
        static string answerAboutArray(int[,] arr)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            string answerOfQues;
            int top = arr.GetLength(0)*2;
            Console.SetCursorPosition(60, top + 8);
            string question = "is your number in this table ?";
            writing(question,25);
            Console.WriteLine();
            Console.SetCursorPosition(48, top + 9);
            string order = "Write \"yes\" or \"no\" or if you want to go back write \"back\"";
            writing(order,25);
            Console.WriteLine();
            answer1:
            Console.SetCursorPosition(70, top + 10);
            answerOfQues = Console.ReadLine().ToLower();
            switch (answerOfQues)
            {
                case "yes":
                case "no":
                    {
                        if (answer_back_ > 1) answer_back_ = 1; //handling the variable.
                        showTheMagic();
                        answer_back_++;        //to make sure that the user won't back two times once.
                        start++;               //to make sure that the user passed the first array and begin in the second step in the game.
                        break;
                    }
                case "back":
                    {
                        // when start equal 3 that means the start is 
                        //increamented by one in the intro then the game starts .
                        if (start == 3) 
                        {
                            Console.SetCursorPosition(50, top + 10);
                            Console.Write("                                                                          ");
                            Console.SetCursorPosition(50, top + 10);
                            Console.Write("invalid answer, You are in the start of the game");
                            Thread.Sleep(1000);
                            Console.SetCursorPosition(50, top + 10);
                            Console.Write("                                                ");
                            goto answer1;
                        }
                        if (answer_back_ >1) //to make sure that the user won't back two times once.
                        {
                            answer_back_++;
                            Console.SetCursorPosition(45, top + 10);
                            Console.WriteLine("                                                                          ");
                            Console.SetCursorPosition(45, top + 10);
                            Console.WriteLine("You have only one time to go back and you have already used it");
                            Thread.Sleep(1000);
                            Console.SetCursorPosition(45, top + 10);
                            Console.WriteLine("                                                               ");
                            goto answer1;
                        }
                        Console.SetCursorPosition(60, top + 10);
                        Console.Write("                                                                          ");
                        answer_back_++;
                        break;
                    }
                
                default:
                    {
                        Console.SetCursorPosition(53, top + 10);
                        Console.Write("                                                                                        ");
                        Console.SetCursorPosition(53, top + 10);
                        Console.WriteLine("invalid answer , please write \"yes\" or \"no\"");
                        Thread.Sleep(1000);
                        Console.SetCursorPosition(53, top + 10);
                        Console.WriteLine("                                               ");
                        goto answer1;
                    }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            return answerOfQues;
        }

        /// <summary>
        /// shuffle th array passed
        /// </summary>
        /// <param name="arr"> array to shuffle </param>
        /// <returns></returns>
        static int[,] shuffleArray(int[,] arr)
        {
            int n = arr.GetLength(0);  //the length of the array.
            int m = arr.GetLength(1);  //the width of the array.

            Random rand = new Random();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    swap(arr, rand.Next(n - i), rand.Next(m - j), i, j);
                }
            }
            return arr;
        }

        /// <summary>
        /// make swap for the elements of the array.
        /// </summary>
        /// <param name="arr"> the array which elements should be swaped. </param>
        /// <param name="position1x"> x component for the position of the first element </param>
        /// <param name="position1y"> y component for the position of the first element </param>
        /// <param name="position2x"> x component for the position of the second element </param>
        /// <param name="position2y"> y component for the position of the second element </param>
        static void swap(int[,] arr, int position1x, int position1y, int position2x, int position2y)
        {
            int temp = arr[position2x, position2y];
            arr[position2x, position2y] = arr[position1x, position1y];
            arr[position1x, position1y] = temp;
        }

        /// <summary>
        /// explain that the number isn't in the range specified.
        /// </summary>
        static void wrongAnswer()
        {
            SpeechSynthesizer sp = new SpeechSynthesizer();
            Console.SetCursorPosition(60, 15);
            string wrong = "your number is out of range";
            writing(wrong, 50);
            Console.WriteLine();
            sp.SpeakAsync("your number is out of range");
            Console.Beep();
        }

        /// <summary>
        /// show th correct answer.
        /// </summary>
        static void correctAnswer()
        {
            SpeechSynthesizer sp = new SpeechSynthesizer();
            Console.SetCursorPosition(65, 15);
            string correct = "your number is ("+ answer + ")";
            writing(correct,50);
            Console.WriteLine();
            sp.SpeakAsync("your number is (" + answer + ")");
            Console.Beep();
        }

        /// <summary>
        /// showing the last array
        /// </summary>
        /// <param name="incrementBack"> the increment of the last array </param>
        /// <param name="incrementOriginal"> increment of the current array </param>
        /// <param name="arrBack"> the last arry </param>
        /// <param name="arrRole"> the current array </param>
        /// <param name="isYesOrNoBack"> the answer of the last array </param>
        /// <param name="isYesOrNoRole"> the answer of the current array </param>
        static void Backing(int incrementBack, int incrementOriginal, int[,] arrBack, int[,] arrRole,ref string isYesOrNoBack,ref string isYesOrNoRole)
        {
            string _return;
            showTheMagic();
            showArray(arrBack);
            _return = answerAboutArray(arrBack);
            if ((_return == "no") && (isYesOrNoBack == "yes")) answer -= incrementBack;
            else if ((_return == "yes") && (isYesOrNoBack == "no")) answer += incrementBack;
            

            //return to second array
            showArray(arrRole);
            _return = answerAboutArray(arrRole);
            isYesOrNoRole = _return;
            if (_return == "yes") answer += incrementOriginal;
            
        }

        /// <summary>
        /// randomize rank of array.
        /// </summary>
        /// <returns> rank </returns>
        static int randomization ()
        {   
            if (start == 2)
            {
                start++;
                return 1;
            }
            int checkRandomizer;
            Random array = new Random();
            checkRandomizer = array.Next(2, 7);
            if (randomizer.Contains(1) && randomizer.Contains(2) && randomizer.Contains(3) && randomizer.Contains(4) && randomizer.Contains(5) && randomizer.Contains(6))
                randomizer.Clear();
            return checkRandomizer;
        }

        /// <summary>
        /// Randomize unique array and show it
        /// </summary>
        /// <param name="arrayRank"> the rank of the array </param>
        /// <param name="arr"> the array which will be shown </param>
        /// <param name="isYesOrNoBack"> the answer of the user about the last array </param>
        /// <param name="isYesOrNoRole"> the answer of the user about the current array </param>
        /// <param name="incrementOriginal"> the increment of the current array </param>
        /// <param name="incrementBack"> the increment of the last array </param>
        /// <param name="arrBack"> the last array showed </param>
        static void arrRandomizer(int arrayRank, int[,] arr, ref string isYesOrNoBack, ref string isYesOrNoRole, int incrementOriginal, int incrementBack, int[,] arrBack)
        {
            int[,] AfterShuffle;
            string _return="";

            //add the rank of the array in the randomizer list 
            //to make sure that the array won't be shown again.
            randomizer.Add(arrayRank);

            AfterShuffle = shuffleArray(arr);
            showArray(AfterShuffle);
            _return = answerAboutArray(AfterShuffle);
            isYesOrNoRole = _return;

            if (_return == "yes") answer += incrementOriginal;
            else if (_return == "back") Backing(incrementBack, incrementOriginal, arrBack, AfterShuffle, ref isYesOrNoBack, ref isYesOrNoRole);
            
        }

        /// <summary>
        /// check if the array is already showed or no by explore 
        /// is the rank of it was exist in the list "randomizer" or no?.
        /// </summary>
        /// <param name="arrayRank"> the rank of the array </param>
        /// <returns></returns>
        static bool check (int arrayRank)
        {
            if (!(randomizer.Contains(arrayRank))) return true;
            else return false;
        }

        /// <summary>
        /// show the frame and the title "The Magic" 
        /// </summary>
        static void showTheMagic()
        {
            if (start == 1) //Animation.
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                leftFrameAnimation(x,y);
                rightFrameAnimation(x,y);
                theMagicFrameAnimation(x,y);
                Console.SetCursorPosition(69, 1);
                Console.ForegroundColor = ConsoleColor.Red;
                writing("The Magic", 100);
            }
            else //no Animation.
            {
                string Erase = new string(' ', 100);        //variable to erase any thing in the frame.
                for (int erase = 7; erase < 25; erase++)
                {
                    Console.SetCursorPosition(24, erase);
                    Console.Write(Erase);
                }
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                leftFrame();
                rightFrame();
                theMagicFrame();
                Console.SetCursorPosition(69, 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("The Magic");
            }
        }

        /// <summary>
        /// determine the back array.
        /// </summary>
        /// <param name="arr1"> first array </param>
        /// <param name="arr2"> second array </param>
        /// <param name="arr3"> third array </param>
        /// <param name="arr4"> fourth array </param>
        /// <param name="arr5"> fifth array </param>
        /// <param name="arr6"> sixth array </param>
        /// <returns> the last array showed </returns>
        static int [,] back_Array(int [,]arr1, int[,] arr2, int[,] arr3, int[,] arr4, int[,] arr5,int [,] arr6)
        {
            if (randomizerCount == 1) return arr1;
            else if (randomizerCount == 2) return arr2;
            else if (randomizerCount == 3) return arr3;
            else if (randomizerCount == 4) return arr4;
            else if (randomizerCount == 5) return arr5;
            else return arr6;
        }

        /// <summary>
        /// determine the answer of the user in the last array.
        /// </summary>
        /// <param name="isYesOrNo1"> answer about the first array </param>
        /// <param name="isYesOrNo2"> answer about the second array </param>
        /// <param name="isYesOrNo3"> answer about the third array </param>
        /// <param name="isYesOrNo4"> answer about the fourth array </param>
        /// <param name="isYesOrNo5"> answer about the fifth array </param>
        /// <param name="isYesOrNo6"> answer about the sixth array </param>
        /// <returns> the answer of the last array showed </returns>
        static string CheckIsYesOrNo(string isYesOrNo1, string isYesOrNo2, string isYesOrNo3, string isYesOrNo4, string isYesOrNo5, string isYesOrNo6)
        {
            if (randomizerCount == 1) return isYesOrNo1;
            else if (randomizerCount == 2) return isYesOrNo2;
            else if (randomizerCount == 3) return isYesOrNo3;
            else if (randomizerCount == 4) return isYesOrNo4;
            else if (randomizerCount == 5) return isYesOrNo5;
            else return isYesOrNo6;
        }

        /// <summary>
        /// determine the in value of the increment of the last array.
        /// </summary>
        /// <returns> the value of the increment of the last array</returns>
        static int incrementOfBackArr()
        {
            if (randomizerCount == 1) return 1;
            else if (randomizerCount == 2) return 2;
            else if (randomizerCount == 3) return 4;
            else if (randomizerCount == 4) return 8;
            else if (randomizerCount == 5) return 16;
            else return 32;
        }

        /// <summary>
        /// make intro animation in the begainning of the program.
        /// </summary>
        static void intro()
        {
            fontColor();
            Console.SetWindowSize(151, 30);
            Console.CursorVisible = false;
            string theMagic = @"






      ///////////////////////        
             ///         ///                                //////    //////                                                    
            ///         ///                                /// ///   /// ///                                                      *
           ///         ///            ////////////        ///  ///  ///  ///                                  ///////////////   ///   /////////////
          ///         ///            ///      ///        ///   /// ///   ///        ///////////////          ///         ///   ///   ///
         ///         ////////////   ///      ///        ///    //////    ///       ///         ///          ///         ///   ///   ///
        ///         ///      ///   ////////////        ///     /////     ///      ///         ////         ///         ///   ///   ///
       ///         ///      ///   ///                 ///                ///     ///         /////        ///////////////   ///   ///
      ///         ///      ///   ///                 ///                 ///    ///         /// ///                  ///   ///   ///
     ///         ///      ///   /////////////       ///                  ///   ///////////////  //////              ///   ///   //////////////
                                                                                                                   ///
                                                                                                      ///////////////

                                                     All project was made by Ahmed Hafez
";

            for (int i = 0; i < 15; i++)        //animation of the ascii shape.
            {
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = colors[i];
                Console.WriteLine(theMagic);
                Thread.Sleep(200);
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            char dot = '.';
            Console.SetCursorPosition(65, 13);
            Console.Write("Loading");
            for (int i = 1; i <= 8; i++) //loading animation.
            {
                Console.Write(dot);
                Thread.Sleep(250);
            }
            showTheMagic();

            //the introduction.
            Console.SetCursorPosition(57, 23);
            Console.WriteLine("hit any key to skip the intro");
            int row = 10;
            Console.SetCursorPosition(25, row);
            introduction = "This program determine a number you guess by asking you 6 questions and then determine your number " +
                "please follow the instructions and if you want to go back at any question please write \"Back\" and hit enter ... you have only " +
                "one time to go back for each question.......";
            Thread Skip = new Thread(skip);
            Skip.Start();

            for (int i = 0; i < introduction.Length; i++)
            {
                if (Console.CursorLeft == 115)
                {
                    if (!(introduction[i].Equals(' ')) && (introduction[i - 1].Equals(' ')))
                    {
                        row++;
                        Console.SetCursorPosition(25, row);
                        Console.Write(introduction[i]);
                    }
                    else if (!(introduction[i].Equals(' ')) && !(introduction[i - 1].Equals(' ')))
                    {
                        Console.SetCursorPosition(115, row);
                        Console.Write("-");
                        row++;
                        Console.SetCursorPosition(25, row);
                        Console.Write(introduction[i]);
                    }
                    else
                    {
                        row++;
                        Console.SetCursorPosition(25, row);
                        Console.Write(introduction[i]);
                        Thread.Sleep(20);
                    }
                }
                else
                {
                    Console.Write(introduction[i]);
                    Thread.Sleep(20);
                }
                if (skipping) break;
            }
            if (!skipping) Thread.Sleep(3000);
        }

        /// <summary>
        ///make skip in intro.
        /// </summary>
        static void skip()
        {
            ConsoleKeyInfo theKeyHit = Console.ReadKey();
            skipping = true;
            ch1InOk = theKeyHit.KeyChar;
        }

        /// <summary>
        /// fill the colors in the color list
        /// </summary>
        static void fontColor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            colors.Add(ConsoleColor.Red);
            colors.Add(ConsoleColor.Cyan);
            colors.Add(ConsoleColor.DarkBlue);
            colors.Add(ConsoleColor.DarkCyan);
            colors.Add(ConsoleColor.DarkGray);
            colors.Add(ConsoleColor.DarkGreen);
            colors.Add(ConsoleColor.DarkMagenta);
            colors.Add(ConsoleColor.DarkRed);
            colors.Add(ConsoleColor.DarkYellow);
            colors.Add(ConsoleColor.Gray);
            colors.Add(ConsoleColor.Green);
            colors.Add(ConsoleColor.Magenta);
            colors.Add(ConsoleColor.Blue);
            colors.Add(ConsoleColor.White);
            colors.Add(ConsoleColor.Yellow);
        }

        /// <summary>
        /// show the word thinging and make animation
        /// </summary>
        static void thinking()
        {
            Console.SetCursorPosition(69, 15);
            Console.WriteLine("thinking");
            
            char star = '*';
            for (int repeat = 0; repeat < 4; repeat++)
            {
                Console.SetCursorPosition(71, 12);
                for (int top = 0; top < 3; top++)
                {
                    Console.ForegroundColor = colors[top];
                    Console.Write("{0} ", star);
                    Thread.Sleep(150);
                }
                Console.ForegroundColor = colors[3];
                Console.SetCursorPosition(75, 13);
                Console.Write(star);
                Thread.Sleep(150);
                for (int bottom = 5, back = 0; bottom < 8; bottom++, back += 2)
                {
                    Console.SetCursorPosition(75 - back, 14);
                    Console.ForegroundColor = colors[bottom];
                    Console.Write(star);
                    Thread.Sleep(150);
                }
                Console.ForegroundColor = colors[8];
                Console.SetCursorPosition(71, 13);
                Console.Write(star);
                Thread.Sleep(150);
                for (int erase = 0; erase < 3; erase++)
                {
                    Console.SetCursorPosition(71, 12 + erase);
                    Console.Write("                 ");
                }

            }
            Console.ForegroundColor = ConsoleColor.Red;
        }

        /// <summary>
        /// write the string passed in animation mode.
        /// </summary>
        /// <param name="written"> the string which will be written </param>
        /// <param name="duration"> the duration of writting </param>
        static void writing (string written, int duration)
        {
            for (int i = 0; i < written.Length; i++)
            {
                Console.Write(written[i]);
                Thread.Sleep(duration);
            }
        }

        /// <summary>
        /// frames making in static mode and animation mode.
        /// </summary>
        /// <param name="x"> ascii code </param>
        /// <param name="y"> ascii code </param>
        static void leftFrameAnimation(int x, int y)
        {
            for (int top = 3, left = 8; ; top++)
            {
                for (int repeatLeft = 1; repeatLeft <= 5; repeatLeft++)
                {
                    Console.SetCursorPosition(left, 2);
                    Console.WriteLine((char)y);
                    left++;
                    Thread.Sleep(5);
                }

                Console.SetCursorPosition(8, top);
                Console.WriteLine((char)x);
                Thread.Sleep(10);
                if (top == 28)
                {
                    for (int repeatLeft = 1; repeatLeft <= 3; repeatLeft++)
                    {
                        Console.SetCursorPosition(left, 2);
                        Console.WriteLine((char)y);
                        left++;
                        Thread.Sleep(5);
                    }
                    break;
                }
            }
        }
        static void rightFrameAnimation(int x, int y)
        {
            for (int top = 28, left = 140; ; top--)
            {
                for (int repeatLeft = 1; repeatLeft <= 5; repeatLeft++)
                {
                    Console.SetCursorPosition(left, 28);
                    Console.WriteLine((char)y);
                    left--;
                    Thread.Sleep(5);
                }

                Console.SetCursorPosition(141, top);
                Console.WriteLine((char)x);
                Thread.Sleep(10);
                if (top == 3)
                {
                    for (int ii = 1; ii <= 2; ii++)
                    {
                        Console.SetCursorPosition(left, 28);
                        Console.WriteLine((char)y);
                        left--;
                        Thread.Sleep(5);
                    }
                    break;
                }
            }
        }
        static void theMagicFrameAnimation (int x, int y)
        {
            for (int top = 1, left = 54; top < 3; top++)
            {
                Console.SetCursorPosition(54, top);
                Console.WriteLine((char)x);
                for (int Repeat = 1; Repeat <= 20; Repeat++)
                {
                    Console.SetCursorPosition(left, 0);
                    Console.WriteLine((char)y);
                    left++;
                    Thread.Sleep(5);
                }
                Console.SetCursorPosition(94, top);
                Console.WriteLine((char)x);
            }
        }
        static void leftFrame()
        {
            for (int top = 3, left = 8; ; top++)
            {
                for (int repeatLeft = 1; repeatLeft <= 5; repeatLeft++)
                {
                    if (left == 54 || left == 94)
                    {
                        left++;
                        continue;
                    }
                    Console.SetCursorPosition(left, 2);
                    Console.WriteLine((char)y);
                    left++;
                }

                Console.SetCursorPosition(8, top);
                Console.WriteLine((char)x);
                if (top == 28)
                {
                    for (int repeatLeft = 1; repeatLeft <= 3; repeatLeft++)
                    {
                        Console.SetCursorPosition(left, 2);
                        Console.WriteLine((char)y);
                        left++;
                    }
                    break;
                }
            }
        }
        static void rightFrame()
        {
            for (int top = 28, left = 140; ; top--)
            {
                for (int repeatLeft = 1; repeatLeft <= 5; repeatLeft++)
                {
                    Console.SetCursorPosition(left, 28);
                    Console.WriteLine((char)y);
                    left--;
                }

                Console.SetCursorPosition(141, top);
                Console.WriteLine((char)x);
                if (top == 3)
                {
                    for (int ii = 1; ii <= 2; ii++)
                    {
                        Console.SetCursorPosition(left, 28);
                        Console.WriteLine((char)y);
                        left--;
                    }
                    break;
                }
            }
        }
        static void theMagicFrame()
        {
            for (int top = 1, left = 54; top < 3; top++)
            {
                Console.SetCursorPosition(54, top);
                Console.WriteLine((char)x);
                for (int repeatLeft = 1; repeatLeft <= 20; repeatLeft++)
                {
                    Console.SetCursorPosition(left, 0);
                    Console.WriteLine((char)y);
                    left++;
                }
                Console.SetCursorPosition(94, top);
                Console.WriteLine((char)x);
            }
        }
        
        /// <summary>
        /// animation in the close of the program.
        /// </summary>
        static void ending()
        {
            SpeechSynthesizer sp = new SpeechSynthesizer();
            sp.SpeakAsync("Good Byye");
            string GoodBye = @"





           ////////////////////                                                                ///    ////////////////
          ///                                                                                 ///    ////         ///  ///         ///
         ///                                                                                 ///    ////         ///  ///         ///
        ///                                                                                 ///    ////         ///  ///         ///   ///////////////
       ///                                                                                 ///    ////       ///    ///         ///   ///         ///
      ///                             /////////////////    /////////////////              ///    ////     ///      ///////////////   ///         ///
     ///       ///////////////////   ///           ///    ///           ///              ///    /////////                     ///   ///         ///
    ///               ///           ///           ///    ///           ///    /////////////    ////     ///                  ///   ///////////////
   ///               ///           ///           ///    ///           ///    ///       ///    ////       ///                ///   ///
  ///               ///           ///           ///    ///           ///    ///       ///    ////         ///              ///   ///
 ///               ///           ///           ///    ///           ///    ///       ///    ////         ///              ///   ///
/////////////////////           /////////////////    /////////////////    /////////////    ////////////////   //////////////   //////////////////
";
            for (int i = 0; i < 15; i++)
            {
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = colors[i];
                Console.WriteLine(GoodBye);
                Thread.Sleep(100);
            }
        }
    }
}