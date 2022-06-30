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
        private static Dictionary<string, SymbolVariable>  EmptyDictionary = new();

        public IDE()
        {
            InitializeComponent();
        }

        private void BackgroundParser_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string outputText = "Something went wrong...";
            IEnumerable<KeyValuePair<string, SymbolVariable>> references = EmptyDictionary;
            try
            {
                ISymbol symbol = TextParser.Parse((string)e.Argument);
                references = TextParser.GetReferences();
                outputText = symbol.Flatten().Value;
            } catch (Exception ex)
            {
                outputText = ex.Message;
            }

            e.Result = (outputText, references);
        }

        private void BackgroundParser_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            (string, IEnumerable<KeyValuePair<string, SymbolVariable>>) parseResult =
                ((string, IEnumerable<KeyValuePair<string, SymbolVariable>>)) e.Result;

            outputPane.Text = parseResult.Item1.Replace("\\n", Environment.NewLine);

            ReferencesPane.Text = "";
            foreach (var reference in parseResult.Item2)
                ReferencesPane.AppendText($"{reference.Key} =     {reference.Value}{Environment.NewLine}");

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
            e.ChangedRange.SetStyle(blue, "#[A-Za-z][A-Za-z0-9_]*");
            e.ChangedRange.SetStyle(blue, "#[A-Za-z][A-Za-z0-9_]*:[0-9.]*");
            e.ChangedRange.SetStyle(blue, "%[0-9.]*");
            e.ChangedRange.SetStyle(red, "[`<].*?[`>]");
            e.ChangedRange.SetStyle(red, "[`|].*?[`|]");
            e.ChangedRange.SetStyle(green, "@[A-Za-z][A-Za-z0-9_]*");
            e.ChangedRange.SetStyle(grey, "/\\*.*?\\*/");
        }
    }
}