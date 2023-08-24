using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Repositorio.Dtos
{
    public class ClientRegisterDto
    {
        [Required]
        public string NamesAndSurnames { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Gmail { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
