import React from 'react'
import { Button } from 'reactstrap';

const DestroyRecipe = ({ id }) => {
    const handleClick = (id) => {
        fetch('/api/currentUserRecipes')
            .then(response => response.json())
            .then(json => console.log(json))
            .catch(err => console.error(err))
    }

    return (
        <Button onClick={() => handleClick(id)}>CurrentUserTest</Button>
    )
}

export default DestroyRecipe
