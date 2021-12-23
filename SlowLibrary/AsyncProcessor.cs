namespace SlowLibrary;

/// <summary>
/// The AsyncProcessor class implements the IAsyncEnumerable interface
/// which allows us to use the object in a foreach loop.
/// To simulate a long-running process, the enumerator contains
/// a call to Task.Delay() in the MoveNext() method.
/// </summary>
/// <example>
///   var processor = new AsyncProcessor(100);
///   await foreach (var item in processor)
///   {
///     Console.WriteLine(item.ToString());
///   }
/// </example>
public class AsyncProcessor : IAsyncEnumerable<int>
{
    private readonly int totalIterations;

    public AsyncProcessor(int iterations)
    {
        this.totalIterations = iterations;
    }

    public async IAsyncEnumerator<int> GetAsyncEnumerator(
        CancellationToken cancellationToken = default)
    {
        int currentPosition = 0;
        while (currentPosition < totalIterations)
        {
            // START - Interesting async stuff goes here
            await Task.Delay(100, cancellationToken);
            // END - Interesting async stuff goes here
            currentPosition++;
            if (currentPosition >= 65)
                throw new IndexOutOfRangeException("Value cannot be over 65");
            yield return currentPosition;
        }
    }
}