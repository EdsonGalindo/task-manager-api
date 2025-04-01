using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Core.DomainObjects
{
    /// <summary>
    /// The solution entity base class
    /// </summary>
    public class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
