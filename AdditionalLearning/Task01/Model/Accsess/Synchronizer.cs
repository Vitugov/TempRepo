using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task01.Model.Data;

namespace Task01.Model.Accsess
{
    public class Synchronizer
    {
        public Type Type { get; }
        public Session Session { get; }
        private Dictionary<ExpandoObject, DataAccessor> CollectionToDataAccessorDic { get; set; }
        public DynamicItemCollection Collection {  get; set; }

        public Synchronizer(Session session, Type type)
        {
            Session = session;
            Type = type;
            CollectionToDataAccessorDic = [];
            Collection = [];
            UpdateDataFromDataAccessor();
        }
        public void UpdateDataFromDataAccessor()
        {
            var list = DataAccessor.GetListOfTypeForUser(Session, Type);
            CollectionToDataAccessorDic = [];
            foreach (var item in list)
            {
                CollectionToDataAccessorDic[item.DynamicObject] = item;
            }
            Collection = new DynamicItemCollection([.. CollectionToDataAccessorDic.Keys]);
        }

        public void Update(ExpandoObject obj)
        {
            if (!CollectionToDataAccessorDic.ContainsKey(obj))
                throw new NullReferenceException("Object to update havn't finded");
            CollectionToDataAccessorDic[obj].UpdateAssociatedObject();
        }

        public void Delete(ExpandoObject obj)
        {
            Serialization.Serialize();
            if (!CollectionToDataAccessorDic.ContainsKey(obj))
                throw new NullReferenceException("Object to update havn't finded");
            Collection.Remove(obj);
            CollectionToDataAccessorDic[obj].DeleteAssociatedObject();
            CollectionToDataAccessorDic.Remove(obj);
        }

        public ExpandoObject CreateNew()
        {
            var newDAObj = new DataAccessor(Session, Type);
            CollectionToDataAccessorDic[newDAObj.DynamicObject] = newDAObj;
            Collection.Add(newDAObj.DynamicObject);
            return newDAObj.DynamicObject;
        }

        public HashSet<string> GetDynamicObjectAllProperties(ExpandoObject obj)
        {
            var dictionary = obj as IDictionary<string, object>;
            return [.. dictionary.Keys];
        }

        public bool Validate(ExpandoObject obj, string property, out List<string> validationErrors)
        {
            return Validation.Validate(obj, Type, property, Session.User.Role, out validationErrors);
        }

        public ObservableCollection<Edit> GetEdits(ExpandoObject obj)
        {
            return new ObservableCollection<Edit>(CollectionToDataAccessorDic[obj].GetObjectEdits());
        }
    }
}
