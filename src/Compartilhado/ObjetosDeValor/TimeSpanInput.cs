using System;
using TemplateApi.Recurso;
using System.Diagnostics.CodeAnalysis;

namespace TemplateApi.Compartilhado.ObjetosDeValor
{
    public class TimeSpanInput
        : IFormattable, IConvertible, IInputType,
        IEquatable<TimeSpanInput>, IEquatable<TimeSpan>,
        IEquatable<TimeSpan?>, IEquatable<string>
    {
        public TimeSpanInput() { }

        public TimeSpanInput(string input)
        {
            TryParse(input, out TimeSpanInput output);
            _inptValue = output._inptValue;
            _value = output._value;
            _isValid = output._isValid;
        }

        public TimeSpanInput(TimeSpan? input)
        {
            _inptValue = input?.ToString();
            _value = input;
            _isValid = !(input is null);
        }

        private string _inptValue;
        private TimeSpan? _value;
        private bool _isValid;

        public static explicit operator string(TimeSpanInput input)
        {
            return input?.ToString();
        }

        public static explicit operator TimeSpanInput(string input)
        {
            return input is null ? null : new TimeSpanInput(input);
        }

        public static explicit operator TimeSpan?(TimeSpanInput input)
        {
            return input?._value;
        }

        public static explicit operator TimeSpanInput(TimeSpan? input)
        {
            return input is null ? null : new TimeSpanInput(input);
        }

        /// <summary>
        /// Return value string.Empty
        /// </summary>
        public static readonly TimeSpanInput Empty = new TimeSpanInput(string.Empty);

        public static TimeSpanInput Parse(string input)
        {
            if (TryParse(input, out TimeSpanInput result))
            {
                return result;
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

        public static bool TryParse(string input, out TimeSpanInput output)
        {
            input = input?.Trim();
            bool result = TimeSpan.TryParse(input, out TimeSpan value);
            output = new TimeSpanInput
            {
                _isValid = result,
                _inptValue = result ? value.ToString() : input,
                _value = result ? value : (TimeSpan?)null
            };

            return result;
        }

        public bool IsValid()
        {
            return _isValid;
        }

        public override string ToString()
        {
            return ToString(null);
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

        public bool Equals(TimeSpanInput other)
        {
            return _inptValue == other._inptValue
                && _value == other._value;
        }

        public bool Equals(TimeSpan other)
        {
            return _value == other;
        }

        public bool Equals([AllowNull] TimeSpan? other)
        {
            return _value == other;
        }

        public bool Equals([AllowNull] string other)
        {
            return _inptValue == other;
        }

        public override bool Equals(object obj)
        {
            return (obj is null && _value is null)
                || (obj is TimeSpanInput typeA && Equals(typeA))
                || (obj is TimeSpan typeB && Equals(typeB))
                || (obj is string typeC && Equals(typeC));
        }

        public static bool operator ==(TimeSpanInput left, TimeSpanInput right)
        {
            return (left is null && right is null) ||
                (left is TimeSpanInput l && right is TimeSpanInput r && l.Equals(r));
        }

        public static bool operator !=(TimeSpanInput left, TimeSpanInput right)
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