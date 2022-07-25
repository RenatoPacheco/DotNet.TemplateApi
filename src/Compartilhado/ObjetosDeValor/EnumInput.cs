using System;
using System.Linq;
using TemplateApi.RecursoResx;

namespace TemplateApi.Compartilhado.ObjetosDeValor
{
    public struct EnumInput<T>
        : IFormattable, IComparable, IConvertible,
        IComparable<EnumInput<T>>, IComparable<T>,
        IEquatable<EnumInput<T>>, IEquatable<T>
        where T: struct
    {
        public EnumInput(string input)
        {
            TryParse(input, out EnumInput<T> output);
            this = output;
        }

        public EnumInput(T input)
        {
            _inptValue = input.ToString();
            _value = input;
            _isValid = true;
            _touched = true;
        }

        private string _inptValue;
        private T _value;
        private bool _isValid;
        private bool _touched;

        public static explicit operator string(EnumInput<T> input) => input._touched ? input.ToString() :Empty.ToString();
        public static explicit operator EnumInput<T>(string input) => new EnumInput<T>(input);

        public static explicit operator T(EnumInput<T> input) => input._touched ? input._value : Empty._value;
        public static explicit operator EnumInput<T>(T input) => new EnumInput<T>(input);

        /// <summary>
        /// Return value string.Empty
        /// </summary>
        public static readonly EnumInput<T> Empty = new EnumInput<T>(Enum.GetValues(typeof(T)).Cast<T>().First());

        public static void Parse(string input, out EnumInput<T> output)
        {
            if (TryParse(input, out EnumInput<T> result))
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

        public static bool TryParse(string input, out EnumInput<T> output)
        {
            input = input?.Trim();
            bool result = Enum.TryParse(input, true, out T value);
            output = new EnumInput<T>
            {
                _isValid = result,
                _inptValue = result ? value.ToString() : input,
                _value = value,
                _touched = true
            };

            return result;
        }

        public bool IsValid() => !_touched || _isValid;

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
            return _touched ? _inptValue : _value.ToString();
        }

        public override int GetHashCode()
        {
            return $"{_inptValue}:{GetType()}".GetHashCode();
        }

        public bool Equals(EnumInput<T> other)
        {
            return _inptValue == other._inptValue;
        }

        public bool Equals(T other)
        {
            return _inptValue == other.ToString();
        }

        public override bool Equals(object obj)
        {
            return (obj is EnumInput<T> typeA && Equals(typeA))
                || (obj is T typeB && Equals(typeB));
        }

        public int CompareTo(EnumInput<T> other)
        {
            return _inptValue.CompareTo(other._inptValue);
        }

        public int CompareTo(T other)
        {
            return _inptValue.CompareTo(other.ToString());
        }

        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                return 1;
            }

            if (obj is EnumInput<T> typeA)
            {
                return CompareTo(typeA);
            }

            if (obj is T typeB)
            {
                return CompareTo(typeB);
            }

            throw new ArgumentException(
                nameof(obj), AvisosResx.TipoInvalido);
        }

        public static bool operator ==(EnumInput<T> left, EnumInput<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(EnumInput<T> left, EnumInput<T> right)
        {
            return !(left == right);
        }

        public static bool operator >(EnumInput<T> left, EnumInput<T> right)
        {
            return left.CompareTo(right) == 1;
        }

        public static bool operator <(EnumInput<T> left, EnumInput<T> right)
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