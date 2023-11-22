using System.ComponentModel.DataAnnotations;

namespace EasyWord.Server.Commands; 

public class SentenceCommand {
    [Required]
    public string Word { get; set; }
}