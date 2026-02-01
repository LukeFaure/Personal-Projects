from turtle import *

speed(1000)
bgcolor("lightskyblue")

hideturtle()

penup() 
goto(-600, 200)
right(5)

color("black", "burlywood")

pendown()
begin_fill()
for x in range(2):
    forward(1285)
    right(90)
    forward(300)
    right(90)
end_fill()

penup()
goto(653.853277157, -210.876449275)
pendown()


right(90)
forward(200)

penup() 

goto(-626, -99)

pendown()

forward(200)

penup()



goto(0, 0)

left(50)

def drawPenEasy():
    colorOfPenOne = input("enter the color of your pen: ")
    colorOfPenTwo = input("enter the color of you cap: ")

    drawpen(colorOfPenOne, colorOfPenTwo)

def title():
    title = input("Enter the title of your piece: ")
    penup()
    goto(0, 250)
    color("black")
    pendown()
    write(title, font=("Ariel", 40, "bold"))

def drawpen(colorone, colortwo):

    color("black", colorone)

    pendown()

    begin_fill() 
    for x in range(2):
        forward(100)
        right(90)
        forward(10)
        right(90) 
    end_fill()

    pendown()

    forward(100)
    left(90)

    pendown()

    color("black", colortwo) 

    begin_fill()

    forward(2)
    right(90)
    forward(25)
    right(90)
    forward(14)
    right(90)
    forward(25)
    right(90)
    forward(2)

    end_fill()

drawPenEasy()

penup()

goto(100, 0)

right(90)

drawPenEasy()

penup()

goto(-100, 0)

right(90)

drawPenEasy()

penup()
goto(-250, 0) 

def drawlaptop(colorOne, colorTwo, colorThree): 

    color("black", colorOne)

    begin_fill() 

    forward(100)
    left(100)
    forward(50)
    left(80)
    forward(100)
    left(100)
    forward(50)

    end_fill()

    color("black", colorTwo)

    begin_fill()

    backward(50)
    right(80)
    forward(2)
    left(80)
    forward(51)
    left(80)



    forward(102)
    left(90)
    forward(2)

    left(90)
    forward(100)
    left(60)
    forward(2)

    end_fill()

    penup()
    goto(-245, 60)

    pendown()

    color(colorThree)
    circle(10)
    right(100)
    forward(5)
    circle(10)

    hideturtle()

colorune = input("what is the color of the laptop: ")
colordeux = input("what is the second color of the laptop (preferablly darker than the first color): ")
colortrois = input("what is the color of the logo: ")


drawlaptop(colorune, colordeux, colortrois)

title()




