import React, { useState } from 'react';
import ResolvePage from "./ResolvePage";
import linkResolveSvc from "../../webSvc/linkResolveSvc";
import apiRoutes from "../../routes/apiRoutes";
import ErrorMessage from "../../components/ErrorMessage";
import { isShortURL } from "../../tools/urlHelper";

const linkSvc = new linkResolveSvc(apiRoutes.linksvc.url, () => { });

const ResolvePageController = () => {

    const [longLink, setLongLink] = useState("");
    const [errMessage, setErrMessage] = useState("");
    const [showError, setShowError] = useState(false);

    const resolveLink = (shortLink) => {
        if (isShortURL(shortLink))
            linkSvc.resolve(shortLink)
                .then(resp => { setLongLink(resp.data); setShowError(false); })
                .catch(resp => { setErrMessage(resp.message); setShowError(true); });
        else {
            setErrMessage("Invalid Short URL. Please correct it and try again");
            setShowError(true);
        }
    }

    return (
        <div>
            <ResolvePage longLink={longLink} onSubmit={resolveLink} />
            {showError && <ErrorMessage message={errMessage} />}
        </div>
    );
}

export default ResolvePageController;