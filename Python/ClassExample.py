#Task 
#Read two integers from STDIN and print three lines where:

#The first line contains the sum of the two numbers.
#The second line contains the difference of the two numbers (first - second).
#The third line contains the product of the two numbers.

#Input Format
#The first line contains the first integer, . The second line contains the second integer, .

#Output Format
#Print the three lines as explained above.

def sumOfInput( inputA, inputB ) :
    print("Entered sumOfInput")
    sum = inputA + inputB
    print("Sum inside of sumOfInput", sum)
    return sum


class MyClass:
    """A simple example class"""
    i = 12345
    
    def f(self):
        return 'hello world'

class Blizzard:
    def __init__(self, input1):
        self.userInput = input1    # instance variable unique to each instance
        return self


a = int(input())
b = Blizzard(a)
print(b)

#x = MyClass()
#y = MyClass.f
#print(y)
#print(x.i)



#print("Input Number")
#a = int(input())
#print("Input Number")
#b = int(input())

#print("Calling sumOfInput")
#answer1 = sumOfInput(a, b)
#print("answer1 = ", answer1)