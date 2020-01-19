import React, { useState } from 'react';
import { placeHolders } from "../../tools/urlHelper";
import PropTypes from "prop-types";

const ResolvePage = (props) => {

    const [shortLink, setShortLink] = useState("");

    return (
        <div>
            <div className="form-inline">
                <label>Short link:</label>
                <input className="form-control" type="text" value={shortLink} onChange={e => setShortLink(e.target.value)}
                    placeholder={placeHolders.shortLink} />
                <button className="btn btn-primary" type="button" onClick={() => props.onSubmit(shortLink)} >Resolve</button>

            </div>
            {
                props.longLink && <div className="form-inline">
                    <label>Short link:</label>
                    <a href={props.longLink}>
                        {props.longLink.length > 50 ? props.longLink.substring(0, 50) + "..." : props.longLink}
                    </a>
                </div>
            }
        </div>
    );
}

ResolvePage.propTypes = {
    longLink: PropTypes.string.isRequired,
    onSubmit: PropTypes.func.isRequired
}

ResolvePage.defaultProps = {
    longLink: "",
    onSubmit: () => { }
}

export default ResolvePage;