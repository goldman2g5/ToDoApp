using System;
using System.Collections.Generic;
using ToDoApp.Client.Models;

namespace ToDoApp.Client.Models;

public partial class ConnectionClient
{
    public int Id { get; set; }

    public int User { get; set; }

    public int Board { get; set; }

    public virtual BoardClient BoardNavigation { get; set; } = null!;

    public virtual UserClient UserNavigation { get; set; } = null!;
}
