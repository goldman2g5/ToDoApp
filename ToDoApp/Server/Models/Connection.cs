using System;
using System.Collections.Generic;
using ToDoApp.Server.Models;

namespace ToDoApp.Server.Models;

public partial class Connection
{
    public int Id { get; set; }

    public int User { get; set; }

    public int Board { get; set; }

    public virtual Board BoardNavigation { get; set; } = null!;

    public virtual User UserNavigation { get; set; } = null!;
}
