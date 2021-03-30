import React from "react";
import { useQueryClient, useMutation } from "react-query";
import CommentForm from "./CommentForm";
import { create as createComment } from "../../helpers/commentsApi";
import { useParams } from "react-router-dom";
import { Alert } from "reactstrap";

const CommentAddNew = () => {
  const { id } = useParams();
  const queryClient = useQueryClient();
  const addCommentMutation = useMutation(
    (comment) => createComment({ ...comment, UserId: "test" }),
    {
      onSuccess: () => {
        queryClient.invalidateQueries(`comments-${id}`);
      },
      onError: (err, variables, previousValue) =>
        queryClient.setQueryData(`comments-${id}`, previousValue),
    }
  );

  if (addCommentMutation.isError) {
    return (
      <Alert color="danger">
        Coś poszło nie tak, spróbuj ponownie później.
      </Alert>
    );
  }

  if (addCommentMutation.isSuccess) {
    return <Alert color="success">Komentarz został dodany pomyślnie</Alert>;
  }

  return (
    <>
      <CommentForm recipeId={id} submitFn={addCommentMutation.mutate} />
    </>
  );
};

export default CommentAddNew;
