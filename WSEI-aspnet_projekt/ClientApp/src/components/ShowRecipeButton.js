import React from 'react'
import { Button } from 'reactstrap';
import { Link } from "react-router-dom";

const ShowRecipeButton = ({ id }) => {
    return (
        <Link to={`/recipes/${id}`}><Button color="primary">Show</Button></Link>
    )
}

export default ShowRecipeButton
