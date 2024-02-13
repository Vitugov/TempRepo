using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task01.Model.Accsess;
using Task01.Model.Data;

namespace Task01.Model.Data
{
    public class Edit
    {
        public DateTime DateTime { get; set; }
        public string ChangedData { get; set; }
        public string TypeOfChanges { get; set; }
        public string Author { get; set; }

        public Edit(IStoredData obj, List<string> changedData, string author, string typeOfChanges)
        {
            DateTime = DateTime.Now;
            ChangedData = string.Join(", ", changedData);
            Author = author;
            TypeOfChanges = typeOfChanges;
            Repository.CurrentRepository.AddEdit(obj, this);
        }

        public Edit(IStoredData obj, string author) : this(obj, [], author, "Объект создан") { }

        public Edit() { }
    }
}
