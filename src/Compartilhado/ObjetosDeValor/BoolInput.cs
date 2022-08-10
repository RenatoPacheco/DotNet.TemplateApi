using System;
using TemplateApi.RecursoResx;
using System.Diagnostics.CodeAnalysis;

namespace TemplateApi.Compartilhado.ObjetosDeValor
{
    public class BoolInput
        : IFormattable, IConvertible, IInputType,
        IEquatable<BoolInput>, IEquatable<bool>, IEquatable<bool?>
    {
        public BoolInput() { }

        public BoolInput(string input)
        {
            TryParse(input, out BoolInput output);
            _inptValue = output._inptValue;
            _value = output._value;
            _isValid = output._isValid;
        }

        public BoolInput(bool? input)
        {
            _inptValue = input?.ToString();
            _value = input;
            _isValid = !(input is null);
        }

        private string _inptValue;
        private bool? _value;
        private bool _isValid;

        public static explicit operator string(BoolInput input)
        {
            return input?.ToString();
        }

        public static explicit operator BoolInput(string input)
        {
            return input is null ? null : new BoolInput(input);
        }

        public static explicit operator bool?(BoolInput input)
        {
            return input?._value;
        }

        public static explicit operator BoolInput(bool? input)
        {
            return input is null ? null : new BoolInput(input);
        }

        /// <summary>
        /// Return value string.Empty
        /// </summary>
        public static readonly BoolInput Empty = new BoolInput(string.Empty);

        public static void Parse(string input, out BoolInput output)
        {
            if (TryParse(input, out BoolInput result))
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

        public static bool TryParse(string input, out BoolInput output)
        {
            input = input?.Trim();
            bool result = bool.TryParse(input, out bool value);
            output = new BoolInput
            {
                _isValid = result,
                _inptValue = result ? value.ToString() : input,
                _value = result ? value : (bool?)null
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

        public bool Equals(BoolInput other)
        {
            return _inptValue == other?._inptValue;
        }

        public bool Equals(bool other)
        {
            return _inptValue == other.ToString();
        }

        public bool Equals([AllowNull] bool? other)
        {
            return other is bool o && Equals(o);
        }

        public override bool Equals(object obj)
        {
            return (obj is BoolInput typeA && Equals(typeA))
                || (obj is bool typeB && Equals(typeB));
        }

        public static bool operator ==(BoolInput left, BoolInput right)
        {
            return (left is null && right is null) ||
                (left is BoolInput l && right is BoolInput r && l.Equals(r));
        }

        public static bool operator !=(BoolInput left, BoolInput right)
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