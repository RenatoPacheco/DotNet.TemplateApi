using System;
using TemplateApi.RecursoResx;
using System.Diagnostics.CodeAnalysis;

namespace TemplateApi.Compartilhado.ObjetosDeValor
{
    public class LongInput
        : IFormattable, IConvertible,
        IEquatable<LongInput>, IEquatable<long>, IEquatable<long?>
    {
        public LongInput() { }

        public LongInput(string input)
        {
            TryParse(input, out LongInput output);
            _inptValue = output._inptValue;
            _value = output._value;
            _isValid = output._isValid;
        }

        public LongInput(long? input)
        {
            _inptValue = input?.ToString();
            _value = input;
            _isValid = !(input is null);
        }

        private string _inptValue;
        private long? _value;
        private bool _isValid;

        public static explicit operator string(LongInput input) => input.ToString();
        public static explicit operator LongInput(string input) => new LongInput(input);

        public static explicit operator long?(LongInput input) => input._value;
        public static explicit operator LongInput(long? input) => new LongInput(input);

        /// <summary>
        /// Return value string.Empty
        /// </summary>
        public static readonly LongInput Empty = new LongInput(string.Empty);

        public static void Parse(string input, out LongInput output)
        {
            if (TryParse(input, out LongInput result))
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

        public static bool TryParse(string input, out LongInput output)
        {
            input = input?.Trim();
            bool result = long.TryParse(input, out long value);
            output = new LongInput {
                _isValid = result,
                _inptValue = result ? value.ToString() : input,
                _value = result ? value : (long?)null
            };

            return result;
        }

        public bool IsValid() => _isValid;

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

        public bool Equals(LongInput other)
        {
            return _inptValue == other?._inptValue;
        }

        public bool Equals(long other)
        {
            return _inptValue == other.ToString();
        }

        public bool Equals([AllowNull] long? other)
        {
            return other is long o && Equals(o);
        }

        public override bool Equals(object obj)
        {
            return (obj is LongInput typeA && Equals(typeA))
                || (obj is long typeB && Equals(typeB));
        }

        public static bool operator ==(LongInput left, LongInput right)
        {
            return (left is null && right is null) ||
                (left is LongInput l && right is LongInput r && l.Equals(r));
        }

        public static bool operator !=(LongInput left, LongInput right)
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