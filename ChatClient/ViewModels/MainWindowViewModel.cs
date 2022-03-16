using ChatClient.Models;
using ChatClient.ServiceChat;
using DSF_GEOS_Visualisation.Infrastructure.Commadns;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ChatClient.ViewModels
{
    public class MainWindowViewModel : Base.ViewModel, IServiceChatCallback
    {
        public ObservableCollection<MessageModel> Messages { get; set; }

        private MainWindowFields MainViewModelFields = new MainWindowFields();
        private string theme = "Light";
        private ServiceChatClient _client;
        private int ID;


        #region Поля

        /// <summary>Заголовок окна</summary>
        private string _title = "ChatClient";

        public string title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        /// <summary>Icon theme</summary>
        private string _iconTheme = MainWindowFields.LightTheme.iconTheme;
        public string iconTheme
        {
            get => _iconTheme;
            set => Set(ref _iconTheme, value);
        }

        /// <summary> Цвет фона </summary>
        private SolidColorBrush _backgroundColor = MainWindowFields.LightTheme.background;
        public SolidColorBrush backgroundColor
        {
            get => _backgroundColor;
            set => Set(ref _backgroundColor, value);
        }

        /// <summary> Цвет фона заголовка </summary>
        private SolidColorBrush _titleColor = MainWindowFields.LightTheme.title;
        public SolidColorBrush titleColor
        {
            get => _titleColor;
            set => Set(ref _titleColor, value);
        }

        /// <summary> Цвет текста </summary>
        private SolidColorBrush _foreground = MainWindowFields.LightTheme.foreground;
        public SolidColorBrush foreground
        {
            get => _foreground;
            set => Set(ref _foreground, value);
        }

        
        private int _selectedTabIndex = 0;
        public int selectedTabIndex
        {
            get => _selectedTabIndex;
            set => Set(ref _selectedTabIndex, value);
        }

        /// <summary>Сообщение</summary>
        private string _message;
        public string message
        {
            get => _message;
            set => Set(ref _message, value);
        }


        private string _name;
        public string name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        #endregion

        #region Комманды

        /// <summary>
        ///// Change theme command
        /// </summary>
        public ICommand ChangeThemeCommand { get; }
        private bool CanChangeThemeCommandExecute(object p) => true;
        private void OnChangeThemeCommandExecuted(object p)
        {
            if (theme == "Dark")
            {
                LightTheme();
            }
            else
            {
                DarkTheme();
            }
        }

        /// <summary>
        /// Подключение к чату
        /// </summary>
        public ICommand ConnectCommand { get; }
        private bool CanConnectCommandExecute(object p) => true;
        private void OnConnectCommandExecuted(object p)
        {
            if (name != null && name != string.Empty)
            {
                // Connect user to chat here
                ConnectUser();

                selectedTabIndex = 1;
            }
        }

        /// <summary>
        /// Отправка сообщений
        /// </summary>
        public ICommand SendMessageCommand { get; }
        private bool CanSendMessageCommandExecute(object p) => true;
        private void OnSendMessageCommandExecuted(object p)
        {
            if (message != null && message != string.Empty)
            {
                // Send Message here
                if (message != String.Empty && message != String.Empty && _client != null)
                {
                    _client.SendMessage(message, ID);
                    message = String.Empty;
                }
            }
        }

        /// <summary>
        /// Выход из чата
        /// </summary>
        public ICommand ExitChatCommand { get; }
        private bool CanExitChatCommandExecute(object p) => true;
        private void OnExitChatCommandExecuted(object p)
        {
            DisonnectUser();
        }

        #endregion

        #region Методы

        private void LightTheme()
        {
            backgroundColor = MainWindowFields.LightTheme.background;
            foreground = MainWindowFields.LightTheme.foreground;
            titleColor = MainWindowFields.LightTheme.title;
            theme = "Light";
            iconTheme = MainWindowFields.LightTheme.iconTheme;
        }

        private void DarkTheme()
        {
            backgroundColor = MainWindowFields.DarkTheme.background;
            foreground = MainWindowFields.DarkTheme.foreground;
            titleColor = MainWindowFields.DarkTheme.title;
            theme = "Dark";
            iconTheme = MainWindowFields.DarkTheme.iconTheme;
        }

        public void MessageCallback(string name, string time, string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add(new MessageModel()
                {
                    Name = name,
                    Time = time,
                    Content = message
                });
            });
        }


        private async void ConnectUser()
        {
            await Task.Run(() =>
            {
                _client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                ID = _client.Connect(name);
            });
        }

        private async void DisonnectUser()
        {
            await Task.Run(() =>
            {
                _client.Disconnect(ID);
                _client = null;

                Application.Current.Shutdown();
            });
        }

        #endregion
        public MainWindowViewModel()
        {
            ChangeThemeCommand = new LambdaCommand(OnChangeThemeCommandExecuted, CanChangeThemeCommandExecute);
            ConnectCommand = new LambdaCommand(OnConnectCommandExecuted, CanConnectCommandExecute);
            SendMessageCommand = new LambdaCommand(OnSendMessageCommandExecuted, CanSendMessageCommandExecute);
            ExitChatCommand = new LambdaCommand(OnExitChatCommandExecuted, CanExitChatCommandExecute);

            Messages = new ObservableCollection<MessageModel>();
        }
    }
}