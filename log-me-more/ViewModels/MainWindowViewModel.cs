using ReactiveUI;

namespace log_me_more.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    private string _keySearchText = "";
    private string _valueFilterText = "";

    public string keySearchText {
        get => _keySearchText;
        set => this.RaiseAndSetIfChanged(ref _keySearchText, value);
    }

    public string valueFilterText {
        get => _valueFilterText;
        set => this.RaiseAndSetIfChanged(ref _valueFilterText, value);
    }
}