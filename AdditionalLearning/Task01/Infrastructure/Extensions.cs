using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Infrastructure
{
    static class Extensions
    {
        public static ExpandoObject Clone(this ExpandoObject original)
        {
            var clone = new ExpandoObject();
            var cloneDict = (IDictionary<string, object>)clone;
            var originalDict = (IDictionary<string, object>)original;

            foreach (var kvp in originalDict)
            {
                cloneDict.Add(kvp.Key, kvp.Value);
            }

            return clone;
        }

        //private static object DeepCloneObject(object obj)
        //{
        //    if (obj is ExpandoObject expando)
        //    {
        //        return DeepCloneExpando(expando);
        //    }
        //    // Альтернатива: реализовать дополнительную логику для других типов данных
        //    // (например, для коллекций, классов с INotifyPropertyChanged и т.д.)

        //    // Для простых типов данных и неизвестных объектов просто возвращаем исходный объект
        //    return obj;
        //}
    }
}
