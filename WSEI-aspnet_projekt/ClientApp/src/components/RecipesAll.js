import React from 'react'
import { useSelector } from 'react-redux'

import RecipesList from './RecipesList'


 const RecipesAll = () => {
    const selectRecipes = useSelector(state => state.recipes.entities)

    return (
        <div className=" rounded">
            {selectRecipes && <RecipesList recipes={selectRecipes} />}
        </div>
    )
}

export default RecipesAll