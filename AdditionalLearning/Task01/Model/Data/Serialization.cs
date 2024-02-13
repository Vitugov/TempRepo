using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Shapes;
using Task01.Model.Data;

namespace Task01.Model.Data
{
    internal static class Serialization
    {
        internal static string Path = "_Data.json";
        internal static void Serialize()
        {
            var serializingData = Repository.CurrentRepository.Edits[typeof(Client)].ToList()
                .Select(pair => new KeyValuePair<Client, List<Edit>>(pair.Key as Client, pair.Value))
                .ToList();

            string jsonString = JsonConvert.SerializeObject(serializingData,
                Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });
            File.WriteAllText(Path, jsonString);
        }

        internal static void Deserialize()
        {
            var jsonString = File.ReadAllText(Path);
            var deserializedData = JsonConvert.DeserializeObject<List<KeyValuePair<Client, List<Edit>>>>(jsonString);
            Repository.CurrentRepository.Edits[typeof(Client)] = deserializedData.ToDictionary(x => x.Key as IStoredData, x => x.Value);
        }
    }
}
