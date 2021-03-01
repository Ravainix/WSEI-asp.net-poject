import React from "react";
import { Button } from "reactstrap";
import { useFormik } from "formik";
import styled from "styled-components";

const StyledForm = styled.form`
  display: flex;
  flex-direction: column;
  margin: 1rem 0 2rem;
`;

const StyledTextarea = styled.textarea`
  resize: both;
`;

const CommentForm = ({ recipeId, submitFn }) => {
  const formik = useFormik({
    initialValues: {
      RecipeId: parseInt(recipeId),
      content: "",
    },
    onSubmit: (values) => {
      debugger;
      submitFn(values);
    },
  });

  return (
    <StyledForm onSubmit={formik.handleSubmit}>
      <StyledTextarea
        rows="5"
        name="content"
        id="content"
        onChange={formik.handleChange}
        value={formik.values.content}
      />
      <Button color="primary" type="submit">
        Wyślij
      </Button>
    </StyledForm>
  );
};

export default CommentForm;
