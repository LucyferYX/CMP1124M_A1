using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1124M_A1 {
    internal class Sorting {
        // Bubble sort method
        public static int BubbleSort(int[] a, bool ascending) {
            int steps = 0;
            int n = a.Length;
            for (int i=0; i < n-1; i++) {
                bool swapped = false;
                for (int j=0; j < n-1-i; j++) {
                    steps++;
                    // Comparing adjacent values
                    if (ascending ? a[j+1] < a[j] : a[j+1] > a[j]) {
                        // Swapping values with a tuple
                        (a[j+1], a[j]) = (a[j], a[j+1]);
                        swapped = true;
                    }
                }
                // Breaking loop early if no swaps occur
                if (!swapped) {
                    break;
                }
            }

            return steps;
        }

        // Insertion sort method
        public static int InsertionSort(int[] a, bool ascending) {
            int steps = 0;
            int n = a.Length;
            for (int i=1; i<n; ++i) {
                int value = a[i];
                int j = i - 1;

                // Comparing values
                while (j >= 0 && (ascending ? a[j] > value : a[j] < value)) {
                    a[j+1] = a[j];
                    j--;
                    steps++;
                }
                // Inserting value in correct index
                a[j+1] = value;
            }

            return steps;
        }

        // Merge sort method
        public static int MergeSort(int[] a, int left, int right, bool ascending) {
            int steps = 0;

            if (left < right) {
                // Getting middle index
                int mid = left + (right - left) / 2;

                // Recursively sort both halves of array
                steps += MergeSort(a, left, mid, ascending);
                steps += MergeSort(a, mid + 1, right, ascending);

                // Merge the halves
                steps += Merge(a, left, mid, right, ascending);
            }

            return steps;
        }

        // Method to merge two sorted halves in array
        private static int Merge(int[] a, int left, int mid, int right, bool ascending) {
            int steps = 0;
            // Holding merged result
            int[] temp = new int[right - left + 1];
            int i = left, j = mid + 1, k = 0;

            // Merge the two halves by choosing the smaller element at each step
            while (i <= mid && j <= right) {
                steps++;
                if ((ascending && a[i] <= a[j]) || (!ascending && a[i] >= a[j])) {
                    temp[k++] = a[i++];
                } else {
                    temp[k++] = a[j++];
                }
            }

            // Copying remaining elements from the left subarray
            while (i <= mid) {
                steps++;
                temp[k++] = a[i++];
            }

            // Copying remaining elements from the right subarray
            while (j <= right) {
                steps++;
                temp[k++] = a[j++];
            }

            // Copying elements from temp back to a
            for (i = left; i <= right; i++) {
                steps++;
                a[i] = temp[i - left];
            }

            return steps;
        }


        // Quicksort method
        public static int QuickSort(int[] a, int left, int right, bool ascending) {
            int steps = 0;

            if (left < right) {
                int[] result = Partition(a, left, right, ascending);
                // Extracting pivot index and counting steps taken in Partition method
                int pi = result[0];
                steps += result[1];

                // Recursively sort both halves of array (before pivot and after pivot)
                steps += QuickSort(a, left, pi-1, ascending);
                steps += QuickSort(a, pi+1, right, ascending);
            }

            return steps;
        }

        // Method to arrange elements based on pivot
        private static int[] Partition(int[] a, int left, int right, bool ascending) {
            int steps = 0;
            int pivot = a[right];
            int i = left - 1;

            for (int j=left; j <= right-1; j++) {
                steps++;
                // Checking if current element should be behind pivot
                if ((ascending ? a[j] < pivot : a[j] > pivot)) {
                    i++;
                    // Swapping elements
                    (a[i], a[j]) = (a[j], a[i]);
                }
            }
            // Moving the pivot with a swap
            (a[i+1], a[right]) = (a[right], a[i+1]);
            steps++;

            return [i+1, steps];
        }

    }
}
