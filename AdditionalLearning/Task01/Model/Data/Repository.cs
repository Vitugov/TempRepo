using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Task01.Model.Accsess;

namespace Task01.Model.Data
{
    public class Repository
    {
        public static Repository CurrentRepository { get; set; }
        public Dictionary<Type, Dictionary<IStoredData, List<Edit>>> Edits { get; set; }
        static Repository()
        {
            CurrentRepository = new Repository();
        }

        public Repository()
        {
            Edits = [];
        }

        public void Add(IStoredData obj)
        {
            Type objectType = obj.GetType();
            if (!Edits.ContainsKey(objectType))
                Edits[objectType] = [];
            Edits[objectType].Add(obj, []);
        }

        public void Delete(IStoredData obj)
        {
            Type objectType = obj.GetType();
            Edits[objectType].Remove(obj);
        }

        public HashSet<IStoredData> GetDataOfType(Type type)
        {
            return new HashSet<IStoredData>(Edits[type].Keys);
        }

        public List<Edit> GetObjectEdits(IStoredData obj)
        {
            var type = obj.GetType();
            return Edits[type][obj];
        }

        public void AddEdit(IStoredData obj, Edit edit)
        {
            Type objectType = obj.GetType();
            Edits[objectType][obj].Add(edit);
        }
    }

}
