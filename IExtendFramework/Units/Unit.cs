using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IExtendFramework.Units
{
    public class Unit
    {
        public Number Value { get; set; }
        public virtual AdvancedString Abbreviation
        {
            get
            {
                throw new InvalidOperationException("The base Unit class has no abbreviation");
            }
        }

        public Unit()
        {
        }

        public static implicit operator Unit(Number n)
        {
            return new Unit() { Value = n };
        }

        public static Unit operator *(Unit a, Unit b)
        {
            return UnitConverter.Convert(a, b).Value * a.Value;
        }

        public static Unit operator +(Unit a, Unit b)
        {
            return UnitConverter.Convert(a, b).Value + a.Value;
        }

        public static Unit operator /(Unit a, Unit b)
        {
            return UnitConverter.Convert(a, b).Value / a.Value;
        }

        public static Unit operator -(Unit a, Unit b)
        {
            return UnitConverter.Convert(a, b).Value - a.Value;
        }

    }

    public class Meter : Unit
    {
        public override AdvancedString Abbreviation
        {
            get
            {
                return "m";
            }
        }
    }

    public class Celsius : Unit
    {
        public override AdvancedString Abbreviation
        {
            get
            {
                return "°C";
            }
        }
    }

    public class Kelvin : Unit
    {
        public override AdvancedString Abbreviation
        {
            get
            {
                return "K";
            }
        }
    }
}
