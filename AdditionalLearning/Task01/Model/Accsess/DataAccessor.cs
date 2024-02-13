using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Task01.Model.Data;
using System.Collections.ObjectModel;

namespace Task01.Model.Accsess
{
    public class DataAccessor
    {
        public Session Session { get; }
        public ExpandoObject DynamicObject { get; }
        private IStoredData AssociatedObject { get; }
        

        public DataAccessor(Session session, IStoredData associatedObj)
        {
            AssociatedObject = associatedObj;
            Session = session;
            DynamicObject = CreateExpandoObject(associatedObj);
        }

        public DataAccessor(Session session, Type type) : this(session, Activator.CreateInstance(type) as IStoredData)
        {
            _ = new Edit(AssociatedObject, [], Session.User.ToString(), "Объект создан");
        }

        private dynamic CreateExpandoObject(IStoredData associatedObj)
        {
            dynamic dynamicObject = new ExpandoObject();
            var dictionary = dynamicObject as IDictionary<string, object>;
            var type = associatedObj.GetType();
            var rules = Session.User.Role[type];
            var propertyList = type.GetProperties()
                .Select(property => property.Name)
                .Where(propertyName =>rules.ContainsKey(propertyName) && rules[propertyName].Read)
                .ToList();
            propertyList
                .ForEach(propertyName => { dictionary[propertyName] = type.GetProperty(propertyName).GetValue(associatedObj); });
            return dynamicObject;
        }

        public void UpdateAssociatedObject()
        {
            var editedProperies = UpdateProperties();
            _ = new Edit(AssociatedObject, editedProperies, Session.User.ToString(), "Объект изменен");
        }

        public static List<DataAccessor> GetListOfTypeForUser(Session session, Type type)
        {
            var dynamicList = new List<DataAccessor>();
            var list = session.Repository.GetDataOfType(type);
            foreach (var item in list)
            {
                dynamicList.Add(new DataAccessor(session, item));
            }
            
            return dynamicList;
        }

        public void DeleteAssociatedObject()
        {
            Session.Repository.Delete(AssociatedObject);
        }

        public List<Edit> GetObjectEdits()
        {
            return Session.Repository.GetObjectEdits(AssociatedObject);
        }

        private List<string> UpdateProperties()
        {
            var dictionary = DynamicObject as IDictionary<string, object>;
            var objType = AssociatedObject.GetType();
            var rules = Session.User.Role[objType];
            var result = dictionary.Keys
                .Where((property) => Session.User.Role.IsChangeable(objType, property))
                .Where((property) => objType.GetProperty(property) != null)
                .Where((property) => objType.GetProperty(property).GetValue(AssociatedObject) != dictionary[property])
                .ToList();
            result.ForEach((prop) => objType.GetProperty(prop).SetValue(AssociatedObject, dictionary[prop]));
            return result;
        }
    }
}
