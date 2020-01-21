const URL = '/api'

export const recipesApiURL = id =>
    id ? `${URL}/recipes/${id}` : `${URL}/recipes`