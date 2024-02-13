using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Model.Data
{
    public abstract class IStoredData
    {
        public IStoredData()
        {
            Repository.CurrentRepository.Add(this);
        }
    }
}
