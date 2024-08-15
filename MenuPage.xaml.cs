using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using FSSY_v3.classes;

namespace FSSY_v3;

public partial class MenuPage : Page
{
    private readonly SavegameStates _savegameStates;
    private readonly PathOverlay _pathOverlay = new PathOverlay();
        public MenuPage() {
            InitializeComponent();
            _savegameStates = new SavegameStates();
            LoadCheckBoxStates();
        }

        private void PlayButton(object sender, RoutedEventArgs e) {
            Process.Start(@"C:\Program Files (x86)\Farming Simulator 2022\x64\FarmingSimulator2022Game.exe"); // Spiel starten
        }
        private void SynchronizeButton(object sender, RoutedEventArgs e) {
            RunTickedPaths(sender, e);
        }
        private void SynchronizeAndPlayButton(object sender, RoutedEventArgs e) {
            SynchronizeButton(sender, e);
            PlayButton(sender, e);
        }
        private void PathsButton(object sender, RoutedEventArgs e) {
            var mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.NavigateToPathPage();
        }

        private void ExitButton(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Window.GetWindow(this);
        mainWindow.ExitButton(sender,e);
        }

        public void SaveOnClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveCheckBoxStates();
        }
        private void SaveCheckBoxStates()
        {
            var state = new CheckBoxState
            {
                CheckBox1 = CheckBox1.IsChecked ?? false,
                CheckBox2 = CheckBox2.IsChecked ?? false,
                CheckBox3 = CheckBox3.IsChecked ?? false,
                CheckBox4 = CheckBox4.IsChecked ?? false,
                CheckBox5 = CheckBox5.IsChecked ?? false,
                CheckBox6 = CheckBox6.IsChecked ?? false,
                CheckBox7 = CheckBox7.IsChecked ?? false,
                CheckBox8 = CheckBox8.IsChecked ?? false,
                CheckBox9 = CheckBox9.IsChecked ?? false,
                CheckBox10 = CheckBox10.IsChecked ?? false,
                CheckBox11 = CheckBox11.IsChecked ?? false,
                CheckBox12 = CheckBox12.IsChecked ?? false,
                CheckBox13 = CheckBox13.IsChecked ?? false,
                CheckBox14 = CheckBox14.IsChecked ?? false,
                CheckBox15 = CheckBox15.IsChecked ?? false,
                CheckBox16 = CheckBox16.IsChecked ?? false,
                CheckBox17 = CheckBox17.IsChecked ?? false,
                CheckBox18 = CheckBox18.IsChecked ?? false,
                CheckBox19 = CheckBox19.IsChecked ?? false,
                CheckBox20 = CheckBox20.IsChecked ?? false,
            };

            _savegameStates.Save(state);
        }
        private void LoadCheckBoxStates()
        {
            var state = _savegameStates.Load();

            CheckBox1.IsChecked = state.CheckBox1;
            CheckBox2.IsChecked = state.CheckBox2;
            CheckBox3.IsChecked = state.CheckBox3;
            CheckBox4.IsChecked = state.CheckBox4;
            CheckBox5.IsChecked = state.CheckBox5;
            CheckBox6.IsChecked = state.CheckBox6;
            CheckBox7.IsChecked = state.CheckBox7;
            CheckBox8.IsChecked = state.CheckBox8;
            CheckBox9.IsChecked = state.CheckBox9;
            CheckBox10.IsChecked = state.CheckBox10;
            CheckBox11.IsChecked = state.CheckBox11;
            CheckBox12.IsChecked = state.CheckBox12;
            CheckBox13.IsChecked = state.CheckBox13;
            CheckBox14.IsChecked = state.CheckBox14;
            CheckBox15.IsChecked = state.CheckBox15;
            CheckBox16.IsChecked = state.CheckBox16;
            CheckBox17.IsChecked = state.CheckBox17;
            CheckBox18.IsChecked = state.CheckBox18;
            CheckBox19.IsChecked = state.CheckBox19;
            CheckBox20.IsChecked = state.CheckBox20;
        }

        private void RunTickedPaths(object sender, RoutedEventArgs e)
        {
            const string exePath = @"C:\Program Files\FreeFileSync\FreeFileSync.exe";

            var paths = PathsManager.BatchPaths;
            var checkBoxes = CheckBoxContainer.Children;

            for (var index = 0; index < checkBoxes.Count; index++)
            {
                var checkBox = (CheckBox)checkBoxes[index];
                if (checkBox.IsChecked == true)
                {
                    var arguments = $"\"{paths[index]}\"";
                    Process.Start(exePath, arguments);
                }
            }
    }

        private void HandleOpenOverlay(object sender, RoutedEventArgs e)
        {
            ModalFrame.Navigate(_pathOverlay);
        }

        public Frame GetModalFrame()
        {
            return ModalFrame;
        }
}