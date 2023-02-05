using Syncfusion.Blazor.Schedule.Internal;
using System;
using System.Collections.Generic;
using ToDoApp.Client.Models;

namespace ToDoApp.Client.Models;

public partial class BoardClient
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public virtual ICollection<AppointmentData> AppointmentData { get; } = new List<AppointmentData>();

    public virtual ICollection<ConnectionClient> Connections { get; } = new List<ConnectionClient>();
}
