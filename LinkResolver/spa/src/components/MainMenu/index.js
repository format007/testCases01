import React from 'react';
import { NavLink } from "react-router-dom";
import clientRoutes from "../../routes/clientRoutes";

const MainMenu = () => {
    return (
        <ul className="nav">
            <li className="nav-item">
                <NavLink className="nav-link" activeClassName="active" to={clientRoutes.convert.url}>Convert</NavLink>
            </li>
            <li className="nav-item">
                <NavLink className="nav-link" activeClassName="active" to={clientRoutes.resolve.url}>Resolve</NavLink>
            </li>
        </ul>
    );
}

export default MainMenu;