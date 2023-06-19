package main

import (
	"fmt"
	"log"
	"math"
	"os"
	"strconv"

	"github.com/urfave/cli/v2"
)

func main() {
	var precision int

	app := &cli.App{
		Name:                   "rcalc",
		Usage:                  "Calculates the fourth number that fits the ratio with the three numbers given",
		UsageText:              "rcalc [option] <first number> <second number> <third number>",
		Version:                "2.0.0",
		UseShortOptionHandling: true,
		Flags: []cli.Flag{
			&cli.IntFlag{
				Name:        "precision",
				Aliases:     []string{"p"},
				Value:       2,
				Usage:       "How many digits after the decimal point to round off?",
				Destination: &precision,
				Action: func(ctx *cli.Context, v int) error {
					if v < 0 {
						return fmt.Errorf("precision value must be higher than 0: %v", v)
					}
					return nil
				},
			},
		},
		Action: func(ctx *cli.Context) error {
			// Check argument length
			len := ctx.Args().Len()
			if len != 3 {
				return cli.Exit("Number of arguments must be 3", 1)
			}
			// Get arguments
			n1s := ctx.Args().Get(0)
			n2s := ctx.Args().Get(1)
			n3s := ctx.Args().Get(2)
			// Check arguments validity
			n1, err1 := strconv.ParseFloat(n1s, 64)
			n2, err2 := strconv.ParseFloat(n2s, 64)
			n3, err3 := strconv.ParseFloat(n3s, 64)
			if err1 != nil {
				return cli.Exit("First argument is not a valid number", 1)
			}
			if err2 != nil {
				return cli.Exit("Second argument is not a valid number", 1)
			}
			if err3 != nil {
				return cli.Exit("Third argument is not a valid number", 1)
			}
			// Calculate ratio
			n4 := (n2 * n3) / n1
			// Print result
			fmt.Print("Result: ")
			fmt.Println(roundFloat(n4, uint(precision)))
			fmt.Printf("%.4f : [%.4f] = %.4f : %.4f", n3, n4, n1, n2)
			return nil
		},
	}

	if err := app.Run(os.Args); err != nil {
		log.Fatal(err)
	}
}

func roundFloat(n float64, p uint) float64 {
	ratio := math.Pow(10, float64(p))
	return math.Round(n*ratio) / ratio
}
