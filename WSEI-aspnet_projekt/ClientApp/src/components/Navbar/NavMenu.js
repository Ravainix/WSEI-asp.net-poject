import React, { useState, useEffect } from "react";
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
import authService from "../api-authorization/AuthorizeService";

const ROUTES = [
  {
    name: "Przeglądaj",
    path: "/recipes",
    authenticated: false,
  },
  {
    name: "Moje przepisy",
    path: "/recipes/user",
    authenticated: true,
  },
  {
    name: "Dodaj przepis",
    path: "/recipes/add",
    authenticated: true,
  },
];

const NavMenu = () => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isCollapsed, setIsCollapsed] = useState(false);

  useEffect(() => {
    const authenticate = async () => {
      const resault = await authService.isAuthenticated();

      setIsAuthenticated(!!resault);
    };

    authenticate();
  }, []);

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
            {ROUTES.map(({ path, name, authenticated }) =>
              isAuthenticated === authenticated || !authenticated ? (
                <NavItem key={path}>
                  <NavLink tag={Link} className="text-dark" to={path}>
                    {name}
                  </NavLink>
                </NavItem>
              ) : null
            )}
            <LoginMenu />
          </ul>
        </Collapse>
      </Navbar>
    </nav>
  );
};
export default NavMenu;
