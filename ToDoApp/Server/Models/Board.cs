using System;
using System.Collections.Generic;

namespace ToDoApp.Server.Models;

public partial class Board
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Connection> Connections { get; } = new List<Connection>();
}
