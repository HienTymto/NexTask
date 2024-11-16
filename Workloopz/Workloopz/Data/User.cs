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

    public string Bu { get; set; } = null!;

    public string JobTitle { get; set; } = null!;

    public virtual BusinessUnit BuNavigation { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Task> TaskCreatorNavigations { get; set; } = new List<Task>();

    public virtual ICollection<Task> TaskOwnerNavigations { get; set; } = new List<Task>();
}
