using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;

namespace rcalc {
  internal class Program {
    // TODO: Find a way to define minimum value of Value and Option
    public class Options {
      private readonly decimal firstNumber;
      private readonly decimal secondNumber;
      private readonly decimal thirdNumber;
      private readonly int precision = 2;

      public Options(decimal firstNumber, decimal secondNumber, decimal thirdNumber, int precision) {
        this.firstNumber = firstNumber;
        this.secondNumber = secondNumber;
        this.thirdNumber = thirdNumber;
        this.precision = precision;
      }

      public Options(decimal firstNumber, decimal secondNumber, decimal thirdNumber) {
        this.firstNumber = firstNumber;
        this.secondNumber = secondNumber;
        this.thirdNumber = thirdNumber;
      }

      [Value(0, MetaValue = "FIRST", Required = true, HelpText = "First number. This will pair with second number. Must be positive number.")]
      public decimal FirstNumber { get { return firstNumber; } }
      [Value(1, MetaValue = "SECOND", Required = true, HelpText = "Second number. This will pair with first number. Must be positive number.")]
      public decimal SecondNumber { get { return secondNumber; } }
      [Value(2, MetaValue = "THIRD", Required = true, HelpText = "Third number. This will pair with fourth number, which will be calculated. Must be positive number.")]
      public decimal ThirdNumber { get { return thirdNumber; } }
      [Option('p', "precision", Default = 2, HelpText = "Precision of calculated value. Must be positive number.", MetaValue = "POSITIVE_INTEGER", Required = false)]
      public int Precision { get { return precision; } }

      [Usage(ApplicationAlias = "rcalc.exe")]
      public static IEnumerable<Example> Examples {
        get {
          return new List<Example>() {
            new Example("Get fourth number from third number(3) and ratio of first number(1) and second number(2)", new Options(1, 2, 3))
          };
        }
      }
    }

    static void Main(string[] args) {
      var parser = new Parser(config => config.HelpWriter = Console.Out);

      parser.ParseArguments<Options>(args).WithParsed(Run).WithNotParsed(HandleParseError);
    }

    private static void HandleParseError(IEnumerable<Error> errs) {
      if (errs.IsVersion() || errs.IsHelp()) return;
      Console.WriteLine("Failed to parse arguments");
    }

    private static void Run(Options opts) {
      CheckPositive(opts.FirstNumber);
      CheckPositive(opts.SecondNumber);
      CheckPositive(opts.ThirdNumber);
      CheckPositive(opts.Precision);

      decimal fourthNumber = opts.SecondNumber / opts.FirstNumber * opts.ThirdNumber;

      Console.WriteLine("Output: " + fourthNumber.ToString($"F{opts.Precision}"));
      Console.WriteLine("Equation: [" + opts.FirstNumber + "] : [" +  opts.SecondNumber + "] = [" + opts.ThirdNumber + "] : [" + fourthNumber + "]");
    }

    private static void CheckPositive(decimal value) {
      if (value <= 0) {
        Console.Error.WriteLine("Given number (" + value + ") is not positive number. Aborting...");
        System.Environment.Exit(1);
      }
    }

    private static void CheckPositive(int value) {
      if (value < 0) {
        Console.Error.WriteLine("Given number (" + value + ") is not positive number. Aborting...");
        System.Environment.Exit(1);
      }
    }
  }
}
