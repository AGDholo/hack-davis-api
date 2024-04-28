using System;
using System.Collections.Generic;

namespace GetResearch.Db.Entity;

public partial class application
{
    public Guid id { get; set; }

    public Guid? research_id { get; set; }

    public Guid? student_id { get; set; }

    public int? status { get; set; }

    public string? letter { get; set; }

    public DateTime? time { get; set; }
}
