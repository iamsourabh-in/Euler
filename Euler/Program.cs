﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Euler
{
    class Program
    {

        static void Main(string[] args)
        {
            MainWrapper();
        }

        public static void MainWrapper()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            int selectedProblem = 1;

            int selection = 1;
            string nextStep = "Home";

            switch (selection)
            {
                case 1:
                    ListProblems(ref selectedProblem);

                    Console.Clear();

                    Executor exec = new Executor(Creator.Create(selectedProblem));

                    //Waiting for user Input to navigate
                    Console.ReadKey();

                    HandleNavigation(ref nextStep);

                    if (nextStep == "Home")
                        goto case 1;
                    else
                        Environment.Exit(1);
                    break;
                case 2:
                    goto case 1;
                default:
                    break;

            }
        }

        private static void ListProblems(ref int selectedProblem)
        {
            ConsoleKeyInfo key;
            do
            {

                Console.Clear();
                var problems = AppDomain.CurrentDomain.GetAssemblies()
                     .SelectMany(assembly => assembly.GetTypes())
                     .Where(type => type.IsSubclassOf(typeof(AbstractProblem)));
                Console.WriteLine("Here are the list of  Euler problems, Select the one you want to execute.\n");
                var ProblemNumbersList = new List<int>();
                foreach (var problem in problems)
                {

                    ProblemNumbersList.Add(Convert.ToInt32(problem.Name.Substring(7, problem.Name.Length - 7)));

                }
                ProblemNumbersList.Sort();

                foreach (var problem in ProblemNumbersList)
                {
                    var curPRob = problems.Where(item => item.Name == "Problem" + problem).FirstOrDefault();
                    var descriptionAttribute = curPRob.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;

                    if (problem == selectedProblem)
                    {
                        Console.Write(">>" + curPRob.Name.ToString());
                    }
                    else
                    {
                        Console.Write("  " + curPRob.Name.ToString());
                    }
                    if (descriptionAttribute != null)
                    {
                        Console.WriteLine(" - " + descriptionAttribute.Description);
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }

                key = Console.ReadKey(true);
                if (key.Key.ToString() == "DownArrow")
                {
                    if (selectedProblem == problems.Count())
                        selectedProblem = 0;
                    if (selectedProblem < problems.Count())
                        selectedProblem = selectedProblem + 1;

                }
                else if (key.Key.ToString() == "UpArrow")
                {
                    if (selectedProblem == 1)
                        selectedProblem = problems.Count() + 1;
                    if (selectedProblem - 1 > 0)
                        selectedProblem = selectedProblem - 1;

                }
            }
            while (key.KeyChar != 13);
        }

        private static void HandleNavigation(ref string nextStep)
        {
            ConsoleKeyInfo wizKey;

            do
            {
                Console.Clear();
                foreach (var val in new List<string>() { "Home", "Exit" })
                {

                    if (val == nextStep)
                    {
                        Console.WriteLine(">>" + val.ToString());
                    }
                    else
                    {
                        Console.WriteLine("  " + val.ToString());
                    }
                }
                wizKey = Console.ReadKey(true);

                if (wizKey.Key.ToString() == "DownArrow" || wizKey.Key.ToString() == "UpArrow")
                {
                    nextStep = nextStep == "Home" ? "Exit" : "Home";
                }
            }
            while (wizKey.KeyChar != 13);


        }

    }
}
