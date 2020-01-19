import React, { useState } from 'react';
import ConvertPage from "./ConvertPage";
import linkResolveSvc from "../../webSvc/linkResolveSvc";
import apiRoutes from "../../routes/apiRoutes";
import ErrorMessage from "../../components/ErrorMessage";
import { isURL } from "../../tools/urlHelper";

const linkSvc = new linkResolveSvc(apiRoutes.linksvc.url, () => { });

const ConvertPageController = () => {

    const [shortLink, setShortLink] = useState("");
    const [errMessage, setErrMessage] = useState("");
    const [showError, setShowError] = useState(false);

    const convertLink = (longLink) => {

        if (isURL(longLink))
            linkSvc.convert(longLink)
                .then(resp => { setShortLink(resp.data); setShowError(false); })
                .catch(resp => { setErrMessage(resp.message); setShowError(true); });
        else {
            setErrMessage("Invalid Long URL. Please correct it and try again");
            setShowError(true);
        }
    }

    return (
        <div>
            <ConvertPage onSubmit={convertLink} shortLink={shortLink} />
            {showError && <ErrorMessage message={errMessage} />}
        </div>
    );
}

export default ConvertPageController;