using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PanPakapon
{
    /// <summary>
    /// Interaction logic for ConvertWindow.xaml
    /// </summary>
    public partial class ConvertWindow : Window
    {
        public ConvertWindow()
        {
            InitializeComponent();
            Owner = App.Current.MainWindow;
            var logger = new TextBoxLogger(LogBox);
            logger.Clear();
            logger.LogMessage("message");
            logger.LogMessage("message");
            logger.LogError("error");
            logger.LogMessage("message");
            logger.LogWarning("warning");
            logger.LogMessage("message");
            logger.LogError("error");
            logger.LogWarning("warning");
            logger.LogMessage("message");
            logger.LogMessage("message");
            logger.LogMessage("message");
            logger.LogWarning("warning");
            logger.LogMessage("message");
            logger.LogError("error");
            logger.LogMessage("message");
            logger.LogMessage("message");
            logger.LogError("error");
            logger.LogMessage("message");
            logger.LogWarning("warning");
            logger.LogMessage("message");
            logger.LogError("error");
            logger.LogWarning("warning");
            logger.LogMessage("message");
            logger.LogMessage("message");
            logger.LogMessage("message");
            logger.LogWarning("warning");
            logger.LogMessage("message");
            logger.LogError("error");

        }
    }
}
