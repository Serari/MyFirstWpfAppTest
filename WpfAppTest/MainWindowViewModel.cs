using System.ComponentModel;
using System.Windows.Input;

namespace WpfAppTest
{
    // ViewModel：画面のロジックを担当するクラス。MVVMパターンではViewから切り離してここにロジックを書く
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // ランダムに表示する文字列の候補リスト
        private static readonly string[] Items = ["ライチ☆光クラブ", "ヤマノススメ", "放課後さいころ倶楽部"];
        private static readonly Random Rng = new();

        // 画面に表示する文字列。セットするとOnPropertyChangedが呼ばれ、Viewに変更が通知される
        private string _resultText = string.Empty;
        public string ResultText
        {
            get => _resultText;
            set { _resultText = value; OnPropertyChanged(nameof(ResultText)); }
        }

        // ボタンにバインドするコマンド。ボタンが押されるとExecuteが呼ばれる
        public ICommand PickCommand { get; }

        public MainWindowViewModel()
        {
            // ボタンが押されたらItemsからランダムに1つ選んでResultTextにセット
            PickCommand = new RelayCommand(() => ResultText = Items[Rng.Next(Items.Length)]);
        }

        // プロパティ変更をViewに通知するためのイベントとヘルパーメソッド
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    // ICommandの汎用実装。Actionを渡すだけでコマンドとして使えるようにしたヘルパークラス
    public class RelayCommand(Action execute) : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public bool CanExecute(object? parameter) => true; // 常に実行可能
        public void Execute(object? parameter) => execute();
    }
}
