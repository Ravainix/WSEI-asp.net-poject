import React from 'react'
import { Button } from 'reactstrap';
import * as RecipeApi from '../helpers/recipesApi'

const DestroyRecipe = ({ id }) => {
    const handleClick = (id) => {
        if (window.confirm("Are you sure you want to delete?")) {
            RecipeApi.destroy(id)
            alert("Deleted, reload page - WORK IN PROGRESS")
        } 
    }

    return (
            <Button color="danger" onClick={() => handleClick(id)}>Delete</Button>
    )
}

export default DestroyRecipe
