using System;
using System.Collections.Generic;

namespace GetResearch.Db.Entity;

public partial class user
{
    public Guid id { get; set; }

    public string? name { get; set; }

    public bool? professor { get; set; }

    public string? lastname { get; set; }

    public string? firstname { get; set; }

    public string? gender { get; set; }

    public string? pronounce { get; set; }

    public string? biography { get; set; }

    public string? eduemail { get; set; }

    public int? phonenumber { get; set; }

    public string? personal_homepage { get; set; }

    public string? featured_publications { get; set; }

    public string? award_honor { get; set; }

    public string? department { get; set; }

    public string? photo { get; set; }

    public int? research_area { get; set; }

    public string? middlename { get; set; }

    public string? university { get; set; }

    public DateTime? time { get; set; }

    public string? user_id { get; set; }
}
