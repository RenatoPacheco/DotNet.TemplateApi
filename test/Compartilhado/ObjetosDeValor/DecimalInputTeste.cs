using Xunit;
using TemplateApi.Compartilhado.ObjetosDeValor;
using System;

namespace TemplateApi.Teste.Compartilhado.ObjetosDeValor
{
    public class DecimalInputTeste
    {
        [Theory]
        [InlineData("2", 2, "2")]
        [InlineData("2.0", 2, "2")]
        [InlineData("2.1", 2.1, "2.1")]
        [InlineData("-2", -2, "-2")]
        [InlineData("-2.0", -2, "-2")]
        [InlineData("-2.1", -2.1, "-2.1")]
        public void Receber_string_valida(string entrada, decimal numeroEsperado, string textoEsperado)
        {
            DecimalInput valor = new(entrada);

            Assert.True(valor.IsValid());
            Assert.Equal(numeroEsperado, (decimal)valor);
            Assert.Equal(textoEsperado, valor.ToString());

            valor = (DecimalInput)entrada;

            Assert.True(valor.IsValid());
            Assert.Equal(numeroEsperado, (decimal)valor);
            Assert.Equal(textoEsperado, valor.ToString());
        }

        [Theory]
        [InlineData(2, 2, "2")]
        [InlineData(2.0, 2, "2")]
        [InlineData(2.1, 2.1, "2.1")]
        [InlineData(-2, -2, "-2")]
        [InlineData(-2.0, -2, "-2")]
        [InlineData(-2.1, -2.1, "-2.1")]
        public void Receber_decimal_valida(decimal entrada, decimal numeroEsperado, string textoEsperado)
        {
            DecimalInput valor = new(entrada);

            Assert.True(valor.IsValid());
            Assert.Equal(numeroEsperado, (decimal)valor);
            Assert.Equal(textoEsperado, valor.ToString());

            valor = (DecimalInput)entrada;

            Assert.True(valor.IsValid());
            Assert.Equal(numeroEsperado, (decimal)valor);
            Assert.Equal(textoEsperado, valor.ToString());
        }

        [Theory]
        [InlineData("texto", "texto")]
        [InlineData("2,1", "2,1")]
        [InlineData("2,0", "2,0")]
        public void Receber_string_invalida(string entrada, string textoEsperado)
        {
            DecimalInput valor = new(entrada);

            Assert.False(valor.IsValid());
            Assert.Null((decimal?)valor);
            Assert.Equal(textoEsperado, valor.ToString());

            valor = (DecimalInput)entrada;

            Assert.False(valor.IsValid());
            Assert.Null((decimal?)valor);
            Assert.Equal(textoEsperado, valor.ToString());
        }

        [Fact]
        public void Receber_nulo_invalida()
        {
            string texto = null;
            DecimalInput valor = new(texto);

            Assert.False(valor.IsValid());
            Assert.Null((decimal?)valor);
            Assert.Null(valor.ToString());

            valor = (DecimalInput)texto;

            Assert.Null(valor);
            Assert.Null((decimal?)valor);

            decimal? numero = null;
            valor = new(numero);

            Assert.False(valor.IsValid());
            Assert.Null((decimal?)valor);
            Assert.Null(valor.ToString());

            valor = (DecimalInput)numero;

            Assert.Null(valor);
            Assert.Null((decimal?)valor);
        }
    }
}
