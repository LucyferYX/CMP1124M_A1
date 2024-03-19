using System;
using System.IO;
using System.Linq;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CMP1124M_A1 {
    internal class Program {
        static void Main(string[] args) {
            bool doProgram = true;
            bool valid = false;
            bool ascending = true;

            int sortingSteps = 0;
            int searchingSteps = 0;
            int chosenValue = 0;
            int[] chosenArray = [];

            string chosenName = "";
            string chosenSorting = "";
            string chosenSearching = "";

            Console.WriteLine("Program launched. Select array groups -> array name -> sorting way -> sorting method -> searched value -> continue program.");
            ConsoleKeyInfo keyInfo;

            while (doProgram) {

                // Selecting arrays
                Console.WriteLine("\nWhat arrays to select? Press:\n[1] for 256 files \n[2] for 2048 files \n[3] for merging 256 files \n[4] for merging 2048 files \n[X] to Exit");
                valid = false;
                while (!valid) {
                    keyInfo = Console.ReadKey(intercept: true);
                    switch (keyInfo.Key) {
                        case ConsoleKey.D1:
                            valid = true;
                            (chosenArray, chosenName) = Utilities.ChooseArray(["Net_1_256.txt", "Net_2_256.txt", "Net_3_256.txt"]);
                            break;
                        case ConsoleKey.D2:
                            valid = true;
                            (chosenArray, chosenName) = Utilities.ChooseArray(["Net_1_2048.txt", "Net_2_2048.txt", "Net_3_2048.txt"]);
                            break;
                        case ConsoleKey.D3:
                            valid = true;
                            (chosenArray, chosenName) = Utilities.MergeAndReadArray("Net_1_256.txt", "Net_3_256.txt");
                            break;
                        case ConsoleKey.D4:
                            valid = true;
                            (chosenArray, chosenName) = Utilities.MergeAndReadArray("Net_1_2048.txt", "Net_3_2048.txt");
                            break;
                        case ConsoleKey.X:
                            Environment.Exit(0);
                            break;
                        default:
                            Utilities.WriteRedLine("Invalid input. Try again.");
                            break;
                    }
                }

                // Choosing sorting way
                Console.WriteLine("\nWhich way to sort array? Press:\n[1] for Ascending \n[2] for Descending \n[X] to Exit");
                valid = false;
                while (!valid) {
                    keyInfo = Console.ReadKey(intercept: true);
                    switch (keyInfo.Key) {
                        case ConsoleKey.D1:
                            valid = true;
                            ascending = true;
                            Console.WriteLine("Picked ascending order.");
                            break;
                        case ConsoleKey.D2:
                            valid = true;
                            ascending = false;
                            Console.WriteLine("Picked descending order.");
                            break;
                        case ConsoleKey.X:
                            Environment.Exit(0);
                            break;
                        default:
                            Utilities.WriteRedLine("Invalid input. Try again.");
                            break;
                    }
                }

                // Choosing sorting method
                Console.WriteLine("\nWhich sorting method to choose? Press:\n[1] for Bubble Sort \n[2] for Insertion Sort \n[3] for Merge Sort \n[4] for Quicksort \n[5] for all \n[X] to Exit");
                valid = false;
                while (!valid) {
                    try {
                        keyInfo = Console.ReadKey(intercept: true);
                        switch (keyInfo.Key) {
                            case ConsoleKey.D1:
                                chosenSorting = "Bubble Sort";
                                sortingSteps = Sorting.BubbleSort(chosenArray, ascending);
                                Console.WriteLine($"Number of steps for {chosenSorting}: {sortingSteps}");
                                valid = true;
                                break;
                            case ConsoleKey.D2:
                                chosenSorting = "Insertion Sort";
                                sortingSteps = Sorting.InsertionSort(chosenArray, ascending);
                                Console.WriteLine($"Number of steps for {chosenSorting}: {sortingSteps}");
                                valid = true;
                                break;
                            case ConsoleKey.D3:
                                chosenSorting = "Merge Sort";
                                sortingSteps = Sorting.MergeSort(chosenArray, 0, chosenArray.Length-1, ascending);
                                Console.WriteLine($"Number of steps for {chosenSorting}: {sortingSteps}");
                                valid = true;
                                break;
                            case ConsoleKey.D4:
                                chosenSorting = "Quicksort";
                                sortingSteps = Sorting.QuickSort(chosenArray, 0, chosenArray.Length-1, ascending);
                                Console.WriteLine($"Number of steps for {chosenSorting}: {sortingSteps}");
                                valid = true;
                                break;
                            case ConsoleKey.D5:
                                valid = true;
                                chosenSorting = "All Sorting methods";
                                int[] arrayCopy1 = (int[])chosenArray.Clone();
                                int steps1 = Sorting.BubbleSort(arrayCopy1, ascending);
                                Console.WriteLine($"Number of steps for Bubble Sort: {steps1}");
                                int[] arrayCopy2 = (int[])chosenArray.Clone();
                                int steps2 = Sorting.InsertionSort(arrayCopy2, ascending);
                                Console.WriteLine($"Number of steps for Insertion Sort: {steps2}");
                                int[] arrayCopy3 = (int[])chosenArray.Clone();
                                int steps3 = Sorting.MergeSort(arrayCopy3, 0, chosenArray.Length-1, ascending);
                                Console.WriteLine($"Number of steps for Merge Sort: {steps3}");
                                int steps4 = Sorting.QuickSort(chosenArray, 0, chosenArray.Length-1, ascending);
                                Console.WriteLine($"Number of steps for Quicksort: {steps4}");
                                sortingSteps = steps1 + steps2 + steps3 + steps4;
                                Console.WriteLine($"Total steps: {sortingSteps}");
                                break;
                            case ConsoleKey.X:
                                Environment.Exit(0);
                                break;
                            default:
                                Utilities.WriteRedLine("Invalid input. Try again.");
                                break;
                        }
                    } catch (Exception ex) {
                        Utilities.WriteRedLine($"Error: {ex.Message} Try again.");
                    }
                }

                // Choosing which values to output
                Console.WriteLine("\nWhich values to output? Press:\n[1] for every 10th \n[2] for every 50th \n[3] for all \n[4] for none \n[X] to Exit");
                valid = false;
                while (!valid) {
                    try {
                        keyInfo = Console.ReadKey(intercept: true);
                        switch (keyInfo.Key) {
                            case ConsoleKey.D1:
                                Console.WriteLine($"{chosenName} contents for every 10th value:");
                                Utilities.OutputArray(chosenArray, 10);
                                valid = true;
                                break;
                            case ConsoleKey.D2:
                                Console.WriteLine($"{chosenName} contents for every 50th value:");
                                Utilities.OutputArray(chosenArray, 50);
                                valid = true;
                                break;
                            case ConsoleKey.D3:
                                Console.WriteLine($"{chosenName} contents:");
                                Utilities.OutputArray(chosenArray, 1);
                                valid = true;
                                break;
                            case ConsoleKey.D4:
                                valid = true;
                                break;
                            case ConsoleKey.X:
                                Environment.Exit(0);
                                break;
                            default:
                                Utilities.WriteRedLine("Invalid input. Try again.");
                                break;
                        }
                    } catch (Exception ex) {
                        Utilities.WriteRedLine($"Error: {ex.Message} Try again.");
                    }
                }

                // Checking whether array is sorted fully
                Console.WriteLine("\nCheck whether array is sorted? Press:\n[1] for Yes \n[2] for No \n[X] to Exit");
                valid = false;
                while (!valid) {
                    try {
                        keyInfo = Console.ReadKey(intercept: true);
                        switch (keyInfo.Key) {
                            case ConsoleKey.D1:
                                Console.WriteLine(Utilities.CheckSortOrder(chosenArray));
                                valid = true;
                                break;
                            case ConsoleKey.D2:
                                valid = true;
                                break;
                            case ConsoleKey.X:
                                Environment.Exit(0);
                                break;
                            default:
                                Utilities.WriteRedLine("Invalid input. Try again.");
                                break;
                        }
                    } catch (Exception ex) {
                        Utilities.WriteRedLine($"Error: {ex.Message} Try again.");
                    }
                }

                // Choosing which value to search
                Console.WriteLine("\nWrite down which value to search for. Enter X to exit.");
                valid = false;
                while (!valid) {
                    try {
                        string input = Console.ReadLine();

                        if (input.Equals("X", StringComparison.OrdinalIgnoreCase)) {
                            Environment.Exit(0);
                        }

                        chosenValue = int.Parse(input);
                        valid = true;
                    } catch (Exception ex) {
                        Utilities.WriteRedLine($"Error: {ex.Message} Try again.");
                    }
                }

                // Choosing searching method
                Console.WriteLine("\nWhich searching method to choose? Press:\n[1] for Linear Search \n[2] for Binary Search \n[3] for both \n[X] to Exit");
                valid = false;
                while (!valid) {
                    keyInfo = Console.ReadKey(intercept: true);
                    switch (keyInfo.Key) {
                        case ConsoleKey.D1:
                            valid = true;
                            chosenSearching = "Linear Search";
                            searchingSteps = Searching.LinearSearch(chosenArray, chosenValue, ascending);
                            Console.WriteLine($"Number of steps: {searchingSteps}");
                            break;
                        case ConsoleKey.D2:
                            valid = true;
                            chosenSearching = "Binary Search";
                            searchingSteps = Searching.BinarySearch(chosenArray, chosenValue, ascending);
                            Console.WriteLine($"Number of steps: {searchingSteps}");
                            break;
                        case ConsoleKey.D3:
                            valid = true;
                            chosenSearching = "All Searching methods";
                            int steps1 = Searching.LinearSearch(chosenArray, chosenValue, ascending);
                            Console.WriteLine($"Number of steps for Linear Search: {steps1}");
                            int steps2 = Searching.BinarySearch(chosenArray, chosenValue, ascending);
                            Console.WriteLine($"Number of steps for Binary Search: {steps2}");
                            searchingSteps = steps1 + steps2;
                            Console.WriteLine($"Total steps: {searchingSteps}");
                            break;
                        case ConsoleKey.X:
                            Environment.Exit(0);
                            break;
                        default:
                            Utilities.WriteRedLine("Invalid input. Try again.");
                            break;
                    }
                }

                // Results
                Utilities.WriteResults(chosenName, chosenArray.Length, chosenSorting, ascending, sortingSteps, chosenSearching, chosenValue, searchingSteps);

                // Continuing the program
                Console.WriteLine("\n\nWould you like to continue? Press 1 for Yes, 2 for No.");
                valid = false;
                while (!valid) {
                    keyInfo = Console.ReadKey(intercept: true);
                    if (keyInfo.Key == ConsoleKey.D1) {
                        valid = true;
                    } else if (keyInfo.Key == ConsoleKey.D2) {
                        valid = true;
                        doProgram = false;
                    } else {
                        Utilities.WriteRedLine("Invalid input. Try again.");
                    }
                }

            }
        }
    }
}
