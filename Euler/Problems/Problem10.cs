﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Euler.Problems
{
    [Description("Summation of primes")]
    public class Problem10 : AbstractProblem
    {
        public override void Execute()
        {
            double sum = 0;

            for (int i = 1; i < 10; i++)
            {
                if (Cognition.IsPrime(i))
                    sum = sum + i;
            }

            Console.WriteLine("Sumation of Prime below 20000 is : " + sum);
        }
    }
}
