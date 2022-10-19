using System.ComponentModel.DataAnnotations;

namespace Models;

public abstract class Entity
{
    [Key]
    public int ID { get; set; }
}