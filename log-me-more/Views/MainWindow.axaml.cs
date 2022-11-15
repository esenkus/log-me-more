using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Selection;
using Avalonia.Interactivity;
using log_me_more.Models;
using log_me_more.Services;
using log_me_more.ViewModels;
using ReactiveUI;

namespace log_me_more.Views;

public partial class MainWindow : Window {
    private readonly AdbWrapper adbWrapper = new();
    private readonly List<string> logKeys;
    private readonly SelectionModel<string> logKeySelectionModel;
    private readonly List<string> logLevels;
    private readonly SelectionModel<string> logLevelSelectionModel;
    private List<string> devices;

    public MainWindow() {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        Console.Out.WriteLine("HOWDY");

        logLevelSelectionModel = new SelectionModel<string> { SingleSelect = false };
        logLevels = LogLevels.getAllLevels();

        logKeySelectionModel = new SelectionModel<string> { SingleSelect = false };
        logKeys = FakeDataService.FAKE_LOG_KEYS;

        KeySearchTextBox.ObservableForProperty(textBox => textBox.Text, skipInitial: true)
            .Subscribe(keySearchTextBoxChanged);
        ValueSearchTextBox.ObservableForProperty(textBox => textBox.Text, skipInitial: true)
            .Subscribe(valueSearchTextBoxChanged);

        devices = adbWrapper.getDevices();
        devices.ForEach(Console.Out.WriteLine);

        devices = FakeDataService.FAKE_DEVICES;

        DevicePickerComboBox.Items = devices;

        LogLevelListBox.Items = logLevels;
        LogLevelListBox.Selection = logLevelSelectionModel;
        logLevelSelectionModel.SelectAll();

        LogKeyListBox.Items = logKeys;
        LogKeyListBox.Selection = logKeySelectionModel;
        logKeySelectionModel.SelectAll();

        LogLevelPanel.Background = LogLevelListBox.Background;
        LogKeyPanel.Background = LogKeyListBox.Background;

        ProcessPickerComboBox.Items = new List<string> { "asd", "qwerty" };
        LogTextBox.Text = FakeDataService.FAKE_LOG;
    }

    private void devicePickerSelectionChanged(object? sender, SelectionChangedEventArgs e) {
        var comboBox = (ComboBox)sender!;
        Console.Out.WriteLine("Selection changed to " + comboBox.SelectedItem);
        DevicePickerTextBlock.IsVisible = comboBox.SelectedIndex == -1;
        ProcessPickerComboBox.IsEnabled = comboBox.SelectedIndex != -1;
    }

    private void devicePickerTapped(object? sender, RoutedEventArgs e) {
        var newDevices = FakeDataService.FAKE_DEVICES; // adbWrapper.getDevices();
        if (devices.SequenceEqual(newDevices)) {
            return;
        }
        if (DevicePickerComboBox.SelectedIndex == -1) {
            devices = newDevices;
            DevicePickerComboBox.Items = devices;
            return;
        }
        var currentDevice = devices[DevicePickerComboBox.SelectedIndex];
        devices = newDevices;
        DevicePickerComboBox.Items = devices;
        var currentDeviceIndex = devices.IndexOf(currentDevice);
        if (currentDeviceIndex != -1) {
            DevicePickerComboBox.SelectedIndex = currentDeviceIndex;
        } else {
            DevicePickerComboBox.SelectedIndex = -1;
        }
    }

    private void processPickerTapped(object? sender, RoutedEventArgs e) {
        
    }

    private void processPickerSelectionChanged(object? sender, SelectionChangedEventArgs e) {
        var comboBox = (ComboBox)sender!;
        Console.Out.WriteLine("Selection changed to " + comboBox.SelectedItem);
        ProcessPickerTextBlock.IsVisible = comboBox.SelectedIndex == -1;
    }

    private void keySearchTextBoxChanged(IObservedChange<TextBox, string> newValue) {
        KeySearchTextBlock.IsVisible = newValue.Value.Length == 0;
        Console.Out.WriteLine("Current text: " + newValue.Value);
        Console.Out.WriteLine("prop text: " + getContext().keySearchText);
    }

    private void valueSearchTextBoxChanged(IObservedChange<TextBox, string> newValue) {
        ValueSearchTextBlock.IsVisible = newValue.Value.Length == 0;
        Console.Out.WriteLine("Current text: " + newValue.Value);
        Console.Out.WriteLine("prop text: " + getContext().valueSearchText);
    }

    private MainWindowViewModel getContext() {
        return (MainWindowViewModel)DataContext!;
    }

    private void windowClicked(object? sender, RoutedEventArgs e) {
        var clickedOn = e.Source!.InteractiveParent!.GetType();
        if (clickedOn == typeof(ContentPresenter) || clickedOn == typeof(Button)) {
            return;
        }
        if (clickedOn != typeof(ListBoxItem)) {
            LogLevelPanel.IsVisible = false;
            LogKeyPanel.IsVisible = false;
        }
    }

    private void logLevelButtonClicked(object? sender, RoutedEventArgs e) {
        LogLevelPanel.IsVisible = true;
    }

    private void logLevelSelectionChanged(object? sender, SelectionChangedEventArgs e) {
        Console.Out.WriteLine("Selected items changed, we now have: " +
                              string.Join(", ", logLevelSelectionModel.SelectedItems));
    }

    private void logLevelSelectAllButtonClicked(object? sender, RoutedEventArgs e) {
        logLevelSelectionModel.SelectAll();
    }

    private void logLevelSelectNoneButtonClicked(object? sender, RoutedEventArgs e) {
        logLevelSelectionModel.DeselectRange(0, int.MaxValue);
    }

    private void logKeyButtonClicked(object? sender, RoutedEventArgs e) {
        LogKeyPanel.IsVisible = true;
    }

    private void logKeySelectionChanged(object? sender, SelectionChangedEventArgs e) {
        Console.Out.WriteLine("Selected items changed, we now have: " +
                              string.Join(", ", logKeySelectionModel.SelectedItems));
    }

    private void logKeySelectAllButtonClicked(object? sender, RoutedEventArgs e) {
        logKeySelectionModel.SelectAll();
    }

    private void logKeySelectNoneButtonClicked(object? sender, RoutedEventArgs e) {
        logKeySelectionModel.DeselectRange(0, int.MaxValue);
    }
}