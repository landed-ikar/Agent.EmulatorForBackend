using Agent.EmulatorForBackend.ViewModels;
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

namespace Agent.EmulatorForBackend
{
    /// <summary>
    /// Interaction logic for EditAgentDialog.xaml
    /// </summary>
    public partial class EditAgentDialog : Window
    {
        public EditAgentDialog()
        {
            InitializeComponent();
        }

        public EditAgentDialog(IAgentViewModel agentViewModel)
        {
            InitializeComponent();
            this.DataContext = agentViewModel;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
