import React from 'react';
import { BrowserRouter as Router, Route, Switch, Redirect } from "react-router-dom";
import MainMenu from "./components/MainMenu";
import clientRoutes from "./routes/clientRoutes";
import ConvertPage from "./pages/ConvertPage";
import ResolvePage from "./pages/ResolvePage";

function App() {
    return (
        <div className="center-screen">
            <Router>
                <div className="d-flex justify-content-center m-2 p-2">
                    <MainMenu />
                </div>
                <div className="d-flex justify-content-center m-2 p-2">
                    <Switch>
                        <Route path={clientRoutes.convert.url} component={ConvertPage} />
                        <Route path={clientRoutes.resolve.url} component={ResolvePage} />
                        <Redirect to={clientRoutes.convert.url} />
                    </Switch>
                </div>
            </Router>
        </div>
    );
}

export default App;
