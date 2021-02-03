import React from "react";
import { SHARE_URL } from "../constants";
import {
  FacebookShareButton,
  FacebookIcon,
  TwitterShareButton,
  TwitterIcon,
  WhatsappShareButton,
  WhatsappIcon,
} from "react-share";

const ShareIngredient = () => {
  const title = document.title;

  return (
    <div className="shareRecipe">
      <span className="shareRecipe-text">Udostępnij:</span>
      <FacebookShareButton url={SHARE_URL} quote={title}>
        <FacebookIcon size={24} />
      </FacebookShareButton>
      <TwitterShareButton url={SHARE_URL} title={title}>
        <TwitterIcon size={24} />
      </TwitterShareButton>
      <WhatsappShareButton url={SHARE_URL} quote={title}>
        <WhatsappIcon size={24} />
      </WhatsappShareButton>
    </div>
  );
};

export default ShareIngredient;
