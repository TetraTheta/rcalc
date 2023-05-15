package main

import (
	"fmt"
	"math/big"
	"os"
	"strconv"
)

func main() {
	if len(os.Args) != 4 {
		fmt.Println("Error: rcalc only accepts three numeric arguments")
		os.Exit(1)
	}

	x := convertToFloat(os.Args[1])
	y := convertToFloat(os.Args[2])
	z := convertToFloat(os.Args[3])
	r := y / x * z

	sx := format(new(big.Float).SetFloat64(x))
	sy := format(new(big.Float).SetFloat64(y))
	sz := format(new(big.Float).SetFloat64(z))
	sr := format(new(big.Float).SetFloat64(r))

	fmt.Printf("Result = %s (%s : %s = %s : %s)", sr, sx, sy, sz, sr)
}

func convertToFloat(s string) float64 {
	r, err := strconv.ParseFloat(s, 64)
	if err != nil {
		fmt.Println("Error: given argument(s) is not number")
		os.Exit(1)
	}
	return r
}

func format(x *big.Float) string {
	return x.Text('f', -1)
}
