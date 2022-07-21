using System;
using System.Globalization;
using TemplateApi.RecursoResx;

namespace TemplateApi.Compartilhado.ObjetosDeValor
{
    public struct DateTimeInput
        : IFormattable, IComparable, IConvertible,
        IComparable<DateTimeInput>, IComparable<DateTime>,
        IEquatable<DateTimeInput>, IEquatable<DateTime>
    {
        public DateTimeInput(string input)
        {
            TryParse(input, out DateTimeInput output);
            this = output;
        }

        public DateTimeInput(DateTime input)
        {
            _inptValue = input.ToString();
            _value = input;
            _isValid = true;
        }

        private string _inptValue;
        private DateTime _value;
        private bool _isValid;

        public static explicit operator string(DateTimeInput input) => input.ToString();
        public static explicit operator DateTimeInput(string input) => new DateTimeInput(input);

        public static explicit operator DateTime(DateTimeInput input) => input._value;
        public static explicit operator DateTimeInput(DateTime input) => new DateTimeInput(input);

        /// <summary>
        /// Return value string.Empty
        /// </summary>
        public static readonly DateTimeInput Empty = new DateTimeInput
        {
            _inptValue = $"{new DateTime()}",
            _value = new DateTime(),
            _isValid = true
        };

        public static void Parse(string input, out DateTimeInput output)
        {
            if (TryParse(input, out DateTimeInput result))
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

        public static bool TryParse(string input, out DateTimeInput output)
        {
            input = input?.Trim();
            DateTimeStyles styles = DateTimeStyles.NoCurrentDateDefault;
            IFormatProvider provider = null;
            string[] formats = new string[]
            {
                "dd/MM/yyyy",
                "dd/MM/yyyy HH:mm",
                "dd/MM/yyyy HH:mm:ss",
                "dd-MM-yyyy",
                "dd-MM-yyyy HH:mm",
                "dd-MM-yyyy HH:mm:ss",
                "yyyy/MM/dd",
                "yyyy/MM/dd HH:mm",
                "yyyy/MM/dd HH:mm:ss",
                "yyyy-MM-dd",
                "yyyy-MM-dd HH:mm",
                "yyyy-MM-dd HH:mm:ss"
            };
            bool result = false;
            DateTime value = new DateTime();
            foreach (string format in formats)
            {
                if (DateTime.TryParseExact(
                            input,
                            format,
                            provider,
                            styles,
                            out value))
                {
                    result = true;
                    break;
                }
            }
            output = new DateTimeInput {
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

        public bool Equals(DateTimeInput other)
        {
            return _inptValue == other._inptValue;
        }

        public bool Equals(DateTime other)
        {
            return _inptValue == other.ToString();
        }

        public override bool Equals(object obj)
        {
            return (obj is DateTimeInput typeA && Equals(typeA))
                || (obj is DateTime typeB && Equals(typeB));
        }

        public int CompareTo(DateTimeInput other)
        {
            return _inptValue.CompareTo(other._inptValue);
        }

        public int CompareTo(DateTime other)
        {
            return _inptValue.CompareTo(other.ToString());
        }

        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                return 1;
            }

            if (obj is DateTimeInput typeA)
            {
                return CompareTo(typeA);
            }

            if (obj is DateTime typeB)
            {
                return CompareTo(typeB);
            }

            throw new ArgumentException(
                nameof(obj), AvisosResx.TipoInvalido);
        }

        public static bool operator ==(DateTimeInput left, DateTimeInput right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DateTimeInput left, DateTimeInput right)
        {
            return !(left == right);
        }

        public static bool operator >(DateTimeInput left, DateTimeInput right)
        {
            return left.CompareTo(right) == 1;
        }

        public static bool operator <(DateTimeInput left, DateTimeInput right)
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