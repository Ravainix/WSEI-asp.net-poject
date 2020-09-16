const URL = '/api'

export const recipesApiURL = id =>
    id ? `${URL}/recipes/${id}` : `${URL}/recipes`

export const recipesWithIngredientsApiURL = id =>
    id ? `${URL}/recipesWithIngredients/${id}` : `${URL}/recipesWithIngredients`
