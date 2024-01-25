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
    const [allItemsByItemListId, setAllItemsByItemListId] = useState();
    const [loading, setLoading] = useState(false);
    const [showSelect, setShowSelect] = useState(false);
    const [allItems, setAllItems] = useState();
    const [selectedItem, setSelectItem] = useState();
    const [inputValue, setInputValue] = useState('');
    const [showAdd, setShowAdd] = useState(false);
    const [showBack, setShowBack] = useState(false);
    const [showItemAdd, setShowItemAdd] = useState(true);
    const [userItemDTO, setUserItemDTO] = useState({
        itemListId: itemListId,
        itemId: 0,
        itemState: 0
    })

    // const [itemState, setItemState] = useState();
    // const [userItem, setUserItem] = useState();

    const customStyles = {
        option: (provided, state) => ({
            ...provided,
            borderBottom: '1px solid #ccc',
            color: state.isSelected ? 'white' : 'black',
            background: state.isSelected ? '#0088cc' : 'white',
            zIndex: 9900,
        }),
        menu: (provided) => ({
            ...provided,
            zIndex: 9900,
        }),
    }

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
            setAllItems(response.data || []);
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

    const handleAddClick = () => {
        setShowSelect(true)
        setShowItemAdd(false)
        setShowBack(true)
        setShowAdd(true)
    };

    useEffect(() => {
        showAllItemsByItemListId();
        showAllItems();
    }, []);

    const handleBack = () => {
        setShowItemAdd(true)
        setShowSelect(false);
        setShowBack(false)
        setShowAdd(false)
    }

    const filterItems = (input) => {
        return allItems
            .filter((item) =>
                item.itemName.toLowerCase().includes(input.toLowerCase())
            )
            .map((item) => ({
                value: item.itemName,
                label: item.itemName,
            }));
    };

    const handleAdd = async () => {
        setShowItemAdd(true);
        setShowSelect(false);
        setShowBack(false);
        setShowAdd(false);
    console.log(inputValue)
        const matchingItem = allItems.find((item) => inputValue === item.itemName);
    console.log(matchingItem)
        if (matchingItem) {
            try {
                await axios.post(userItemEndpoints.addUserItem, {
                    itemListId: itemListId,
                    itemId: matchingItem.itemId,
                    itemState: 0,
                });
    
                setUserItemDTO({
                    itemListId: itemListId,
                    itemId: 0,
                    itemState: 0,
                });
            } catch (error) {
                console.error('Error:', error);
            }
        }
    };

    const handleItemsSelect = (selectedOption) => {
        setSelectItem({
            itemName: selectedOption
        })
    }

    return (
        <div className='modalBackground'>
            <div className='modalContainer'>
                <div className='titleCloseBtn'>
                    <button onClick={() => closeModal(false)}></button>
                </div>
                <div className='title'>
                    <h1>{itemListName}</h1>
                    {console.log(allItems)}
                </div>
                <div className='body'>
                    {!loading ? (
                        <Loader />
                    ) : (
                        <div className='itemsAndFooter'>
                            <div className='items'>
                                {allItemsByItemListId.map((item, index) => (
                                    <div className='item-container' key={index}>
                                        <UserItem item={item} />
                                    </div>
                                ))}
                            </div>
                            <div className='footer'>
                                <div className='selectButtons'>
                                    <div className='backBtn'>
                                        {showBack && <Button onClick={handleBack} variant="contained" color="success">
                                            Close
                                        </Button>}
                                    </div>
                                    <div className='addBtn'>
                                        {showAdd && <Button onClick={handleAdd} variant="contained" color="success">
                                            Add
                                        </Button>}
                                    </div>
                                </div>
                                {console.log(userItemDTO)}
                                <div className='selectBtn'>
                                    {showSelect && <Select
                                        className='map-input'
                                        defaultValue={inputValue}
                                        // value={null}
                                        options={filterItems(inputValue)}
                                        onChange={handleItemsSelect}
                                        onInputChange={(value) => setInputValue(value)}
                                        placeholder='Type to search...'
                                        styles={customStyles}
                                    />}
                                </div>
                                {/* {showInput && (
                                <input
                                    type="text"
                                    placeholder="Type here..."
                                />
                            )} */}

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