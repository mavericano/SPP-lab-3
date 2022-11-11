using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AssemblyBrowser;
using Microsoft.Win32;

namespace WpfApplication1.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        // [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand ButtonClickCommand => new OnClickCommand(OpenFile);

        private List<AssemblyData> _list = new List<AssemblyData>();
        public List<AssemblyData> List
        {
            get
            {
                return _list;
            }
            set
            {
                _list = value;
                OnPropertyChanged(nameof(List));
            }
        }

        private void OpenFile()
        {
            ReaderImpl reader = new ReaderImpl();
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Assemblies|*.dll;*.exe",
                Title = "Select assembly",
                Multiselect = false
            };
            var isOpen = openFileDialog.ShowDialog();
            if (isOpen != null && isOpen.Value)
            {
                List = reader.GetAssemblyData(openFileDialog.FileName);               
            }
            else
            {
                System.Windows.MessageBox.Show("Error while processing selected assembly");
            }
        }
    }
}
