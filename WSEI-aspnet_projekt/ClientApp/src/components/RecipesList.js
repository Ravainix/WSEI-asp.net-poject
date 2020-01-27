import {Row} from "reactstrap";
import SingleRecipe from "./SingleRecipe";
import React from "react";

const RecipesList = ({recipes, userRecipes}) => {
    return (
    <Row className="mx-1">
        {recipes.map(recipe => <SingleRecipe key={recipe.id} userRecipes={userRecipes} {...recipe} />)}
    </Row>
    )
}

export default RecipesList