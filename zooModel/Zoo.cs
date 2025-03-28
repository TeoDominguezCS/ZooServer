using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace zooModel;

[Table("zoos")]
public partial class Zoo
{
    [Key]
    [Column("zoo_id")]
    public int ZooId { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("address")]
    [StringLength(50)]
    public string Address { get; set; } = null!;

    [Column("state")]
    [StringLength(13)]
    public string State { get; set; } = null!;

    [InverseProperty("Zoo")]
    public virtual ICollection<Habitat> Habitats { get; set; } = new List<Habitat>();
}
