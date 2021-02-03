import React from "react";
import { useQueryClient, useMutation } from "react-query";
import CommentForm from "./CommentForm";
import { create as createComment } from "../../helpers/commentsApi";
import { useParams } from "react-router-dom";

const CommentAddNew = () => {
  const { id } = useParams();
  const queryClient = useQueryClient();
  const addCommentMutation = useMutation((comment) => createComment(comment), {
    onSuccess: () => {
      queryClient.invalidateQueries(`comment-${id}`);
    },
    onError: (err, variables, previousValue) =>
      queryClient.setQueryData(`comment-${id}`, previousValue),
  });

  return <CommentForm recipeId={id} submitFn={addCommentMutation.mutate} />;
};

export default CommentAddNew;
