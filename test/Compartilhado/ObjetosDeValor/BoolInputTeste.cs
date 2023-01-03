using Xunit;
using TemplateApi.Compartilhado.ObjetosDeValor;
using System;

namespace TemplateApi.Teste.Compartilhado.ObjetosDeValor
{
    public class BoolInputTeste
    {
        [Theory]
        [InlineData("true", true)]
        [InlineData("TRUE", true)]
        [InlineData("false", true)]
        [InlineData("FALSE", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("0", false)]
        [InlineData("1", false)]
        [InlineData("texto", false)]
        public void Receber_string(string entrada, bool ehValido)
        {
            BoolInput valor = new(entrada);

            Assert.Equal(ehValido, valor.IsValid());

            valor = (BoolInput)entrada;

            Assert.Equal(valor is null ? null : ehValido, valor?.IsValid());
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, true)]
        [InlineData(null, false)]
        public void Receber_boolean_que_pode_ser_nulo(bool? entrada, bool ehValido)
        {
            BoolInput valor = new(entrada);

            Assert.Equal(ehValido, valor.IsValid());
            Assert.Equal(entrada?.ToString(), valor.ToString());

            valor = (BoolInput)entrada;

            Assert.Equal(valor is null ? null : ehValido, valor?.IsValid());
            Assert.Equal(entrada?.ToString(), valor?.ToString());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Receber_boolean(bool entrada)
        {
            BoolInput valor = new(entrada);

            Assert.True(valor.IsValid());
            Assert.Equal(entrada.ToString(), valor.ToString());

            valor = (BoolInput)entrada;

            Assert.True(valor.IsValid());
            Assert.Equal(entrada.ToString(), valor.ToString());
        }

        [Theory]
        [InlineData(null, null, true)]
        [InlineData("", null, false)]
        [InlineData(null, "", false)]
        [InlineData("true", "true", true)]
        [InlineData("false", "false", true)]
        [InlineData("false", null, false)]
        [InlineData(null, "true", false)]
        public void Checar_operador_de_igual_e_diferente(string valor, string compara, bool ehIgual)
        {
            BoolInput v = valor is null ? null : new BoolInput(valor);
            BoolInput c = compara is null ? null : new BoolInput(compara);

            Assert.Equal(ehIgual, v == c);
            Assert.Equal(!ehIgual, v != c);
        }

        [Theory]
        [InlineData("true")]
        [InlineData("True")]
        [InlineData("false")]
        [InlineData("False")]
        public void Chechar_parse_valido(string valor)
        {
            BoolInput saida = BoolInput.Parse(valor);
            Assert.NotNull(saida);
            Assert.True(saida.IsValid());
        }

        [Theory]
        [InlineData("text0")]
        [InlineData(null)]
        [InlineData("     ")]
        [InlineData("")]
        public void Chechar_parse_argumentException(string valor)
        {
            Assert.Throws<ArgumentException>(() => BoolInput.Parse(valor));
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("texto", false)]
        [InlineData("     ", false)]
        [InlineData("", false)]
        [InlineData("true", true)]
        [InlineData("True", true)]
        [InlineData("false", true)]
        [InlineData("False", true)]
        public void Chechar_try_parse(string valor, bool ehValido)
        {
            Assert.Equal(ehValido, BoolInput.TryParse(valor, out BoolInput saida));
            Assert.Equal(ehValido, saida.IsValid());
            Assert.NotNull(saida);
        }

        [Theory]
        [InlineData("texto", "texto", true)]
        [InlineData("texto", "Texto", false)]
        [InlineData("True", "true", true)]
        [InlineData("true", "true", true)]
        [InlineData("True", "True", true)]
        [InlineData("true", "True", true)]
        [InlineData("", "", true)]
        [InlineData(null, null, true)]
        public void Checar_equal_bool_input(string entrada, string compare, bool ehValido)
        {
            BoolInput input = new(entrada);
            BoolInput check = new(compare);
            Assert.Equal(ehValido, input.Equals(check));
            Assert.Equal(ehValido, input.Equals((object)check));
            Assert.Equal(ehValido, input.GetHashCode() == check.GetHashCode());
        }

        [Theory]
        [InlineData("texto", "texto", true)]
        [InlineData("texto", "Texto", false)]
        [InlineData("True", "true", false)]
        [InlineData("true", "true", false)]
        [InlineData("True", "True", true)]
        [InlineData("true", "True", true)]
        [InlineData("", "", true)]
        [InlineData(null, null, true)]
        public void Checar_equal_string(string entrada, string compare, bool ehValido)
        {
            BoolInput input = new (entrada);
            Assert.Equal(ehValido, input.Equals(compare));
            Assert.Equal(ehValido, input.Equals((object)compare));
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, false)]
        [InlineData(false, false, true)]
        [InlineData(false, true, false)]
        public void Checar_equal_bool(bool entrada, bool compare, bool ehValido)
        {
            BoolInput input = new(entrada);
            Assert.Equal(ehValido, input.Equals(compare));
            Assert.Equal(ehValido, input.Equals((object)compare));
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, false)]
        [InlineData(true, null, false)]
        [InlineData(false, false, true)]
        [InlineData(false, true, false)]
        [InlineData(false, null, false)]
        [InlineData(null, true, false)]
        [InlineData(null, false, false)]
        [InlineData(null, null, true)]
        public void Checar_equal_bool_null(bool? entrada, bool? compare, bool ehValido)
        {
            BoolInput input = new(entrada);
            Assert.Equal(ehValido, input.Equals(compare));
            Assert.Equal(ehValido, input.Equals((object)compare));
        }

        [Theory]
        [InlineData(null, null, true)]
        [InlineData(true, 123, false)]
        [InlineData(true, true, true)]
        [InlineData(true, "True", true)]
        public void Checar_equal_object(bool? entrada, object compare, bool ehValido)
        {
            BoolInput valor = new(entrada);
            Assert.Equal(ehValido, valor.Equals(compare));
        }

        [Fact]
        public void Checar_empty()
        {
            BoolInput valor = BoolInput.Empty;
            Assert.Equal(string.Empty, valor.ToString());
            Assert.False(valor.IsValid());
        }

        [Theory]
        [InlineData("texto")]
        [InlineData("")]
        [InlineData("true")]
        [InlineData("False")]
        [InlineData(null)]
        public void Checar_string_converte_explicito(string entrada)
        {
            BoolInput input = (BoolInput)entrada;
            string output = (string)input;

            Assert.Equal(input?.ToString(), output);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Checar_boolean_converte_explicito(bool entrada)
        {
            BoolInput input = (BoolInput)entrada;
            bool output = (bool)input;

            Assert.Equal(entrada, output);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [InlineData(null)]
        public void Checar_boolean_null_converte_explicito(bool? entrada)
        {
            BoolInput input = (BoolInput)entrada;
            bool? output = (bool?)input;

            Assert.Equal(entrada, output);
        }
    }
}
