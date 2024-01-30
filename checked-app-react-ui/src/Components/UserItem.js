import React from "react";
import Checkbox from '@mui/material/Checkbox';
import FormGroup from '@mui/material/FormGroup';
import FormControlLabel from '@mui/material/FormControlLabel';
import FormControl from '@mui/material/FormControl';
import { orange } from '@mui/material/colors';
import { IconButton } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import '../styles/modal.css'
import { useEffect, useRef } from "react";
import { userItemEndpoints } from "../endpoints";
import { axios } from "../endpoints";

const label = { inputProps: { 'aria-label': 'Checkbox demo' } };

function UserItem({ item, onUserItemUpdate }) {
    const prevItemRef = useRef();

    const id = item.userItemId
    async function deleteUserItem() {
        try {
            const response = await axios.delete(userItemEndpoints.deleteUserItem(id));
            console.log('Delete response:', response);
            alert('Item is deleted');
        } catch (error) {
            console.error('Error deleting item:', error);
        }
    }

    const handleDelete = async (e) => {
        e.preventDefault()
        try {
            await deleteUserItem();
            console.log("Item deleted successfully!");
        } catch (error) {
            console.error("Error deleting item:", error);
        }
    }

    useEffect(() => {
        console.log("UserItem received item:", item);
        onUserItemUpdate();
        prevItemRef.current = item;
    }, [item, onUserItemUpdate]);
    return (
        <>
            {console.log(item)}
            <IconButton aria-label="delete" size="small" color='white'>
                <DeleteIcon className="deleteIcon" onClick={(e) => handleDelete(e)}/>
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
        </>
    )
}

export default UserItem;