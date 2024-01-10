import axios from 'axios';

const baseURL = process.env.REACT_APP_API_URL;

const itemEndpoints = {
  getAllItems: `${baseURL}/Item/GetAll`,
  getItemById: (id) => `${baseURL}/Item/GetById/${id}`,
  addItem: `${baseURL}/Item/AddItem`,
  deleteItem: (id) => `${baseURL}/Item/DeleteItem/${id}`,
  editItem: (id) => `${baseURL}/Item/EditItem/${id}`,
};

const itemListEndpoints = {
  getAllLists: `${baseURL}/ItemList/GetAllLists`,
  // Define other endpoints for ItemListController
};

const userEndpoints = {
  getUserData: (id) => `${baseURL}/User/UserData/${id}`,
  getAllUsersData: `${baseURL}/User/UserData/AllUsers`,
  addUser: `${baseURL}/User/UserData/AddUser`,
  editUser: (id) => `${baseURL}/User/UserData/EditUser/${id}`,
  deleteUser: (id) => `${baseURL}/User/UserData/DeleteUser/${id}`,
};

const userItemEndpoints = {
  getUserItem: (id) => `${baseURL}/UserId/GetUserItem/${id}`,
  getAllUsersItemsByStateInItemList: (state, id) =>
    `${baseURL}/UserId/ByState/${state}/InItemList/${id}`,
  getAllUsersItemsByListId: (id) => `${baseURL}/UserId/ByListId/${id}`,
  addUserItem: `${baseURL}/UserId/AddItemToList`,
  editUserItemStatus: (id) => `${baseURL}/UserId/EditItemOnList/${id}`,
  deleteUserItem: (id) => `${baseURL}/UserId/DeleteItemFromList/${id}`,
};

export {
  axios,
  itemEndpoints,
  itemListEndpoints,
  userEndpoints,
  userItemEndpoints,
};
