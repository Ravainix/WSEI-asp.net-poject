import React from 'react'
import { Button } from 'reactstrap';
import AuthorizeService from './api-authorization/AuthorizeService'

const DestroyRecipe = ({ id }) => {
    const handleClick = async (id) => {
        const token = await AuthorizeService.getAccessToken();
        //console.log(user.id_token)
        fetch('/api/currentUserRecipes', { headers: { 'Authorization': `Bearer ${token}` }})
            .then(response => response.json())
            .then(json => console.log(json))
            .catch(err => console.error(err))
    }

    return (
        <Button onClick={() => handleClick(id)}>CurrentUserTest</Button>
    )
}

export default DestroyRecipe
