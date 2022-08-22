using System;
using System.Buffers;
using System.Text;
using System.Text.Json;

namespace TemplateApi.Compartilhado.Extensoes
{
    public static class Utf8JsonReaderExt
    {
        public static string GetBytesToString(this Utf8JsonReader source)
        {
            ReadOnlySpan<byte> span = source.HasValueSequence ? source.ValueSequence.ToArray() : source.ValueSpan;
            return Encoding.UTF8.GetString(span);
        }
    }
}
