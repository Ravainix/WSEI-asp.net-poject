import * as api from './api'
import { recipesApiURL } from './routes'

//get all recipes 
export const getAll = () => api.get(recipesApiURL())

//create new recipe
export const create = (params) => api.post(recipesApiURL(), { ...params })

//update existing recipe 
export const update = (id, params) => api.put(recipesApiURL(id), { ...params  })

//delete existing recipe
export const destroy = (id) => api.destroy(recipesApiURL(id))