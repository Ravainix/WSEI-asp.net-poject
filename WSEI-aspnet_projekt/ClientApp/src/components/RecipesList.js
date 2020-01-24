import {Row} from "reactstrap";
import SingleRecipe from "./SingleRecipe";
import React from "react";

const RecipesList = ({recipes}) => {
    return (
    <Row className="mx-1">
        {recipes.map(recipe => <SingleRecipe key={recipe.id} {...recipe} />)}
    </Row>
    )
}

export default RecipesList