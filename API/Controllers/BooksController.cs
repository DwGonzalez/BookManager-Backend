using Application.DTOs.Book;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetBooksAsync();
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books);
            return Ok(bookDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound("Book not found");

            var bookDto = _mapper.Map<BookDto>(book);
            return Ok(bookDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookCreateOrUpdateDto createBookDto)
        {
            if (createBookDto == null)
            {
                return BadRequest("Book data is required.");
            }

            var book = _mapper.Map<BookDto>(createBookDto);
            var createdBook = await _bookService.CreateBookAsync(book);

            if (createdBook == null)
            {
                return BadRequest("Failed to create the book.");
            }

            var bookDto = _mapper.Map<BookDto>(createdBook);
            return CreatedAtAction(nameof(GetBookById), new { id = bookDto.Id }, bookDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookCreateOrUpdateDto updateBookDto)
        {
            if (updateBookDto == null)
            {
                return BadRequest("Book data is required.");
            }

            var existingBook = await _bookService.GetBookByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound("Book not found");
            }

            var book = _mapper.Map<BookDto>(updateBookDto);
            book.Id = id;
            var updatedBook = await _bookService.UpdateBookAsync(id, book);

            if (updatedBook == null)
            {
                return BadRequest("Failed to update the book.");
            }

            var bookDto = _mapper.Map<BookDto>(updatedBook);
            return Ok(bookDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var existingBook = await _bookService.GetBookByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound("Book not found");
            }

            var isDeleted = await _bookService.DeleteBookAsync(id);
            if (!isDeleted)
            {
                return BadRequest("Failed to delete the book.");
            }

            return NoContent();
        }
    }
}
