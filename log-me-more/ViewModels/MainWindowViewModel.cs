using ReactiveUI;

namespace log_me_more.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    private string _keyFilterText = "";
    private string _valueFilterText = "";

    public string keyFilterText {
        get => _keyFilterText;
        set => this.RaiseAndSetIfChanged(ref _keyFilterText, value);
    }

    public string valueFilterText {
        get => _valueFilterText;
        set => this.RaiseAndSetIfChanged(ref _valueFilterText, value);
    }
}