using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiAutores.Validaciones;

namespace WebApiAutores.Test.PruebasUnitarias
{
    [TestClass]
    public class PrimeraLetraMayusculaAttributeTest
    {
        [TestMethod]
        public void PrimeraLetraMinusculaError()
        {
            //preparar
            var primeraLetraMayusculaClass = new PrimeraLetraMayusculaAttribute();
            var Valor = "carlhos";
            var ValCOnt = new ValidationContext(new { Nombre = Valor });
            //probar
            var resultado = primeraLetraMayusculaClass.GetValidationResult(Valor,ValCOnt);
            //verificar
            Assert.AreEqual("La primera Letra debe ser mayuscula",resultado.ErrorMessage);
        }
        [TestMethod]
        public void NUllnoDevuelveError()
        {
            //preparar
            var primeraLetraMayusculaClass = new PrimeraLetraMayusculaAttribute();
            String Valor = null;
            var ValCOnt = new ValidationContext(new { Nombre = Valor });
            //probar
            var resultado = primeraLetraMayusculaClass.GetValidationResult(Valor, ValCOnt);
            //verificar
            Assert.IsNull(resultado);
        }
    }
}
