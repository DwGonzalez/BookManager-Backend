using Application.DTOs.Author;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            var authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return Ok(authorsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorsById(int id)
        {
            var author = await _authorService.GetAuthorById(id);
            if (author == null)
                return NotFound("Author not found");

            var authorDto = _mapper.Map<AuthorDto>(author);
            return Ok(authorDto);
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetAuthorsByBookId(int bookId)
        {
            var authors = await _authorService.GetAuthorsByBookId(bookId);
            if (authors == null)
                return NotFound("Authors not found");

            var authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return Ok(authorsDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorCreateOrUpdateDto createAuthorDto)
        {
            if (createAuthorDto == null)
            {
                return BadRequest("Author data is required.");
            }

            var author = _mapper.Map<AuthorDto>(createAuthorDto);
            var createdBook = await _authorService.CreateAuthorAsync(author);

            if (createdBook == null)
            {
                return BadRequest("Failed to create the book.");
            }

            var authorDto = _mapper.Map<AuthorDto>(createdBook);
            return CreatedAtAction(nameof(GetAuthorsById), new { id = authorDto.Id }, authorDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorCreateOrUpdateDto updateAuthorDto)
        {
            if (updateAuthorDto == null)
            {
                return BadRequest("Book data is required.");
            }

            var existingAuthor = await _authorService.GetAuthorById(id);
            if (existingAuthor == null)
            {
                return NotFound("Author not found");
            }

            var book = _mapper.Map<AuthorDto>(updateAuthorDto);
            book.Id = id;
            var updateAuthor = await _authorService.UpdateAuthorAsync(id, book);

            if (updateAuthor == null)
            {
                return BadRequest("Failed to update the book.");
            }

            var authorDto = _mapper.Map<AuthorDto>(updateAuthor);
            return Ok(authorDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var existingAuthor = await _authorService.GetAuthorById(id);
            if (existingAuthor == null)
            {
                return NotFound("Author not found");
            }

            var isDeleted = await _authorService.DeleteAuthorAsync(id);
            if (!isDeleted)
            {
                return BadRequest("Failed to delete the author.");
            }

            return NoContent();
        }
    }
}
