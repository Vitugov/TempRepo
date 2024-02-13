using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Model.Accsess
{
    public class Permission
    {
        public bool Read { get; }
        public bool Write { get; }
        public bool Create { get; }

        public Permission(bool read = false, bool write = false, bool create = false)
        {
            Read = read;
            Write = write;
            Create = create;
        }
    }
}
