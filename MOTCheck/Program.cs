using MOTCheck.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MOTCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            ApiHelper.InitializeClient();

            //get the registration input - validate it within this method
            string registration = AskForRegistration("Please enter your vehicle registration number: ");

            try
            {
                var MOTDetails = LoadMOTDetails(registration);

                RenderMOTDataToConsole(MOTDetails.Result);
            }
            catch (Exception ex)
            {
                //there was an exception thrown while calling the API so show an error message
                RenderAPIErrorMessage();
            }

            Console.WriteLine("");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static string AskForRegistration(string question)
        {
            Console.WriteLine(question);

            while (true) //use a loop to keep asking the user if they didn't provide a valid answer
            {
                string input = Console.ReadLine();

                //trim the space in the reg if entered.
                //keeps the regex simpler
                input = input.Replace(" ", "");

                if (!ValidateRegistration(input))
                {
                    Console.WriteLine("Not a valid registration. Please enter another: ");
                }
                else
                {
                    return input; //exit this method, returning the int
                }
            }
        }

        static bool ValidateRegistration(string input)
        {
            //regex to match the uk registration format
            string regex = @"^(?=.{1,7})(([a-zA-Z]?){1,3}(\d){1,3}([a-zA-Z]?){1,3})$";
            var match = Regex.Match(input, regex, RegexOptions.IgnoreCase);

            return match.Success;
        }

        private static async Task<MOTResponseModel> LoadMOTDetails(string registration)
        {
            var MOTDetails = await MOTProcessor.LoadMOTDetails(registration);
            return MOTDetails;
        }

        static void RenderMOTDataToConsole(MOTResponseModel details)
        {
            if (details != null)
            {
                Console.WriteLine("");
                Console.WriteLine("Vehicle details:");
                Console.WriteLine($"Make: { details.make }");
                Console.WriteLine($"Model: { details.model }");
                Console.WriteLine($"Colour: { details.primaryColour }");

                if (details.motTests != null && details.motTests.Count > 0)
                {
                    MotTest test = details.motTests[0];
                    Console.WriteLine($"MOT Expiry Date: { test.expiryDate }");
                    Console.WriteLine($"Milage at last MOT: { test.odometerValue } { test.odometerUnit }");
                }
            }
        }

        static void RenderAPIErrorMessage()
        {
            Console.WriteLine("We did not find any information for the registration entered.");
        }

        static void RenderValidationErrorMessage()
        {
            Console.WriteLine("The registration was not valid.");
        }
    }
}
