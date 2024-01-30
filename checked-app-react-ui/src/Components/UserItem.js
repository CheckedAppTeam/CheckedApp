import React from "react";
import Checkbox from '@mui/material/Checkbox';
import FormGroup from '@mui/material/FormGroup';
import FormControlLabel from '@mui/material/FormControlLabel';
import FormControl from '@mui/material/FormControl';
import { orange } from '@mui/material/colors';
import { IconButton } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import '../styles/modal.css'
import { useEffect, useRef, useState } from "react";
import { userItemEndpoints } from "../endpoints";
import { axios } from "../endpoints";

const label = { inputProps: { 'aria-label': 'Checkbox demo' } };

function UserItem({ item }) {
    const [state, setState] = useState(0);
    const [id, setId] = useState();


    const updateItemState = async () => {
        if(id!==null){

            try {
                console.log(id)
                const response = await axios.put(
                    userItemEndpoints.editUserItem(id),
                    state,
                    { headers: { 'Content-Type': 'application/json' } }
                );
    
                console.log('Update response:', response);
                alert('Item state updated successfully');
            } catch (error) {
                console.error('Error updating item state:', error);
            }

        }
    };

    const handleCheckboxChange = (event, checkboxType) => {
console.log(checkboxType)
        if (checkboxType == 'packed') {
            setState(2)
        } else if (checkboxType == 'toBuy') {
            setState(1)
        }

        updateItemState();

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
        console.log("UserItem received item:", item);
        // onUserItemUpdate();
        // prevItemRef.current = item;
    }, [item]);

    const handleDelete = async (e) => {
        e.preventDefault()
        try {
            await deleteUserItem();
            console.log("Item deleted successfully!");
        } catch (error) {
            console.error("Error deleting item:", error);
        }
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
                            // checked={item.userItemState === 2}
                            onChange={(event) => handleCheckboxChange(event, 'packed')}
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
                            // checked={item.userItemState === 1}
                            onChange={(event) => handleCheckboxChange(event, 'toBuy')}
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