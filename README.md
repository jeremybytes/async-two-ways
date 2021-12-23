# 2 Ways of Handling Async Enumeration in C#

## Abstract  

*Work in progress*  
This code shows how to handle asynchronous enumerations two different ways. The first uses a standard IEnumerable&lt;T&gt; object with the asynchronous code on the consuming side (i.e. the code that iterates the enumeration with "foreach"). The second uses an IAsyncEnumerable&lt;T&gt; object with the asynchronous code in the library.  

Both examples show how to run the enumeration asynchronously, deal with exceptions, and handle cancellation.  

*Work in progress - this is just a rough outline at this point. The code is (more or less) complete, but more descriptions and instructions will be coming as this gets built out more. The title and location of this repository may also change.*  

## Projects  
**SlowLibrary**  
Contains the IEnumerable and IAsyncEnumerable objects used by the other projects.
* SlowProcessor - the IEnumerable object (no async)
* AsyncProcessor - the IAsyncEnumerable object (async)

**Tasks.WPF**  
A WPF (Windows desktop) application that uses the SlowProcessor (non-async) object. The asynchronous bits are handled in the code-behind of the form.
* MainWindow.xaml.cs - contains the relevant code

**AsyncEnum.WPF** 
A WPF (Windows dekstop) application that uses the AsyncProcessor (async) object. 
* MainWindow.xaml.cs - contains the relevant code

## Running the Projects
Note: These projects use .NET 6.0, and since they are Windows dekstop projects, they will run on Windows only (not Linux or macOS).

**In Visual Studio 2022**, set the appropriate application (Task.WPF or AsyncEnum.WFP) as the startup project and run.

**From the command line**, navigate to the appropriate application folder (/Tasks.WPF or /AsyncEnum.WPF) and type *dotnet run*.