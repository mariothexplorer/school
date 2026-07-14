package main

import "fmt"

func main() {
	var starting int
	var years int
	var percent float64 = 0.07
	var money float64
	fmt.Println("Welcome to MP's Rough Investment Calculator!")
	fmt.Println("Tell me about an investment account and I'll tell yoy some stats!")
	fmt.Println("How many dollars is starting in this accont?")
	fmt.Scanf("%d\n", &starting)
	//fmt.Scanln(&starting)
	fmt.Println("How many years will this count be allowed to grow?")
	//fmt.Scanln(&years)
	fmt.Scanf("%d\n", &years)
	fmt.Println("")
	fmt.Println("Calculating...")
	money = ((float64(years)*float64(percent) + 1) * (float64(starting)))
	fmt.Println("")
	fmt.Println("After", years, ", the starting amount of", starting, "will turn", money, "dollars.")
	fmt.Println("That would be", int(money)/100, "$100 bills and", (int(money) - ((int(money)/100)*100)/1), "$1 bills.")
	fmt.Println("")

}
