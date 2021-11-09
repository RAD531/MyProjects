
arrayInput = str(input('Please enter numbers for the array, sepearated by a comma: '))
try:
    quickSortArray = [x.strip() for x in arrayInput.split(',')]
    max = 0
    for x in quickSortArray:
        x = int(x)
        pivot = x
        max = max + 1

    i = quickSortArray[0]
    j = quickSortArray[0]

    for x in quickSortArray:
        if j > pivot:
            j = j + 1
        elif j < pivot:
            temp = quickSortArray[i]
            quickSortArray[i] = quickSortArray[j]
            quickSortArray[j] = temp
            i = 1 + 1
            j = j + 1
        if j == (max - 1):
            temp = quickSortArray[max]
            quickSortArray[max] = quickSortArray[i]
            quickSortArray[i] = temp

except ValueError:
    print("Array not in correct format, please seperate values by comma")


