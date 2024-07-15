namespace BuberDinner.Application.Common.Wrappers;

public class PaginationResponse<TResponse> 
{
    public bool Success { get; set; }
    public List<TResponse>? Data { get; set; }
    public PagedResponseMeta? Meta { get; set; }

    public PaginationResponse()
    {
        Success = true;
        Data = new List<TResponse>();
        Meta = new PagedResponseMeta();
    }
}

public class PagedResponseMeta
{
    public int? TotalCount { get; set; }
    public int? CurrentPage { get; set; }
    public int? PageSize { get; set; }
    public int? TotalPages { get; set; }
    public bool? HasPrevious { get; set; }
    public bool? HasNext { get; set; }
}

