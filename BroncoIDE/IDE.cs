using BroncoLibrary;
using BroncoTextParser;
using FastColoredTextBoxNS;
using System.ComponentModel;

namespace BroncoIDE
{
    public partial class IDE : Form
    {
        private static FontStyle style = FontStyle.Regular;
        private static TextStyle black = new TextStyle(Brushes.Black, null, style);
        private static TextStyle blue = new TextStyle(Brushes.LightBlue, null, style);
        private static TextStyle red = new TextStyle(Brushes.Red, null, style);
        private static TextStyle green = new TextStyle(Brushes.Green, null, style);
        private static TextStyle grey = new TextStyle(Brushes.Gray, null, style);
        private static Dictionary<string, SymbolVariable>  EmptyDictionary = new();

        private string _currentFile = "";

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

            referencesPane.Text = "";
            foreach (var reference in parseResult.Item2)
                referencesPane.AppendText($"{reference.Key} =     {reference.Value.ToString()}{Environment.NewLine}");

            generateButton.Enabled = true;
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            BackgroundParser.RunWorkerAsync(inputPane.Text);
            generateButton.Enabled = false;
        }
        
        private void inputPane_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(blue, red, green, grey, black);
            e.ChangedRange.SetStyle(blue, "#[A-Za-z][A-Za-z0-9_]*");
            e.ChangedRange.SetStyle(blue, "#[A-Za-z][A-Za-z0-9_]*:[0-9.]*");
            e.ChangedRange.SetStyle(black, "`.*?`");
            e.ChangedRange.SetStyle(red, "<.*?>");
            e.ChangedRange.SetStyle(red, "\\[.*?\\]");
            e.ChangedRange.SetStyle(blue, "%[0-9.]*");
            e.ChangedRange.SetStyle(green, "@[A-Za-z][A-Za-z0-9_]*");
            e.ChangedRange.SetStyle(grey, "/\\*.*?\\*/");
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if(_currentFile.Length == 0)
            {
                saveAsButton_Click(sender, e);
                return;
            }

            SaveFile(File.OpenWrite(_currentFile));
        }

        private void saveAsButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream file;
                if ((file = saveFileDialog.OpenFile()) != null)
                {
                    SaveFile(file);
                    SetOpenFile(saveFileDialog.FileName);
                }
            }
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream file;
                if ((file = openFileDialog.OpenFile()) != null)
                {
                    StreamReader reader = new StreamReader(file);
                    string text = reader.ReadToEnd();
                    reader.Close();
                    file.Close();

                    inputPane.Text = text;
                    SetOpenFile(openFileDialog.FileName);
                }
            }
        }

        private void SaveFile(Stream file)
        {
            StreamWriter writer = new StreamWriter(file);
            writer.Write(inputPane.Text);
            writer.Close();
            file.Close();
        }

        private void SetOpenFile(string file)
        {
            _currentFile = file;
            Text = $"Bronco Editor - {file}";
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentFile = "";
            Text = $"Bronco Editor";
            inputPane.Text = "";
        }
    }
}