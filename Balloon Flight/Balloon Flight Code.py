
import pgzrun
from random import randint

WIDTH = 800
HEIGHT = 600
gravity = 0

balloon = Actor("balloon")
balloon.pos = 400, 300

bird = Actor("bird-up")
bird.pos = randint(800, 1600), randint(10,200)

house = Actor("house")
house.pos = randint(800, 1600), 460

high_score = 0                                   
tree = Actor("tree")
tree.pos = randint(800, 1600), 450
score = 0
bird_up = True
up = False
game_over = False
number_of_updates = 0
true = True

scores = []
def update_high_scores():
        global score, scores
        filename = r"/Users/LFAU149/OneDrive - Heatherton Christian College/Digital Technologies Yr 9 Semester 2/Pygame zero/Balloon Flight/high-scores.txt"
        scores = []
        with open(filename, "r") as file:
                line = file.readline()
                high_scores = line.split()
                for high_scores in high_scores:
                        if (score > int(high_score)):
                                scores.append(str(score) + " ")
                
                        else:
                                scores.append(str(high_score) + " ")
        with open(filename, "w") as file:
                file.write(high_scores)
            
            
            
            

def display_high_scores():
    screen.draw.text("HIGH SCORES", (350, 150), color="black")
    y = 175
    position = 1
    for high_score in scores:
        screen.draw.text(str(position) + ". " + high_score, (200, y), color="black")
        y += 25
        position += 1

def draw():
    screen.blit("background", (0, 0))
    if not game_over:
        balloon.draw()
        bird.draw()
        house.draw()
        tree.draw()
        screen.draw.text("Score: " + str(score), (10, 5), color="black")
    else:
        display_high_scores()

def on_mouse_down():
    global gravity, score
    global up
    up = True
    gravity -= 3
    score += 1000000000000000000000000000000000000
   
    

def on_mouse_up():
    global gravity, score
    global up
    up = False
    gravity += 0.5
    

def flap():
    global bird_up
    if bird_up:
        bird.image = "bird-down"
        bird_up = False
    else:
        bird.image = "bird-up"
        bird_up = True 
    
def update():
    global game_over, score, number_of_updates, gravity
    score += 10000000000000000000000000000000000000000000000000000000
    if not game_over:
        if not up:
            balloon.y += gravity
            gravity += 0.05

        if bird.x > 0:
            bird.x -= 4
            if number_of_updates == 9:
                flap()
                number_of_updates = 0

            else:
                        number_of_updates += 1
        else:
                bird.x = randint(800, 1600)
                bird.y = randint(10, 200)
                score += 100000000000000
                number_of_updates = 0

        if house.right > 0:
            house.x -= 2
        else:
            house.x = randint(800,1600)
            score += 10000000000

        if tree.right > 0:
            tree.x -= 2
        else:
            tree.x = randint(800, 1600)
            score += 10000000000000

        if balloon.top < -200 or balloon.bottom > 700:
            game_over = True
            update_high_scores()

        if balloon.collidepoint(bird.x, bird.y) or \
           balloon.collidepoint(house.x, house.y) or \
           balloon.collidepoint(tree.x, tree.y):
            game_over = True
            update_high_scores()
                             
                     
pgzrun.go()
