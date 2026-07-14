package main

import "fmt"

func main() {
	var name string
	var anOne string
	var anTwo string
	var trust int = 0
	var textRed string = "\033[31m"
	var reset string = "\033[0m"
	fmt.Println("Enter your name...")
	fmt.Print(": ")
	fmt.Scan(&name)
	for i := 0; i < 10; i++ {
		fmt.Println("-")
	}
	fmt.Println(textRed, "Hello, is this", name, "?")
	fmt.Print(reset)
	fmt.Println("[y]es")
	fmt.Println("[n]o")
	fmt.Print(": ")
	fmt.Scan(&anOne)
	fmt.Print(textRed)
	if anOne == "y" || anOne == "Y" {
		fmt.Println("Good.")
		trust = trust + 1
	} else if anOne == "N" || anOne == "n" {
		fmt.Println("Oh really. What is your name?")
		fmt.Print(reset)
		fmt.Print(": ")
		fmt.Scan(&name)
	} else {
		fmt.Printf("Hm, '%s' doesn't seem like and answer. I asked 'y' or 'n'.", anOne)
		trust = trust - 1
	}

	fmt.Print(textRed)
	fmt.Printf("\n \n Alright, \"%s\", where were you on the night of Feb 14th?", name)
	fmt.Print(reset)
	fmt.Println("\n[A]t a lovely dinner with myspouse.")
	fmt.Println("[T]aking a walk in the park.")
	fmt.Print(": ")
	fmt.Scan(&anTwo)
	fmt.Print(textRed)
	if anTwo == "A" || anTwo == "a" {
		fmt.Println("Alringt, I's sure you have a receipt to confirm that.")
		trust = trust + 1
	} else if anTwo == "T" || anTwo == "t" {
		fmt.Println("And I bet no one was around to witness you...")
		trust = trust - 1
	} else {
		fmt.Printf("Hm, '%s' doesn't seem like and answer. I asked 'A' or 'T'.", anTwo)
		trust = trust - 1
	}
	fmt.Println(" ")
	if trust >= 0 {
		fmt.Println("I think we're done here, have a nice evening.")
	} else {
		fmt.Println("\nWe're going to have you go back to the station.")
	}
	fmt.Println(reset)
}
