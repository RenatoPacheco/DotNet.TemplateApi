using System;
using DotNetCore.API.Template.Recurso;

namespace DotNetCore.API.Template.Compartilhado.ObjetosDeValor
{
    public struct LongInputData
        : IFormattable, IComparable, IConvertible,
        IComparable<LongInputData>, IComparable<long>,
        IEquatable<LongInputData>, IEquatable<long>
    {
        public LongInputData(string input)
        {
            TryParse(input, out LongInputData output);
            this = output;
        }

        public LongInputData(long input)
        {
            _inptValue = input.ToString();
            _value = input;
            _isValid = true;
        }

        private string _inptValue;
        private long _value;
        private bool _isValid;

        public static implicit operator string(LongInputData input) => input.ToString();
        public static implicit operator LongInputData(string input) => new LongInputData(input);

        public static implicit operator long(LongInputData input) => input._value;
        public static implicit operator LongInputData(long input) => new LongInputData(input);

        /// <summary>
        /// Return value string.Empty
        /// </summary>
        public static readonly LongInputData Empty = new LongInputData { _inptValue = "0" };

        public static void Parse(string input, out LongInputData output)
        {
            if (TryParse(input, out LongInputData result))
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

        public static bool TryParse(string input, out LongInputData output)
        {
            input = input?.Trim();
            bool result = long.TryParse(input, out long value);
            output = new LongInputData {
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

        public bool Equals(LongInputData other)
        {
            return _inptValue == other._inptValue;
        }

        public bool Equals(long other)
        {
            return _inptValue == other.ToString();
        }

        public override bool Equals(object obj)
        {
            return (obj is LongInputData typeA && Equals(typeA))
                || (obj is long typeB && Equals(typeB));
        }

        public int CompareTo(LongInputData other)
        {
            return _inptValue.CompareTo(other._inptValue);
        }

        public int CompareTo(long other)
        {
            return _inptValue.CompareTo(other.ToString());
        }

        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                return 1;
            }

            if (obj is LongInputData typeA)
            {
                return CompareTo(typeA);
            }

            if (obj is long typeB)
            {
                return CompareTo(typeB);
            }

            throw new ArgumentException(
                nameof(obj), AvisosResx.TipoInvalido);
        }

        public static bool operator ==(LongInputData left, LongInputData right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LongInputData left, LongInputData right)
        {
            return !(left == right);
        }

        public static bool operator >(LongInputData left, LongInputData right)
        {
            return left.CompareTo(right) == 1;
        }

        public static bool operator <(LongInputData left, LongInputData right)
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