﻿// See https://aka.ms/new-console-template for more information
string filePath = "list.txt";

List<String> myList = new List<string>();

if (File.Exists(filePath)){myList.AddRange(File.ReadAllLines(filePath));}

Console.Clear();
while (true)
{
    string input = Menu();

    if(input == "A") {ViewList();}
    if(input == "B") { AddList();}
    if(input == "C") { RemoveList();}
    if(input == "D") { Environment.Exit(0); }
}

string Menu() {
   string input = "";
   string error = "";
        do
        {
            Console.Clear();  // Clear the console every time before showing the menu
            Console.WriteLine("Press a key");
            Console.WriteLine("A. View List");
            Console.WriteLine("B. Add to List");
            Console.WriteLine("C. Remove from List");
            Console.WriteLine("D. Exit App");

            if(error != "") {
                Console.WriteLine(error);
            }

            // Wait for a key press
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);  // 'true' to hide the key pressed in the console

            // Check which key was pressed
            if (keyInfo.Key == ConsoleKey.A)
            {
                input = "A";
            }
            else if (keyInfo.Key == ConsoleKey.B)
            {
                input = "B";
            }
            else if (keyInfo.Key == ConsoleKey.C)
            {
                input = "C";
            }
            else if (keyInfo.Key == ConsoleKey.D)
            {
                input = "D";
            }
            Console.Clear();
            error = "Please press valid key";

        } while (input != "A" && input != "B" && input != "C" && input != "D");

        return input;
    }
void ViewList() {
    Console.WriteLine("Press Enter for Menu ...");
    foreach(string item in myList) {
                Console.WriteLine(item);
            }
    do 
    {
        
    }while (Console.ReadKey(intercept: true).Key != ConsoleKey.Enter);
}
void AddList() {
    Console.Write("Type item for list: ");
    myList.Add(Console.ReadLine());
    File.WriteAllLines(filePath, myList);
}
void RemoveList() {
    string input = Console.ReadLine();
    // Step 4: Remove the item if it exists in the list
        if (!string.IsNullOrEmpty(input))  // Only attempt to remove if the input is not empty
        {
            if (myList.Contains(input))
            {
                myList.Remove(input);
               
          
            }
            File.WriteAllLines(filePath, myList);
}
}