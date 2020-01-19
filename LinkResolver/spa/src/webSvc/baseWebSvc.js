import Axios from "axios";

class BaseWebService {
    constructor(base_url, error_handler) {
        this.BASE_URL = base_url;
        this.error_handler = error_handler;
    }

    sendRequest(url, method, callback, data) {
        const request = Axios.request(
            {
                url: url,
                method: method,
                data: data, 
                headers: {
                    'Content-Type': 'application/json'
                }
            }
        );

        if (callback) {
            request.then(response => callback(response.data))
                .catch(resp => this.error_handler(resp));
        }
        else {
            return request;
        }
    }
};


export default BaseWebService;