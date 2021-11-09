
#determine the size of the collection
total = 444
#create empty array
numbers = []
#use this to show user how many guesses it took to get to answer 
guesses = 0
#use this to find answer in subarrays
number = 44

#add sorted values to array
for i in range(total):
    numbers.append(i)

#get the min and max of array
min = 0
max = len(numbers)

#keep going until min is less than max
while min <= max:
    #increment the guess after each loop
    guesses += 1

    #find the middle value, round down
    middle = int((min + max) / 2)
    #if the number equals the middle value then found 
    if number == middle:
        print("Found Match" + "Number of guesses = " + str(guesses))
        break
    #if number is lower than middle, then max now equals the middle - 1
    elif number < numbers[middle]:
        max = middle - 1
    #if number is higher than middle, then mic now equals the middle + 1
    elif number > numbers[middle]:
        min = middle + 1