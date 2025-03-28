using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace zooModel;

[Table("animals")]
public partial class Animal
{
    [Key]
    [Column("animal_id")]
    public int AnimalId { get; set; }

    [Column("name")]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("adult_num")]
    public int AdultNum { get; set; }

    [Column("child_num")]
    public int ChildNum { get; set; }

    [Column("male_num")]
    public int MaleNum { get; set; }

    [Column("female_num")]
    public int FemaleNum { get; set; }

    [Column("endangered")]
    public bool Endangered { get; set; }

    [Column("population")]
    public int Population { get; set; }

    [Column("habitat_id")]
    public int HabitatId { get; set; }

    [ForeignKey("HabitatId")]
    [InverseProperty("Animals")]
    public virtual Habitat Habitat { get; set; } = null!;
}
