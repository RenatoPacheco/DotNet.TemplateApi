using System;
using TemplateApi.Recurso;

namespace TemplateApi.Compartilhado.ObjetosDeValor
{
    public class GuidInput
        : IFormattable, IConvertible, IInputType,
        IEquatable<GuidInput>, IEquatable<Guid>,
        IEquatable<Guid?>, IEquatable<string>
    {
        public GuidInput() { }

        public GuidInput(string input)
        {
            TryParse(input, out GuidInput output);
            _inptValue = output._inptValue;
            _value = output._value;
            _isValid = output._isValid;
        }

        public GuidInput(Guid? input)
        {
            _inptValue = input?.ToString();
            _value = input;
            _isValid = !(input is null);
        }

        private string _inptValue;
        private Guid? _value;
        private bool _isValid;

        public static explicit operator string(GuidInput input)
        {
            return input?.ToString();
        }

        public static explicit operator GuidInput(string input)
        {
            return input is null ? null : new GuidInput(input);
        }

        public static explicit operator Guid?(GuidInput input)
        {
            return input?._value;
        }

        public static explicit operator GuidInput(Guid? input)
        {
            return input is null ? null : new GuidInput(input);
        }

        /// <summary>
        /// Return value string.Empty
        /// </summary>
        public static readonly GuidInput Empty = new GuidInput(string.Empty);

        public static GuidInput Parse(string input)
        {
            if (TryParse(input, out GuidInput result))
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

        public static bool TryParse(string input, out GuidInput output)
        {
            input = input?.Trim();
            bool result = Guid.TryParse(input, out Guid value);
            output = new GuidInput
            {
                _isValid = result,
                _inptValue = result ? value.ToString() : input,
                _value = result ? value : (Guid?)null
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

        public bool Equals(GuidInput other)
        {
            return _inptValue == other._inptValue
                && _value == other._value;
        }

        public bool Equals(Guid other)
        {
            return _value == other;
        }

        public bool Equals(Guid? other)
        {
            return _value == other;
        }

        public bool Equals(string other)
        {
            return _inptValue == other;
        }

        public override bool Equals(object obj)
        {
            return (obj is null && _value is null)
                || (obj is GuidInput typeA && Equals(typeA))
                || (obj is Guid typeB && Equals(typeB))
                || (obj is string typeC && Equals(typeC));
        }

        public static bool operator ==(GuidInput left, GuidInput right)
        {
            return (left is null && right is null) ||
                (left is GuidInput l && right is GuidInput r && l.Equals(r));
        }

        public static bool operator !=(GuidInput left, GuidInput right)
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