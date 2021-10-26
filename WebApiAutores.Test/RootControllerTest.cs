using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiAutores.Controllers;
using WebApiAutores.Test.mocks;

namespace WebApiAutores.Test
{

    [TestClass]
    public class RootControllerTest
    {
        [TestMethod]
        public async Task SiEsAdminDevuelve4Link()
        {
            //preparacion
            var Autorizacion = new AuthorizationService();
            Autorizacion.Resultado = AuthorizationResult.Success();
            var RootControllerClass = new RootController(Autorizacion);
            RootControllerClass.Url = new URLHelperMOck();
            //prueba
            var resultado = await RootControllerClass.Obtener();
            //Verificacion
            Assert.AreEqual(3,resultado.Value.Count());
        }
        [TestMethod]
        public async Task SiNOEsAdminDevuelve2Link()
        {
            //preparacion
            var Autorizacion = new AuthorizationService();
            Autorizacion.Resultado = AuthorizationResult.Failed();
            var RootControllerClass = new RootController(Autorizacion);
            RootControllerClass.Url = new URLHelperMOck();
            //prueba
            var resultado = await RootControllerClass.Obtener();
            //Verificacion
            Assert.AreEqual(2, resultado.Value.Count());
        }
        [TestMethod]
        public async Task SiNOEsAdminDevuelve2LinkConMoq()
        {
            //preparacion
            var mockAutorizacion = new Mock<IAuthorizationService>();
            mockAutorizacion.Setup(x => x.AuthorizeAsync(

                    It.IsAny<ClaimsPrincipal>(),
                    It.IsAny<object>(),
                    It.IsAny<IEnumerable<IAuthorizationRequirement>>()

                )).Returns(Task.FromResult(AuthorizationResult.Failed()));
            mockAutorizacion.Setup(x => x.AuthorizeAsync(

               It.IsAny<ClaimsPrincipal>(),
               It.IsAny<object>(),
               It.IsAny<String>()

           )).Returns(Task.FromResult(AuthorizationResult.Failed()));
            var UrlMock = new Mock<IUrlHelper>();

            UrlMock.Setup(x=> 
                    x.Link(It.IsAny<String>(),It.IsAny<object>())
                
                ).Returns(String.Empty);

            var RootControllerClass = new RootController(mockAutorizacion.Object);
            RootControllerClass.Url = UrlMock.Object;
            //prueba
            var resultado = await RootControllerClass.Obtener();
            //Verificacion
            Assert.AreEqual(2, resultado.Value.Count());
        }
    }
}
