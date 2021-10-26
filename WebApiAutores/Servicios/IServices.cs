using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.Servicios
{
    public interface IServices
    {
        void HacerTarea() { }
    }


    



    public class ServiceTransient 
    {
        public Guid guid = Guid.NewGuid();
    }
    public class ServiceScoped
    {
        public Guid guid = Guid.NewGuid();
    }
    public class ServiceSingletone
    {
        public Guid guid = Guid.NewGuid();
    }
}
