using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel; // ObservableCollection
using System.ComponentModel; // INotifyPropertyChanged

namespace Lab3
{
    // https://habr.com/ru/post/338518/
    public class MainWindowVM
    {
        readonly private Model model = new Model();

        private event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<PersonData> PersonCollection { get { return model.PersonCollection; } }
        public ObservableCollection<PersonData> PersonSelection { get { return model.PersonSelection; } }

        public object SelectedItem { get; set; }

        public void OnClosing(object sender, EventArgs e)
        {
            model.SaveXml();
        }

        public void OnRemoveItem(object sender, EventArgs e)
        {
            model.RemoveItem(SelectedItem as PersonData);
        }

        public void MergeCollectionWithSelection()
        {
            model.MergeCollectionWithSelection();
        }

        public void AddPersonToSelection(PersonData person)
        {
            model.PersonSelection.Add(person);
        }

        public void ClearSelection()
        {
            model.PersonSelection.Clear();
        }
    }
}
