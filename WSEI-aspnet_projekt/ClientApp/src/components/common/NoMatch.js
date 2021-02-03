import React from "react";
import { useLocation } from "react-router-dom";

const NoMatch = () => {
  let location = useLocation();

  return (
    <div style={{ textAlign: "center" }}>
      <h2>
        No match for <code>{location.pathname}</code>
      </h2>
    </div>
  );
};

export default NoMatch;
