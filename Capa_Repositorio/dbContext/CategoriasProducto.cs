using System;
using System.Collections.Generic;

namespace Capa_Repositorio.dbContext;

public partial class CategoriasProducto
{
    public int IdCategoria { get; set; }

    public string NameCategory { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
