from turtle import *

start = False
speed(200000)
bgcolor("black")
pensize(2)
num = 1

hideturtle()

color_list = ["pink", "lime", "cyan", "aquamarine", "magenta", "mediumvioletred", "red", "blue", "yellow", "blueviolet", "dodgerblue"]

startvar = input("Start: ")

if startvar == "yes":
    start = True

while start == True:
    for i in color_list:
        color(i)
        right(90)
        for i in range(125):
            forward(5)
            left(2)
        num -= 1
        if num < 1:
            start = False
            
                
