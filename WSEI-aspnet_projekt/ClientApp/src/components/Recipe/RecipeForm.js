import React, { useState } from "react";
import styled from "styled-components";
import { Button, Form, FormGroup, Input, Label } from "reactstrap";
import { useForm, useFieldArray } from "react-hook-form";
import Thumb from "../Thumb";

const ImageContainer = styled(FormGroup)`
  display: flex;
  flex-direction: column;
`;

const IngredientsContainer = styled.div`
  margin-bottom: 1rem;
`;

const StyledUl = styled.ul`
  padding-left: 0;
`;

const RecipeForm = ({ handleSubmitForm, initialValues }) => {
  const [file, setFile] = useState(null);
  const { control, register, handleSubmit } = useForm({
    defaultValues: initialValues,
  });
  const { fields, append, remove } = useFieldArray({
    control,
    name: "ingredients",
  });

  const handleImageChange = (e) => {
    setFile(e.target.files[0]);
  };

  const onSubmit = (data) => {
    console.log(data);
  };

  return (
    <Form onSubmit={handleSubmit(handleSubmitForm)}>
      <FormGroup>
        <Label for="name">Nazwa</Label>
        <Input
          type="text"
          name="recipe.name"
          innerRef={register}
          placeholder="Nazwa..."
          required
        />
      </FormGroup>
      <FormGroup>
        <Label for="description">Opis</Label>
        <Input
          type="textarea"
          name="recipe.description"
          innerRef={register}
          placeholder="Opis..."
          required
        />
      </FormGroup>
      <FormGroup>
        <Label for="instruction">Sposób przygotowania</Label>
        <Input
          type="textarea"
          name="recipe.instruction"
          innerRef={register}
          placeholder="Sposób przygotowania..."
          required
        />
      </FormGroup>
      <ImageContainer>
        <Label for="recipeImage">Image</Label>
        {file && <Thumb file={file} />}
        <Input
          type="file"
          name="recipe.image"
          onChange={handleImageChange}
          innerRef={register}
          accept="image/*"
          id="recipeImage"
          required
        />
      </ImageContainer>
      <IngredientsContainer>
        <Label>Składniki</Label>
        <StyledUl>
          {fields.map((item, index) => (
            <li
              key={item.id}
              className="d-flex justify-content-between pb-3 flex-column flex-md-row "
            >
              <div style={{ flex: "1" }}>
                <Input
                  type="textfield"
                  name={`ingredients[${index}].name`}
                  placeholder="Składnik..."
                  innerRef={register()}
                  defaultValue={item.name}
                  required
                />
              </div>
              <div>
                <Input
                  type="number"
                  name={`ingredients[${index}].amount`}
                  defaultValue={item.amount}
                  innerRef={register()}
                  placeholder="Ilość..."
                />
              </div>
              <Button color="danger" onClick={() => remove(index)}>
                Usuń
              </Button>
            </li>
          ))}
        </StyledUl>
        <Button type="button" onClick={() => append({ name: "", amount: 0 })}>
          Dodaj
        </Button>
      </IngredientsContainer>
      <div>
        <Button type="submit" outline color="primary">
          Dodaj przepis
        </Button>
      </div>
    </Form>
  );
};

RecipeForm.defaultProps = {
  initialValues: {
    recipe: {
      name: "",
      description: "",
      instruction: "",
      UserId: "user",
      image: "",
    },
    ingredients: [{ name: "", amount: 0 }],
  },
};

export default RecipeForm;
