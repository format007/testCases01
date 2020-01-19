import React, { useState } from 'react';
import PropTypes from "prop-types";
import { placeHolders } from "../../tools/urlHelper";
import * as clipboard from "clipboard-polyfill";

function ConvertPage(props) {

    const [longLink, setLongLink] = useState("");

    return (
        <div>
            <div className="form-inline">
                <label>Long link:</label>
                <input className="form-control" type="text" value={longLink} onChange={e => setLongLink(e.target.value)}
                    placeholder={placeHolders.httpUrl} />
                <button type="button" className="btn btn-primary" onClick={() => props.onSubmit(longLink)} >Convert</button>
            </div>
            {props.shortLink &&
                <div className="form-inline">
                    <label>Short link:</label>
                    <input className="form-control" type="text" value={props.shortLink} readOnly={true} />
                    <button type="button" className="btn btn-info" onClick={() => clipboard.writeText(props.shortLink)} >Copy</button>
                </div>
            }
        </div>
    );
}

ConvertPage.propTypes = {
    shortLink: PropTypes.string.isRequired,
    onSubmit: PropTypes.func.isRequired
}

ConvertPage.defaultProps = {
    shortLink: "",
    onSubmit: () => { }
}

export default ConvertPage;