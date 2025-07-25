namespace SIGEBI.Application.Contracts.Services
{
    public interface IBookService
    {
        /// <summary>
        /// Checks if a book is available.
        /// </summary>
        /// <param name="bookId">The ID of the book to check.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the book is available, otherwise false.</returns>
        Task<bool> IsBookAvailableAsync(int bookId);
    }
}