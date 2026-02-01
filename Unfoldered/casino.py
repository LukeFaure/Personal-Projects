import curses
from random import randint

def roulette(stdscr):
    curses.curs_set(0)
    stdscr.nodelay(False)
    stdscr.keypad(True)

    stdscr.clear()

    numbers = list(range(1, 51))

    for number in numbers:
        if number % 2 == 0 and number != 1 and number != 50:
            isred = True
        elif number % 2 != 0 and number != 1 and number != 50:
            isblack = True
        elif number == 1 or number == 50:
            isgold = True        

    stdscr.addstr("Welcome to Roulette \nIn this version, there are 50 numbers to land on, the odd numbers are black, even numbers are red. 1 and 50 are gold and quadruple your investment.\nGood Luck!")
    stdscr.addstr("\n\nTable of Odds: \nRed = 24/50 earnings = 1.5x \nBlack = 24/50 earnings = 1.5x \nGold = 2/50 earnings = 2.0x \nNo. = 1/50 earnings = 4.0x\n")

    stdscr.addstr("\nIf you wish to return, press esc, press enter to continue")
    key = stdscr.getch()
    if key == 27:
        menu(stdscr)
    elif key == 10:
        pass
    
    
    def spin(stdscr):
        curses.curs_set(0)
        stdscr.nodelay(False)
        stdscr.keypad(True)
            
        stdscr.addstr("\nEnter betting amount: ")
        userbet = stdscr.getstr().decode("utf-8")
        stdscr.addstr(f"\nBet = {userbet} \nDo you wish to continue? (y/n) ")
        userbet = int(userbet)
        
        key = stdscr.getch()
        
        if key == ord("y"):
            stdscr.refresh()
            stdscr.getch()
            stdscr.clear()
            
            stdscr.addstr("Will you bet on even, odd, gold, or a specific number? ")
            stdscr.addstr("\nEnter 'e' for even, 'o' for odd, 'g' for gold, or a number: ")
            bettype = stdscr.getch()

            if bettype == ord("e"):
                stdscr.clear()
                stdscr.addstr("You have chosen even numbers. The wheel will spin when you hit enter.\n")

            elif bettype == ord("o"):
                stdscr.clear()
                stdscr.addstr("You have chosen odd numbers. The wheel will spin when you hit enter.\n")

            elif bettype == ord("g"):
                stdscr.clear()
                stdscr.addstr("You have chosen gold numbers. The wheel will spin when you hit enter.\n")

            else:
                stdscr.clear()
                stdscr.addstr(f"You have chosen the number {chr(bettype)}. The wheel will spin when you hit enter.\n")

            stdscr.addstr("\nPress Enter to spin the wheel.")
            key = stdscr.getch()  # Wait for Enter to proceed

            if key in (10, 13):  # Enter key
                stdscr.clear()
                stdscr.addstr("Spinning the wheel...\n")

                # Simulate a random number between 1 and 50
                winning_number = randint(1, 50)

                stdscr.addstr(f"The winning number is {winning_number}\n")
                stdscr.refresh()

                # Evaluate if the user's bet wins
                if bettype == ord("e") and winning_number % 2 == 0:
                    stdscr.addstr("You win! Even numbers win.\n")
                elif bettype == ord("o") and winning_number % 2 != 0:
                    stdscr.addstr("You win! Odd numbers win.\n")
                elif bettype == ord("g") and (winning_number == 1 or winning_number == 50):
                    stdscr.addstr("You win! Gold numbers win.\n")
                elif bettype == ord(chr(winning_number)):
                    stdscr.addstr(f"You win! You guessed the correct number: {winning_number}\n")
                else:
                    stdscr.addstr("You lose. Better luck next time.\n")

                stdscr.addstr("\nPress any key to return to the menu.")
                stdscr.getch()

                roulette(stdscr)  # Go back to the roulette screen after the result

        elif key == ord("n"):
            stdscr.refresh()
            stdscr.getch()
            roulette(stdscr)

    spin(stdscr)
        
def menu(stdscr):
    curses.curs_set(0)
    stdscr.nodelay(True)
    stdscr.keypad(True)

    position = 0

    while True:
        stdscr.clear()
        if position == 0:
            stdscr.addstr("[Roulette] \nBlack Jack \nSlots")
        elif position == 1:
            stdscr.addstr("Roulette \n[Black Jack] \nSlots")
        elif position == 2:
            stdscr.addstr("Roulette \nBlack Jack \n[Slots]")
        stdscr.refresh()

        key = stdscr.getch()
        if key == -1:
            continue

        elif key == curses.KEY_UP:
            if position > 0:
                position -= 1

        elif key == curses.KEY_DOWN:
            if position < 2:
                position += 1

        elif key in (10, 13, curses.KEY_ENTER):  # Enter key
            if position == 0:
                roulette(stdscr)
            else:
                continue

        elif key == 27:  # ESC key
            break

def main(stdscr):
    curses.curs_set(0)          # Hide the cursor
    stdscr.nodelay(True)        # Don't block on getch()
    stdscr.keypad(True)         # Enable arrow keys and function keys

    stdscr.addstr("Press r to run or ESC to quit.\n")

    while True:
        key = stdscr.getch()

        if key == -1:
            continue  # No input

        elif key == 114:
            menu(stdscr)

        elif key == 27:  # ESC key
            stdscr.addstr("Escape key pressed. Exiting...\n")
            break

        stdscr.refresh()

curses.wrapper(main)
