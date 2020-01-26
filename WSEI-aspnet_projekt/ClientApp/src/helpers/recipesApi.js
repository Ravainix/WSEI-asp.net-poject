import * as api from './api'
import { recipesApiURL } from './routes'

//get all recipes 
export const getAll = () => api.get(recipesApiURL())

//get all user recipes
export const getAllUser = () => api.get('/api/currentUserRecipes')

//create new recipe
export const create = (params) => api.post(recipesApiURL() + "WithIngredients", { ...params })

//update existing recipe 
export const update = (id, params) => api.put(recipesApiURL(id), { ...params  })

//delete existing recipe
export const destroy = (id) => api.destroy(recipesApiURL(id))

//get existing recipe
export const get = (id) => api.get(recipesApiURL(id))