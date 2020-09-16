import React, { Component } from 'react'
import RecipeForm from './RecipeForm'
import { Redirect } from 'react-router';

import * as RecipeApi from '../helpers/recipesApi'

export default class UpdateRecipe extends Component {

    constructor(props) {
        super(props)

        this.state = {
            draft: null,
            recipe: null
        }
    }

    componentDidMount = async () => {
        const { id } = this.props.match.params

        const response = await RecipeApi.getWithIngredients(id)

        this.setState({ draft: response })
    }



    handleFormSubmit = async formData => {
        const { id } = formData
        console.log(formData)
        const resposne = await RecipeApi.update(id, formData)
        console.log("Recipe updated!")
        this.setState({ recipe: resposne })
    }

    render() {
        const { recipe, draft } = this.state
        return (
            <>
                {draft && <RecipeForm handleSubmitForm={this.handleFormSubmit} initialValues={draft} />}

                {recipe && <Redirect push to={{ pathname: "/recipes" }} />}
            </>
        )
    }
}