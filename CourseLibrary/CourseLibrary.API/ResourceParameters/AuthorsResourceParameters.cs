using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.ResourceParameters
{
    public class AuthorsResourceParameters
    {
        const int maxPageSize = 20;
        public string MainCategory { get; set; }
        public string SearchQuery { get; set; }
        public int pageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int pageSize 
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

        public string OrderBy { get; set; } = "Name";
    }
}
