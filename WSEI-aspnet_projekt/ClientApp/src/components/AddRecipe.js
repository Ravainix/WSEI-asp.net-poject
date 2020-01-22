import React, { Component } from 'react'
import RecipeForm from './RecipeForm'
import { Redirect } from 'react-router';

import * as RecipeApi from '../helpers/recipesApi'

export default class AddRecipe extends Component {

    constructor(props) {
        super(props)

        this.state = {
            recipe: null
        }
    }

    handleFormSubmit = async formData => {
        const resposne = await RecipeApi.create(formData)
        console.log("Recipe created!")
        this.setState({ recipe: resposne })
    }

    render() {
        const { recipe } = this.state
        return (
            <>
            <RecipeForm handleSubmitForm={this.handleFormSubmit} />

            {recipe && <Redirect push to={{ pathname: "/recipes" }} />}
            </>
        )
    }
}