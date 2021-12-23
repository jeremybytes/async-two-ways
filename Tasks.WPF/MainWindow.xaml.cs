using SlowLibrary;
using System.Windows;

namespace Tasks.WPF;

public partial class MainWindow : Window
{
    private CancellationTokenSource? tokenSource;

    public MainWindow()
    {
        InitializeComponent();
    }

    private async void StartButton_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(InputTextBox.Text, out int iterations)) return;

        tokenSource = new CancellationTokenSource();

        ResetUIElements(true);

        var processor = new SlowProcessor(iterations);

        try
        {
            IProgress<(int, string)> progress =
                new Progress<(int, string)>(p =>
                {
                    (int percent, string message) = p;
                    MainProgressBar.Value = percent;
                    OutputTextBox.Text = message;
                });

            await Task.Run(() =>
            {
                foreach (var current in processor)
                {
                    // Cancellation
                    tokenSource.Token.ThrowIfCancellationRequested();

                    // Progress Reporting
                    int percentageComplete =
                        (int)((float)current / iterations * 100);
                    string progressMessage = $"Iteration {current} of {iterations}";
                    progress.Report((percentageComplete, progressMessage));
                }
            });

            OutputTextBox.Text = "Done!";
            MainProgressBar.Value = 0;
        }
        catch (OperationCanceledException)
        {
            OutputTextBox.Text = "CANCELED!";
        }
        catch (Exception ex)
        {
            OutputTextBox.Text = ex.Message;
        }
        finally
        {
            ResetButtons(false);
        }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        tokenSource?.Cancel();
    }

    private void ResetUIElements(bool isRunning)
    {
        ResetButtons(isRunning);
        OutputTextBox.Text = string.Empty;
        MainProgressBar.Value = 0;
    }

    private void ResetButtons(bool isRunning)
    {
        StartButton.IsEnabled = !isRunning;
        CancelButton.IsEnabled = isRunning;
    }
}
