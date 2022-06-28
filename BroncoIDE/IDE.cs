using BroncoLibrary;
using BroncoTextParser;
using FastColoredTextBoxNS;
using System.ComponentModel;

namespace BroncoIDE
{
    public partial class IDE : Form
    {
        private static FontStyle style = FontStyle.Regular;
        private static TextStyle blue = new TextStyle(Brushes.LightBlue, null, style);
        private static TextStyle red = new TextStyle(Brushes.Red, null, style);
        private static TextStyle green = new TextStyle(Brushes.Green, null, style);
        private static TextStyle grey = new TextStyle(Brushes.Gray, null, style);

        public IDE()
        {
            InitializeComponent();
        }

        private void BackgroundParser_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string outputText = "Something went wrong...";

            try
            {
                ISymbol symbol = TextParser.Parse((string)e.Argument);
                outputText = symbol.Flatten().Value;
            } catch (Exception ex)
            {
                outputText = ex.Message;
            }

            e.Result = outputText;
        }

        private void BackgroundParser_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            outputPane.Text = (string) e.Result;
            generateButton.Enabled = true;
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            BackgroundParser.RunWorkerAsync(inputPane.Text);
            generateButton.Enabled = false;
        }
        
        private void inputPane_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(blue, red, green);
            e.ChangedRange.SetStyle(blue, "#[A-Za-z][A-Za-z0-9]*");
            e.ChangedRange.SetStyle(blue, "#[A-Za-z][A-Za-z0-9]*:[0-9.]*");
            e.ChangedRange.SetStyle(blue, "%[0-9.]*");
            e.ChangedRange.SetStyle(red, "['<].*?['>]");
            e.ChangedRange.SetStyle(green, "@[A-Za-z][A-Za-z0-9]*");
            e.ChangedRange.SetStyle(grey, "/\\*.*?\\*/");
        }
    }
}