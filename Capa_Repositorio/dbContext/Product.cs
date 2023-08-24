using System;
using System.Collections.Generic;

namespace Capa_Repositorio.dbContext;

public partial class Product
{
    public int IdProduct { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int Category { get; set; }

    public int IdClient { get; set; }

    public virtual CategoriasProducto CategoryNavigation { get; set; }

    public virtual Client IdClientNavigation { get; set; }
}
