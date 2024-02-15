namespace CheckedAppProject.DATA.Models;
public class ItemsQuery
{
    public string SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}