using System;
using System.Collections.Generic;

namespace Workloopz.Data;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Tittle { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string Password { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string? Bu { get; set; }

    public string JobTitle { get; set; } = null!;

    public bool? Gender { get; set; }

    public string? RandomKey { get; set; }

    public bool? Status { get; set; }

    public DateTime? Birthday { get; set; }

    public int RoleId { get; set; }

    public virtual BusinessUnit? BuNavigation { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
