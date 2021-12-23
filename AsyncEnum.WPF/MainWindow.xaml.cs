using SlowLibrary;
using System.Windows;

namespace AsyncEnum.WPF;

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

        ResetUIElements(true);

        tokenSource = new CancellationTokenSource();

        var processor = new AsyncProcessor(iterations);
        try
        {
            await foreach (int current in processor.WithCancellation(tokenSource.Token))
            {
                int percentageComplete =
                    (int)((float)current / iterations * 100);
                string progressMessage = $"Iteration {current} of {iterations}";
                MainProgressBar.Value = percentageComplete;
                OutputTextBox.Text = progressMessage;
            }
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
