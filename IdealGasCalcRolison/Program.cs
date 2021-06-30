/* Thomas Rolison, trolison1@cnm.edu
 * IdealGasCalculatorClassThomasR.cs
 * 02/02/21
 */


using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace idealGasCalculatorClass
{
    class Program
    {
        //write about program
        static void DisplayHeader()
        {
            Console.WriteLine("This is a calculator to find the pressure " +
             "exerted by a gas in a container using the following inputs from " +
             "the user: the name of the gas, the volume of the gas container " +
             "in cubic meters, the weight of the gas in grams and the temperature " +
             "of the gas in degrees Celsius");
        }

        static void GetMolecularWeights(ref string[] gasNames, ref double[] molecularWeights, out int count)
        {


            using (var sr = new StreamReader("MolecularWeightsGasesAndVapors.csv"))
            {
                //initialize variables and read first line
                string line;
                sr.ReadLine();
                int totalGases = 0;

                //read through each line and add to arrays
                while ((line = sr.ReadLine()) != null)
                {
                    string[] subs = line.Split(',');

                    string names = subs[0];
                    double num = double.Parse(subs[1]);

                    gasNames[totalGases] = names;
                    molecularWeights[totalGases] = num;

                    totalGases++;

                }
                //get total number of gases in arrays and write info to console
                count = totalGases;

                Console.WriteLine($"The total number of elements is {count}.");

            }
        }

        private static void DisplayGasNames(string[] gasNames, int countGases)
        {
            //display gas names, 3 by line. Not sure how to format this properly, was having trouble with it.
            for (int i = 0; i < 82; i += 3)
            {
                Console.WriteLine($"{gasNames[i],-20} {gasNames[i + 1],-20} {gasNames[i + 2],-20}");
            }
        }

        private static double GetMolecularWeightFromName(string gasName, string[] gasNames, double[] molecularWeights, int countGases)
        {
            //initialize variables
            int numOfNameId = 0;

                //get user input for name of gas and check if gas is valid input
            Console.WriteLine("What gas would you like to choose?");
            gasName = Console.ReadLine();
            
                if (gasNames.Contains(gasName))
                {
                    Console.WriteLine($"Gas selected: {gasName}");
                    numOfNameId = Array.IndexOf(gasNames, gasName);
                }
                else if (!gasNames.Contains(gasName))
                {
                    throw new GasFormatException();
                }
            
            //write to console mol weight of gas and return variable
            double molWeight = (double)molecularWeights.GetValue(numOfNameId);
            Console.WriteLine($"Molecular weight for {gasName} is {molWeight}");
      
            return molWeight;

        }


        private static void DisplayPressure(double pressure)
        {
            //calculate and display pressure in pascals
            PaToPSI(pressure);
            Console.WriteLine($"The pressure in pascals is {pressure}");

        }

        static double PaToPSI(double pascals)
        {
            //calculate pressure of psi from pascals and write to console.
            double psi = 0.00014503773;

            double paToPsi = pascals * psi;

            Console.WriteLine($"The pressure in PSI is {paToPsi}");
            return paToPsi;
        }

        static void Main(string[] args)
        {
            //initializing variables
            
            string[] gasNames = new string[84];
            double[] molecularWeights = new double[84];
            int countGases = 0;
            string gasName = "";
            bool answer;
            

            DisplayHeader();
            GetMolecularWeights(ref gasNames, ref molecularWeights, out int numberOfGases);
            DisplayGasNames(gasNames, numberOfGases);

            //do loop to check if user wants to continue on getting information
            do
            {
                //try 
                try
                {
                    //run through each method, and loop if user wants to do multiple calculations
                    double molWeight = GetMolecularWeightFromName(gasName, gasNames, molecularWeights, countGases);
                    //checks if the weight was actually acquired, to insure the program runs correctly
                    if (molWeight != 0.0)
                    {
                        //instantiates the IdealGas class
                        IdealGas gas = new IdealGas();
                        //sets molweight into the class for calculations
                        gas.SetMolecularWeight(molWeight);

                        //asks user for information and sets into class
                        Console.WriteLine("What is the weight of the gas in grams?");
                        double mass = Convert.ToDouble(Console.ReadLine());
                        gas.SetMass(mass);

                        Console.WriteLine("What is the volumn in cubic meters of the container?");
                        double vol = Convert.ToDouble(Console.ReadLine());
                        gas.SetVolume(vol);

                        Console.WriteLine("What is the temperature of the gas in degrees Celsius");
                        double temp = Convert.ToDouble(Console.ReadLine());
                        gas.SetTemp(temp);

                        //gets pressure from class calculations
                        double pressure = gas.GetPressure();
                        //calculates PaToPSI and PSI for gas
                        DisplayPressure(pressure);


                    }
                    //catch exceptions. I tried using the base exception like in our exceptions demo.
                }
                catch(GasFormatException exc)
                {

                }
                catch(OverflowException exc)
                {
                    Console.WriteLine("Overflow exception occurred.");
                }
                catch(Exception exc)
                {
                    Console.WriteLine("Error: " + exc.Message);
                }

                Console.WriteLine("Would you like to calculate another gas? yes or no");
                    string userAnswer = Console.ReadLine();
                    if (userAnswer == "yes")
                    {
                        answer = true;
                    }
                    else
                    {
                        answer = false;
                    }
               

            } while (answer == true);

            Console.WriteLine("Have a nice day!");
        }

    }
}
