using System.ComponentModel.DataAnnotations;

namespace EasyWord.Server.Commands;

public class TextCommand
{
    [Required]
    public string[] words { get; set; }
}