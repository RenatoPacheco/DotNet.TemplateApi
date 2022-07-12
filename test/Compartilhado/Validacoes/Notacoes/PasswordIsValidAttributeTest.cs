using DotNetCore.API.Template.Compartilhado.Validacoes.Notacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DotNetCore.API.Template.Test.Compartilhado.Validacoes.Notacoes
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
