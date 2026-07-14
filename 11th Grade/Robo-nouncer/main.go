package main

import "fmt"

func main() {
	fmt.Println("Hello there! I am robo-nouncer! I only need a little information and I can tell you what to say!")
	fmt.Println("What's the score?")
	fmt.Println("Team 1:")
	var t1 int
	fmt.Scanf("%d\n", &t1)
	fmt.Println("Team 2:")
	var t2 int
	fmt.Scanf("%d\n", &t2)
	fmt.Println("How much time remains in the game? (minutes)")
	var mins int
	fmt.Scanf("%d\n", &mins)
	if mins == 0 {
		if t1 > t2 {
			fmt.Println("Team 1 won!")
		}
		if t1 < t2 {
			fmt.Println("Team 2 is ahead!")
		}
		if t1 == t2 {
			fmt.Println("It was a tie!")
		}
	} else {
		if t1 > t2 {
			fmt.Println("Team 1 is ahead!")
		}
		if t1 < t2 {
			fmt.Println("Team 2 is the lead!")
		}
		if t1 == t2 {
			fmt.Println("Each team has the same score!")
		}
	}
}
