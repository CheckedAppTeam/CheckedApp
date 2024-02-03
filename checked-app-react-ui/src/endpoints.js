import axios from 'axios'

const baseURL = process.env.REACT_APP_API_URL

const itemEndpoints = {
  getAllItems: `${baseURL}/Item/GetAll`,
  getItemById: (id) => `${baseURL}/Item/GetById/${id}`,
  getItemByName: (name) => `${baseURL}/Item/GetByName/${name}`,
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
  addUser: `${baseURL}/Auth/Register`,
  editUser: (id) => `${baseURL}/User/UserData/EditUser/${id}`,
  deleteUser: (id) => `${baseURL}/User/UserData/DeleteUser/${id}`,
  logUser: `${baseURL}/Auth/Login`
}

const userItemEndpoints = {
  getUserItem: (id) => `${baseURL}/UserId/GetUserItem/${id}`,
  getAllUsersItemsByStateInItemList: (state, id) =>
    `${baseURL}/UserId/ByState/${state}/InItemList/${id}`,
  getAllUsersItemsByListId: (id) => `${baseURL}/UserId/ByListId/${id}`,
  addUserItem: `${baseURL}/UserId/AddItemToList`,
  editUserItem: (id) => `${baseURL}/UserId/EditItemOnList/${id}`,
  deleteUserItem: (id) => `${baseURL}/UserId/DeleteItemFromList/${id}`,
}

export {
  axios,
  itemEndpoints,
  itemListEndpoints,
  userEndpoints,
  userItemEndpoints,
}
