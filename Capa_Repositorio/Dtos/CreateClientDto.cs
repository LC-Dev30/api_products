using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Repositorio.Dtos
{
    public class CreateClientDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Gmail { get; set; }
        public string Password { get; set; }
    }
}
