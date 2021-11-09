
def bubbleSort(SortList):
    for i in range(len(SortList) - 1,0,-1):
        for j in range(i):
            if SortList[j] > SortList[j + 1]:
                temp = SortList[j]
                SortList[j] = SortList[j + 1]
                SortList[j + 1] = temp

SortList = [1,3,33,55,32,22]
bubbleSort(SortList)
print(SortList)
            