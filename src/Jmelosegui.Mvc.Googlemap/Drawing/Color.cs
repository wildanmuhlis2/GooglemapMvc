// Copyright (c) Juan M. Elosegui. All rights reserved.
// Licensed under the GPL v2 license. See LICENSE.txt file in the project root for full license information.

namespace Jmelosegui.Mvc.GoogleMap.Drawing
{
    using System;

    public partial struct Color : IEquatable<Color>
    {
        // Stored as RGBA color in the following order
        // |-------|-------|-------|-------
        // A       R       G       B
        private uint argbValue;

        public Color(uint argb)
        {
            this.argbValue = argb;
        }

        public Color(int r, int g, int b)
            : this(r, g, b, byte.MaxValue)
        {
        }

        public Color(int r, int g, int b, int alpha)
        {
            if (((r | g | b | alpha) & 0xFFFFFF00) != 0)
            {
                alpha = Clamp(alpha, byte.MinValue, byte.MaxValue);
                r = Clamp(r, byte.MinValue, byte.MaxValue);
                g = Clamp(g, byte.MinValue, byte.MaxValue);
                b = Clamp(b, byte.MinValue, byte.MaxValue);
            }

            argbValue = ((uint)alpha << 24) | ((uint)r << 16) | ((uint)g << 8) | ((uint)b);
        }

        public byte A
        {
            get
            {
                unchecked
                {
                    return (byte)(this.argbValue >> 24);
                }
            }

            set
            {
                this.argbValue = (this.argbValue & 0x00FFFFFF) | ((uint)value << 24);
            }
        }

        public byte R
        {
            get
            {
                unchecked
                {
                    return (byte)(this.argbValue >> 16);
                }
            }

            set
            {
                this.argbValue = (this.argbValue & 0xFF00FFFF) | ((uint)value << 16);
            }
        }

        public byte G
        {
            get
            {
                unchecked
                {
                    return (byte)(this.argbValue >> 8);
                }
            }

            set
            {
                this.argbValue = (this.argbValue & 0xFFFF00FF) | ((uint)value << 8);
            }
        }

        public byte B
        {
            get
            {
                unchecked
                {
                    return (byte)this.argbValue;
                }
            }

            set
            {
                this.argbValue = (this.argbValue & 0xFFFFFF00) | value;
            }
        }

        public uint ArgbValue => argbValue;

        public static bool operator ==(Color color1, Color color2)
        {
            return color1.argbValue == color2.argbValue;
        }

        public static bool operator !=(Color color1, Color color2)
        {
            return color1.argbValue != color2.argbValue;
        }

        public override int GetHashCode()
        {
            return this.argbValue.GetHashCode();
        }

        public bool Equals(Color other)
        {
            return this.argbValue == other.argbValue;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Color))
            {
                return false;
            }

            Color comp = (Color)obj;
            return comp.argbValue == this.argbValue;
        }

        public override string ToString()
        {
            return $"{{A: {A} R: {R} G: {G} B: {B}}}";
        }

        private static T Clamp<T>(T val, T min, T max)
            where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0)
            {
                return min;
            }

            if (val.CompareTo(max) > 0)
            {
                return max;
            }

            return val;
        }
    }
}
