using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1124M_A1 {
    internal class Utilities {
        // Method to choose 1 out of 3 arrays (for 256 and 2048) and read them into array with ReadArray method
        public static (int[], string) ChooseArray(string[] fileNames) {
            Console.WriteLine($"\nWhich array to read? Press:\n[1] for {fileNames[0]}, \n[2] for {fileNames[1]}, \n[3] for {fileNames[2]}.");
            bool valid = false;
            int[] chosenArray = [];
            string chosenName = "";
            while (!valid) {
                var keyInfo = Console.ReadKey(intercept: true);
                try {
                    if (keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.D2 || keyInfo.Key == ConsoleKey.D3) {
                        int index = keyInfo.Key - ConsoleKey.D1;
                        chosenArray = ReadArray(fileNames[index]);
                        chosenName = Path.GetFileNameWithoutExtension(fileNames[index]);
                        Console.WriteLine($"Array {fileNames[index]} read.");
                        valid = true;
                    } else {
                        WriteRedLine("Invalid input, try again.");
                    }
                } catch (Exception ex) {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return (chosenArray, chosenName);
        }

        // Method to merge 2 arrays and then read them into array
        public static (int[], string) MergeAndReadArray(string fileName1, string fileName2) {
            int[] array1 = ReadArray(fileName1);
            int[] array2 = ReadArray(fileName2);
            int[] chosenArray = new int[array1.Length + array2.Length];
            array1.CopyTo(chosenArray, 0);
            array2.CopyTo(chosenArray, array1.Length);
            string chosenName = $"{Path.GetFileNameWithoutExtension(fileName1)} and {Path.GetFileNameWithoutExtension(fileName2)} merge";
            Console.WriteLine($"Arrays {chosenName} have been merged and read. Length: {chosenArray.Length}");
            return (chosenArray, chosenName);
        }

        // Method to read array from file
        public static int[] ReadArray(string fileName) {
            // Reading file from bin\Debug\net8.0\ directory
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            // Reading the file lines and parsing them to integers
            string[] fileLines = File.ReadAllLines(filePath);
            return fileLines.Select(int.Parse).ToArray();
        }

        // Method to output array
        public static void OutputArray(int[] array, int step) {
            List<int> newNumbers = [];

            for (int i = step - 1; i < array.Length; i += step) {
                newNumbers.Add(array[i]);
            }

            Console.WriteLine(string.Join(", ", newNumbers));
        }

        // Method to check whether array is sorted
        public static string CheckSortOrder(int[] a) {
            bool isAscending = true;
            bool isDescending = true;

            for (int i = 0; i < a.Length - 1; i++) {
                if (a[i] > a[i + 1]) {
                    isAscending = false;
                }
                if (a[i] < a[i + 1]) {
                    isDescending = false;
                }
            }

            if (isAscending) {
                return "The array is fully sorted in ascending order.";
            } else if (isDescending) {
                return "The array is fully sorted in descending order.";
            } else {
                return "The array is not sorted.";
            }
        }

        // Method to output line in red color (for warnings and errors)
        public static void WriteRedLine(string line) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(line);
            Console.ResetColor();
        }

        // Method to output line in yellow color (for results)
        static void WriteResultLine(string line1, string line2) {
            Console.Write(line1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(line2);
            Console.ResetColor();
        }

        // Method to output results at the end of program
        public static void WriteResults(string chosenName, int arrayLength, string chosenSorting, bool ascending, int sortingSteps, string chosenSearching, int chosenValue, int searchingSteps) {
            Console.Write("\nResults: ");
            WriteResultLine("\nArray name: ", chosenName);
            WriteResultLine("\nArray size: ", arrayLength.ToString());
            WriteResultLine("\nSorting method: ", chosenSorting);
            WriteResultLine("\nSorting order: ", (ascending ? "Ascending" : "Descending"));
            WriteResultLine("\nSorting steps: ", sortingSteps.ToString());
            WriteResultLine("\nSearching method: ", chosenSearching);
            WriteResultLine("\nSearched value: ", chosenValue.ToString());
            WriteResultLine("\nSearching steps: ", searchingSteps.ToString());
        }

    }
}
