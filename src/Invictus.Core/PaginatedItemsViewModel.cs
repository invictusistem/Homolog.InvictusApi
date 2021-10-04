using System.Collections.Generic;

namespace Invictus.Core
{
    public class PaginatedItemsViewModel<TEntity> where TEntity : class
    {
        public PaginatedItemsViewModel(int currentPage,
                                       int itemsPerPage,
                                       int totalItemsInDatabase,
                                       List<TEntity> data)
        {
            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;
            TotalItemsInDatabase = totalItemsInDatabase;
            Data = data;
        }

        public int CurrentPage { get; private set; }
        public int ItemsPerPage { get; private set; }
        public long TotalItemsInDatabase { get; private set; }
        public List<TEntity> Data { get; private set; }
    }
}
