import React from "react";

import RecipesList from "../../Recipe/RecipesList";

const RecipesContainer = ({ recipes }) => {
  return (
    <div className="border rounded">
      <RecipesList recipes={recipes} />
    </div>
  );
};

export default RecipesContainer;
