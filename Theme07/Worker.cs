using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme07
{
    struct Worker
    {
        public static int indexer = 0;
        public int? Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public int HeightInCm { get; set; }
        public DateOnly BirthDate { get; set; }
        public string BirthPlace { get; set; }
    }
}
