using FujinoNs.ServerStatus;
using System.Windows;

namespace Fujino.FiveM.ServerStatus_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ServerStatus server = new ServerStatus("127.0.0.1:30120");
            if (server.Status() == true)
            {
                this.lbl_server.Content = "Server Online";
                this.lbl_players.Content = "Player Online: " + server.PlayerCount().ToString();
            }
            else
            {
                this.lbl_server.Content = "Server Offline";
                this.lbl_players.Content = "Server Offline";
            }
        }
    }
}
