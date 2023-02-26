namespace AirlineReservation.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(List<T> values,int count,int page,int pageSize)
        {
            this.AddRange(values);
            TotalPage = (int)Math.Ceiling(count/(double)pageSize);
            Page = page;
        }
        public int TotalPage { get; set; }
        public int Page { get; set; }
        public bool HavePervious { get=>Page>1;}
        public bool HasNext { get=>Page<TotalPage;}
        public static PaginatedList<T> Create(IQueryable<T> query,int pageaSize,int page)
        {
            return new PaginatedList<T>(query.Skip((page-1)*pageaSize).Take(pageaSize).ToList(),query.Count(),page,pageaSize);
        }
    }
}
