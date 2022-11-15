using System.Collections.Generic;
using ReactiveUI;

namespace log_me_more.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    private string _keySearchText = "";
    private string _valueSearchText = "";

    public string keySearchText {
        get => _keySearchText;
        set => this.RaiseAndSetIfChanged(ref _keySearchText, value);
    }

    public string valueSearchText {
        get => _valueSearchText;
        set => this.RaiseAndSetIfChanged(ref _valueSearchText, value);
    }

    public List<string> items => new() { "asd", "zxc" };
}