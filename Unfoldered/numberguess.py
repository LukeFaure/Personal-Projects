#number guessing game

import random 

number = random.randint(1, 100000000000000000000000000000000000000000)
userguess = 0 
guesscount = 0

def guess():
	global userguess 
	userguess  = int(input("What is your guess: "))	
 
guessed = False

def guessing(theguess):
	global guessed, userguess, guesscount
	if theguess == number: 
		print("Congratulations, you guessed the word!")
		print(f"You took {guesscount} guesses") 
		guessed = True
	elif theguess < number: 
		print("Too Low!") 
		guesscount += 1
	else:
		print("Too High!")
		guesscount += 1 
 
while guessed == False:
	guess() 
	guessing(userguess) 
		
