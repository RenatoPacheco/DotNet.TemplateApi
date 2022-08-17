using TemplateApi.Compartilhado.Validacoes.Notacoes;
using Xunit;

namespace TemplateApi.Teste.Compartilhado.Validacoes.Notacoes
{
    public class PasswordIsValidAttributeTest
    {
        private PasswordIsValidAttribute Attribute { get; set; }

        [Theory]
        [InlineData(null)]
        [InlineData("1aZ#5yM&")]
        public void Password_is_valid(string value)
        {
            Attribute = new PasswordIsValidAttribute();
            Assert.True(Attribute.IsValid(value));
        }

        [Theory]
        [InlineData("")]
        [InlineData("01012021")]
        [InlineData("1aZ#555yM&")]
        public void Password_not_is_valid(string value)
        {
            Attribute = new PasswordIsValidAttribute();
            Assert.False(Attribute.IsValid(value));

            Attribute.FormatErrorMessage("Valor");
        }
    }
}
