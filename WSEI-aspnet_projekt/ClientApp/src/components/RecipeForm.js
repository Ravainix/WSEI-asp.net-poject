import React from 'react'
import { Formik } from 'formik'
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

const defaultValues = {
    name: "",
    description: "",
    UserId: "1",
    image: ""
}

const RecipeForm = ({ handleSubmitForm, initialValues = defaultValues }) => {
    const mapObjectNullValuesToEmptyString = obj => {
        const newObj = { ...obj }
        Object.keys(newObj).forEach(key => newObj[key] = newObj[key] === null ? "" : newObj[key])

        return newObj
    }


    return (
        <Formik
            initialValues={mapObjectNullValuesToEmptyString(initialValues) }
            onSubmit={(values, actions) => {
                handleSubmitForm(values)
                actions.setSubmitting(false);
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

export default RecipeForm



