import React from 'react';
import { Link } from "react-router-dom";
import clientRoutes from "../../routes/clientRoutes";

const MainMenu = () => {
    return (
        <div>
            <Link to={clientRoutes.convert.url}>Convert</Link>
            <Link to={clientRoutes.resolve.url}>Resolve</Link>
        </div>
    );
}

export default MainMenu;