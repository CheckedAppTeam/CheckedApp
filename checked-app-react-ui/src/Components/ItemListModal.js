import React from 'react'
import { useState, useEffect } from 'react'
import '../styles/modal.css'
import axios from 'axios';
import { userItemEndpoints } from '../endpoints';
import Loader from '../spinners/Loader';
import Checkbox from '@mui/material/Checkbox';
import FormGroup from '@mui/material/FormGroup';
import FormControlLabel from '@mui/material/FormControlLabel';
import FormControl from '@mui/material/FormControl';
import FormLabel from '@mui/material/FormLabel';
import { orange } from '@mui/material/colors';
import { IconButton } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';


const label = { inputProps: { 'aria-label': 'Checkbox demo' } };


function ItemListModal({ closeModal, itemListName, itemListId }) {
    const [allItemsByItemListId, setAllItemsByItemListId] = useState();
    const [loading, setLoading] = useState(false);
    const [itemState, setItemState] = useState();



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

    useEffect(() => {
        showAllItemsByItemListId();
    }, []);

    return (
        <div className='modalBackground'>
            <div className='modalContainer'>
                <div className='titleCloseBtn'>
                    <button onClick={() => closeModal(false)}></button>
                </div>
                <div className='title'>
                    <h1>{itemListName}</h1>
                    {!loading && <Loader />}
                </div>
                <div className='body'>
                    {console.log(allItemsByItemListId)}
                    <div className='items'>
                        {allItemsByItemListId && allItemsByItemListId.map((item, index) => (
                            <div className='item-container' key={index}>
                                {console.log(item)}
                                {/* <div className='deleteBtn'>
                                    <button></button>
                                </div> */}
                                <IconButton aria-label="delete" size="small" color='white'>
                                    <DeleteIcon className="deleteIcon" />
                                </IconButton>
                                <div className='editBtn'>
                                </div>
                                <div className='item'>
                                    {item.userItemName}
                                </div>
                                <FormControl component="fieldset">
                                    <FormGroup aria-label="position" row>
                                        <FormControlLabel
                                            value="bottom"
                                            control={<Checkbox
                                                {...label}
                                                defaultChecked
                                                sx={{
                                                    color: orange[800],
                                                    '&.Mui-checked': {
                                                        color: orange[600],
                                                    },
                                                }}
                                            />}
                                            label="Packed"
                                            labelPlacement="bottom"
                                        />
                                        <FormControlLabel
                                            value="bottom"
                                            control={<Checkbox
                                                {...label}
                                                defaultChecked
                                                sx={{
                                                    color: orange[800],
                                                    '&.Mui-checked': {
                                                        color: orange[600],
                                                    },
                                                }}
                                            />}
                                            label="To buy"
                                            labelPlacement="bottom"
                                            label-color='white'
                                        />
                                    </FormGroup>
                                </FormControl>
                            </div>
                        ))}
                    </div>
                </div>
                <div className='footer'>
                    <button id='AddBtn'>
                        Add
                    </button>
                    {/* <button onClick={() => closeModal(false)} id='cancelBtn'>
                        Cancel
                    </button> */}
                </div>
            </div>
        </div>
    )
}

export default ItemListModal