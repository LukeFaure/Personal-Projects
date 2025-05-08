
madLibsChoice = 0
nounOne = ""
nounTwo = ""
nounThree = ""
nounFour = ""
nounFive = ""

adjectiveOne = ""
adjectiveTwo = ""
adjectiveThree = ""
adjectiveFour = ""
adjectiveFive = ""


averbOne = ""
verbOne = ""

nouns = []
adjectives = []


def introduction():
    print("Mad Libs for Mad Fun\nWelcome to the Mad Libs game made by Luke Faure\nToday you will be generating your own Mad Libs")
    introAdjective = input("Enter an adjective: ")
    print("Here is an example based on your adjective:\nI was walking in the park when a " + introAdjective + " dog started licking my hand")
    

introduction()

def madLibsOne():
    global nouns, adjectives, verbOne, adverbOne
    print("In the dark woods, there was a " + nouns[0] + ", it was " + adjectives[0] + ".\n I was very frightened when I saw the " + nouns[0] + ", but luckily my " + nouns[1] + " powered robo dog arrived.")
    print("With the power of ten " + nouns[2] + " we defeated the " + nouns[0] + ", unfortunately, a " + adjectives[1] + " dragon started spraying us with " + adjectives[2] + " " + nouns[2])
    print("With my robo dog, I ran " + adverbOne + " into the densest part of the forest. It was so dark, I had to use " + nouns[3] + " to see my hand in front of me.\nRobo dog and I decided to sleep up high in a tree but then the dragon came back and burned down the forest with a terrbile puff of " + nouns[2])
    print("This was the last straw, I was feeling " + adjectives[4] + " so I decided to attack the dragon. I shot at it with a " + nouns[4] + ", This caused the dragon to fall off a cliff and into a void.")

def madLibsTwo():
    global nouns, adjectives, verbOne, adverbOne
    print("There it was, the giant " + nouns[0] + ". Henry wanted to attack it with a " + nouns[1] + " but he held back. He was feeling " + adjectives[0] + ", he would get his chance, but right now the dragon was " + adjectives[1])
    print("Slowly, Henry backed out of the " + nouns[2] + ", unlucky for him, the " + nouns[0] + " awoke. It " + verbOne + " and it unleashed an attack made of " + nouns[3] + ". This bearly missed Henry who was " + adjectives[2])
    print("Henry " + adverbOne + " escaped the area but he tripped on a " + adjectives[3] + nouns[4] + " and tumbled over a cliff. Henry was " + adjectives[4] + ".")

def madLibsThree():
    global nouns, adjectives, verbOne, adverbOne
    print("Gerald was a farmer who lived in " + nouns[0] + " Land. This was a " + adjectives[0] + " place, it was very " + adjectives[1] + ". Gerald was tring to grow " + nouns[1] + "s, unfortunately a " + nouns[2] + " was being very " + adjectives[2] + ".")
    print("Gerald luckily was eating a " + nouns[3] + " which nullified the affects of the " + nouns[2] + ". This allowed him to grow a good harvest of " + adjectives [3] + " " + nouns[1] + ". This harvest enabled a very " + adjectives[4] + " winter.")
    print("Gerald was very happy and moved around " + adverbOne + " and he managed to get a " + nouns[4] + " for his birthday.")

    

def main():
    global madLibsChoice, nounOne, nounTwo, nounThree, nounFour, nounFive, adjectiveOne, adjectiveTwo, adjectiveThree, adjectiveFour, adjectiveFive, adverbOne, verbOne, nouns, adjectives
    madLibsChoice = int(input("Enter the number of the Mad Lib you would like to play, there are four Mad Libs: "))

    nounOne = input("Enter a noun: ")
    nounTwo = input("Enter a noun: ")
    nounThree = input("enter a noun: ")
    nounFour = input("enter a noun: ")
    nounFive = input("enter a noun: ")

    adjectiveOne = input("enter an adjective: ")
    adjectiveTwo = input("enter an adjective: ")
    adjectiveThree = input("enter an adjective: ")
    adjectiveFour = input("enter an adjective: ")
    adjectiveFive = input("enter an adjective: ")

    adverbOne = input("enter an adverb: ")

    verbOne = input("enter a verb: ")

    nouns = [nounOne, nounTwo, nounThree, nounFour, nounFive]
    adjectives = [adjectiveOne, adjectiveTwo, adjectiveThree, adjectiveFour, adjectiveFive] 
    

main()

if madLibsChoice == 1:
    madLibsOne()
elif madLibsChoice == 2:
    madLibsTwo()
elif madLibsChoice == 3:
    madLibsThree()
