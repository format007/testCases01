﻿import React from 'react';

const ErrorMessage = (props) => {
    return (
        <div>
            {props.message || ""}
        </div>
    );
}

export default ErrorMessage;