import React from "react";
import styled, { css } from 'styled-components'

const StyledForm = styled.form`
  position: relative;
  width: 30rem;
  background: #57bd84;
  border-radius: 2rem;
`

const StyledLabel = styled.label`
  position: absolute;
  clip: rect(1px, 1px, 1px, 1px);
  padding: 0;
  border: 0;
  height: 1px;
  width: 1px;
  overflow: hidden;
`
const sharedStyles = css`
  height: 3rem;
  border: 1px solid #343a40;
  color: #2f2f2f;
  font-size: 1.8rem;
`

const StyledButton = styled.button`
  ${sharedStyles}
  display: none;
  position: absolute;
  top: 0;
  right: 0;
  width: 100%;
  font-weight: bold;
  background: #57bd84;
  border-radius: 0 2rem 2rem 0;
`

const StyledInput = styled.input`
  ${sharedStyles}
  outline: 0;
  width: 100%;
  background: #fff;
  padding: 0 1.6rem;
  border-radius: 2rem;
  border-color: #343a40;
  appearance: none;
  transition: all .3s cubic-bezier(0, 0, 0.43, 1.49);
  transition-property: width, border-radius;
  z-index: 1;
  position: relative;

  /* &::not(::placeholder-shown) {
    border-radius: .7rem 0 0 .7rem;
    width: calc(100% - 6rem)

    + ${StyledButton} {
      display: block;
    }
  } */
`

const Searchbar = () => {
  return (
    <StyledForm role="search" onSubmit={(e) => e.stopPropagation()}>
      <StyledLabel for="search">Search for stuff</StyledLabel>
      <StyledInput
        id="search"
        type="search"
        placeholder="Search..."
        required
      />
      <StyledButton type="submit">Go</StyledButton>
    </StyledForm>
  );
};

export default Searchbar;
