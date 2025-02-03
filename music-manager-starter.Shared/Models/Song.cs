using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace music_manager_starter.Shared.Models
{
    public sealed class Song
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Artist is required")]
        public string Artist { get; set; } = string.Empty;

        [Required(ErrorMessage = "Album is required")]
        public string Album { get; set; } = string.Empty;

        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; } = string.Empty;
    }
}
