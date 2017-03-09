# Data Structures, Algorithms and Complexity Homework
### 1. What is the expected running time of the following C# code?

```C#
long Compute(int[] arr)
{
    long count = 0;
    for (int i=0; i<arr.Length; i++)
    {
        int start = 0, end = arr.Length-1;
        while (start < end)
            if (arr[start] < arr[end])
                { start++; count++; }
            else 
                end--;
    }
    return count;
}
```

* Worst case: **O(n²)**
* Average case **O(n²)**
* Explanation: There is one outer loop to **n** , and another inner loop which depending on a condition either increments **start** or decrements **end** that is moving **start** or **end** towards the other. Thus it is effectively the same count of repetitions if only **start** was increasing or only **end** was decreasing. 
* *Side note: The inner loop will always produce the same result because the initial conditions* **start = 0** *and* **end = arr.Length - 1** *are always the same, so* **count** *can be calculated by:*

    ```C#
    long Compute(int[] arr)
    {
        long count = 0;
        int start = 0, 
            end = arr.Length - 1;
        while (start < end)
        {
            if (arr[start] < arr[end])
                { start++; count++; }
            else 
                { end--; }          
        }
    
        return count *= arr.Length;
    }
    ```
*reducing worst case scenario to* **O(n)**

---
### 2. What is the expected running time of the following C# code?

```C#
long CalcCount(int[,] matrix)
{
    long count = 0;
    for (int row=0; row<matrix.GetLength(0); row++)
        if (matrix[row, 0] % 2 == 0)
            for (int col=0; col<matrix.GetLength(1); col++)
                if (matrix[row,col] > 0)
                    count++;
    return count;
}
```
* Worst case: **O(n \* m)**
* Average case: **O(n \* m/2)**
* Explanation: 
    * Worst case: Because there is loop to **n** with a nested loop to **m** 
    * Average case: Because the execution of the nested loop depends on the **condition** that the first element of the current row is **even** in the best posible case that will always be false and the algorithm will run **O(n)** and in the worst case that will always be false resulting in **O(n \* m)**, since the **condition** have only 2 outcomes, the nested loop runs **m / 2** times on average

---
### 3. (*) What is the expected running time of the following C# code?

```C#
long CalcSum(int[,] matrix, int row)
{
    long sum = 0;
    for (int col = 0; col < matrix.GetLength(0); col++) 
        sum += matrix[row, col];
    if (row + 1 < matrix.GetLength(1)) 
        sum += CalcSum(matrix, row + 1);
    return sum;
}

Console.WriteLine(CalcSum(matrix, 0));
```
* Worst case: **O(n \* m)**
* Average case: **O(n/2 \* m)**
* Explanation: The method **CalcSum** is calculating a sum for all cells in a given matrix row **O(m)** if the row is **not** the last row of the matrix the procedure is recursively repeated for the next row. So the worst case would be starting from the first row **O(n \* m)** and the best would be starting from the last row **O(1 \* m)** or just **O(m)**, so the average result would be similar to starting from the middle (n / 2) row.

