using Xunit;
using TemplateApi.Compartilhado.ObjetosDeValor;
using System;

namespace TemplateApi.Teste.Compartilhado.ObjetosDeValor
{
    public class DoubleInputTeste
    {
        [Theory]
        [InlineData("2", 2, "2")]
        [InlineData("2.0", 2, "2")]
        [InlineData("2.1", 2.1, "2.1")]
        [InlineData("-2", -2, "-2")]
        [InlineData("-2.0", -2, "-2")]
        [InlineData("-2.1", -2.1, "-2.1")]
        public void Receber_string_valida(string entrada, double numeroEsperado, string textoEsperado)
        {
            DoubleInput valor = new(entrada);

            Assert.True(valor.IsValid());
            Assert.Equal(numeroEsperado, (double)valor);
            Assert.Equal(textoEsperado, valor.ToString());

            valor = (DoubleInput)entrada;

            Assert.True(valor.IsValid());
            Assert.Equal(numeroEsperado, (double)valor);
            Assert.Equal(textoEsperado, valor.ToString());
        }

        [Theory]
        [InlineData(2, 2, "2")]
        [InlineData(2.0, 2, "2")]
        [InlineData(2.1, 2.1, "2.1")]
        [InlineData(-2, -2, "-2")]
        [InlineData(-2.0, -2, "-2")]
        [InlineData(-2.1, -2.1, "-2.1")]
        public void Receber_double_valida(double entrada, double numeroEsperado, string textoEsperado)
        {
            DoubleInput valor = new(entrada);

            Assert.True(valor.IsValid());
            Assert.Equal(numeroEsperado, (double)valor);
            Assert.Equal(textoEsperado, valor.ToString());

            valor = (DoubleInput)entrada;

            Assert.True(valor.IsValid());
            Assert.Equal(numeroEsperado, (double)valor);
            Assert.Equal(textoEsperado, valor.ToString());
        }

        [Theory]
        [InlineData("texto", "texto")]
        [InlineData("2,1", "2,1")]
        [InlineData("2,0", "2,0")]
        public void Receber_string_invalida(string entrada, string textoEsperado)
        {
            DoubleInput valor = new(entrada);

            Assert.False(valor.IsValid());
            Assert.Null((double?)valor);
            Assert.Equal(textoEsperado, valor.ToString());

            valor = (DoubleInput)entrada;

            Assert.False(valor.IsValid());
            Assert.Null((double?)valor);
            Assert.Equal(textoEsperado, valor.ToString());
        }

        [Fact]
        public void Receber_nulo_invalida()
        {
            string texto = null;
            DoubleInput valor = new(texto);

            Assert.False(valor.IsValid());
            Assert.Null((double?)valor);
            Assert.Null(valor.ToString());

            valor = (DoubleInput)texto;

            Assert.Null(valor);
            Assert.Null((double?)valor);

            double? numero = null;
            valor = new(numero);

            Assert.False(valor.IsValid());
            Assert.Null((double?)valor);
            Assert.Null(valor.ToString());

            valor = (DoubleInput)numero;

            Assert.Null(valor);
            Assert.Null((double?)valor);
        }
    }
}
