import React from 'react'
import { useSelector } from 'react-redux'

import RecipesMenu from "./RecipesMenu";
import RecipesList from './RecipesList'


 const RecipesAll = () => {
    const selectRecipes = useSelector(state => state.recipes.entities)

    return (
        <div className="border rounded">
            {selectRecipes && <RecipesList recipes={selectRecipes} />}
        </div>
    )
}

export default RecipesAll