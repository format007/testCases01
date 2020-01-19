import BaseWebService from "./baseWebSvc";

class LinkResolveSvc extends BaseWebService {
    convert(link, callback) {
        return super.sendRequest(`${this.BASE_URL}/convert`, "post", callback, JSON.stringify(link));
    }

    resolve(link, callback) {
        return super.sendRequest(`${this.BASE_URL}/resolve`, "post", callback, JSON.stringify(link));
    }
}

export default LinkResolveSvc;