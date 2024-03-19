using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1124M_A1 {
    internal class Searching {
        // Linear Search
        public static int LinearSearch(int[] a, int value, bool ascending) {
            int steps = 0;
            // Minimum difference helps finding out distance for closest values to a value
            int minDiff = int.MaxValue;
            // Iterating over the array based on the ascending bool
            int i = ascending ? 0 : a.Length - 1;

            // Storing all indices of the value
            List<int> indices = [];
            // Storing all occorunces of value and index of closest values
            List<(int Value, int Index)> closestValues = [];

            // Checking interval bounds
            while (ascending ? i < a.Length : i >= 0) {
                steps++;
                // Finding current difference between values
                int diff = Math.Abs(a[i] - value);
                // Value has been found
                if (a[i] == value) {
                    indices.Add(i);
                    // Breaking the loop early if next value is different
                    if (ascending ? i + 1 < a.Length && a[i + 1] != value : i - 1 >= 0 && a[i - 1] != value) {
                        break;
                    }
                    // Keeping track of closest values
                } else if (diff <= minDiff) {
                    // Checking if current difference is smaller than minimum difference
                    if (diff < minDiff) {
                        closestValues.Clear();
                        minDiff = diff;
                    }
                    // Updating the closest values and their indices
                    closestValues.Add((a[i], i));
                    // If the next value has a greater difference, break the loop
                    if (ascending ? i + 1 < a.Length && Math.Abs(a[i + 1] - value) > minDiff : i - 1 >= 0 && Math.Abs(a[i - 1] - value) > minDiff) {
                        break;
                    }
                }

                // Moving to next element based on ascending bool
                i = ascending ? i + 1 : i - 1;
            }

            // Results
            if (indices.Count > 0) {
                // Output if value is found
                Console.WriteLine($"Value {value} found at indices: {string.Join(", ", indices)}");
            } else {
                // Output if value is not found
                Console.WriteLine($"Value {value} not found. Closest values are: ");
                foreach (var item in closestValues) {
                    Console.WriteLine($"{item.Value} at index {item.Index}");
                }
            }

            return steps;
        }



        // Binary Search
        public static int BinarySearch(int[] a, int value, bool ascending) {
            int start = 0;
            int end = a.Length - 1;
            int steps = 0;
            // Minimum difference helps finding out distance for closest values to a value
            int minDiff = int.MaxValue;

            // Storing all indices of the value
            List<int> indices = [];
            // Storing all occorunces of value and index of closest values
            List<(int Value, int Index)> closestValues = [];

            while (start <= end) {
                steps++;
                // Getting middle index by dividing the interval into 2 parts
                int mid = (end + start) / 2;

                // Value is in the left half
                if (ascending ? value < a[mid] : value > a[mid]) {
                    end = mid - 1;
                // Value is in the right half
                } else if (ascending ? value > a[mid] : value < a[mid]) {
                    start = mid + 1;
                // Value has been found
                } else {
                    // If the value is found, add its index to the list
                    indices.Add(mid);

                    // Continue searching in both directions for other occurrences
                    int left = mid - 1;
                    while (left >= 0 && a[left] == value) {
                        indices.Add(left);
                        left--;
                    }

                    int right = mid + 1;
                    while (right < a.Length && a[right] == value) {
                        indices.Add(right);
                        right++;
                    }

                    // Exiting the loop after finding all occurrences
                    break;
                }

                // Keeping track of closest values
                closestValues.Clear();
                // Checking interval bounds
                if (end >= 0) {
                    int diff = Math.Abs(a[end] - value);
                    if (diff <= minDiff) {
                        if (diff < minDiff) {
                            closestValues.Clear();
                            minDiff = diff;
                        }
                        closestValues.Add((a[end], end));
                    }
                }
                // Checking interval bounds
                if (start < a.Length) {
                    int diff = Math.Abs(a[start] - value);
                    if (diff <= minDiff) {
                        if (diff < minDiff) {
                            closestValues.Clear();
                            minDiff = diff;
                        }
                        closestValues.Add((a[start], start));
                    }
                }
            }

            // Results
            if (indices.Count > 0) {
                // Output if value is found
                Console.WriteLine($"Value {value} found at indices: {string.Join(", ", indices)}");
            } else {
                // Output if the value is not found
                Console.WriteLine($"Value {value} not found. Closest values are: ");
                foreach (var item in closestValues) {
                    Console.WriteLine($"{item.Value} at index {item.Index}");
                }
            }

            return steps;
        }

    }
}
