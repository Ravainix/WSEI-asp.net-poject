import React, { useState } from "react";
import {
  Collapse,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink,
} from "reactstrap";
import { Link } from "react-router-dom";
import { LoginMenu } from "../api-authorization/LoginMenu";

const NavMenu = () => {
  const [isCollapsed, setIsCollapsed] = useState(false);

  const toggleNavbar = () => {
    setIsCollapsed((state) => !state);
  };

  return (
    <nav>
      <Navbar
        className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow py-3"
        light
      >
        <NavbarBrand tag={Link} to="/" className="ml-md-6">
          Cookbook
        </NavbarBrand>
        <NavbarToggler onClick={toggleNavbar} className="mr-2" />
        <Collapse
          className="d-sm-inline-flex flex-sm-row-reverse"
          style={{ marginRight: "6rem" }}
          isOpen={!isCollapsed}
          navbar
        >
          <ul className="navbar-nav flex-grow">
            <NavItem>
              <NavLink tag={Link} className="text-dark" to="/recipes">
                Recipes
              </NavLink>
            </NavItem>
            <NavItem>
              <NavLink tag={Link} className="text-dark" to="/recipes/add">
                Add recipe
              </NavLink>
            </NavItem>
            <LoginMenu />
          </ul>
        </Collapse>
      </Navbar>
    </nav>
  );
};

export default NavMenu;