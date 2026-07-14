package main

import "fmt"

func main() {

	var anOne string
	var anTwo string
	fmt.Println("Greeting traveler, I am the guardian of this gate.")
	fmt.Println("You will not make it ast me, until you answer my two riddles.")
	fmt.Println("Here is my first one.")
	for {
		fmt.Println("I am an odd number. Take away a letter and i become even. What numer am I?")
		fmt.Scanf("%s\n", &anOne)
		if anOne == "Seven" || anOne == "seven" || anOne == "7" {
			fmt.Println("That is correct!")
			fmt.Println("Here is my second one.")
			break

		} else {
			fmt.Println("Sorry, that's not the right answer. Try again.")
		}
	}
	for {
		fmt.Println("Forward I am heavy; backward I am not. What am I?")
		fmt.Scan(&anTwo)

		if anTwo == "ton" || anTwo == "Ton" {
			fmt.Println("That is also correct!")
			fmt.Println("Congratulations! You've passed the gate.")
			break
		} else {
			fmt.Println("Sorry, that's not the right answer. Try again.")
		}
	}
}
