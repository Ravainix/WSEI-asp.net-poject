import React from 'react'
import { Button } from 'reactstrap';
import { Link } from "react-router-dom";

const UpdateRecipeButton = ({ id }) => {
    return (
        <Link to={`/recipes/update/${id}`}><Button color="info">Edit</Button></Link>
    )
}

export default UpdateRecipeButton
