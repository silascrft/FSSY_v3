using System.Windows;
using System.Windows.Controls;
using FSSY_v3.classes;
using Microsoft.Win32;

namespace FSSY_v3
{
    /// <summary>
    /// Interaction logic for FirstRunOverlay.xaml
    /// </summary>
    public partial class FirstRunOverlay : Page
    {
        private readonly MenuPage _menuPage;

        private readonly List<string> _textBlockHeaders =
        [
            "Game.exe Path",
            "FSS.exe Path",
            "Savegame Folder Path",
            "Cloud Drive Path"
        ];

        private readonly List<string> _defaultPathValues =
        [
            "C:/Game/Path",
            "C:/FSS/Path",
            "C:/Savegame/Folder/Path",
            "C:/Cloud/Drive/Path"
        ];

        private int _currentStep = 0;
        private bool _pathEntered = false;
        private List<string> _enteredPaths = [];

        public FirstRunOverlay(MenuPage menuPage)
        {
            _menuPage = menuPage;
            InitializeComponent();
        }

        private void setTextBlock()
        {
            TextBlock.Text = _textBlockHeaders[_currentStep];
        }

        private void setTextBox()
        {
            TextBox.Text = _defaultPathValues[_currentStep];
        }

        private void nextStep()
        {
            _currentStep++;
            _pathEntered = false;
        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            _menuPage.CloseModal();
        }

        private void HandleModalButtonClicked(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button) return;
            var parameter = button.Tag.ToString();
            if (parameter is "OK")
            {
                if (_pathEntered || TextBox.Text.Contains(".exe"))
                {
                    _enteredPaths.Add(TextBox.Text);
                    nextStep();
                    if (_currentStep == 4)
                    {
                        CloseModal();
                        return;
                    }

                    setTextBlock();
                    setTextBox();
                }
                else
                {
                    MessageBox.Show("No valid file selected.", "Path selection", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            else if (parameter is "SKIP")
            {
                _enteredPaths.Add("");
                nextStep();
                if (_currentStep == 4)
                {
                    CloseModal();
                    return;
                }

                setTextBlock();
                setTextBox();
            }
        }

        private void CloseModal()
        {
            PathsManager.setAllPaths(_enteredPaths[0], _enteredPaths[1], _enteredPaths[2], _enteredPaths[3]);
            _menuPage._pathOverlay.FillPathElements();
            _menuPage.CloseModal();
        }

        private void HandleFileSelection(object sender, RoutedEventArgs e)
        {
            if (_currentStep < 2)
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Executables|*.exe"
                };
                var valid = openFileDialog.ShowDialog();
                if (valid == false)
                {
                    MessageBox.Show("No valid file selected.", "Path selection", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                TextBox.Text = openFileDialog.FileName;
            }
            else
            {
                var openFileDialog = new OpenFolderDialog();
                var valid = openFileDialog.ShowDialog();
                if (valid == false)
                {
                    MessageBox.Show("No valid folder selected.", "Path selection", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                TextBox.Text = openFileDialog.FolderName;
            }

            _pathEntered = true;
        }
    }
}