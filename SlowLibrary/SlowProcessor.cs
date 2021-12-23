using System.Collections;

namespace SlowLibrary;

/// <summary>
/// The SlowProcessor class implements the IEnumerable interface
/// which allows us to use the object in a foreach loop.
/// To simulate a long-running process, the enumerator contains
/// a call to Sleep() in the MoveNext() method.
/// </summary>
/// <example>
///   var processor = new SlowProcessor(100);
///   foreach (var item in processor)
///   {
///     Console.WriteLine(item.ToString());
///   }
/// </example>
public class SlowProcessor : IEnumerable<int>
{
    private readonly int totalIterations;

    public SlowProcessor(int iterations)
    {
        totalIterations = iterations;
    }

    public IEnumerator<int> GetEnumerator()
    {
        int currentPosition = 0;
        while (currentPosition < totalIterations)
        {
            Thread.Sleep(100);
            currentPosition++;
            if (currentPosition >= 65)
                throw new IndexOutOfRangeException("Value cannot be over 65");
            yield return currentPosition;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
