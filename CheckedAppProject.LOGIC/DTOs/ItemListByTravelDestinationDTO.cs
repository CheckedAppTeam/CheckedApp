using CheckedAppProject.DATA;


namespace CheckedAppProject.LOGIC.DTOs
{
    public class ItemListByTravelDestinationDTO
    {
        public string ListName { get; set; }
        public List<UserItem> Items { get; set; }
        public string TravelDestination { get; set; }
        public string MonthName {  get; set; }
        private bool IsPublic { get; set; }
    }
}
