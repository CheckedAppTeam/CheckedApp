using AutoMapper;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CheckedAppProject.LOGIC.Services
{
    public class ItemListService : IItemListService
    {
        private readonly UserItemContext _dbContext;
        private IMapper _mapper;

        ItemListService(UserItemContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public ItemListDTO GetById(int id)
        {
            var itemList = _dbContext
                .ItemLists
                .Include(il => il.Items)
                .FirstOrDefault(il => il.ItemListId == id);

            if (itemList is null) return null;

            var result = _mapper.Map<ItemListDTO>(itemList);
            return result;
        }

        public IEnumerable<ItemListDTO> GetAll()
        {
            var itemLists = _dbContext
                .ItemLists
                .Include(il => il.Items)
                .ToListAsync();

            var itemListsDto = _mapper.Map<IEnumerable<ItemListDTO>>(itemLists);

            return itemListsDto;
        }

        public int Create(CreateItemListDTO dto)
        {
            var itemList = _mapper.Map<ItemList>(dto);
            _dbContext.ItemLists.Add(itemList);
            _dbContext.SaveChanges();

            return itemList.ItemListId;
        }

        public bool Delete(int id)
        {
            var itemList = _dbContext
                .ItemLists
                .FirstOrDefault(il => il.ItemListId == id);

            if (itemList is null) return false;

            _dbContext.ItemLists.Remove(itemList);
            _dbContext.SaveChanges();

            return true;

        }

        //public List<Item> ShowItemList(int id)
        //{
        //    return null;
        //}
        //public void EditItemList(int id) { }
        //public List<Item> GetOneListByName(int id) 
        //{
        //    return null;
        //}
        //public List<ItemList> GetListsByTravelDestination(string destination) 
        //{
        //    return null;
        //}
        //public void AddFoundList(int id) { }
        //public string GetMonthName(DateTime time)
        //{
        //    string monthName = time.ToString("MMMM");
        //    return monthName;
        //}
    }
}
