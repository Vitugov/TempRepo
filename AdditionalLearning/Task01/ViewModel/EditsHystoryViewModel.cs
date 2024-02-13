using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task01.Model.Accsess;
using Task01.Model.Data;

namespace Task01.ViewModel
{
    internal class EditsHystoryViewModel : BaseViewModel
    {
        public Synchronizer Synchronizer { get; }
        public ExpandoObject Object { get; }

        private ObservableCollection<Edit> _EditsList;

        public ObservableCollection<Edit> EditsList
        {
            get => _EditsList;
            set => Set(ref _EditsList, value);
        }

        public EditsHystoryViewModel(Synchronizer synchronizer, ExpandoObject obj)
        {
            Synchronizer = synchronizer;
            Object = obj;
            EditsList = Synchronizer.GetEdits(obj);
        }
    }
}
