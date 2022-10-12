using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Music.Library.Service.Models;
using Music.Library.Service.Repositories;
using Music.Library.Service.Entities;

namespace Music.Library.Service.Controllers
{
    [ApiController]
    [Route("albums")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository itemsRepository;

        public ItemsController(IItemsRepository itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<AlbumDto>> GetAsync()
        {
            var albums = (await itemsRepository.GetAllAsync())
                            .Select(album => album.AsDto());
            return albums;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumDto>> GetByIdAsync(Guid id)
        {
            var album = await itemsRepository.GetAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            return album.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<AlbumDto>> PostAsync(CreateAlbumDto createAlbumDto)
        {
            var album = new Album{
                Title = createAlbumDto.Title,
                Artist = createAlbumDto.Artist
            };

            await itemsRepository.CreateAsync(album);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = album.Id }, album);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateAlbumDto updateAlbumDto)
        {
            var existingAlbum = await itemsRepository.GetAsync(id);

            if(existingAlbum == null)
            {
                return NotFound();
            }

            existingAlbum.Title = updateAlbumDto.Title;
            existingAlbum.Artist = updateAlbumDto.Artist;

            await itemsRepository.UpdateAsync(existingAlbum);


            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var album = await itemsRepository.GetAsync(id);

            if(album == null)
            {
                return NotFound();
            }

            await itemsRepository.RemoveAsync(album.Id);


            return NoContent();

        }

    }
}