using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookCollection.Core
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> Get(int id);
        Task<Book> Add(Book Book);
        Task<string> Update(Book Book);
        Task<Book> Delete(int id);
    }
}
