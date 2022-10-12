using System.ComponentModel.DataAnnotations;

namespace Music.Library.Service.Models
{
    public record AlbumDto(Guid Id, string Title, string Artist);

    public record CreateAlbumDto(string Title, string Artist);

    public record UpdateAlbumDto(string Title, string Artist);




}