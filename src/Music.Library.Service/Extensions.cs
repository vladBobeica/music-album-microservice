using Music.Library.Service.Entities;
using Music.Library.Service.Models;

namespace Music.Library.Service
{
    public static class Extensions
    {
        public static AlbumDto AsDto(this Album album){
            return new AlbumDto(album.Id, album.Title, album.Artist);
        }
    }
}