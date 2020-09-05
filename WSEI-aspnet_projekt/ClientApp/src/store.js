import { configureStore } from '@reduxjs/toolkit';
import recipesSlice from './features/recipes/recipesSlice'

export default configureStore({
    reducer: {
        recipes: recipesSlice,
    },
});