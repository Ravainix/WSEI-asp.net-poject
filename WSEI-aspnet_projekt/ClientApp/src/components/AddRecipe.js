import React, { Component } from 'react'
import { Formik } from 'formik'
import { Button, Form, FormGroup, Label, Input, CustomInput } from 'reactstrap';
import * as RecipeApi from '../helpers/recipesApi'

export default class AddRecipe extends Component {


    render() {
        const { history } = this.props
        return (
            <Formik
                initialValues={{
                    name: "",
                    description: "",
                    UserId: "1",
                    image: ""
                }}
                onSubmit={(values, actions) => {
                    //this.addRecipe(values).then(res => console.log(res))
                    RecipeApi.create(values)
                    history.push('/recipes')
                    }
                }
            >
                {(props) => (
                    <Form onSubmit={props.handleSubmit}>
                        <FormGroup>
                            <Label for="name">Name</Label>
                            <Input
                                type="text"
                                name="name"
                                value={props.values.name}
                                onChange={props.handleChange}
                                placeholder="Recipe name..."
                                required
                            />
                        </FormGroup>
                        <FormGroup>
                            <Label for="description">Description</Label>
                            <Input
                                type="textarea"
                                name="description"
                                value={props.values.description}
                                onChange={props.handleChange}
                                placeholder="Recipe description..."
                                required
                            />
                        </FormGroup>
                        <FormGroup>
                            <Label for="recipeImage">Image</Label>
                            <Input
                                type="url"
                                name="image"
                                value={props.values.image}
                                onChange={props.handleChange}
                                id="recipeImage"
                                placeholder="Recipe image..."
                            />
                        </FormGroup>
                        <Button type="submit" outline color="primary">Add recipe</Button>
                    </Form>
                )}
            </Formik>
        )
    }
}