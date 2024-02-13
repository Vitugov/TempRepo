using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Model.Accsess
{
    public class DynamicItemCollection : ObservableCollection<ExpandoObject>, IList, ITypedList
    {
        public DynamicItemCollection() : base() { }

        public DynamicItemCollection(List<ExpandoObject> list) : base(list) { }

        public DynamicItemCollection(IEnumerable<ExpandoObject> collection) : base(collection) { }

        public string GetListName(PropertyDescriptor[] listAccessors)
        {
            return null;
        }

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            var dynamicDescriptors = new PropertyDescriptor[0];
            if (this.Any())
            {
                var firstItem = this[0] as IDictionary<string, object>;

                dynamicDescriptors =
                    firstItem.Keys
                    .Select(p => new DynamicPropertyDescriptor(p))
                    .Cast<PropertyDescriptor>()
                    .ToArray();
            }

            return new PropertyDescriptorCollection(dynamicDescriptors);
        }
    }
}
