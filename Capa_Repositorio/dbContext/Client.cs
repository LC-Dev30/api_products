using System;
using System.Collections.Generic;

namespace Capa_Repositorio.dbContext;

public partial class Client
{
    public int IdClient { get; set; }

    public string NamesAndSurnames { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
