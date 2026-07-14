package main

import "fmt"

func main() {
	var work string
	var times int
	var allmoney float64 = 0
	var items string
	var itemsCount int
	var currentMon float64
	fmt.Println("What kind of work are you doing?")
	fmt.Scanf("%s\n", &work)
	fmt.Println("How many times did you do this work?")
	fmt.Scanf("%d\n", &times)
	for i := 1; 0 < 10; i++ {
		var mon [70]float64
		fmt.Println("How much did you make for  job ", i, "?")
		fmt.Scanln(&mon[i])
		allmoney = allmoney + mon[i]
		if i == times {
			fmt.Printf("You made %.2f dollars!", allmoney)
			fmt.Println("")
			break
		}
	}
	fmt.Println("What are you buying with this?")
	fmt.Scanf("%s\n", &items)
	fmt.Println("How many do you want?")
	fmt.Scanf("%d\n", &itemsCount)
	currentMon = allmoney
	for i := 1; 0 < 10; i++ {
		var monit [70]float64
		fmt.Println("How much did you pay for ", items, i, "?")
		fmt.Scanln(&monit[i])
		if monit[i] > currentMon {
			fmt.Println("You do not have enough money, choose an item with a price in your budget!")
			fmt.Println(" ")
			i = i - 1
		} else {
			currentMon = currentMon - monit[i]
			fmt.Printf("You have %.2f dollars left.", currentMon)
			fmt.Println(" ")
		}
		if i == itemsCount {
			break
		}
	}
	fmt.Printf("You made %.2f dollars from %s %d time(s).", allmoney, work, times)
	fmt.Println("")
	fmt.Printf("You bought %d %s and have %.2f dollars remaining.", itemsCount, items, currentMon)

}
