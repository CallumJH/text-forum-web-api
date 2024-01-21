using System.ComponentModel.DataAnnotations;

public class ContentToggle
{
    [Required]
    public int ContentId { get; set; }
    [Required]
    public ContentTypeEnum ContentType { get; set; }
    public bool IsFalseInformation { get; set; } = false;
    public bool IsMisleading { get; set; } = false;
}