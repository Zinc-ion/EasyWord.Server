using System.ComponentModel.DataAnnotations;

namespace EasyWord.Server.Commands;

public class ImageCommand
{
    [Required]
    public IFormFile File { get; set; }
}