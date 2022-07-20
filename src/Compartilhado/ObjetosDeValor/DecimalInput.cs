using System;
using TemplateApi.RecursoResx;

namespace TemplateApi.Compartilhado.ObjetosDeValor
{
    public struct DecimalInput
        : IFormattable, IComparable, IConvertible,
        IComparable<DecimalInput>, IComparable<decimal>,
        IEquatable<DecimalInput>, IEquatable<decimal>
    {
        public DecimalInput(string input)
        {
            TryParse(input, out DecimalInput output);
            this = output;
        }

        public DecimalInput(decimal input)
        {
            _inptValue = input.ToString();
            _value = input;
            _isValid = true;
        }

        private string _inptValue;
        private decimal _value;
        private bool _isValid;

        public static explicit operator string(DecimalInput input) => input.ToString();
        public static explicit operator DecimalInput(string input) => new DecimalInput(input);

        public static explicit operator decimal(DecimalInput input) => input._value;
        public static explicit operator DecimalInput(decimal input) => new DecimalInput(input);

        /// <summary>
        /// Return value string.Empty
        /// </summary>
        public static readonly DecimalInput Empty = new DecimalInput
        {
            _inptValue = "0",
            _value = 0,
            _isValid = true
        };

        public static void Parse(string input, out DecimalInput output)
        {
            if (TryParse(input, out DecimalInput result))
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

        public static bool TryParse(string input, out DecimalInput output)
        {
            input = input?.Trim();
            bool result = decimal.TryParse(input, out decimal value);
            output = new DecimalInput {
                _isValid = result,
                _inptValue = result ? value.ToString() : input,
                _value = value
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

        public bool Equals(DecimalInput other)
        {
            return _inptValue == other._inptValue;
        }

        public bool Equals(decimal other)
        {
            return _inptValue == other.ToString();
        }

        public override bool Equals(object obj)
        {
            return (obj is DecimalInput typeA && Equals(typeA))
                || (obj is decimal typeB && Equals(typeB));
        }

        public int CompareTo(DecimalInput other)
        {
            return _inptValue.CompareTo(other._inptValue);
        }

        public int CompareTo(decimal other)
        {
            return _inptValue.CompareTo(other.ToString());
        }

        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                return 1;
            }

            if (obj is DecimalInput typeA)
            {
                return CompareTo(typeA);
            }

            if (obj is decimal typeB)
            {
                return CompareTo(typeB);
            }

            throw new ArgumentException(
                nameof(obj), AvisosResx.TipoInvalido);
        }

        public static bool operator ==(DecimalInput left, DecimalInput right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DecimalInput left, DecimalInput right)
        {
            return !(left == right);
        }

        public static bool operator >(DecimalInput left, DecimalInput right)
        {
            return left.CompareTo(right) == 1;
        }

        public static bool operator <(DecimalInput left, DecimalInput right)
        {
            return left.CompareTo(right) == -1;
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