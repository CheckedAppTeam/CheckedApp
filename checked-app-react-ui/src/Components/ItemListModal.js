import React from 'react'
import { useState, useEffect } from 'react'
import '../styles/modal.css'
import axios from 'axios';
import { itemEndpoints, userItemEndpoints } from '../endpoints';
import Loader from '../spinners/Loader';
import UserItem from './UserItem';
import Select from 'react-select';
import Button from '@mui/material/Button';


function ItemListModal({ closeModal, itemListName, itemListId }) {
    const [allItemsByItemListId, setAllItemsByItemListId] = useState([]);
    const [loading, setLoading] = useState(false);
    const [showSelect, setShowSelect] = useState(false);
    const [allItems, setAllItems] = useState([]);
    const [selectedItem, setSelectItem] = useState();
    const [inputValue, setInputValue] = useState('');
    const [showAdd, setShowAdd] = useState(false);
    const [showBack, setShowBack] = useState(false);
    const [showItemAdd, setShowItemAdd] = useState(true);
    const [showNewItemForm, setShowNewItemForm] = useState(false);
    const [newItemName, setNewItemName] = useState('');
    const [userItemDTO, setUserItemDTO] = useState({
        itemListId: 0,
        itemId: 0,
        itemState: 0
    })

    // const [itemState, setItemState] = useState();
    // const [userItem, setUserItem] = useState();

    // const customStyles = {
    //     option: (provided, state) => ({
    //         ...provided,
    //         borderBottom: '1px solid #ccc',
    //         color: state.isSelected ? 'white' : 'black',
    //         background: state.isSelected ? '#0088cc' : 'white',
    //         zIndex: 9900,
    //     }),
    //     menu: (provided) => ({
    //         ...provided,
    //         zIndex: 9900,
    //     }),
    // }

    const customStyles = {
        option: (provided, state) => ({
          ...provided,
          borderBottom: '1px solid #ccc',
          color: state.isSelected ? 'white' : 'white',
          background: state.isSelected ? '#FF9B36' : '#9B9BFF',
          zIndex: 9900,
        }),
        menu: (provided) => ({
          ...provided,
          maxHeight: '150px',
          zIndex: 99999,
          position: 'absolute',
        }),
        control: (provided) => ({
            ...provided,
            backgroundColor: '#9B9BFF',
            color: 'white',
            borderRadius: '8px',
          }),
          placeholder: (provided) => ({
            ...provided,
            color: 'white', 
          }),
          noOptionsMessage: (provided) => ({
            ...provided,
            backgroundColor: '#9B9BFF',
            color: 'white', // Change color for "No options" message
          }),
      };

    const handleAddNewItem = async () => {
        setShowNewItemForm(true);
    };


    const handleNewItemSubmit = async () => {
        try {
            const newItemResponse = await axios.post(itemEndpoints.addItem, { itemName: newItemName });
            const newItemId = newItemResponse.data.itemId;
    
            const newUserItemDTO = {
                itemListId: itemListId,
                itemId: newItemId,
                itemState: 0,
            };

            await axios.post(userItemEndpoints.addUserItem, newUserItemDTO);
    
            showAllItemsByItemListId();
            
            setNewItemName('');
            setShowNewItemForm(false);
        } catch (error) {
            console.error('Error adding new item:', error);
        }
    };

    const showAllItemsByItemListId = async () => {
        try {
            const response = await axios.get(userItemEndpoints.getAllUsersItemsByListId(itemListId));
            console.log('Response:', response);
            setAllItemsByItemListId(response.data);
            setLoading(true);
        } catch (error) {
            console.error('Error:', error);
        }
    };

    const showAllItems = async () => {
        try {
            const response = await axios.get(itemEndpoints.getAllItems);
            console.log('Response:', response);
            setAllItems(response.data);
        } catch (error) {
            console.error('Error:', error);
        }
    };

    // const sendUserItem = async () => {
    //     try {
    //         await axios.post(userItemEndpoints.addUserItem, userItemDTO);
    //         setUserItemDTO({
    //             itemListId: itemListId,
    //             itemId: 0,
    //             itemState: 0
    //         });
    //     } catch (error) {
    //         console.error('Error:', error);
    //     }
    // }

    const handleAddClick = (e) => {
        e.preventDefault();
        setShowSelect(true)
        setShowItemAdd(false)
        setShowBack(true)
        setShowAdd(true)
    };

    useEffect(() => {
        const currentToken = localStorage.getItem('token')
        if (currentToken) {
            axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`
        } else {
            delete axios.defaults.headers.common['Authorization']
        }
        showAllItemsByItemListId();
        showAllItems();
    }, [itemListId]);

    const handleBack = () => {
        setShowItemAdd(true)
        setShowSelect(false);
        setShowBack(false)
        setShowAdd(false)
    }

    const filterItems = (input) => {
        if (input.length > 2) {
            return allItems
                .filter((item) =>
                    item.itemName.toLowerCase().includes(input.toLowerCase())
                )
                .map((item) => ({
                    value: item.itemName,
                    label: item.itemName,
                }));
        }
    };

    const handleItemsSelect = (selectedOption) => {
        setSelectItem({
            itemName: selectedOption
        })
    }

    const handleAdd = async () => {
        setShowItemAdd(true);
        setShowSelect(false);
        setShowBack(false);
        setShowAdd(false);

        console.log(selectedItem.itemName.value)
        const matchingItem = allItems.find((item) => selectedItem.itemName.value.toLowerCase() === item.itemName.toLowerCase());
        console.log(matchingItem)
        console.log(itemListId)
        console.log(matchingItem.itemId)
        if (matchingItem) {
            const userItemDTO = {
                itemListId: itemListId,
                itemId: matchingItem.itemId,
                itemState: 0,
            }
            console.log(userItemDTO)
            try {
                await axios.post(userItemEndpoints.addUserItem, userItemDTO);
                setUserItemDTO(userItemDTO)
                setAllItemsByItemListId(prevItems => [...prevItems, matchingItem])
                showAllItemsByItemListId();
            } catch (error) {
                console.error('Error:', error);
            }
        }
    };

    const handleItemChange = () => {
        showAllItemsByItemListId();
    };

    const renderButtons = () => {
        if (selectedItem) {
            return (
                <div className='onlyOneButton'>
                    <div id='addBtn'>
                        <Button onClick={handleAdd} variant="contained" color="success">
                            Add
                        </Button>
                    </div>
                </div>
            );
        } else {
            return (
                <div className='twoButtons'>
                    <div id='backBtn'>
                        <Button onClick={handleBack} variant="contained" color="success">
                            Back
                        </Button>
                    </div>
                    <div id='addNewItemBtn'>
                        <p className='modalText'>Can't find any matching item?</p>
                        <Button onClick={handleAddNewItem} variant="contained" color="success">
                            Add New
                        </Button>
                    </div>
                </div>
            );
        }
    };


    return (
        <div className='modalBackground'>
            <div className='modalContainer'>
                <div className='titleCloseBtn'>
                    <button onClick={() => closeModal(false)}></button>
                </div>
                <div className='title'>
                    <h1>{itemListName}</h1>
                </div>
                <div className='body'>
                    {!loading ? (
                        <Loader />
                    ) : (
                        <div className='itemsAndFooter'>
                            <div className='items'>
                                {allItemsByItemListId
                                    .sort((a, b) => a.userItemId - b.userItemId)
                                    .map((item, index) => (
                                        <div className='item-container' key={item.userItemId}>
                                            {console.log(item)}
                                            <UserItem item={item} onItemChange={handleItemChange} />
                                        </div>
                                    ))}
                            </div>
                            <div className='footer'>
                                <div className='selectBtn'>
                                    {showSelect && <Select
                                        className='map-input'
                                        defaultValue={inputValue}
                                        options={filterItems(inputValue)}
                                        onChange={handleItemsSelect}
                                        onInputChange={(value) => setInputValue(value)}
                                        placeholder='Type to search...'
                                        styles={customStyles}
                                    />}
                                </div>
                                <div className='selectButtons'>
                                {!showItemAdd && renderButtons()}
                                </div>
                                {showItemAdd && <Button id='AddItemBtn' onClick={handleAddClick}>
                                    Add
                                </Button>}
                            </div>
                        </div>
                    )}
                </div>
            </div>
        </div>
    )
}

export default ItemListModal