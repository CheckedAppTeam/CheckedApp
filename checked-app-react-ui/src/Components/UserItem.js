import React from "react";
import Checkbox from '@mui/material/Checkbox';
import FormGroup from '@mui/material/FormGroup';
import FormControlLabel from '@mui/material/FormControlLabel';
import FormControl from '@mui/material/FormControl';
import { orange } from '@mui/material/colors';
import { IconButton } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import '../styles/modal.css'
import { useEffect, useState } from "react";
import { userItemEndpoints } from "../endpoints";
import { axios } from "../endpoints";

const label = { inputProps: { 'aria-label': 'Checkbox demo' } };

function UserItem({ item, onItemChange }) {
    const [state, setState] = useState(0);
    const [id, setId] = useState();


    const updateItemState = async (updatedState) => {
        if (id !== null) {
            try {
                const response = await axios.put(
                    userItemEndpoints.editUserItem(id),
                    updatedState,
                    { headers: { 'Content-Type': 'application/json' } }
                );
                console.log('Update response:', response);
            } catch (error) {
                console.error('Error updating item state:', error);
            }
        }
        onItemChange();
    };

    const handleCheckboxChange = (e, checkboxType) => {
        const isChecked = e.target.checked;
        console.log('isChecked:', isChecked);
        console.log('item.userItemState:', item.itemState);
        console.log(item)

        setState(() => {
            const newState = (checkboxType === 'packed' && isChecked) ? 2 :
                (checkboxType === 'toBuy' && isChecked) ? 1 : 0;

            updateItemState(newState);
            return newState;
        });
    };

    async function deleteUserItem() {
        try {
            const response = await axios.delete(userItemEndpoints.deleteUserItem(id));
            console.log('Delete response:', response);
            alert('Item is deleted');
        } catch (error) {
            console.error('Error deleting item:', error);
        }
    }

    useEffect(() => {
        setId(item.userItemId)
        console.log(id)
    }, [item]);

    // useEffect(() => {
    //     console.log(state)
    // }, [state]);

    const handleDelete = async (e) => {
        e.preventDefault()
        try {
            await deleteUserItem();
            console.log("Item deleted successfully!");
        } catch (error) {
            console.error("Error deleting item:", error);
        }
        onItemChange()
    }


    return (
        <>
            {/* {console.log(item)} */}
            <IconButton aria-label="delete" size="small" color='white'>
                <DeleteIcon className="deleteIcon" onClick={(e) => handleDelete(e)} />
            </IconButton>
            <div className='item'>
                {item.userItemName}
            </div>
            <FormControl component="fieldset">
                <FormGroup aria-label="position" row>
                    <FormControlLabel
                        value="bottom"
                        control={<Checkbox
                            {...label}
                            checked={item.itemState == 2}
                            onChange={(e) => handleCheckboxChange(e, 'packed')}
                            sx={{
                                color: orange[500],
                                '&.Mui-checked': {
                                    color: orange[600],
                                },
                            }}
                        />}
                        label={<span style={{ color: 'orange' }}>Packed</span>}
                        labelPlacement="bottom"
                    />
                    <FormControlLabel
                        value="bottom"
                        control={<Checkbox
                            {...label}
                            checked={item.itemState == 1}
                            onChange={(e) => handleCheckboxChange(e, 'toBuy')}
                            sx={{
                                color: orange[500],
                                '&.Mui-checked': {
                                    color: orange[600],
                                },
                            }}
                        />}
                        label={<span style={{ color: 'orange' }}>To buy</span>}
                        labelPlacement="bottom"
                    />
                </FormGroup>
            </FormControl>
        </>
    )
}

export default UserItem;