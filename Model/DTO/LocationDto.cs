using System.ComponentModel.DataAnnotations;

namespace MachinTest_Backend.Model.DTO
{
    public class LocationDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "The field must have 10 Numbers")]
        public string Phone { get; set; }
        [Required]
        public double? Latitude { get; set; }
        [Required]
        public double? Longitude { get; set; }
        [Required]
        public string? Company { get; set; }
    }
}
