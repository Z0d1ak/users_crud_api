using System;
using System.Collections.Generic;
using System.Text;

namespace app.Data.Entities
{
    public sealed class User : ICloneable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
