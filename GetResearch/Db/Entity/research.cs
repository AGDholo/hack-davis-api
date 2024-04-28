using System;
using System.Collections.Generic;

namespace GetResearch.Db.Entity;

public partial class research
{
    public Guid id { get; set; }

    public string? title { get; set; }

    public Guid? professor_id { get; set; }

    public string? description { get; set; }

    public int? money { get; set; }

    public string? location { get; set; }

    public string? univercity { get; set; }

    public bool? isfulltime { get; set; }

    public DateTime? time { get; set; }
}
