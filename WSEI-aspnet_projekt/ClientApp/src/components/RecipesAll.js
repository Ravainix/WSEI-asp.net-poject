import React from "react";

import RecipesList from "./RecipesList";

const RecipesAll = () => {
  return (
    <div className="rounded">
      {selectRecipes && <RecipesList recipes={selectRecipes} />}
    </div>
  );
};

export default RecipesAll;
