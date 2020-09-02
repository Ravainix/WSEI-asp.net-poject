import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { getAll } from '../../helpers/recipesApi'

export const fetchAllRecipes = createAsyncThunk('/recipes/FetchAll', async (thunkAPI) => {
    const response = await getAll();
    return response;
});

const recipesSlice = createSlice({
    name: 'recipes',
    initialState: { entities: null, loading: true, error: null },
    reducers: {
        // standard reducer logic, with auto-generated action types per reducer
    },
    extraReducers: {
        [fetchAllRecipes.pending]: (state, action) => {
            state.loading = true;
        },
        [fetchAllRecipes.fulfilled]: (state, action) => {
            state.entities = action.payload.map((el) => ({
                ...el,
                ListPrice: el.ListPrice.toFixed(),
                RBP: el.RBP.toFixed(),
                ASP: el.ASP.toFixed(),
            }));
            state.loading = false;
        },
        [fetchAllRecipes.rejected]: (state, action) => {
            state.error = action.payload;
        },
    },
});

export default modelsSlice.reducer;