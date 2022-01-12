using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos.Utils
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

        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public long TotalItemsInDatabase { get; set; }
        public List<TEntity> Data { get; set; }
    }
}
