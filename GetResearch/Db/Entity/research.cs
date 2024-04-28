using System;
using System.Collections.Generic;

namespace GetResearch.Db.Entity;

public partial class research
{
    public Guid id { get; set; }

    public string title { get; set; } = null!;

    public Guid professor_id { get; set; }

    public string description { get; set; } = null!;

    public int? money { get; set; }

    public string location { get; set; } = null!;

    public string univercity { get; set; } = null!;

    public bool isfulltime { get; set; }

    public DateTime? time { get; set; }
}
