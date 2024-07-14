using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WPF_DataGrid
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            items.Add(new TabItem() { ID = 1, Video = "John Doe", Protocol = ProtoEnum.RTSP, State = StateEnum.Init });
            items.Add(new TabItem() { ID = 2, Video = "Gaofei244617", Protocol = ProtoEnum.RTSP, State = StateEnum.Init });
            items.Add(new TabItem() { ID = 3, Video = "AAA", Protocol = ProtoEnum.RTSP, State = StateEnum.Init });

            this.MyTable.ItemsSource = items;
        }

        private void PushStream_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                // 获取当前行数据
                if (button.DataContext is TabItem item)
                {
                    StateEnum state = item.State;
                    if (state == StateEnum.Init)
                    {
                        item.State = StateEnum.Running;
                        button.Content = "Stop";
                    }
                    else if (state == StateEnum.Running)
                    {
                        item.State = StateEnum.Stop;
                        button.Content = "推流";
                    }
                    else if (state == StateEnum.Stop)
                    {
                        item.State = StateEnum.Running;
                        button.Content = "Stop";
                    }
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                // 获取当前行数据
                if (button.DataContext is TabItem item)
                {
                    items.Remove(item);
                }
            }
        }

        // Protocol ComboBox
        private void Protocol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                ProtoEnum proto = (ProtoEnum)comboBox.SelectedItem;
                if (comboBox.DataContext is TabItem item)
                {
                    item.Protocol = proto;
                }
            }
        }

        // DataGrid Binding data
        private ObservableCollection<TabItem> items = new ObservableCollection<TabItem>();
    }

    public enum ProtoEnum
    {
        RTSP,
        RTMP
    }

    public enum StateEnum
    {
        Init,
        Running,
        Stop
    }

    public class TabItem : INotifyPropertyChanged
    {
        private int _id;

        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
            }
        }

        private string? _video;

        public string? Video
        {
            get => _video;
            set
            {
                _video = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Video)));
            }
        }

        private ProtoEnum _protocol;

        public ProtoEnum Protocol
        {
            get => _protocol;
            set
            {
                _protocol = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Protocol)));
            }
        }

        private StateEnum _state;

        public StateEnum State
        {
            get => _state;
            set
            {
                _state = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(State)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}