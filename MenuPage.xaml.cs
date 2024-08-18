using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using FSSY_v3.classes;

namespace FSSY_v3;

public partial class MenuPage : Page
{
    private readonly MainWindow _mainWindow;
    private readonly SavegameStates _savegameStates;
    private readonly PathOverlay _pathOverlay;

    public MenuPage(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        InitializeComponent();
        _savegameStates = new SavegameStates();
        _pathOverlay = new PathOverlay(this);
        LoadCheckBoxStates();
    }

    public void SaveStates()
    {
        SaveCheckBoxStates();
        _pathOverlay.SavePaths();
    }

    public void OpenModal()
    {
        ToggleBlur(true);
        ModalFrame.Navigate(_pathOverlay);
    }

    public void CloseModal()
    {
        ToggleBlur(false);
        ModalFrame.Navigate(null);
    }

    private void ToggleBlur(bool enable)
    {
        MenuGrid.Effect = enable ? new BlurEffect { Radius = 20 } : null;
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

    private void SynchronizeSelectedPaths()
    {
        var exePath = PathsManager.FfsExePath;

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

    private void HandlePlay(object sender, RoutedEventArgs e)
    {
        Process.Start(PathsManager.GameExePath);
    }

    private void HandleSynchronize(object sender, RoutedEventArgs e)
    {
        SynchronizeSelectedPaths();
    }

    private void HandleSynchronizeAndPlay(object sender, RoutedEventArgs e)
    {
        HandleSynchronize(sender, e);
        HandlePlay(sender, e);
    }

    private void HandlePaths(object sender, RoutedEventArgs e)
    {
        _mainWindow.NavigateToPage(_mainWindow._pathsPage);
    }

    private void HandleExit(object sender, RoutedEventArgs e)
    {
        _mainWindow.ExitButton(sender, e);
    }

    private void HandleOpenModal(object sender, RoutedEventArgs e)
    {
        OpenModal();
    }
}