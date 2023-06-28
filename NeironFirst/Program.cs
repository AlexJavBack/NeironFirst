using System;

namespace NeironFirst

{
    class Program
    {
        public bool choiseIteraconi = false;
        public class Neiron
        {
            public decimal weight = 0.5m;
            public decimal LastError
            {
                get; private set;
            }
            public decimal Smoothing { get; set; } = 0.00001m;
            public decimal ProcessingInputData(decimal input)
            {
                return input * weight;
            }
            public decimal RestoreInputData(decimal output)
            {
                return output * weight;
            }

            public void Train(decimal input, decimal exspectedResult)
            {
                var actualResult = input * weight;
                LastError = exspectedResult - actualResult;
                var correction = (LastError / actualResult) * Smoothing;
                weight += correction;

            }
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            Console.WriteLine("Do you want to input interacon on window terminal? pint yes or no");
            string input = "";
            while (!input.Equals("yes") && !input.Equals("no"))
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "yes":
                        program.choiseIteraconi = true;
                        break;
                    case "no":
                        program.choiseIteraconi = false;
                        break;
                    default:
                        Console.WriteLine("Incorrect answer. Print only yes or no");
                        input = Console.ReadLine();
                        break;
                }
            }           
            decimal usd = 1;
            decimal eur = 0.92m;

            Neiron neiron = new Neiron();

            int i = 0;

            do
            {
                i++;
                neiron.Train(usd, eur);
                if (program.choiseIteraconi)
                {
                    if(i % 100000 == 0)
                    {
                        Console.WriteLine($"Interacon: {i}\tError:\t{neiron.LastError}");
                    }
                }               
            }
            while (neiron.LastError > neiron.Smoothing || neiron.LastError < -neiron.Smoothing);
            Console.WriteLine("Training is complite!");

            Console.WriteLine($"{neiron.ProcessingInputData(100)} eur in {100} usd");
            Console.WriteLine($"{neiron.ProcessingInputData(25)} eur in {25} usd ");
            Console.WriteLine($"{neiron.RestoreInputData(46)} usd in {46} eur");
        }
    }
}
