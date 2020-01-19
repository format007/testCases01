import React from 'react';

const ErrorMessage = (props) => {
    return (
        <div className="alert alert-danger">
            {props.message || ""}
        </div>
    );
}

export default ErrorMessage;