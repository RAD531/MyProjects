from decimal import Decimal
import time

def time_convert(sec):
    sec = sec % 60
    print("Time Lapsed = ", sec)



#used in while loop to indentify if valid answer is given
validMessage = False
#used to tell if the user wants to carry on with another calculation, starts at false
exit = False

#once exit = true, end application
while not exit == True: #keep looping until valid number is given
        #reset the validMessage after yes answer is given
        validMessage = False
        #try to convert the input into float
        try:
            #input from user
            number = Decimal(input('please enter the the decimal: '))

            #start the stopwatch
            start_time = time.time()

            numberTemp = int(number)
            #get the remainder for binary conversion with decimal fractions
            remainder = number - numberTemp
            # array to store binary values
            binaryNumbersArray = []
            #keep looping until inout number is less than 0
            while (number > 0):
                #value of the remainder when divided by 2 - if any
                roundedNum = int(float(number % 2))
                #add remainder value to array
                binaryNumbersArray.append(roundedNum)
                #ensure when divided, its rounded down to avoid half numbers
                number = int(number / 2)

            #create array to store remainders in binary
            binaryNumberArrayRem = []
            #create a dot message if conversion is infinate
            dotMessage = ""
            #only do remainder version if exist
            if (remainder > 0):
                #keep converting until above 0
                while remainder > 0:
                    remainder = remainder * 2
                    #round the number down to get binary number 
                    roundedNum = int(remainder)
                    #if binary is 1, the divide remainder by 10 for next loop
                    if roundedNum == 1:
                        remainder = remainder - roundedNum

                    #add binary to list
                    binaryNumberArrayRem.append(roundedNum)

                    #if the array is over 20, chances are, its on a endless loop, so stop the loop
                    if (len(binaryNumberArrayRem) > 20):
                        remainder = -1
                        dotMessage = "..."

            #create string var for remainder binary answer
            binarystringrem = ""
            #reverse the array and count down from total
            for i in binaryNumberArrayRem[::-1]:
                binarystringrem = binarystringrem + str(i)

            #create string var for binary answer
            binarystring = ""
            #reverse the array and count down from total
            for i in binaryNumbersArray[::-1]:
                binarystring = binarystring + str(i)

            #combine both lists together for complete answer, if no remainder, then it will show blank
            binarystring = binarystring + "." + binarystringrem + dotMessage
            #print the answer
            print(binarystring)

            #stop the stopwatch
            end_time = time.time()
            #do calculation and run timer function
            time_lapsed = end_time - start_time
            time_convert(time_lapsed)

            #while valid answer is does not meet the valid responses 
            while validMessage == False:

                #get the response
                exitResponse = str(input('Would you like to make another calculation?'))
                #if user types in variations of yes, then stop loop, go back to outer while loop
                if (exitResponse == 'yes' or exitResponse == 'y' or exitResponse=='Yes'):
                    validMessage = True
                #if no, then come out of both while loops
                elif (exitResponse == 'no' or exitResponse== 'n' or exitResponse == 'No'):
                    validMessage=True
                    exit = True
                #if anthing else, then send message back to user and keep going round the loop
                else:
                    print('Please specify a valid response, either yes or no')
                    validMessage = False

        except Exception:
            # not a valid number, loop back
            print("Please enter a valid number")

