import axios from 'axios'

const baseURL = process.env.REACT_APP_API_URL

const itemEndpoints = {
  getAllItems: `${baseURL}/Item/GetAll`,
  getItemById: (id) => `${baseURL}/Item/GetById/${id}`,
  addItem: `${baseURL}/Item/AddItem`,
  deleteItem: (id) => `${baseURL}/Item/DeleteItem/${id}`,
  editItem: (id) => `${baseURL}/Item/EditItem/${id}`,
}

const itemListEndpoints = {
  getAllLists: `${baseURL}/ItemList/GetAllLists`,
  getByUserId: (userid) => `${baseURL}/ItemList/User/${userid}`,
  getList: (itemlistid) => `${baseURL}/ItemList/GetList/${itemlistid}`,
  getByCity: (city) => `${baseURL}/ItemList/City/${city}`,
  getByDateAndCity: (city, date) =>
    `${baseURL}/ItemList/CityAndDate/${city}/${date}`,
  addList: (userid) => `${baseURL}/ItemList/AddList/${userid}`,
  copyItemList: (itemListid, userid) =>
    `${baseURL}/ItemList/User/${itemListid}/${userid}`,
  updateItemList: (id) => `${baseURL}/ItemList/EditListSpecification/${id}`,
  deleteList: (id) => `${baseURL}/ItemList/DeleteList/${id}`,
}

const userEndpoints = {
  getUserData: (id) => `${baseURL}/User/UserData/${id}`,
  getAllUsersData: `${baseURL}/User/UserData/AllUsers`,
  addUser: `${baseURL}/User/UserData/AddUser`,
  editUser: (id) => `${baseURL}/User/UserData/EditUser/${id}`,
  deleteUser: (id) => `${baseURL}/User/UserData/DeleteUser/${id}`,
}

const userItemEndpoints = {
  getUserItem: (id, userId) => `${baseURL}/${userId}/GetUserItem/${id}`,
  getAllUsersItemsByStateInItemList: (state, id, userId) =>
    `${baseURL}/${userId}/ByState/${state}/InItemList/${id}`,
  getAllUsersItemsByListId: (id, userId) => `${baseURL}/${userId}/ByListId/${id}`,
  addUserItem: (userId) => `${baseURL}/${userId}/AddItemToList`,
  editUserItemStatus: (id, userId) => `${baseURL}/${userId}/EditItemOnList/${id}`,
  deleteUserItem: (id, userId) => `${baseURL}/${userId}/DeleteItemFromList/${id}`,
}

export {
  axios,
  itemEndpoints,
  itemListEndpoints,
  userEndpoints,
  userItemEndpoints,
}
