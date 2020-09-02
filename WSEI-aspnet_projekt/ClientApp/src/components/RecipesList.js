import {Row} from "reactstrap";
import RecipeItem from "./RecipeItem";
import React from "react";

const RecipesList = ({recipes, userRecipes}) => {
    return (
    <Row className="mx-1">
        {recipes.map(recipe => <RecipeItem key={recipe.id} userRecipes={userRecipes} {...recipe} />)}
    </Row>
    )
}

export default RecipesList