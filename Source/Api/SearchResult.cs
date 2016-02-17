using System.Collections.Generic;

namespace Affecto.PositiveFeedback.Api
{
    public class SearchResult
    {
        public string SearchCriteria { get; set; }
        public List<Employee> Employees { get; set; }
    }
}