using System;
using System.Globalization;
using TemplateApi.Recurso;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace TemplateApi.Compartilhado.ObjetosDeValor
{
    public class FloatInput
        : IFormattable, IConvertible, IInputType,
        IEquatable<FloatInput>, IEquatable<float>,
        IEquatable<float?>, IEquatable<string>
    {
        public FloatInput() { }

        public FloatInput(string input)
        {
            TryParse(input, out FloatInput output);
            _inptValue = output._inptValue;
            _value = output._value;
            _isValid = output._isValid;
        }

        public FloatInput(float? input)
        {
            TryParse(input?.ToString(Culture()), out FloatInput output);
            _inptValue = output._inptValue;
            _value = output._value;
            _isValid = output._isValid;
        }

        private string _inptValue;
        private float? _value;
        private bool _isValid;

        public static explicit operator string(FloatInput input)
        {
            return input?.ToString();
        }

        public static explicit operator FloatInput(string input)
        {
            return input is null ? null : new FloatInput(input);
        }

        public static explicit operator float?(FloatInput input)
        {
            return input?._value;
        }

        public static explicit operator FloatInput(float? input)
        {
            return input is null ? null : new FloatInput(input);
        }

        /// <summary>
        /// Return value string.Empty
        /// </summary>
        public static readonly FloatInput Empty = new FloatInput(string.Empty);

        public static FloatInput Parse(string input)
        {
            if (TryParse(input, out FloatInput result))
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

        public static bool TryParse(string input, out FloatInput output)
        {
            input = input?.Trim();
            NumberStyles style = NumberStyles.Number;
            bool result = float.TryParse(input, style, Culture(), out float value);
            result = result && !(input is null) && input.IndexOf(",") < 0;
            if (result)
            {
                input = value.ToString(Culture());
                if (Regex.IsMatch(input, @"\.[0]+$"))
                {
                    value = (float)Math.Floor(value);
                    input = value.ToString(Culture());
                }
            }
            output = new FloatInput
            {
                _isValid = result,
                _inptValue = input,
                _value = result ? value : (float?)null
            };

            return result;
        }

        private static CultureInfo Culture() => CultureInfo.CreateSpecificCulture("en-US");

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

        public bool Equals(FloatInput other)
        {
            return _inptValue == other._inptValue
                && _value == other._value;
        }

        public bool Equals(float other)
        {
            return _value == other;
        }

        public bool Equals([AllowNull] float? other)
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
                || (obj is FloatInput typeA && Equals(typeA))
                || (obj is float typeB && Equals(typeB))
                || (obj is string typeC && Equals(typeC));
        }

        public static bool operator ==(FloatInput left, FloatInput right)
        {
            return (left is null && right is null) ||
                (left is FloatInput l && right is FloatInput r && l.Equals(r));
        }

        public static bool operator !=(FloatInput left, FloatInput right)
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