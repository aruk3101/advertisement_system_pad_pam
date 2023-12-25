using Projekt.Models.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Common
{
    public class ColorObject : IEquatable<ColorObject>
    {
        public string Name { get; set; }
        public Color Color { get; set; }

        public bool Equals(ColorObject other)
        {
            if (other == null) return false;
            return (this.Color.ToHex().Equals(other.Color.ToHex()));
        }
    }
}
