using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace zooModel;

[Table("habitats")]
public partial class Habitat
{
    [Key]
    [Column("habitat_id")]
    public int HabitatId { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("population")]
    [StringLength(50)]
    public string Population { get; set; } = null!;

    [Column("zoo_id")]
    public int ZooId { get; set; }

    [InverseProperty("Habitat")]
    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();

    [ForeignKey("ZooId")]
    [InverseProperty("Habitats")]
    public virtual Zoo Zoo { get; set; } = null!;
}
