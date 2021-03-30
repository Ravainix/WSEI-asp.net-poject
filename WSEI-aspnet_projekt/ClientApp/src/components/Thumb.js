import React, { useEffect, useState } from "react";

const Thumb = ({ file }) => {
  const [isLoading, setIsLoading] = useState(false);
  const [isError, setIsError] = useState(false);
  const [thumb, setThumb] = useState(undefined);

  useEffect(() => {
    setIsLoading(true);
    setIsError(false);

    let reader = new FileReader();

    reader.onloadend = () => {
      setIsLoading(false);
      setThumb(reader.result);
    };

    reader.onerror = function () {
      setIsError(true);
      console.log(reader.error);
    };

    reader.readAsDataURL(file);
  }, [file]);

  if (isLoading) {
    return <p>Ładowanie...</p>;
  }

  if (isError) {
    return <p>Wystąpił błąd podczas ładowania</p>;
  }

  return (
    <img
      src={thumb}
      alt={file.name}
      className="img-thumbnail mt-2"
      height={200}
      width={200}
    />
  );
};

export default Thumb;
