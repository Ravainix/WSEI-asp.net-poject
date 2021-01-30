import React from "react";

const Suggestions = ({ suggestions }) => {
  return (
    <ul>
      {suggestions.map((r) => (
        <li key={r.id}>{r.name}</li>
      ))}
    </ul>
  );
};

export default Suggestions;
