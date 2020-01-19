import React, { useState } from 'react';
import PropTypes from "prop-types";

function ConvertPage(props){

    const [longLink, setLongLink] = useState("");

    return (
        <div>
            <div>
                <label>Long link:</label>
                <input type="text" value={longLink} onChange={e => setLongLink(e.target.value)}
                    placeholder="http://www.example.com"/>
                <button type="button" onClick={() => props.onSubmit(longLink)} >Convert</button>
            </div>
            <div>
                <label>Short link:</label>
                <input type="text" value={props.shortLink} readOnly={true} />
            </div>
        </div>
    );
}

ConvertPage.propTypes = {
    shortLink: PropTypes.string.isRequired,
    onSubmit: PropTypes.func.isRequired
}

ConvertPage.defaultProps  = {
    shortLink: "",
    onSubmit: () => { }
}

export default ConvertPage;