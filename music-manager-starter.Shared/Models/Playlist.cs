using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace music_manager_starter.Shared.Models
{
    public sealed class Playlist
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public List<Song> Songs { get; set; } = new List<Song>();
    }
}
