import React from 'react'
import { Formik, FieldArray, Field } from 'formik'
import { Button, Form, FormGroup, Input, Label } from 'reactstrap';
import authService from "./api-authorization/AuthorizeService";

const defaultValues = {
    name: "",
    description: "",
    UserId: authService.getUser(),
    image: "",
    ingredients: [
        { name: "", amount: ""}
    ]
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
                    <FormGroup>
                        <Label for="ingredients">Ingredients</Label>
                        <FieldArray name="ingredients">
                            {({ push, remove }) => (
                                <div>
                                    {props.values.ingredients.map( (p, index) => {
                                        return (
                                            <div key={index} className="d-flex pb-2 flex-column flex-md-row justify-content-between">
                                                <div className="" style={{flex: "1"}}>
                                                <Input
                                                    type="textfield"
                                                    name={`ingredients[${index}].name`}
                                                    value={props.values.ingredients[index].name}
                                                    onChange={props.handleChange}
                                                    placeholder="Ingredient name..."
                                                    required
                                                />
                                            </div>
                                                <div className="" >
                                                    <Input 
                                                        type="textfield" 
                                                        name={`ingredients[${index}].amount`} 
                                                        value={props.values.ingredients[index].amount}
                                                        onChange={props.handleChange}
                                                        placeholder="Amount..."
                                                    />
                                                </div>
                                                <Button color="danger" className="" onClick={() => remove(index)}>X</Button>
                                            </div>
                                        )
                                    })}
                                    <Button
                                        type="button"
                                        onClick={() => push({name: "", amount: ""} )}
                                    >
                                        Add ingredient
                                    </Button>
                                </div>
                            )}
    
                        </FieldArray>
                    </FormGroup>
                    <Button type="submit" outline color="primary">Add recipe</Button>
                    <pre>
                        {JSON.stringify(props.values, null, 2)}
                    </pre>
                </Form>
            )}
        </Formik>
    )
}

export default RecipeForm
