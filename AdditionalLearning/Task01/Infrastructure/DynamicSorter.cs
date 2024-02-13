using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Infrastructure
{
    public static class DynamicSorter
    {
        public static void Sort(IList<ExpandoObject> collection, string propertyName, ListSortDirection direction)
        {
            var propertyComparer = new DynamicPropertyComparer(propertyName, direction);
            var sorted = collection.OrderBy(x => x, propertyComparer).ToList();

            collection.Clear();
            foreach (var item in sorted)
            {
                collection.Add(item);
            }
        }

        private class DynamicPropertyComparer : IComparer<ExpandoObject>
        {
            private readonly string _propertyName;
            private readonly ListSortDirection _direction;

            public DynamicPropertyComparer(string propertyName, ListSortDirection direction)
            {
                _propertyName = propertyName;
                _direction = direction;
            }

            public int Compare(ExpandoObject x, ExpandoObject y)
            {
                var xValue = ((IDictionary<string, object>)x)[_propertyName];
                var yValue = ((IDictionary<string, object>)y)[_propertyName];
                int result = Comparer<object>.Default.Compare(xValue, yValue);

                return _direction == ListSortDirection.Ascending ? result : -result;
            }
        }
    }
}
