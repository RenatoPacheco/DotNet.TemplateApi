using System;
using System.Globalization;
using TemplateApi.RecursoResx;
using System.Diagnostics.CodeAnalysis;

namespace TemplateApi.Compartilhado.ObjetosDeValor
{
    public class DoubleInput
        : IFormattable, IConvertible, IInputType,
        IEquatable<DoubleInput>, IEquatable<double>, IEquatable<double?>
    {
        public DoubleInput() { }

        public DoubleInput(string input)
        {
            TryParse(input, out DoubleInput output);
            _inptValue = output._inptValue;
            _value = output._value;
            _isValid = output._isValid;
        }

        public DoubleInput(double? input)
        {
            _inptValue = input?.ToString();
            _value = input;
            _isValid = !(input is null);
        }

        private string _inptValue;
        private double? _value;
        private bool _isValid;

        public static explicit operator string(DoubleInput input)
        {
            return input?.ToString();
        }

        public static explicit operator DoubleInput(string input)
        {
            return input is null ? null : new DoubleInput(input);
        }

        public static explicit operator double?(DoubleInput input)
        {
            return input?._value;
        }

        public static explicit operator DoubleInput(double? input)
        {
            return input is null ? null : new DoubleInput(input);
        }

        /// <summary>
        /// Return value string.Empty
        /// </summary>
        public static readonly DoubleInput Empty = new DoubleInput(string.Empty);

        public static void Parse(string input, out DoubleInput output)
        {
            if (TryParse(input, out DoubleInput result))
            {
                output = result;
            }
            else
            {
                if (input == null)
                    throw new ArgumentException(
                        nameof(input), AvisosResx.NaoPodeSerNulo);
                else
                    throw new ArgumentException(
                        nameof(input), AvisosResx.FormatoInvalido);
            }
        }

        public static bool TryParse(string input, out DoubleInput output)
        {
            input = input?.Trim();
            NumberStyles style = NumberStyles.Number;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            bool result = double.TryParse(input, style, culture, out double value);
            output = new DoubleInput
            {
                _isValid = result,
                _inptValue = result ? value.ToString() : input,
                _value = result ? value : (double?)null
            };

            return result;
        }

        public bool IsValid()
        {
            return _isValid;
        }

        public override string ToString()
        {
            return ToString(null, null);
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return _inptValue;
        }

        public override int GetHashCode()
        {
            return $"{_inptValue}:{GetType()}".GetHashCode();
        }

        public bool Equals(DoubleInput other)
        {
            return _inptValue == other?._inptValue;
        }

        public bool Equals(double other)
        {
            return _inptValue == other.ToString();
        }

        public bool Equals([AllowNull] double? other)
        {
            return other is double o && Equals(o);
        }

        public override bool Equals(object obj)
        {
            return (obj is DoubleInput typeA && Equals(typeA))
                || (obj is double typeB && Equals(typeB));
        }

        public static bool operator ==(DoubleInput left, DoubleInput right)
        {
            return (left is null && right is null) ||
                (left is DoubleInput l && right is DoubleInput r && l.Equals(r));
        }

        public static bool operator !=(DoubleInput left, DoubleInput right)
        {
            return !(left == right);
        }

        #region IConvertible implementation

        public TypeCode GetTypeCode()
        {
            return TypeCode.String;
        }

        /// <internalonly/>
        string IConvertible.ToString(IFormatProvider provider)
        {
            return _inptValue;
        }

        /// <internalonly/>
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(_inptValue);
        }

        /// <internalonly/>
        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(_inptValue);
        }

        /// <internalonly/>
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(_inptValue);
        }

        /// <internalonly/>
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(_inptValue);
        }

        /// <internalonly/>
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(_inptValue);
        }

        /// <internalonly/>
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(_inptValue);
        }

        /// <internalonly/>
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(_inptValue);
        }

        /// <internalonly/>
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(_inptValue);
        }

        /// <internalonly/>
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(_inptValue);
        }

        /// <internalonly/>
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(_inptValue);
        }

        /// <internalonly/>
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(_inptValue);
        }

        /// <internalonly/>
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(_inptValue);
        }

        /// <internalonly/>
        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(_inptValue);
        }

        /// <internalonly/>
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(_inptValue);
        }

        /// <internalonly/>
        object IConvertible.ToType(System.Type type, IFormatProvider provider)
        {
            return Convert.ChangeType(this, type, provider);
        }

        #endregion
    }
}