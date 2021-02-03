import React from "react";
import { Button } from "reactstrap";
import { useParams } from "react-router-dom";
import styled from "styled-components";
import { useQueryClient, useMutation } from "react-query";

import { create as createComment } from "../helpers/commentsApi";

const StyledTextarea = styled.textarea`
  width: 20rem;
  resize: both;
`;

const CommentForm = () => {
  const { id } = useParams();
  const queryClient = useQueryClient();
  const addCommentMutation = useMutation((comment) => createComment(comment), {
    onSuccess: () => {
      queryClient.invalidateQueries(`comment-${id}`);
    },
    onError: (err, variables, previousValue) =>
      queryClient.setQueryData(`comment-${id}`, previousValue),
  });
  return (
    <>
      <StyledTextarea rows="5" />
      <Button color="primary">Wyślij</Button>
    </>
  );
};

export default CommentForm;
